using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class GuestRequest
    {
        public long GuestRequestKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string MailAddress { get; set; }
        public GuestReqStatusEnum Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public AreaEnum Area { get; set; }
        public string SubArea { get; set; }
        public GuestReqTypeEnum Type { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public optionsEnum Pool { get; set; }
        public optionsEnum Jacuzzi { get; set; }
        public optionsEnum Garden { get; set; }
        public optionsEnum ChildrensAttractions { get; set; }


        public override string ToString()
        {
            string ret = "Guest Request Key: " + GuestRequestKey + "\nPrivate Name: " + PrivateName + "\nFamily Name: " + FamilyName
                         + "\nMail Address: " + MailAddress + "\nAdults: " + Adults + "\nChildren: " + Children
                         + "\nRegistrationDate: " + RegistrationDate.ToString() + "\nEntryDate: " + EntryDate.ToString() + "\nReleaseDate: " + ReleaseDate.ToString()
                         + "\nStatus: " + Status + "\nArea: " + Area
                         + "\nSub Area: " + SubArea + "\nType: " + Type + "\nPool: " + Pool
                         + "\nJacuzzi: " + Jacuzzi + "\nGarden: " + Garden + "\nChildrens Attractions: " + ChildrensAttractions;
            return ret;
        }
    }
}
