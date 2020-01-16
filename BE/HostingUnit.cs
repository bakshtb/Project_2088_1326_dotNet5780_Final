using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class HostingUnit
    {
        public long HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }
        public AreaEnum Area { get; set; }
        public string SubArea { get; set; }


        public List<DateTime> AllDates { get; set; }

        public override string ToString()
        {
            string ret = "מספר יחידת אירוח: " + HostingUnitKey
                        + "\nשם היחידה: " + HostingUnitName
                        + "\nאיזור: " + HebrewEnum.Area(Area);

           
            return ret;
        }
    }
}
