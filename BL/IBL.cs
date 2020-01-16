using System;
using System.Collections.Generic;
using System.Linq;
using BE;

namespace BL
{
    public interface IBL
    {
        #region Guest Request Functions

        long addGuestReq(GuestRequest guestRequest);

        void updateGuestReq(GuestRequest guestRequest);

        GuestRequest GetGuestRequest(long key);

        IEnumerable<GuestRequest> getListGuestRequest(Func<GuestRequest, bool> predicate = null);

        #endregion

        #region Hosting Unit Functions

        long addHostingUnit(HostingUnit hostingUnit);

        bool deleteHostingUnit(long key);

        bool deleteHostingUnit(HostingUnit hostingUnit);

        void updateHostingUnit(HostingUnit hostingUnit);

        HostingUnit GetHostingUnit(long key);

        IEnumerable<HostingUnit> getListHostingUnit(Func<HostingUnit, bool> predicate = null);


        #endregion

        #region Order Functions

        long addOrder(Order order);

        void updateOrder(long key, OrderStatusEnum status);

        void updateOrder(Order order);

        Order GetOrder(long key);

        IEnumerable<Order> getListOrders(Func<Order, bool> predicate = null);

        #endregion

        #region Bank Branchs Unit Functions

        List<BankBranch> getListBankBranchs();

        #endregion

        #region Host Functions

        Host GetHost(long key);

        long addHost(Host host);

        void updateHost(long key);

        void updateHost(Host host);

        IEnumerable<Host> getListHosts(Func<Host, bool> predicate = null);

        IEnumerable<HostingUnit> GetHostingUnitsOfHost(long key);

        #endregion

        #region Other Functions

        IEnumerable<HostingUnit> GetAvailableHostingUnits(DateTime date, int length);

        IEnumerable<Order> ordersBiggestThan(int length);

        int getSumOrders(GuestRequest guestRequest);

        int SumOfApprovedOrder(HostingUnit hostingUnit);

        IEnumerable<IGrouping<AreaEnum, GuestRequest>> GetGuestRequestsAreaByGroups();

        IEnumerable<IGrouping<int, GuestRequest>> GetGuestRequestsSumOfPeoplesByGroups();

        IEnumerable<IGrouping<int, Host>> GetHostsNumOfUnitsByGroups();

        IEnumerable<IGrouping<AreaEnum, HostingUnit>> GetHostingUnitsAreaByGroups();

        #endregion

        
    }
}
