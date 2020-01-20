using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class Dal_imp : IDAL
    {
        public Dal_imp()
        {
            

            for (int i = 0; i < 3; i++)
            {
                DS.DataSource.tempHostList[i].BankBranchDetails = getListBankBranchs()[i];

                addGuestReq(DS.DataSource.tempGuestRequestList[i]);
                addHostingUnit(DS.DataSource.tempHostingUnitList[i]);
                addOrder(DS.DataSource.tempOrderList[i]);
                addHost(DS.DataSource.tempHostList[i]);
            }
        }

        #region Host Functions

        public long addHost(Host host)
        {
            Host tempHost = getListHosts().FirstOrDefault(host1 => host1.HostKey == host.HostKey);

            if (tempHost != null)
                throw new Exception("יש כבר מארח עם אותו תעודת זהות");
            else
            {
                DS.DataSource.HostList.Add(host.Copy());
                return host.HostKey;
            }
        }

        public void updateHost(Host host)
        {
            int index = DS.DataSource.HostList.FindIndex(h => h.HostKey == host.HostKey);
            if (index == -1)
                throw new Exception("התעודת זהות שהוזנה לא מייצגת מארח במערכת");

            DS.DataSource.HostList[index] = (host).Copy();
        }

        public Host getHost(long key)//return host by Guest Request Key.
        {
            Host host = getListHosts().FirstOrDefault(h => h.HostKey == key);

            if (host == null)
                throw new Exception("מספר תעודת זהות לא נכון");
            else
                return host.Copy();
        }

        public IEnumerable<Host> getListHosts(Func<Host, bool> predicate = null)
        {
            if (predicate == null)
                return (DS.DataSource.HostList).Copy();

            return (DS.DataSource.HostList.Where(predicate)).Copy();
        }

        public bool deleteHost(long key)
        {
            return DS.DataSource.HostList.Remove(DS.DataSource.HostList.FirstOrDefault(HU => HU.HostKey == key));
        }

        #endregion

        #region Guest Request Functions

        public long addGuestReq(GuestRequest guestRequest)
        {
            guestRequest.Status = GuestReqStatusEnum.not_addressed;
            guestRequest.GuestRequestKey = Configuration.GuestRequestKey++;
            guestRequest.RegistrationDate = DateTime.Today;
            DS.DataSource.GuestRequestList.Add(guestRequest.Copy());
            return guestRequest.GuestRequestKey;
        }

        public IEnumerable<GuestRequest> getListGuestRequest(Func<GuestRequest, bool> predicate = null)
        {
            if (predicate == null)
                return (DS.DataSource.GuestRequestList).Copy();

            return DS.DataSource.GuestRequestList.Where(predicate).Copy();
        }

        public GuestRequest getGuestRequest(long key)//return guest Request by Guest Request Key.
        {
            GuestRequest guestRequest = getListGuestRequest().FirstOrDefault(req => req.GuestRequestKey == key);

            if (guestRequest == null)
                throw new Exception("מספר בקשה לא נכון");
            else
                return guestRequest.Copy();
        }

        public void updateGuestReq(GuestRequest guestRequest)
        {
            int index = DS.DataSource.GuestRequestList.FindIndex(s => s.GuestRequestKey == guestRequest.GuestRequestKey);
            if (index == -1)
                throw new Exception("היחידת האירוח המבוקשת לא נימצאה");

            DS.DataSource.GuestRequestList[index] = (guestRequest).Copy();
        }

        public GuestRequest getGuestReqByOrder(BE.Order order)//return guest req by Order.
        {
            return (getGuestRequest((order.GuestRequestKey))).Copy();
        }

        #endregion

        #region Hosting Unit Functions

        public long addHostingUnit(HostingUnit hostingUnit)
        {

            hostingUnit.HostingUnitKey = Configuration.HostingUnitKey++;

            hostingUnit.AllDates = new List<DateTime>();

            DS.DataSource.HostingUnitList.Add(hostingUnit.Copy());
            return hostingUnit.HostingUnitKey;

        }

        public bool deleteHostingUnit(HostingUnit hostingUnit)
        {
            return DS.DataSource.HostingUnitList.Remove(DS.DataSource.HostingUnitList.FirstOrDefault(HU => HU.HostingUnitKey == hostingUnit.HostingUnitKey));
        }

        public IEnumerable<HostingUnit> getListHostingUnit(Func<HostingUnit, bool> predicate = null)
        {
            if (predicate == null)
                return (DS.DataSource.HostingUnitList).Copy();

            return (DS.DataSource.HostingUnitList.Where(predicate)).Copy();
        }

        public HostingUnit getHostingUnit(long key)//return Hosting Unit by Guest Hosting Unit Key.
        {
            HostingUnit hostingUnit = getListHostingUnit().FirstOrDefault(unit => unit.HostingUnitKey == key);

            if (hostingUnit == null)
                throw new Exception("מספר יחידת אירוח לא נכון");
            else
                return hostingUnit.Copy();
        }

        public void updateHostingUnit(HostingUnit hostingUnit)
        {
            int index = DS.DataSource.HostingUnitList.FindIndex(s => s.HostingUnitKey == hostingUnit.HostingUnitKey);
            if (index == -1)
                throw new Exception("יחדית האירוח לא נימצאה");

            DS.DataSource.HostingUnitList[index] = (hostingUnit).Copy();
        }

        public HostingUnit getHostingUnitByOrder(BE.Order order)//return Hosting Unit by Order.
        {
            return (getHostingUnit(order.HostingUnitKey)).Copy();
        }

        #endregion

        #region Order Functions

        public long addOrder(Order order)
        {            
            order.OrderKey = Configuration.OrderKey++;
            DS.DataSource.OrderList.Add(order.Copy());
            return order.OrderKey;
        }

        public Order GetOrder(long key)
        {
            Order Order = getListOrders().FirstOrDefault(order => order.OrderKey == key);

            if (Order == null)
                throw new Exception("מספר הזמנה לא נכון");
            else
                return Order.Copy();
        }

        public void deleteOrder(long key)
        {
            DS.DataSource.OrderList.Remove(DS.DataSource.OrderList.FirstOrDefault(or => or.OrderKey == key));
        }

        public IEnumerable<Order> getListOrders(Func<Order, bool> predicate = null)
        {
            if (predicate == null)
                return (DS.DataSource.OrderList).Copy();

            return from item in DS.DataSource.OrderList
                   where predicate(item)
                   select item.Copy();//(DS.DataSource.OrderList.Where(predicate)).Copy();
        }

        public void updateOrder(Order order)
        {
            int index = DS.DataSource.OrderList.FindIndex(s => s.OrderKey == order.OrderKey);
            if (index == -1)
                throw new Exception("ההזמנה לא נמצאה");

            DS.DataSource.OrderList[index] = (order).Copy();
        }

        #endregion

        #region Bank Branchs Functions

        public List<BankBranch> getListBankBranchs()
        {
            List<BankBranch> ret = new List<BankBranch>()
            {
                new BankBranch
                {
                    BankNumber = 12,
                    BankName = "בנק הפועלים",
                    BranchNumber = 101,
                    BranchAddress = "שדרות רוטשילד 50",
                    BranchCity = "תל אביב"
                },
                new BankBranch
                {
                    BankNumber = 10,
                    BankName = "בנק לאומי לישראל",
                    BranchNumber = 101,
                    BranchAddress = "רחוב יהודה הלוי",
                    BranchCity = "תל אביב",
                },
                new BankBranch
                {
                    BankNumber = 4,
                    BankName = "בנק יהב",
                    BranchNumber = 101,
                    BranchAddress = "רחוב ירמיהו",
                    BranchCity = "ירושלים",
                },
                new BankBranch
                {
                    BankNumber = 54,
                    BankName = "בנק ירושלים",
                    BranchNumber = 101,
                    BranchAddress = "מרכז העיר",
                    BranchCity = "ירושלים",
                },
                new BankBranch
                {
                    BankNumber = 11,
                    BankName = "בנק דיסקונט",
                    BranchNumber = 101,
                    BranchAddress = "רחוב יהודה הלוי",
                    BranchCity = "תל אביב",
                }
            };

            return ret.Copy();
        }

        #endregion
    }
}