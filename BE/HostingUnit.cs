using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HostingUnit
    {
        public long HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }
        public bool[,] Diary { get; set; }
        public AreaEnum Area { get; set; }
        public string SubArea { get; set; }


        

        public override string ToString()
        {
            string ret = "Hosting Unit Key: " + HostingUnitKey
                        + "\nOwner: " + Owner
                        + "\nHosting Unit Name: " + HostingUnitName
                        + "\nDiary: ";

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    ret += Diary[i, j].ToString() + " , ";
                }
                ret += "\n";
                    
            }

            ret += "\nArea: " + Area
                 + "\nSubArea: " + SubArea;
            return ret;
        }
    }
}
