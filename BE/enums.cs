using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BE
{
    public enum optionsEnum { necessary, Possible, not_interested }
  
    public enum GuestReqTypeEnum { Hotel, Zimmer, Villa }
    public enum AreaEnum
    {
        All_Israel,
        north_Israel,
        Central_Israel,
        South_Israel,
        Jerusalem_area
    }
    public enum OrderStatusEnum { Not_yet_addressed, mail_has_been_sent, Closes_out_of_customer_disrespect, Closes_with_customer_response }
    public enum GuestReqStatusEnum { closed, not_addressed }


    public static class HebrewEnum
    {
        public static IEnumerable<string> getListStrings<T>()
        {
            if (typeof(T) == typeof(AreaEnum))
                return new List<string> { "כל הארץ", "צפון", "מרכז", "דרום ישראל", "ירושלים \nוהסביבה" };
            else if (typeof(T) == typeof(optionsEnum))
                return new List<string> { "הכרחי", "אפשרי", "לא מעוניין" };
            else if (typeof(T) == typeof(GuestReqTypeEnum))
                return new List<string> { "בית מלון", "צימר", "וילה" };
            else if (typeof(T) == typeof(OrderStatusEnum))
                return new List<string> { "טרם טופל", "נשלח מייל", "נסגר מחוסר הענות של הלקוח", "נסגר בהיענות של הלקוח" };
            else if (typeof(T) == typeof(GuestReqStatusEnum))
                return new List<string> { "נסגר", "לא טופל" };
            else
                return null;
        }

        public static string Area(AreaEnum myEnum)
        {
            return getListStrings<AreaEnum>().ToList()[(int)myEnum];
        }        

        public static string options(optionsEnum myEnum)
        {
            return getListStrings<optionsEnum>().ToList()[(int)myEnum];
        }        

        public static string GuestReqType(GuestReqTypeEnum myEnum)
        {
            return getListStrings<GuestReqTypeEnum>().ToList()[(int)myEnum];
        }
       
        public static string OrderStatus(OrderStatusEnum myEnum)
        {
            return getListStrings<OrderStatusEnum>().ToList()[(int)myEnum];
        }

        public static string GuestReqStatus(GuestReqStatusEnum myEnum)
        {
            return getListStrings<GuestReqStatusEnum>().ToList()[(int)myEnum];
        }
    }
}
