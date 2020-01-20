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
            string ret = "בקשה מספר: " + GuestRequestKey
                         + "\nשם פרטי: " + PrivateName
                         + "\t\tשם משפחה: " + FamilyName
                         + "\nמייל: " + MailAddress
                         + "\tמספר מבוגרים: " + Adults
                         + "\nמספר ילדים: " + Children
                         + "\t\tתאריך רישום: " + RegistrationDate.ToString("dd/MM/yyyy")
                         + "\nתאריך כניסה: " + EntryDate.ToString("dd/MM/yyyy")
                         + "\tתאריך יציאה: " + ReleaseDate.ToString("dd/MM/yyyy")
                         + "\nסטטוס: " + HebrewEnum.GuestReqStatus(Status)
                         + "\t\tאיזור: " + HebrewEnum.Area(Area)
                         + "\nסוג: " + HebrewEnum.GuestReqType(Type)
                         + "\t\tבריכה: " + HebrewEnum.options(Pool)
                         + "\nג'קוז: " + HebrewEnum.options(Jacuzzi)
                         + "\t\tגינה: " + HebrewEnum.options(Garden)
                         + "\nאטרקציות לילדים: " + HebrewEnum.options(ChildrensAttractions);

            return ret;
        }
    }
}
