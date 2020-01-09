using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        public int HostKey { get; set; }
        public int FhoneNumber { get; set; }
        public int BankAccountNumber { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string MailAddress{ get; set; }
        public bool CollectionClearance { get; set; }
        public BankBranch BankBranchDetails { get; set; }

        public override string ToString()
        {
            string ret = "Host Key:" + HostKey
                        + "\nPrivate Name:" + PrivateName
                        + "\nFamily Name:" + FamilyName
                        + "\nFhone Number:" + FhoneNumber
                        + "\nMail Address:" + MailAddress
                        + "\nBank Branch Details:" + BankBranchDetails.ToString()
                        + "\nBank Account Number:" + BankAccountNumber
                        + "\nCollection Clearance:" + CollectionClearance.ToString();
            return ret;
        }
    }
}
