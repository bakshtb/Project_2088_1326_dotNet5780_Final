using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Order
    {
        public int HostingUnitKey { get; set; }
        public int GuestRequestKey { get; set; }
        public long OrderKey { get; set; }
        public OrderStatusEnum Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OrderDate { get; set; }
        public bool isClosed { get; set; }
        public int cost { get; set; }

        public override string ToString()
        {
            string ret = "Hosting Unit Key: " + HostingUnitKey
                        + "\nGuest Request Key: " + GuestRequestKey
                        + "\nOrder Key: " + OrderKey
                        + "\nStatus: " + Status
                        + "\nCreate Date: " + CreateDate.ToString()
                        + "\nOrder Date: " + OrderDate.ToString();
            return ret;
        }
    }
}
