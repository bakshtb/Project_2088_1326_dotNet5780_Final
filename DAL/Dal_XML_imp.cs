using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;

namespace DAL
{
    public class Dal_XML_imp : IDAL
    {
        XElement GuestRequestRoot, HostingUnitRoot, HostRoot, OrderRoot , BankBranchRoot;
        public string HostingUnitPath = @"HostingUnitXML.xml",
            GuestRequestPath = @"GuestRequestXML.xml",
            AdminPath = @"AdminXML.xml",
            HostPath = @"HostXML.xml",
            OrderdPath = @"OrderXML.xml",
            BankBranchPath = @"BranchesXML.xml";

        public Dal_XML_imp()
        {
            try
            {
                if (!File.Exists(HostingUnitPath))
                    CreateHostingUnitFile();
                else LoadHostingUnitData();
                if (!File.Exists(GuestRequestPath))
                    CreateGuestRequestFile();
                else LoadGuestRequestData();
                if (!File.Exists(AdminPath))
                    CreateAdminFile();
                else LoadAdminFile();
                if (!File.Exists(HostPath))
                    CreateHostFile();
                else LoadHostFile();
                if (!File.Exists(OrderdPath))
                    CreateOrderFile();
                else LoadOrderFile();
                if (!File.Exists(BankBranchPath))
                    CreateBankBranchFile();
                else LoadBankBranchFile();
            }
            catch { }

        }

        private void LoadBankBranchFile()
        {
            try
            {
                BankBranchRoot = XElement.Load(BankBranchPath);
            }
            catch
            {
                throw new InvalidOperationException("שגיאה בטעינת קובץ סניפי בנק");
            }
        }

        private void CreateBankBranchFile()
        {                       
            WebClient wc = new WebClient();
            try
            {
                string xmlServerPath = @"https://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                wc.DownloadFile(xmlServerPath, BankBranchPath);
            }
            catch (Exception)
            {
                string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(xmlServerPath, BankBranchPath);
            }
            finally
            {
                wc.Dispose();
            }
            LoadBankBranchFile();
        }

        private void LoadOrderFile()
        {
            try
            {
                OrderRoot = XElement.Load(OrderdPath);
            }
            catch
            {
                throw new InvalidOperationException("שגיאה בטעינת קובץ הזמנות");
            }
        }

        private void CreateOrderFile()
        {
            OrderRoot = new XElement("Orders");
            OrderRoot.Save(HostPath);

        }

        private void LoadHostFile()
        {
            try
            {
                HostRoot = XElement.Load(HostPath);
            }
            catch
            {
                throw new InvalidOperationException("שגיאה בטעינת קובץ מארח");
            }
        }

        private void CreateHostFile()
        {
            HostRoot = new XElement("Hosts");
            HostRoot.Save(HostPath);
        }

        private void LoadAdminFile()
        {

        }

        private void CreateAdminFile()
        {

        }

        private void LoadGuestRequestData()
        {
            try
            {
                GuestRequestRoot = XElement.Load(GuestRequestPath);
            }
            catch
            {
                throw new InvalidOperationException("שגיאה בטעינת קובץ בקשות לקוח");
            }
        }

        private void CreateGuestRequestFile()
        {
            GuestRequestRoot = new XElement("GuestRequests");
            GuestRequestRoot.Save(GuestRequestPath);
        }

        private void LoadHostingUnitData()
        {
            try
            {
                HostingUnitRoot = XElement.Load(HostingUnitPath);
            }
            catch
            {
                throw new InvalidOperationException("שגיאה בטעינת קובץ יחידות האירוח");
            }
        }

        private void CreateHostingUnitFile()
        {
            HostingUnitRoot = new XElement("HostingUnits");
            HostingUnitRoot.Save(HostingUnitPath);
        }

