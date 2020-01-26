using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;
using DS;

namespace DAL
{
    public class XMLHandler
    {
        XElement GuestRequestRoot, HostingUnitRoot, HostRoot, OrderRoot;
        public string HostingUnitPath = @"HostingUnitXML.xml",
            GuestRequestPath = @"GuestRequestXML.xml",
            AdminPath = @"AdminXML.xml",
            HostPath = @"HostXML.xml",
            OrderdPath = @"OrderXML.xml";

        private static XMLHandler Handler = null;

        public static XMLHandler GetXMLHandler()
        {
            if (Handler == null)
                Handler = new XMLHandler();
            return Handler;
        }

        private XMLHandler()
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
            }
            catch { }

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
            OrderRoot = new XElement("Order");
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
            HostRoot = new XElement("Host");
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
            GuestRequestRoot = new XElement("GuestRequest");
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
            HostingUnitRoot = new XElement("HostingUnit");
            HostingUnitRoot.Save(HostingUnitPath);
        }
    }
}
