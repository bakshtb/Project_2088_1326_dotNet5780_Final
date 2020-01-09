using BE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public class BL_basic : IBL
    {
        DAL.Dal_imp dal = new DAL.Dal_imp();

        public long addGuestReq(GuestRequest guestRequest)
        {
            if (guestRequest.EntryDate < guestRequest.ReleaseDate )
                return dal.addGuestReq(guestRequest);
            else
                throw new Exception("Entrance day must be before departure day!");
        }

        public long addHostingUnit(HostingUnit hostingUnit)
        {

            return dal.addHostingUnit(hostingUnit);
        }

        public long addOrder(Order order)
        {
            bool isHostingUnitExists = (from hostingUnit in getListHostingUnit()
                                        select hostingUnit.HostingUnitKey == order.HostingUnitKey).Count() != 0;

            if(!isHostingUnitExists)
                throw new Exception("The hosting unit does not exist");

            bool isGuestRequestExists = (from guestRequest in getListGuestRequest()
                                        select guestRequest.GuestRequestKey == order.GuestRequestKey).Count() != 0;

            if(!isGuestRequestExists)
                throw new Exception("The guest request does not exist");

            if (!isOrderDateAvailable(order))
                throw new Exception("The requested date is not available");

            return dal.addOrder(order);
        }

        public bool deleteHostingUnit(long key)
        {
           return deleteHostingUnit(GetHostingUnit(key));
        }

        public bool deleteHostingUnit(HostingUnit hostingUnit)
        {
            if (!IsTherOpenOrderForTheHostingUnit(hostingUnit))
                return dal.deleteHostingUnit(hostingUnit);
            else
                throw new Exception("Unable to delete the hosting unit, there are open bookings for this unit");
        }     

        public void updateHostingUnit(HostingUnit hostingUnit)
        {
            dal.updateHostingUnit(hostingUnit);
        }



        public void updateOrder(long key, OrderStatusEnum status)
        {
            Order order = GetOrder(key);
            order.Status = status;
            updateOrder(order);
        }

        public void updateOrder(Order order)
        {
            if (order.isClosed)
                throw new Exception("Order status cannot be changed after it is closed.");

            if (order.Status == OrderStatusEnum.Closes_with_customer_response || order.Status == OrderStatusEnum.Closes_out_of_customer_disrespect)
            {
                if (!isOrderDateAvailable(order))
                    throw new Exception("dates are no longer available!");
                else
                    order.isClosed = true;
            }

            if (order.Status == OrderStatusEnum.Closes_with_customer_response)
            {
                HostingUnit unit = dal.getHostingUnitByOrder(order);
                GuestRequest request = dal.getGuestReqByOrder(order);
                DateTime EntryDate = request.EntryDate, ReleaseDate = request.ReleaseDate;
                int length = (ReleaseDate - EntryDate).Days;

                for (int i = 0; i < length; i++)
                {
                    unit.Diary[EntryDate.Month-1, EntryDate.Day-1] = false;
                    EntryDate = EntryDate.AddDays(1);
                }
                dal.updateHostingUnit(unit);

                GuestRequest guestRequest = dal.getGuestReqByOrder(order);

                string PrivateName = guestRequest.PrivateName, FamilyName =  guestRequest.FamilyName;

                foreach (var item in dal.getListGuestRequest())
                {
                    if (item.FamilyName == FamilyName && item.PrivateName == PrivateName)
                        item.Status = GuestReqStatusEnum.closed;
                }
            }

            if (order.Status == OrderStatusEnum.mail_has_been_sent)
            {
                if (dal.getHostingUnitByOrder(order).Owner.CollectionClearance == false)
                    throw new Exception("It is necessary to approve collection clearance ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tAn email was sent to the applicant");
                Console.ForegroundColor = ConsoleColor.White;
            }

            order.cost += configuration.fee;
            dal.updateOrder(order);
        }

        public void updateGuestReq(GuestRequest guestRequest)
        {
            dal.updateGuestReq(guestRequest);
        }

        public bool CheckAvailability(HostingUnit hostingUnit , DateTime date , int length)
        {
            bool isAvailable = true;

            DateTime temp = date;

            for (int i = 0; i < length && isAvailable; i++)
            {
                isAvailable = hostingUnit.Diary[temp.Month - 1, temp.Day - 1];
                temp = temp.AddDays(1);
            }
            return isAvailable;
        }

        public bool isOrderDateAvailable(Order order)
        {
            HostingUnit hostingUnit = dal.getHostingUnitByOrder(order);
            GuestRequest guestRequest = dal.getGuestReqByOrder(order);

            return CheckAvailability(hostingUnit, guestRequest.EntryDate, daysDistance(guestRequest.EntryDate , guestRequest.ReleaseDate) );
        }

        public List<BankBranch> getListBankBranchs()
        {
            return dal.getListBankBranchs();
        }

        public HostingUnit GetHostingUnit (long key)
        {
            return dal.getHostingUnit(key);
        }

        public IEnumerable<HostingUnit> getListHostingUnit(Func<HostingUnit, bool> predicate = null)
        {
            return dal.getListHostingUnit(predicate);
        }

        public GuestRequest GetGuestRequest(long key)
        {
            return dal.getGuestRequest(key);
        }

        public IEnumerable<GuestRequest> getListGuestRequest(Func<GuestRequest, bool> predicate = null)
        {
            return dal.getListGuestRequest(predicate);
        }

        public Order GetOrder(long key)
        {
            return dal.GetOrder(key);
        }

        public Host GetHost(long key)
        {
            return dal.getHost(key);
        }

        public IEnumerable<Order> getListOrders(Func<Order, bool> predicate = null)
        {
            return dal.getListOrders(predicate);
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
                   where daysDistance(dal.getGuestReqByOrder(order).EntryDate , dal.getGuestReqByOrder(order).ReleaseDate) >= length
                   select order;
        }

        public int getSumOrders(GuestRequest guestRequest)
        {
            return (from order in getListOrders()
                    where dal.getGuestReqByOrder(order).GuestRequestKey == guestRequest.GuestRequestKey && order.Status == OrderStatusEnum.mail_has_been_sent
                    select order).Count();
        }

        public int SumOfApprovedOrder(HostingUnit hostingUnit)
        {
            return (from order in getListOrders()
                    where dal.getHostingUnitByOrder(order).HostingUnitKey == hostingUnit.HostingUnitKey && order.Status == OrderStatusEnum.Closes_with_customer_response
                    select order).Count();
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

        public long addHost(Host host)
        {
            return dal.addHost(host);
        }


        public void updateHost(long key)
        {
            dal.addHost(GetHost(key));
        }

        public void updateHost(Host host)
        {
            Host newHost = host, prevHost = GetHost(host.HostKey);


            if (host.CollectionClearance == false && prevHost.CollectionClearance == true)
                foreach (var item in getListHostingUnit())
                {
                    if (item.Owner.HostKey == host.HostKey)
                    {
                        if (IsTherOpenOrderForTheHostingUnit(item))
                            throw new Exception("You can not cancel the collection clearance: The host: "+host.HostKey+ " owns a hosting unit "+item.HostingUnitKey+ " that has an open order");
                    }
                }   
            dal.updateHost(host);
        }

        public IEnumerable<Host> getListHosts(Func<Host, bool> predicate = null)
        {
            return dal.getListHosts(predicate);
        }
    }
}
