using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    
}
