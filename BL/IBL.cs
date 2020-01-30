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

        void updateOrder(Order order);

        void deleteOrder(long key);

        Order GetOrder(long key);

        IEnumerable<Order> getListOrders(Func<Order, bool> predicate = null);

        IEnumerable<Order> getOrdersOfHost(Host host);

        IEnumerable<object> GetReadableListOfOrder(IEnumerable<Order> fromList = null);

        #endregion

        #region Bank Branchs Unit Functions

        List<BankBranch> getListBankBranchs();

        List<BankBranch> getListBanks();

        List<BankBranch> getListBankBranchs(int bankNumber);

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

        /// <summary>
        /// The function returns the list of all units available on a starting date "date" and ending after "length" days.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        IEnumerable<HostingUnit> GetAvailableHostingUnits(DateTime date, int length);

        /// <summary>
        /// The function returns all orders whose order length is greater than "length"
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        IEnumerable<Order> ordersBiggestThan(int length);

        /// <summary>
        /// The function returns sum of orders that mailded
        /// </summary>
        /// <returns></returns>
        int getSumMaildedOrders();


        /// <summary>
        /// The function returns the amount of requests from a particular customer when those requests are emailed.
        /// </summary>
        /// <param name="guestRequest"></param>
        /// <returns></returns>
        int getSumMaildedOrdersOfGuestRequest(GuestRequest guestRequest);

        /// <summary>
        /// The function get a hosting unit and returns the amount of orders approved for that unit
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <returns></returns>
        int SumOfApprovedOrderOfHostingUnit(HostingUnit hostingUnit);


        /// <summary>
        /// /// The function get a host and returns the amount of orders that have been approved for that host
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        int SumOfApprovedOrderOfHost(Host host);


        /// <summary>
        /// The function returns the amount of all approved orders
        /// </summary>
        /// <returns></returns>
        int SumOfApprovedOrderOfAllHosts();


        /// <summary>
        /// The function returns groups of customer requests by areas
        /// </summary>
        /// <returns></returns>
        IEnumerable<IGrouping<AreaEnum, GuestRequest>> GetGuestRequestsAreaByGroups();

        /// <summary>
        /// The function returns groups of customer requests by Sum Of Peoples
        /// </summary>
        /// <returns></returns>
        IEnumerable<IGrouping<int, GuestRequest>> GetGuestRequestsSumOfPeoplesByGroups();

        /// <summary>
        /// The function returns groups of hosts by num of units they own
        /// </summary>
        /// <returns></returns>
        IEnumerable<IGrouping<int, Host>> GetHostsNumOfUnitsByGroups();


        /// <summary>
        /// The function returns groups of hosting units by areas
        /// </summary>
        /// <returns></returns>
        IEnumerable<IGrouping<AreaEnum, HostingUnit>> GetHostingUnitsAreaByGroups();



        /// <summary>
        /// The function receives a string and returns whether it contains digits
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        bool isHaveDigit(string st);

        /// <summary>
        /// The function receives a string and returns whether it contains no digits
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        bool isNotDigit(string st);

        #endregion

        #region Admin Functions

        void setAdminPass(string pass);

        string getAdminPass();

        int getAdminProfit();

        #endregion Admin Functions

        /// <summary>
        /// The function receives an email address and returns whether the address is valid
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsValidEmail(string email);

        void setFee(int fee);

        int getFee();
    }
}
