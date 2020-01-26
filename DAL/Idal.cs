using System;
using System.Collections.Generic;
using BE;

namespace DAL
{
    public interface IDAL
    {
        void addHost(Host host);

        void updateHost(Host host);

        Host getHost(long key);

        bool deleteHost(long key);


        IEnumerable<Host> getListHosts(Func<Host, bool> predicate = null);

        void addGuestReq(GuestRequest guestRequest);

        IEnumerable<GuestRequest> getListGuestRequest(Func<GuestRequest, bool> predicate = null);

        GuestRequest getGuestRequest(long key);

        void updateGuestReq(GuestRequest guestRequest);

        



        void addHostingUnit(HostingUnit hostingUnit);

        bool deleteHostingUnit(long key);

        IEnumerable<HostingUnit> getListHostingUnit(Func<HostingUnit, bool> predicate = null);

        HostingUnit getHostingUnit(long key);

        void updateHostingUnit(HostingUnit hostingUnit);



        void addOrder(Order order);

        void deleteOrder(long key);

        Order GetOrder(long key);

        IEnumerable<Order> getListOrders(Func<Order, bool> predicate = null);

        void updateOrder(Order order);


        List<BankBranch> getListBankBranchs();

        void setAdminPass(string pass);

        string getAdminPass();

        void AddProfitToAdmin(int amount);

        int getAdminProfit();
    }
}
