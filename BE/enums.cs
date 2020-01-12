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


    public static class EnumToString
    {
        public static IEnumerable<string> ToFriendlyString()
        {

            return new List<string> { "כל הארץ", "מרכז הארץ" };

            //switch (areaEnum)
            //{
            //    case AreaEnum.All_Israel:
            //        return "כל הארץ";
            //    case AreaEnum.Central_Israel:
            //        return "מרכז הארץ";
            //    case AreaEnum.Jerusalem_area:
            //        return "איזור ירושלים";
            //    case AreaEnum.north_Israel:
            //        return "צפון הארץ";
            //    default:
            //        return "דרום הארץ";
            //}
        }
    }
}
