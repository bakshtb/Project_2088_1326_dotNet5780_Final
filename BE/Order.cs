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
        public long HostingUnitKey { get; set; }
        public long GuestRequestKey { get; set; }
        public long OrderKey { get; set; }
        public OrderStatusEnum Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OrderDate { get; set; }
        public bool isClosed { get; set; }
        public bool isSendMail { get; set; }
        public int cost { get; set; }

        public override string ToString()
        {
            string ret = "מספר יחידת אירוח: " + HostingUnitKey
                        + "\nמספר בקשה: " + GuestRequestKey
                        + "\nמספר הזמנה: " + OrderKey
                        + "\nסטטוס: " + HebrewEnum.OrderStatus(Status)
                        + "\nתאריך יצירה: " + CreateDate.ToString("dd/MM/yyyy");

            if (isSendMail)
            {
                ret += "\nתאריך משלוח מייל ללקוח: " + CreateDate.ToString("dd/MM/yyyy");
            }
                        
            return ret;
        }
    }
}
