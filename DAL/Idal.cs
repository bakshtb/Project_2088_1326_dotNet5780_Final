using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDAL
    {
        long addHost(BE.Host host);
        void updateHost(BE.Host host);

        long addGuestReq(BE.GuestRequest guestRequest);
        void updateGuestReq(BE.GuestRequest guestRequest);

        long addHostingUnit(BE.HostingUnit hostingUnit);
        bool deleteHostingUnit(BE.HostingUnit hostingUnit);
        void updateHostingUnit(BE.HostingUnit hostingUnit);

        long addOrder(BE.Order order);
        void updateOrder(BE.Order order);

        IEnumerable<BE.HostingUnit> getListHostingUnit(Func<BE.HostingUnit, bool> predicate = null);
        IEnumerable<BE.GuestRequest> getListGuestRequest(Func<BE.GuestRequest, bool> predicate = null);
        IEnumerable<BE.Order> getListOrders(Func<BE.Order, bool> predicate = null);
        IEnumerable<BE.Host> getListHosts(Func<BE.Host, bool> predicate = null);

        List<BE.BankBranch> getListBankBranchs();
    }
}
