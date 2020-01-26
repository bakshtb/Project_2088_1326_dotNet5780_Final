using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace BL
{
    public class BL_basic : IBL
    {
        DAL.IDAL dal;

        public BL_basic() 
        {
            dal = DAL.FactoryDal.GetDal(); 
        }

        #region Guest Request Functions

        public void addGuestReq(GuestRequest guestRequest)
        {
            if (guestRequest.EntryDate < guestRequest.ReleaseDate )
                dal.addGuestReq(guestRequest);
            else
                throw new Exception("תאריך כניסה צריך להיות לפני תאריך היציאה");
        }

        public void updateGuestReq(GuestRequest guestRequest)
        {
            dal.updateGuestReq(guestRequest);
        }

        public GuestRequest GetGuestRequest(long key)
        {
            return dal.getGuestRequest(key);
        }

        public IEnumerable<GuestRequest> getListGuestRequest(Func<GuestRequest, bool> predicate = null)
        {
            return dal.getListGuestRequest(predicate);
        }

        public GuestRequest getGuestReqByOrder(BE.Order order)//return guest req by Order.
        {
            return (dal.getGuestRequest((order.GuestRequestKey)));
        }

        public IEnumerable<object> GetReadableListOfGuestRequest(IEnumerable<GuestRequest> fromList = null)
        {
            if (fromList == null)
                fromList = getListGuestRequest();

            return from item in fromList
                   where true
                   select new
                   {
                       GuestRequestKey = item.GuestRequestKey,
                       PrivateName = item.PrivateName,
                       FamilyName = item.FamilyName,
                       MailAddress = item.MailAddress,
                       Status = HebrewEnum.GuestReqStatus(item.Status),
                       RegistrationDate = item.RegistrationDate.ToString("dd/MM/yyyy"),
                       EntryDate = item.EntryDate.ToString("dd/MM/yyyy"),
                       ReleaseDate = item.RegistrationDate.ToString("dd/MM/yyyy"),
                       Area = HebrewEnum.Area(item.Area),
                       Type = HebrewEnum.GuestReqType(item.Type),
                       Adults = item.Adults,
                       Children = item.Children,
                       Pool = HebrewEnum.options(item.Pool),
                       Jacuzzi = HebrewEnum.options(item.Jacuzzi),
                       Garden = HebrewEnum.options(item.Garden),
                       ChildrensAttractions = HebrewEnum.options(item.ChildrensAttractions)
                   };
        }

        #endregion

        #region Hosting Unit Functions

        public void addHostingUnit(HostingUnit hostingUnit)
        {

            dal.addHostingUnit(hostingUnit);
        }

        public bool deleteHostingUnit(long key)
        {
            return deleteHostingUnit(GetHostingUnit(key));
        }

        public bool deleteHostingUnit(HostingUnit hostingUnit)
        {
            if (!IsTherOpenOrderForTheHostingUnit(hostingUnit))
                return dal.deleteHostingUnit(hostingUnit.HostingUnitKey);
            else
                throw new Exception("לא ניתן למחוק את יחידת האירוח, יש הזמנות פתוחות עבורה");
        }

        public void updateHostingUnit(HostingUnit hostingUnit)
        {
            dal.updateHostingUnit(hostingUnit);
        }

        public HostingUnit GetHostingUnit(long key)
        {
            return dal.getHostingUnit(key);
        }

        public IEnumerable<HostingUnit> getListHostingUnit(Func<HostingUnit, bool> predicate = null)
        {
            return dal.getListHostingUnit(predicate);
        }


        public HostingUnit getHostingUnitByOrder(Order order)//return Hosting Unit by Order.
        {
            return (dal.getHostingUnit(order.HostingUnitKey));
        }

        public List<object> GetReadableListOfHostingUnits(IEnumerable<HostingUnit> fromList = null)
        {
            if (fromList == null)
                fromList = getListHostingUnit();

            return (from item in fromList
                   select new
                   {
                       Area = HebrewEnum.Area(item.Area),
                       HostingUnitKey = item.HostingUnitKey,
                       HostingUnitName = item.HostingUnitName,
                       Owner = item.Owner,                      
                   }).ToList<object>();
        }

        #endregion

        #region Order Functions

        public IEnumerable<Order> getOrdersOfHost(Host host)
        {
            return from hostingUnit in GetHostingUnitsOfHost(host.HostKey)
                   from order in getListOrders()
                   where order.HostingUnitKey == hostingUnit.HostingUnitKey
                   select order;
        }

        public void addOrder(Order order)
        {
            bool isHostingUnitExists = (from hostingUnit in getListHostingUnit()
                                        select hostingUnit.HostingUnitKey == order.HostingUnitKey).Count() != 0;

            if(!isHostingUnitExists)
                throw new Exception("היחידת אירוח לא נמצאה");

            bool isGuestRequestExists = (from guestRequest in getListGuestRequest()
                                        select guestRequest.GuestRequestKey == order.GuestRequestKey).Count() != 0;

            if(!isGuestRequestExists)
                throw new Exception("הבקשה לא נמצאה");

            if (!isOrderDateAvailable(order))
                throw new Exception("התאריכים לא זמינים");

            order.isClosed = false;
            order.Status = OrderStatusEnum.Not_yet_addressed;
            order.CreateDate = DateTime.Now;

            dal.addOrder(order);
        }

        public void updateOrder(long key, OrderStatusEnum status)
        {
            Order order = GetOrder(key);
            order.Status = status;
            updateOrder(order);
        }

        public void deleteOrder(long key)
        {
            dal.deleteOrder(key);
        }

        public void updateOrder(Order order, string pass = "")
        {
            HostingUnit unit = getHostingUnitByOrder(order);
            GuestRequest request = getGuestReqByOrder(order);
            DateTime EntryDate = request.EntryDate, ReleaseDate = request.ReleaseDate;
            int length = daysDistance(EntryDate, ReleaseDate);

            if (order.isClosed)
                throw new Exception("ההזמנה נסגרה, לא ניתן לשנות את הסטטוס שלה");

            if (order.Status == OrderStatusEnum.Closes_with_customer_response || order.Status == OrderStatusEnum.Closes_out_of_customer_disrespect)
            {
                if (!isOrderDateAvailable(order))
                    throw new Exception("התאריכים כבר לא זמינים");
                else
                    order.isClosed = true;
            }

            if (order.Status == OrderStatusEnum.Closes_with_customer_response)
            {
                dal.AddProfitToAdmin(Configuration.fee);

                for (int i = 0; i < length; i++)
                {

                    unit.AllDates.Add(EntryDate);

                    EntryDate = EntryDate.AddDays(1);
                }
                dal.updateHostingUnit(unit);

                GuestRequest guestRequest = getGuestReqByOrder(order);

                string PrivateName = guestRequest.PrivateName, FamilyName = guestRequest.FamilyName, MailAddress = guestRequest.MailAddress;

                foreach (var item in dal.getListGuestRequest())
                {
                    if (item.FamilyName == FamilyName && item.PrivateName == PrivateName && item.MailAddress == MailAddress)
                    {
                        item.Status = GuestReqStatusEnum.closed;
                        dal.updateGuestReq(item);
                    }
                }
            }

            if (order.Status == OrderStatusEnum.mail_has_been_sent)
            {
                if (getHostingUnitByOrder(order).Owner.CollectionClearance == false)
                    throw new Exception("לא ניתן לשלוח מייל כל עוד ולא אושרה ההרשאה לחיוב");

                MailMessage mail = new MailMessage();

                mail.To.Add(getGuestReqByOrder(order).MailAddress);

                mail.From = new MailAddress(getHostingUnitByOrder(order).Owner.MailAddress);

                mail.Subject = "הצעת נופש ממארח";

                mail.Body = "פירטי יחידת האירוח:\n" + unit.ToString() + "\n\nפירטי מארח:" + unit.Owner.ToString() + "\nיש לאשר במייל חוזר למארח בכתובת: " + unit.Owner.MailAddress;

                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.gmail.com";

                smtp.Credentials = new System.Net.NetworkCredential(getHostingUnitByOrder(order).Owner.MailAddress, pass);

                smtp.EnableSsl = true;
                try
                {
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                order.OrderDate = DateTime.Now;
                order.isSendMail = true;
            }
            
            dal.updateOrder(order);
        }

        public Order GetOrder(long key)
        {
            return dal.GetOrder(key);
        }

        public IEnumerable<Order> getListOrders(Func<Order, bool> predicate = null)
        {
            return dal.getListOrders(predicate);
        }

        public IEnumerable<object> GetReadableListOfOrder(IEnumerable<Order> fromList = null)
        {
            if (fromList == null)
                fromList = getListOrders();

            return from item in fromList
                   where true
                   select new
                   {
                       HostingUnitKey = item.HostingUnitKey,
                       GuestRequestKey = item.GuestRequestKey,
                       OrderKey = item.OrderKey,
                       Status = HebrewEnum.OrderStatus(item.Status),
                       CreateDate = item.CreateDate.ToString("dd/MM/yyyy"),
                       OrderDate = item.OrderDate.ToString("dd/MM/yyyy"),                       
                   };
        }

        #endregion

        #region Bank Branchs Unit Functions


        public List<BankBranch> getListBankBranchs()
        {
            return dal.getListBankBranchs();
        }

        #endregion

        #region Host Functions

        public Host GetHost(long key)
        {
            return dal.getHost(key);
        }

        public void addHost(Host host)
        {
            dal.addHost(host);
        }

        public void updateHost(long key)
        {
            dal.addHost(GetHost(key));
        }

        public void updateHost(Host host)
        {
            Host newHost = host, prevHost = GetHost(host.HostKey);


            if (host.CollectionClearance == false && prevHost.CollectionClearance == true)
                foreach (var item in GetHostingUnitsOfHost(host.HostKey))
                {
                    if (IsTherOpenOrderForTheHostingUnit(item))
                        throw new Exception("לא ניתו לבטל את ההרשאה לחיוב חשבון, למארח שמספרו:  " + host.HostKey + " יש יחידת אירוח " + item.HostingUnitKey + " שיש עבורה הזמנות פתוחות");
                }
            dal.updateHost(host);
        }

        public bool deleteHost(long key)
        {
            return dal.deleteHost(key);
        }

        public IEnumerable<Host> getListHosts(Func<Host, bool> predicate = null)
        {
            return dal.getListHosts(predicate);
        }

        public IEnumerable<HostingUnit> GetHostingUnitsOfHost(long key)
        {
            return from item in getListHostingUnit()
                   where item.Owner.HostKey == key
                   select item;
        }

        public List<object> GetReadableListOfHosts(IEnumerable<Host> fromList = null)
        {
            if (fromList == null)
                fromList = getListHosts();

            return (from item in fromList
                    select new
                    {
                        BankAccountNumber = item.BankAccountNumber,
                        BankBranchDetails = item.BankBranchDetails,                        
                        CollectionClearance = boolToHebrew(item.CollectionClearance),
                        FamilyName = item.FamilyName,
                        FhoneNumber = item.FhoneNumber,
                        HostKey = item.HostKey,
                        MailAddress = item.MailAddress,
                        PrivateName = item.PrivateName,
                        SumOfApprovedOrder = SumOfApprovedOrderOfHost(item)
                    }).ToList<object>();
        }

        #endregion

        #region Other Functions

        public string boolToHebrew(bool boolean)
        {
            return boolean ? "כן" : "לא";
        }

        public bool CheckAvailability(HostingUnit hostingUnit , DateTime date , int length)
        {
            List<DateTime> list = hostingUnit.AllDates;
            List<DateTime> tempList = new List<DateTime>();

            for (int i = 0; i < length; i++)
            {
                if (list.Where(dt => dt == date).Count() != 0)
                    return false;

                date = date.AddDays(1);
            }

            return true ;
        }

        public bool isOrderDateAvailable(Order order)
        {
            HostingUnit hostingUnit = getHostingUnitByOrder(order);
            GuestRequest guestRequest = getGuestReqByOrder(order);

            return CheckAvailability(hostingUnit, guestRequest.EntryDate, daysDistance(guestRequest.EntryDate , guestRequest.ReleaseDate) );
        }

        public bool IsTherOpenOrderForTheHostingUnit(HostingUnit hostingUnit)
        {
            IEnumerable <Order> orders = getListOrders(order => (order.HostingUnitKey == hostingUnit.HostingUnitKey && !order.isClosed));

            return orders.Count() != 0;
        }

        public IEnumerable<HostingUnit> GetAvailableHostingUnits(DateTime date, int length)
        {
            return from hostingUnit in dal.getListHostingUnit()
                   where CheckAvailability(hostingUnit, date, length)
                   select hostingUnit;
        }

        public int daysDistance(DateTime d1, DateTime d2)
        {
            return (d2 - d1).Days +1 ;
        }

        public int daysDistance(DateTime d1)
        {
            return (DateTime.Now - d1).Days + 1;
        }

        public IEnumerable<Order> ordersBiggestThan(int length)
        {
            return from order in dal.getListOrders()
                   where daysDistance(getGuestReqByOrder(order).EntryDate , getGuestReqByOrder(order).ReleaseDate) >= length
                   select order;
        }

        public int getSumMaildedOrdersOfGuestRequest(GuestRequest guestRequest)
        {
            return (from order in getListOrders()
                    where getGuestReqByOrder(order).GuestRequestKey == guestRequest.GuestRequestKey && order.Status == OrderStatusEnum.mail_has_been_sent
                    select order).Count();
        }

        public int getSumMaildedOrders()
        {
            int sum = 0;

            foreach (var item in getListGuestRequest())
            {
                sum += getSumMaildedOrdersOfGuestRequest(item);
            }
            return sum;
        }

        public int SumOfApprovedOrderOfHostingUnit(HostingUnit hostingUnit)
        {
            return (from order in getListOrders()
                    where getHostingUnitByOrder(order).HostingUnitKey == hostingUnit.HostingUnitKey && order.Status == OrderStatusEnum.Closes_with_customer_response
                    select order).Count();
        }

        public int SumOfApprovedOrderOfHost(Host host)
        {
            int sum = 0;

            foreach(var hostingUnit in GetHostingUnitsOfHost(host.HostKey))
            {
                sum += SumOfApprovedOrderOfHostingUnit(hostingUnit);
            }
            return sum;
        }

        public int SumOfApprovedOrderOfAllHosts()
        {
            int sum = 0;

            foreach (var host in getListHosts())
            {
                sum += SumOfApprovedOrderOfHost(host);
            }
            return sum;
        }

        public IEnumerable<IGrouping<AreaEnum , GuestRequest>> GetGuestRequestsAreaByGroups()
        {
            return from guestRequest in getListGuestRequest()
                   group guestRequest by guestRequest.Area;
        }

        public IEnumerable<IGrouping<int, GuestRequest>> GetGuestRequestsSumOfPeoplesByGroups()
        {
            return from guestRequest in getListGuestRequest()
                   group guestRequest by guestRequest.Adults + guestRequest.Children;
        }

        public IEnumerable<IGrouping<int, Host>> GetHostsNumOfUnitsByGroups()
        {
            return from host in dal.getListHosts()
                   group host by GetHostsNumOfUnits(host);
        }

        public int GetHostsNumOfUnits(Host host)
        {
            return (from hostingUnit in getListHostingUnit()
                   where hostingUnit.Owner.HostKey == host.HostKey
                   select hostingUnit).Count();
        }

        public IEnumerable<IGrouping<AreaEnum, HostingUnit>> GetHostingUnitsAreaByGroups()
        {
            return from hostingUnit in getListHostingUnit()
                   group hostingUnit by hostingUnit.Area;
        }

        public bool isHaveDigit(string st)
        {
            return st.FirstOrDefault(char.IsDigit) != '\0';
        }

        public bool isNotDigit(string st)
        {
            return int.TryParse(st, out int temp) == false;
        }

        #endregion


        public void setAdminPass(string pass)
        {
            dal.setAdminPass(pass);
        }

        public string getAdminPass()
        {
            return dal.getAdminPass();
        }

        public int getAdminProfit()
        {
            return dal.getAdminProfit();
        }
    }
}
