using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    
    public static class DataSource
    {
        public static List<BE.GuestRequest> GuestRequestList;
        public static List<BE.HostingUnit> HostingUnitList;
        public static List<BE.Order> OrderList;
        public static List<BE.Host> HostList;
        public static List<BE.GuestRequest> tempGuestRequestList;
        public static List<BE.HostingUnit> tempHostingUnitList;
        public static List<BE.Order> tempOrderList;
        public static List<BE.Host> tempHostList;

        static DataSource()
        {
            GuestRequestList = new List<BE.GuestRequest>();
            HostingUnitList = new List<BE.HostingUnit>();
            OrderList = new List<BE.Order>();
            HostList = new List<BE.Host>();
            tempGuestRequestList = new List<BE.GuestRequest>();
            tempHostList = new List<BE.Host>();
            tempOrderList = new List<BE.Order>();
            tempHostingUnitList = new List<BE.HostingUnit>();


            tempGuestRequestList.Add(new BE.GuestRequest()
            {
                PrivateName = "יצחק",
                FamilyName = "אברהמסון",
                MailAddress = "Itzhak@gmail.com",
                Status = BE.GuestReqStatusEnum.not_addressed,
                RegistrationDate = new DateTime(1948, 01, 01),
                EntryDate = new DateTime(2019, 1, 2),
                ReleaseDate = new DateTime(2019, 1, 5),
                Area = 0,
                
                Type = 0,
                Adults = 2,
                Children = 2,
                Pool = 0,
                Jacuzzi = 0,
                Garden = 0,
                ChildrensAttractions = 0
            });

            tempGuestRequestList.Add(new BE.GuestRequest()
            {

                PrivateName = "יעקןב",
                FamilyName = "יצחקסון",
                MailAddress = "Yahakov@gmail.com",
                Status = BE.GuestReqStatusEnum.not_addressed,
                RegistrationDate = new DateTime(1950, 01, 01),
                EntryDate = new DateTime(2018, 10, 10),
                ReleaseDate = new DateTime(2018, 11, 10),
                Area = BE.AreaEnum.north_Israel,
                
                Type = 0,
                Adults = 3,
                Children = 2,
                Pool = 0,
                Jacuzzi = 0,
                Garden = 0,
                ChildrensAttractions = 0
            });
            tempGuestRequestList.Add(new BE.GuestRequest()
            {

                PrivateName = "ראובן",
                FamilyName = "יעקובסון",
                MailAddress = "Reuven@gmail.com",
                Status = BE.GuestReqStatusEnum.not_addressed,
                RegistrationDate = new DateTime(1955, 01, 01),
                EntryDate = new DateTime(2017, 10, 10),
                ReleaseDate = new DateTime(2017, 11, 10),
                Area = 0,
                
                Type = 0,
                Adults = 2,
                Children = 2,
                Pool = 0,
                Jacuzzi = 0,
                Garden = 0,
                ChildrensAttractions = 0
            });

            tempHostList.Add(new BE.Host()
            {
                HostKey = 2113020,
                PrivateName = "יהודה",
                FamilyName = "יעקבוסון",
                FhoneNumber = "0504763953",
                MailAddress = "Yeuda@gmail.com",                
                BankAccountNumber = 344565,
                CollectionClearance = true
            });
            tempHostList.Add(new BE.Host()
            {
                HostKey = 87654321,
                PrivateName = "לוי",
                FamilyName = "יעקובסון",
                FhoneNumber = "0533367345",
                MailAddress = "Levi@gmail.com",                
                BankAccountNumber = 654321,
                CollectionClearance = true
            });
            tempHostList.Add(new BE.Host()
            {
                HostKey = 12345678,
                PrivateName = "שמעון",
                FamilyName = "יעקובסון",
                FhoneNumber = "0543456654",
                MailAddress = "Shimon@gmail.com",                
                BankAccountNumber = 123456,
                CollectionClearance = true
            });

            tempOrderList.Add(new BE.Order()
            {
                HostingUnitKey = 10000000,
                GuestRequestKey = 10000000,
                OrderKey = 10000000,
                Status = BE.OrderStatusEnum.Not_yet_addressed,
                CreateDate = new DateTime(2020, 1, 1),
                OrderDate = new DateTime(2020, 10, 1),
                isClosed = false,
                isSendMail = false,
                cost = 1000
            });
            tempOrderList.Add(new BE.Order()
            {
                HostingUnitKey = 10000001,
                GuestRequestKey = 10000001,
                OrderKey = 10000001,
                Status = BE.OrderStatusEnum.Closes_with_customer_response,
                CreateDate = new DateTime(2020, 1, 2),
                OrderDate = new DateTime(2020, 1, 3),
                isClosed = true,
                isSendMail = true,
                cost = 2000
            });
            tempOrderList.Add(new BE.Order()
            {
                HostingUnitKey = 10000002,
                GuestRequestKey = 10000002,
                OrderKey = 10000002,
                Status = BE.OrderStatusEnum.mail_has_been_sent,
                CreateDate = new DateTime(2020, 3, 3),
                OrderDate = new DateTime(2020, 3, 3),
                isClosed = false,
                isSendMail = true,
                cost = 3000
            });

            tempHostingUnitList.Add(new BE.HostingUnit()
            {   
                Owner = tempHostList[0],
                HostingUnitName = "הצימר של שמעון",
                
                Area = BE.AreaEnum.Jerusalem_area,
            });
            tempHostingUnitList.Add(new BE.HostingUnit()
            {                
                Owner = tempHostList[1],
                HostingUnitName = "הצימר של לוי",
                
                Area = BE.AreaEnum.All_Israel,
            });
            tempHostingUnitList.Add(new BE.HostingUnit()
            {
                Owner = tempHostList[2],
                HostingUnitName = "הצימר של יהודה",
                
                Area = BE.AreaEnum.Central_Israel,
            });
        }


       
    }
}
