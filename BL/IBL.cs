using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IBL
    {
        void updateHost(BE.Host host);
        long addHost(BE.Host host);

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


        IEnumerable<BE.HostingUnit> GetAvailableHostingUnits(DateTime date, int length);

        int daysDistance(DateTime d1 , DateTime d2);
        int daysDistance(DateTime d1);

        IEnumerable<BE.Order> ordersBiggestThan(int length);

        int getSumOrders(BE.GuestRequest guestRequest);

        int SumOfApprovedOrder(BE.HostingUnit hostingUnit);

        IEnumerable<IGrouping<BE.AreaEnum, BE.GuestRequest>> GetGuestRequestsAreaByGroups();
        IEnumerable<IGrouping<int, BE.GuestRequest>> GetGuestRequestsSumOfPeoplesByGroups();
        IEnumerable<IGrouping<int, BE.Host>> GetHostsNumOfUnitsByGroups();
        IEnumerable<IGrouping<BE.AreaEnum, BE.HostingUnit>> GetHostingUnitsAreaByGroups();
    }
}
