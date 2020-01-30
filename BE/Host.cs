using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class Host
    {
        public long HostKey { get; set; }

        public string FhoneNumber { get; set; }        
        public int BankAccountNumber { get; set; }   
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string MailAddress{ get; set; }

        //commission fee for admin
        public bool CollectionClearance { get; set; }
        public BankBranch BankBranchDetails { get; set; }

        public override string ToString()
        {
            string ret = "מספר ת.ז. " + HostKey
                        + "\nשם: " + PrivateName +" "+ FamilyName
                        + "\nמספר טלפון: " + FhoneNumber
                        + "\nמייל: " + MailAddress;
            return ret;
        }
    }
}
