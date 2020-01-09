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
                PrivateName = "Itzhak",
                FamilyName = "Avrahamson",
                MailAddress = "Itzhak@gmail.com",
                Status = BE.GuestReqStatusEnum.not_addressed,
                RegistrationDate = new DateTime(1948, 01, 01),
                EntryDate = new DateTime(2019, 1, 2),
                ReleaseDate = new DateTime(2019, 1, 5),
                Area = 0,
                SubArea = "Jerusalem",
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

                PrivateName = "Yahakov",
                FamilyName = "Itzhakson",
                MailAddress = "Yahakov@gmail.com",
                Status = BE.GuestReqStatusEnum.not_addressed,
                RegistrationDate = new DateTime(1950, 01, 01),
                EntryDate = new DateTime(2018, 10, 10),
                ReleaseDate = new DateTime(2018, 11, 10),
                Area = BE.AreaEnum.north_Israel,
                SubArea = "Beer Sheva",
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

                PrivateName = "Reuven",
                FamilyName = "Yakovson",
                MailAddress = "Reuven@gmail.com",
                Status = BE.GuestReqStatusEnum.not_addressed,
                RegistrationDate = new DateTime(1955, 01, 01),
                EntryDate = new DateTime(2017, 10, 10),
                ReleaseDate = new DateTime(2017, 11, 10),
                Area = 0,
                SubArea = "Hevron",
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
                HostKey = 12344321,
                PrivateName = "Yeuda",
                FamilyName = "Yakovson",
                FhoneNumber = 0504763953,
                MailAddress = "Yeuda@gmail.com",
                BankBranchDetails = new BE.BankBranch
                {
                    BankNumber = 1,
                    BankName = "Yahav",
                    BranchNumber = 7543,
                    BranchAddress = "Yafo 16",
                    BranchCity = "Tel Aviv",
                },
                BankAccountNumber = 344565,
                CollectionClearance = false
            });
            tempHostList.Add(new BE.Host()
            {
                HostKey = 87654321,
                PrivateName = "Levi",
                FamilyName = "Yakovson",
                FhoneNumber = 0533367345,
                MailAddress = "Levi@gmail.com",
                BankBranchDetails = new BE.BankBranch
                {
                    BankNumber = 1,
                    BankName = "Leumi",
                    BranchNumber = 4321,
                    BranchAddress = "King David St 18",
                    BranchCity = "Jerusalem",
                },
                BankAccountNumber = 654321,
                CollectionClearance = true
            });
            tempHostList.Add(new BE.Host()
            {
                HostKey = 12345678,
                PrivateName = "Shimon",
                FamilyName = "Yakovson",
                FhoneNumber = 0543456654,
                MailAddress = "Shimon@gmail.com",
                BankBranchDetails = new BE.BankBranch
                {
                    BankNumber = 1,
                    BankName = "Pepper",
                    BranchNumber = 1234,
                    BranchAddress = "Vitzman St 24",
                    BranchCity = "Tel Aviv",
                },
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
                OrderDate = new DateTime(2020, 1, 1),
                isClosed = false,
                cost = 1000
            });
            tempOrderList.Add(new BE.Order()
            {
                HostingUnitKey = 10000001,
                GuestRequestKey = 10000001,
                OrderKey = 10000001,
                Status = BE.OrderStatusEnum.Closes_with_customer_response,
                CreateDate = new DateTime(2020, 2, 2),
                OrderDate = new DateTime(2020, 2, 2),
                isClosed = true,
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
                cost = 3000
            });

            tempHostingUnitList.Add(new BE.HostingUnit()
            {

                Owner = tempHostList[0],
                HostingUnitName = "Shimom's Tzimer",
                Diary = new bool[12, 31],
                Area = BE.AreaEnum.Jerusalem_area,
                SubArea = "Jerusalem"
            });
            tempHostingUnitList.Add(new BE.HostingUnit()
            {

                Owner = tempHostList[1],
                HostingUnitName = "Levi's Tzimer",
                Diary = new bool[2, 31],
                Area = BE.AreaEnum.All_Israel,
                SubArea = "Heifa"
            });
            tempHostingUnitList.Add(new BE.HostingUnit()
            {

                Owner = tempHostList[2],
                HostingUnitName = "Yeuda's Tzimer",
                Diary = new bool[12, 31],
                Area = BE.AreaEnum.Central_Israel,
                SubArea = "Tel-Aviv"
            });
        }


       
    }
}