        public void addGuestReq(GuestRequest guestRequest)
        {
            XElement GuestRequestKey = new XElement("GuestRequestKey", guestRequest.GuestRequestKey);
            XElement PrivateName = new XElement("firstName", guestRequest.PrivateName);
            XElement FamilyName = new XElement("lastName", guestRequest.FamilyName);
            XElement name = new XElement("name", PrivateName, FamilyName);
            XElement MailAddress = new XElement("MailAddress", guestRequest.MailAddress);
            XElement Status = new XElement("Status", guestRequest.Status);
            XElement RegistrationDate = new XElement("RegistrationDate", guestRequest.RegistrationDate);
            XElement EntryDate = new XElement("EntryDate", guestRequest.EntryDate);
            XElement ReleaseDate = new XElement("ReleaseDate", guestRequest.ReleaseDate);
            XElement Dates = new XElement("Dates", EntryDate , ReleaseDate);
            XElement Area = new XElement("Area", guestRequest.Area);
            XElement Type = new XElement("Type", guestRequest.Type);
            XElement Adults = new XElement("Adults", guestRequest.Adults);
            XElement Children = new XElement("Children", guestRequest.Children);
            XElement Guests = new XElement("Guests", Adults , Children);
            XElement Pool = new XElement("Pool", guestRequest.Pool);
            XElement Jacuzzi = new XElement("Jacuzzi", guestRequest.Jacuzzi);
            XElement Garden = new XElement("Garden", guestRequest.Garden);
            XElement ChildrensAttractions = new XElement("ChildrensAttractions", guestRequest.ChildrensAttractions);
            XElement additionalOptions = new XElement("additionalOptions", Pool , Jacuzzi , Garden , ChildrensAttractions);

            GuestRequestRoot.Add(new XElement("student", GuestRequestKey, name , MailAddress, Status, RegistrationDate, Dates, Area, Type, Guests , additionalOptions));
            GuestRequestRoot.Save(GuestRequestPath);
        }

        public IEnumerable<GuestRequest> getListGuestRequest(Func<GuestRequest, bool> predicate = null)
        {
            return (from item in GuestRequestRoot.Elements()
                    where getGuestRequest(long.Parse(item.Element("GuestRequestKey").Value)) != null
                    select getGuestRequest(long.Parse(item.Element("GuestRequestKey").Value)));
        }

        public GuestRequest getGuestRequest(long key)
        {
            GuestRequest guestRequest;
            try
            {
                guestRequest = (from req in GuestRequestRoot.Elements()
                           where int.Parse(req.Element("GuestRequestKey").Value) == key
                           select new GuestRequest()
                           {
                               GuestRequestKey = long.Parse(req.Element("GuestRequestKey").Value),
                               PrivateName = req.Element("name").Element("PrivateName").Value,
                               FamilyName = req.Element("name").Element("FamilyName").Value,
                               MailAddress = req.Element("MailAddress").Value,
                               Status = getEnumFromString<GuestReqStatusEnum>(req.Element("Status").Value),
                               RegistrationDate = DateTime.Parse(req.Element("RegistrationDate").Value),
                               EntryDate = DateTime.Parse(req.Element("Dates").Element("EntryDate").Value),
                               ReleaseDate = DateTime.Parse(req.Element("Dates").Element("ReleaseDate").Value),
                               Area = getEnumFromString<AreaEnum>(req.Element("Area").Value),
                               Type = getEnumFromString<GuestReqTypeEnum>(req.Element("Type").Value),
                               Adults = int.Parse(req.Element("Guests").Element("Adults").Value),
                               Children = int.Parse(req.Element("Guests").Element("Children").Value),
                               Pool = getEnumFromString<optionsEnum>(req.Element("additionalOptions").Element("Pool").Value),
                               Jacuzzi = getEnumFromString<optionsEnum>(req.Element("additionalOptions").Element("Jacuzzi").Value),
                               Garden = getEnumFromString<optionsEnum>(req.Element("additionalOptions").Element("Garden").Value),
                               ChildrensAttractions = getEnumFromString<optionsEnum>(req.Element("additionalOptions").Element("ChildrensAttractions").Value),
                           }).FirstOrDefault();
            }
            catch
            {
                guestRequest = null;
            }
            return guestRequest;
        }

        public void updateGuestReq(GuestRequest guestRequest)
        {
            XElement guestRequsetElement = (from req in GuestRequestRoot.Elements()
                                            where int.Parse(req.Element("GuestRequestKey").Value) == guestRequest.GuestRequestKey
                                            select req).FirstOrDefault();

            guestRequsetElement.Element("name").Element("PrivateName").Value = guestRequest.PrivateName;
            guestRequsetElement.Element("name").Element("FamilyName").Value = guestRequest.FamilyName;
            guestRequsetElement.Element("MailAddress").Value = guestRequest.MailAddress;
            guestRequsetElement.Element("Status").Value = guestRequest.Status.ToString();
            guestRequsetElement.Element("RegistrationDate").Value = guestRequest.RegistrationDate.ToString();
            guestRequsetElement.Element("Dates").Element("EntryDate").Value = guestRequest.EntryDate.ToString();
            guestRequsetElement.Element("Dates").Element("ReleaseDate").Value = guestRequest.ReleaseDate.ToString();
            guestRequsetElement.Element("Area").Value = guestRequest.Area.ToString();
            guestRequsetElement.Element("Type").Value = guestRequest.Type.ToString();
            guestRequsetElement.Element("Guests").Element("Adults").Value = guestRequest.Adults.ToString();
            guestRequsetElement.Element("Guests").Element("Children").Value = guestRequest.Children.ToString();
            guestRequsetElement.Element("additionalOptions").Element("Pool").Value = guestRequest.Pool.ToString();
            guestRequsetElement.Element("additionalOptions").Element("Jacuzzi").Value = guestRequest.Jacuzzi.ToString();
            guestRequsetElement.Element("additionalOptions").Element("Garden").Value = guestRequest.Garden.ToString();
            guestRequsetElement.Element("additionalOptions").Element("ChildrensAttractions").Value = guestRequest.ChildrensAttractions.ToString();

            GuestRequestRoot.Save(GuestRequestPath);
        }


