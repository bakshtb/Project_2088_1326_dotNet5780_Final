using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
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
            string ret = "מספר יחידת אירוח: " + HostingUnitKey
                        + "\nמספר בקשה: " + GuestRequestKey
                        + "\nמספר הזמנה: " + OrderKey
                        + "\nסטטוס: " + Status
                        + "\nתארי יצירה: " + CreateDate.ToString()
                        + "\nתאריך הזמנה: " + OrderDate.ToString();
            return ret;
        }
    }
}
