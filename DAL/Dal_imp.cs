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
                addGuestReq(DS.DataSource.tempGuestRequestList[i]);
                addHostingUnit(DS.DataSource.tempHostingUnitList[i]);
                addOrder(DS.DataSource.tempOrderList[i]);
                addHost(DS.DataSource.tempHostList[i]);
            }
        }

        public long addHost(Host host)
        {
            Host tempHost = getListHosts().FirstOrDefault(host1 => host1.HostKey == host.HostKey);

            if (tempHost != null)
                throw new Exception("There is already a host with the same ID");
            else
            {
                DS.DataSource.HostList.Add(host);
                return host.HostKey;
            }
        }

        public void updateHost(BE.Host host)
        {
            int index = DS.DataSource.HostList.FindIndex(h => h.HostKey == host.HostKey);
            if (index == -1)
                throw new Exception("host with the same key not found...");

            DS.DataSource.HostList[index] = (host);
        }

        public long addGuestReq(GuestRequest guestRequest)
        {
            guestRequest.GuestRequestKey = Configuration.GuestRequestKey++;
            DS.DataSource.GuestRequestList.Add(guestRequest);
            return guestRequest.GuestRequestKey;
        }

        public long addHostingUnit(HostingUnit hostingUnit)
        {

            hostingUnit.HostingUnitKey = Configuration.HostingUnitKey++;

            hostingUnit.Diary = new bool[12, 31];
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    hostingUnit.Diary[i, j] = true;
                }
            }

            DS.DataSource.HostingUnitList.Add(hostingUnit);
            return hostingUnit.HostingUnitKey;

        }

        public long addOrder(Order order)
        {
            order.Status = OrderStatusEnum.Not_yet_addressed;
            order.OrderKey = Configuration.OrderKey++;
            DS.DataSource.OrderList.Add(order);
            return order.OrderKey;
        }

        public bool deleteHostingUnit(HostingUnit hostingUnit)
        {
            return DS.DataSource.HostingUnitList.Remove(hostingUnit);
        }

        public List<BankBranch> getListBankBranchs()
        {
            List<BankBranch> ret = new List<BankBranch>()
            {
                new BankBranch
                {
                    BankNumber = 12,
                    BankName = "Bank Hapoalim",
                    BranchNumber = 101,
                    BranchAddress = "50 Rothschild Blvd",
                    BranchCity = "TEL AVIV - YAFO"
                },
                new BankBranch
                {
                    BankNumber = 10,
                    BankName = "Bank Leumi Le-Israel",
                    BranchNumber = 101,
                    BranchAddress = "Yehuda Halevy St",
                    BranchCity = "TEL AVIV - YAFO",
                },
                new BankBranch
                {
                    BankNumber = 4,
                    BankName = "Bank Yahav",
                    BranchNumber = 101,
                    BranchAddress = "Yirmiyahu St",
                    BranchCity = "JERUSALEM",
                },
                new BankBranch
                {
                    BankNumber = 54,
                    BankName = "Bank of Jerusalem",
                    BranchNumber = 101,
                    BranchAddress = "2, Hanegev St., Airport city",
                    BranchCity = "JERUSALEM",
                },
                new BankBranch
                {
                    BankNumber = 11,
                    BankName = "Israel Discount Bank Ltd",
                    BranchNumber = 101,
                    BranchAddress = "Yehuda Halevy St",
                    BranchCity = "TEL AVIV - YAFO",
                }
            };

            return ret;
        }

        public IEnumerable<GuestRequest> getListGuestRequest(Func<GuestRequest, bool> predicate = null)
        {
            if (predicate == null) 
                return (DS.DataSource.GuestRequestList).Copy(); 

            return DS.DataSource.GuestRequestList.Where(predicate).Copy();
        }

        public Host getHost(long key)//return host by Guest Request Key.
        {
            Host host = getListHosts().FirstOrDefault(h => h.HostKey == key);

            if (host == null)
                throw new Exception("Wrong host ID");
            else
                return host.Copy();
        }

        public GuestRequest getGuestRequest(long key)//return guest Request by Guest Request Key.
        {
            GuestRequest guestRequest = getListGuestRequest().FirstOrDefault(req => req.GuestRequestKey == key);

            if (guestRequest == null)
                throw new Exception("Wrong guest request key");
            else
                return guestRequest.Copy();
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
                throw new Exception("Wrong hosting unit key");
            else
                return hostingUnit.Copy();
        }

        public Order GetOrder(long key)
        {
            Order Order = getListOrders().FirstOrDefault(order => order.OrderKey == key);

            if (Order == null)
                throw new Exception("Wrong order key");
            else
                return Order.Copy();
        }

        public IEnumerable<Order> getListOrders(Func<Order, bool> predicate = null)
        {
            if (predicate == null)
                return (DS.DataSource.OrderList).Copy();

            return (DS.DataSource.OrderList.Where(predicate)).Copy();
        }

        public IEnumerable<Host> getListHosts(Func<Host, bool> predicate = null)
        {
            if (predicate == null)
                return (DS.DataSource.HostList).Copy();

            return (DS.DataSource.HostList.Where(predicate)).Copy();
        }

        public void updateHostingUnit(HostingUnit hostingUnit)
        {
            int index = DS.DataSource.HostingUnitList.FindIndex(s => s.HostingUnitKey == hostingUnit.HostingUnitKey);
            if (index == -1)
                throw new Exception("hosting Unit with the same key not found...");

            DS.DataSource.HostingUnitList[index] = (hostingUnit).Copy();
        }

        public void updateOrder(Order order)
        {
            int index = DS.DataSource.OrderList.FindIndex(s => s.OrderKey == order.OrderKey);
            if (index == -1)
                throw new Exception("Order with the same key not found...");

            DS.DataSource.OrderList[index] = (order).Copy();
        }

        public void updateGuestReq(GuestRequest guestRequest)
        {
            int index = DS.DataSource.GuestRequestList.FindIndex(s => s.GuestRequestKey == guestRequest.GuestRequestKey);
            if (index == -1)
                throw new Exception("hosting Unit with the same key not found...");

            DS.DataSource.GuestRequestList[index] = (guestRequest).Copy();
        }

        public HostingUnit getHostingUnitByOrder(BE.Order order)//return Hosting Unit by Order.
        {
            return (getHostingUnit(order.HostingUnitKey)).Copy();
        }

        public GuestRequest getGuestReqByOrder(BE.Order order)//return guest req by Order.
        {
            return (getGuestRequest((order.GuestRequestKey))).Copy();
        }

        
    }
}