        public void addHost(Host host)
        {
            XElement HostKey = new XElement("HostKey", host.HostKey);
            XElement FhoneNumber = new XElement("FhoneNumber", host.FhoneNumber);
            XElement BankAccountNumber = new XElement("BankAccountNumber", host.BankAccountNumber);
            XElement PrivateName = new XElement("PrivateName", host.PrivateName);
            XElement FamilyName = new XElement("FamilyName", host.FamilyName);
            XElement name = new XElement("name", PrivateName, FamilyName);           
            XElement MailAddress = new XElement("MailAddress", host.MailAddress);
            XElement CollectionClearance = new XElement("CollectionClearance", host.CollectionClearance);    
            
            XElement BankNumber = new XElement("BankNumber", host.BankBranchDetails.BankNumber);
            XElement BankName = new XElement("BankName", host.BankBranchDetails.BankName);
            XElement Bank = new XElement("Bank", BankNumber , BankName);            
            XElement BranchNumber = new XElement("BranchNumber", host.BankBranchDetails.BranchNumber);
            XElement BranchAddress = new XElement("BranchAddress", host.BankBranchDetails.BranchAddress);
            XElement BranchCity = new XElement("BranchCity", host.BankBranchDetails.BranchCity);
            XElement Address = new XElement("Address", BranchAddress , BranchCity);


            XElement BankBranchDetails = new XElement("BankBranchDetails", Bank, BranchNumber, Address);

            HostRoot.Add(new XElement("Host", HostKey, name, FhoneNumber, BankAccountNumber , MailAddress, MailAddress, CollectionClearance, BankBranchDetails));
            HostRoot.Save(HostPath);
        }

