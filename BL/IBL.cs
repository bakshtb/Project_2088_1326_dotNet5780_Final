using System;
using System.Collections.Generic;
using System.Linq;
using BE;

namespace BL
{
    public interface IBL
    {
        #region Guest Request Functions

        void addGuestReq(GuestRequest guestRequest);

        void updateGuestReq(GuestRequest guestRequest);

        GuestRequest GetGuestRequest(long key);

        IEnumerable<GuestRequest> getListGuestRequest(Func<GuestRequest, bool> predicate = null);

        GuestRequest getGuestReqByOrder(Order order);

        IEnumerable<object> GetReadableListOfGuestRequest(IEnumerable<GuestRequest> fromList = null);

        #endregion

        #region Hosting Unit Functions

        void addHostingUnit(HostingUnit hostingUnit);

        bool deleteHostingUnit(long key);

        bool deleteHostingUnit(HostingUnit hostingUnit);

        void updateHostingUnit(HostingUnit hostingUnit);

        HostingUnit GetHostingUnit(long key);

        IEnumerable<HostingUnit> getListHostingUnit(Func<HostingUnit, bool> predicate = null);

        HostingUnit getHostingUnitByOrder(Order order);

        List<object> GetReadableListOfHostingUnits(IEnumerable<HostingUnit> fromList = null);


        #endregion

        #region Order Functions

        void addOrder(Order order);

        void updateOrder(long key, OrderStatusEnum status);

        void updateOrder(Order order , string pass = "");

        void deleteOrder(long key);

        Order GetOrder(long key);

        IEnumerable<Order> getListOrders(Func<Order, bool> predicate = null);

        IEnumerable<Order> getOrdersOfHost(Host host);

        IEnumerable<object> GetReadableListOfOrder(IEnumerable<Order> fromList = null);

        #endregion

        #region Bank Branchs Unit Functions

        List<BankBranch> getListBankBranchs();

        #endregion

        #region Host Functions

        Host GetHost(long key);

        void addHost(Host host);

        void updateHost(long key);

        void updateHost(Host host);

        bool deleteHost(long key);

        IEnumerable<Host> getListHosts(Func<Host, bool> predicate = null);

        IEnumerable<HostingUnit> GetHostingUnitsOfHost(long key);

        List<object> GetReadableListOfHosts(IEnumerable<Host> fromList = null);

        #endregion

        #region Other Functions

        IEnumerable<HostingUnit> GetAvailableHostingUnits(DateTime date, int length);

        IEnumerable<Order> ordersBiggestThan(int length);

        int getSumMaildedOrders();

        int getSumMaildedOrdersOfGuestRequest(GuestRequest guestRequest);

        int SumOfApprovedOrderOfHostingUnit(HostingUnit hostingUnit);

        int SumOfApprovedOrderOfHost(Host host);

        int SumOfApprovedOrderOfAllHosts();

        IEnumerable<IGrouping<AreaEnum, GuestRequest>> GetGuestRequestsAreaByGroups();

        IEnumerable<IGrouping<int, GuestRequest>> GetGuestRequestsSumOfPeoplesByGroups();

        IEnumerable<IGrouping<int, Host>> GetHostsNumOfUnitsByGroups();

        IEnumerable<IGrouping<AreaEnum, HostingUnit>> GetHostingUnitsAreaByGroups();

        bool isHaveDigit(string st);

        bool isNotDigit(string st);

        #endregion


        void setAdminPass(string pass);

        string getAdminPass();

        int getAdminProfit();
    }
}
