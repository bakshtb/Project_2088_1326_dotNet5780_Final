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
        public GuestReqTypeEnum Type { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public optionsEnum Pool { get; set; }
        public optionsEnum Jacuzzi { get; set; }
        public optionsEnum Garden { get; set; }
        public optionsEnum ChildrensAttractions { get; set; }



        public override string ToString()
        {
            string ret =   "בקשה מספר: " + GuestRequestKey
                         + "\nשם פרטי: " + PrivateName
                         + "\nשם משפחה: " + FamilyName
                         + "\nמייל: " + MailAddress
                         + "\nמספר מבוגרים: " + Adults
                         + "\nמספר ילדים: " + Children
                         + "\nתאריך רישום: " + RegistrationDate.ToString()
                         + "\nתאריך כניסה: " + EntryDate.ToString()
                         + "\nתאריך יציאה: " + ReleaseDate.ToString()
                         + "\nסטטוס: " + Status
                         + "\nאיזור: " + Area
                         + "\nסוג: " + Type 
                         + "\nבריכה: " + Pool
                         + "\nג'קוזי: " + Jacuzzi
                         + "\nגינה: " + Garden 
                         + "\nאטרקציות לילדים: " + ChildrensAttractions;
            return ret;
        }
    }
}