        public bool deleteHost(long key)
        {
            XElement Hostelement;
            try
            {
                Hostelement = (from item in HostRoot.Elements()
                                  where int.Parse(item.Element("HostKey").Value) == key
                                  select item).FirstOrDefault();
                Hostelement.Remove();
                HostRoot.Save(HostPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Host getHost(long key)
        {
            Host host;
            try
            {
                host = (from item in HostRoot.Elements()
                                where int.Parse(item.Element("HostKey").Value) == key
                                select new Host()
                                {
                                    HostKey = long.Parse(item.Element("HostKey").Value),
                                    BankAccountNumber = int.Parse(item.Element("BankAccountNumber").Value),
                                    FamilyName = item.Element("FamilyName").Value,
                                    PrivateName = item.Element("PrivateName").Value,
                                    MailAddress = item.Element("MailAddress").Value,
                                    CollectionClearance = bool.Parse(item.Element("CollectionClearance").Value),
                                    FhoneNumber = item.Element("FhoneNumber").Value,
                                    BankBranchDetails = new BankBranch()
                                    {
                                        BankName = item.Element("BankBranchDetails").Element("Bank").Element("BankName").Value,
                                        BankNumber = int.Parse(item.Element("BankBranchDetails").Element("Bank").Element("BankNumber").Value),
                                        BranchAddress = item.Element("BankBranchDetails").Element("Address").Element("BranchAddress").Value,
                                        BranchCity = item.Element("BankBranchDetails").Element("Address").Element("BranchCity").Value,
                                        BranchNumber = int.Parse(item.Element("BankBranchDetails").Element("BranchNumber").Value)
                                    }
                                }).FirstOrDefault();
            }
            catch
            {
                host = null;
            }
            return host;
        }

        public IEnumerable<Host> getListHosts(Func<Host, bool> predicate = null)
        {
            return (from item in HostRoot.Elements()
                    where getHost(long.Parse(item.Element("HostKey").Value)) != null
                    select getHost(long.Parse(item.Element("HostKey").Value)));
        }

        public void updateHost(Host host)
        {
            XElement HostElement = (from item in GuestRequestRoot.Elements()
                                            where int.Parse(item.Element("HostKey").Value) == host.HostKey
                                            select item).FirstOrDefault();

            HostElement.Element("name").Element("PrivateName").Value = host.PrivateName;
            HostElement.Element("name").Element("FamilyName").Value = host.FamilyName;
            HostElement.Element("MailAddress").Value = host.MailAddress;
            HostElement.Element("FhoneNumber").Value = host.FhoneNumber;
            HostElement.Element("BankAccountNumber").Value = host.BankAccountNumber.ToString();
            HostElement.Element("CollectionClearance").Value = host.CollectionClearance.ToString();
            HostElement.Element("BankBranchDetails").Element("Bank").Element("BankName").Value = host.BankBranchDetails.BankName;
            HostElement.Element("BankBranchDetails").Element("Bank").Element("BankNumber").Value = host.BankBranchDetails.BankNumber.ToString();
            HostElement.Element("BankBranchDetails").Element("Address").Element("BranchAddress").Value = host.BankBranchDetails.BranchAddress;
            HostElement.Element("BankBranchDetails").Element("Address").Element("BranchCity").Value = host.BankBranchDetails.BranchCity;
            HostElement.Element("BankBranchDetails").Element("BranchNumber").Value = host.BankBranchDetails.BranchNumber.ToString();

            HostRoot.Save(HostPath);
        }

        public void addHostingUnit(HostingUnit hostingUnit)
        {
            XElement HostingUnitKey = new XElement("HostingUnitKey", hostingUnit.HostingUnitKey);
            XElement AllDates = new XElement("AllDates", ListDateTimeToString(hostingUnit.AllDates));
            XElement Area = new XElement("Area", hostingUnit.Area);
            XElement HostingUnitName = new XElement("HostingUnitName", hostingUnit.HostingUnitName);

            XElement OwnerPrivateName = new XElement("OwnerPrivateName", hostingUnit.Owner.PrivateName);
            XElement OwnerFamilyName = new XElement("OwnerFamilyName", hostingUnit.Owner.FamilyName);
            XElement OwnerFhoneNumber = new XElement("OwnerFhoneNumber", hostingUnit.Owner.FhoneNumber);
            XElement OwnerHostKey = new XElement("OwnerHostKey", hostingUnit.Owner.HostKey);
            XElement OwnerMailAddress = new XElement("OwnerMailAddress", hostingUnit.Owner.MailAddress);
            XElement OwnerBankAccountNumber = new XElement("OwnerBankAccountNumber", hostingUnit.Owner.BankAccountNumber);
            XElement OwnerCollectionClearance = new XElement("OwnerCollectionClearance", hostingUnit.Owner.CollectionClearance);

            XElement BankName = new XElement("BankName", hostingUnit.Owner.BankBranchDetails.BankName);
            XElement BankNumber = new XElement("BankNumber", hostingUnit.Owner.BankBranchDetails.BankNumber);
            XElement BranchAddress = new XElement("BranchAddress", hostingUnit.Owner.BankBranchDetails.BranchAddress);
            XElement BranchCity = new XElement("BranchCity", hostingUnit.Owner.BankBranchDetails.BranchCity);
            XElement BranchNumber = new XElement("BranchNumber", hostingUnit.Owner.BankBranchDetails.BranchNumber);

            XElement Branch = new XElement("BankBranch", BankName , BankNumber , BranchAddress , BranchCity , BranchNumber);

            XElement Owner = new XElement("Owner", OwnerPrivateName, OwnerFamilyName, OwnerFhoneNumber, OwnerHostKey, OwnerMailAddress, OwnerCollectionClearance, OwnerBankAccountNumber , Branch);

            HostingUnitRoot.Add(new XElement("HostingUnit", HostingUnitKey, AllDates, Area, HostingUnitName, Owner));
            HostingUnitRoot.Save(HostingUnitPath);
        }

        public HostingUnit getHostingUnit(long key)
        {
            HostingUnit hostingUnit;
            try
            {
                hostingUnit = (from item in HostingUnitRoot.Elements()
                               where long.Parse(item.Element("HostingUnitKey").Value) == key
                               select new HostingUnit()
                               {
                                   HostingUnitKey = int.Parse(item.Element("HostingUnitKey").Value),
                                   Area = getEnumFromString<AreaEnum>(item.Element("Area").Value),
                                   HostingUnitName = item.Element("HostingUnitName").Value,
                                   AllDates = stringToListDateTime(item.Element("AllDates").Value),
                                   Owner = new Host()
                                   {
                                       HostKey = long.Parse(item.Element("Owner").Element("OwnerHostKey").Value),
                                       BankAccountNumber = int.Parse(item.Element("Owner").Element("OwnerBankAccountNumber").Value),
                                       FamilyName = item.Element("Owner").Element("OwnerFamilyName").Value,
                                       PrivateName = item.Element("Owner").Element("OwnerPrivateName").Value,
                                       MailAddress = item.Element("Owner").Element("OwnerMailAddress").Value,
                                       CollectionClearance = bool.Parse(item.Element("Owner").Element("OwnerCollectionClearance").Value),
                                       FhoneNumber = item.Element("Owner").Element("OwnerFhoneNumber").Value,
                                       BankBranchDetails = new BankBranch()
                                       {
                                           BankName = item.Element("Owner").Element("BankBranch").Element("BankName").Value,
                                           BankNumber = int.Parse(item.Element("Owner").Element("BankBranch").Element("BankNumber").Value),
                                           BranchAddress = item.Element("Owner").Element("BankBranch").Element("BranchAddress").Value,
                                           BranchCity = item.Element("Owner").Element("BankBranch").Element("BranchCity").Value,
                                           BranchNumber = int.Parse(item.Element("Owner").Element("BankBranch").Element("BranchNumber").Value)
                                       }
                                   }                                 
                               }).FirstOrDefault();
            }
            catch
            {
                hostingUnit = null;
            }
            return hostingUnit;            
        }

        public IEnumerable<HostingUnit> getListHostingUnit(Func<HostingUnit, bool> predicate = null)
        {
            return (from item in HostingUnitRoot.Elements()
                    where getHostingUnit(long.Parse(item.Element("HostingUnitKey").Value)) != null
                    select getHostingUnit(long.Parse(item.Element("HostingUnitKey").Value)));
        }


        public bool deleteHostingUnit(long key)
        {
            XElement HostingUnitElement;
            try
            {
                HostingUnitElement = (from item in HostingUnitRoot.Elements()
                                      where long.Parse(item.Element("HostingUnitKey").Value) == key
                                      select item).FirstOrDefault();
                HostingUnitElement.Remove();
                HostingUnitRoot.Save(HostingUnitPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void updateHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }


        private string ListDateTimeToString(List<DateTime> dateTimes)
        {
            string result = "";

            for (int i = 0; i < dateTimes.Count(); i++)
                result +=  dateTimes[i].ToString("dd/MM/yyyy") + "$";

            return result;
        }

        private List<DateTime> stringToListDateTime(string st)
        {

            List<DateTime> dateTimes = new List<DateTime>();

            string[] Dates = st.Split('$');

            for (int i = 0; i < Dates.Length - 1; i++)
            {
                dateTimes.Add(DateTime.ParseExact(Dates[i], "dd/MM/yyyy", null));
            }
            return dateTimes;

        }

        public void addOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void AddProfitToAdmin(int amount)
        {
            throw new NotImplementedException();
        }

       

       

        public void deleteOrder(long key)
        {
            throw new NotImplementedException();
        }

        public string getAdminPass()
        {
            throw new NotImplementedException();
        }

        public int getAdminProfit()
        {
            throw new NotImplementedException();
        }       
        
        

        public List<BankBranch> getListBankBranchs()
        {

            int i = BankBranchRoot.Elements().Count();

            List<BankBranch> bankBranchs;
            try
            {
                bankBranchs = (from item in BankBranchRoot.Elements()
                               select new BankBranch()
                               {
                                   BankNumber = int.Parse(item.Element("קוד_בנק").Value),
                                   BankName = item.Element("שם_בנק").Value,
                                   BranchNumber = int.Parse(item.Element("קוד_סניף").Value),
                                   BranchAddress = item.Element("כתובת_ה-ATM").Value,
                                   BranchCity = item.Element("ישוב").Value
                               }).ToList();
            }
            catch
            {
                bankBranchs = null;
            }
            return bankBranchs;

        }

        
        

        public IEnumerable<Order> getListOrders(Func<Order, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(long key)
        {
            throw new NotImplementedException();
        }

        public void setAdminPass(string pass)
        {
            throw new NotImplementedException();
        }

        

        

        

        public void updateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public T getEnumFromString<T>(string st)
        {
            return (from item in (T[])Enum.GetValues(typeof(T))
                    where item.ToString() == st
                    select item).FirstOrDefault();
        }
    }
}
