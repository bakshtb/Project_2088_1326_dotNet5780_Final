//מגישים: ברוך באקשט 211302088, חזקי באטוויניק 312731326

using System;
using System.Linq;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            BL.BL_basic bl = new BL.BL_basic();
            int coise = -1;
            int key , num;
            int tempEnum;
            do
            {
                Console.WriteLine("Please select one of the following options: " +
                "\n\t0. Exit" +
                "\n\t1. Add a guest request" +
                "\n\t2. Add host" +
                "\n\t3. Add a hosting unit" +
                "\n\t4. Add order (match a guest request with a hosting unit)." +
                "\n\t5. Delete a hosting unit" +
                "\n\t6. Update order status." +
                "\n\t7. Update host." +
                "\n\t8. Update hosting unit." +
                "\n\t9. Update guest request." +
                "\n\t10. Print information of hosts." +
                "\n\t11. Print information of hosting units." +
                "\n\t12. Print information of guest requests." +
                "\n\t13. Print information of orders." +
                "\n\t14. Print information of all bank branches." +
                "\n\t15. Print all hosting units that availables on a date." +
                "\n\t16. Print all orders that are larger than." +
                "\n\t17. Print the amount of orders sent to guest" +
                "\n\t18. Print the number of bookings accepted for hosting unit"+
                "\n\t19. Print customer requirements list that grouped according to the required area." +
                "\n\t20. Print Customer requirements list is grouped according to the number of vacationers." +
                "\n\t21. Print a hosts list that grouped by the number of hosting units they hold" +
                "\n\t22. Print List of hosting units grouped according to the required area.");
                
                try
                {
                    coise = int.Parse(Console.ReadLine());

                    switch (coise)
                    {
                        case 0: break;

                        case 1:
                            BE.GuestRequest request = new BE.GuestRequest();


                            Console.Write("PrivateName = ");
                            request.PrivateName = Console.ReadLine();
                            Console.Write("FamilyName = ");
                            request.FamilyName = Console.ReadLine();
                            Console.Write("MailAddress = ");
                            request.MailAddress = Console.ReadLine();
                            Console.Write("RegistrationDate [D/M/Y]= ");
                            request.RegistrationDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("EntryDate [D/M/Y]= ");
                            request.EntryDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("ReleaseDate [D/M/Y]= ");
                            request.ReleaseDate = DateTime.Parse(Console.ReadLine());
                            do
                            {
                                Console.Write("Please select an area: " +
                               "\n\t1:All Israel" +
                               "\n\t2:north Israel" +
                               "\n\t3:Central Israel" +
                               "\n\t4:South Israel" +
                               "\n\t5:Jerusalem area\n");
                                Console.Write("your choice (1-5): ");
                                tempEnum = int.Parse(Console.ReadLine());
                            } while (tempEnum < 1 || tempEnum > 5);

                            request.Area = (BE.AreaEnum)(tempEnum -1);

                            Console.Write("subArea = ");
                            request.SubArea = Console.ReadLine();

                            do
                            {
                                Console.Write("Please select an hosting unit type: " +
                                "\n\t1:Hotel" +
                                "\n\t2:Zimmer " +
                                "\n\t3:Villa" +
                                "\n\t4:South\n");
                                Console.Write("your choice (1-4): ");
                                tempEnum = int.Parse(Console.ReadLine());
                            } while (tempEnum < 1 || tempEnum > 4);

                            request.Type = (BE.GuestReqTypeEnum)(tempEnum - 1);

                            Console.Write("Amount of children = ");
                            request.Children = int.Parse(Console.ReadLine());
                            Console.Write("Amount of adults = ");
                            request.Adults = int.Parse(Console.ReadLine());

                            do
                            {
                                Console.Write("Please select the pool options: " +
                                "\n\t1:necessary" +
                                "\n\t2:Possible " +
                                "\n\t3:not interested\n");
                                Console.Write("your choice (1-3): ");
                                tempEnum = int.Parse(Console.ReadLine());
                            } while (tempEnum < 1 || tempEnum > 3);

                            request.Pool = (BE.optionsEnum)(tempEnum - 1);

                            do
                            {
                                Console.Write("Please select the jacuzzi options: " +
                                "\n\t1:necessary" +
                                "\n\t2:Possible " +
                                "\n\t3:not interested\n");
                                Console.Write("your choice (1-3): ");
                                tempEnum = int.Parse(Console.ReadLine());
                            } while (tempEnum < 1 || tempEnum > 3);

                            request.Jacuzzi = (BE.optionsEnum)(tempEnum - 1);

                            do
                            {
                                Console.Write("Please select the garden options: " +
                                "\n\t1:necessary" +
                                "\n\t2:Possible " +
                                "\n\t3:not interested\n");
                                Console.Write("your choice (1-3): ");
                                tempEnum = int.Parse(Console.ReadLine());
                            } while (tempEnum < 1 || tempEnum > 3);

                            request.Garden = (BE.optionsEnum)(tempEnum - 1);

                            do
                            {
                                Console.Write("Please select the ChildrensAttractions options: " +
                                "\n\t1:necessary" +
                                "\n\t2:Possible " +
                                "\n\t3:not interested\n");
                                Console.Write("your choice (1-3): ");
                                tempEnum = int.Parse(Console.ReadLine());
                            } while (tempEnum < 1 || tempEnum > 3);

                            request.ChildrensAttractions = (BE.optionsEnum)(tempEnum - 1);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\tGuest request NO: " + bl.addGuestReq(request) + " added successfully!");
                            Console.ForegroundColor = ConsoleColor.White;

                            break;

                        case 2:

                            BE.Host host= new BE.Host();

                            Console.Write("Host ID = ");
                            host.HostKey = int.Parse(Console.ReadLine());
                            Console.Write("Host fhone number = ");
                            host.FhoneNumber = int.Parse(Console.ReadLine());
                            Console.Write("Host bank account number = ");
                            host.BankAccountNumber = int.Parse(Console.ReadLine());
                            Console.Write("Host private name = ");
                            host.PrivateName = Console.ReadLine();
                            Console.Write("Host family name = ");
                            host.FamilyName = Console.ReadLine();
                            Console.Write("Host Mail address = ");
                            host.MailAddress = Console.ReadLine();
                            Console.Write("Is Collection Clearance? [0 = no, 1 = yes] = ");
                            host.CollectionClearance = (int.Parse(Console.ReadLine()) == 1);

                            do
                            {
                                Console.WriteLine("Please select from the following list who is your bank branch");
                                int count = 1;
                                foreach (var bankBranchs in bl.getListBankBranchs())
                                {

                                    Console.WriteLine("------------------------");
                                    Console.WriteLine("           " + (count++));
                                    Console.WriteLine("------------------------");
                                    Console.WriteLine(bankBranchs.ToString());
                                }
                                Console.WriteLine("------------------------");
                                Console.Write("Your bank branch is (1-5): ");

                                num = int.Parse(Console.ReadLine());
                            } while (num < 1 || num > 5);

                            
                            host.BankBranchDetails = bl.getListBankBranchs()[num - 1];

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\tHosting unit NO: " + bl.addHost(host) + " added successfully!");
                            Console.ForegroundColor = ConsoleColor.White;

                            break;

                        case 3:

                            BE.HostingUnit hostingUnit = new BE.HostingUnit();

                            Console.Write("host ID: ");
                            int ID = int.Parse(Console.ReadLine());

                            hostingUnit.Owner = bl.GetHost(ID);
                            
                            Console.Write("Hosting unit name = ");
                            hostingUnit.HostingUnitName = Console.ReadLine();

                            do
                            {
                                Console.Write("Please select an area: " +
                               "\n\t1:All Israel" +
                               "\n\t2:north Israel" +
                               "\n\t3:Central Israel" +
                               "\n\t4:South Israel" +
                               "\n\t5:Jerusalem area\n");
                                Console.Write("your choice (1-5): ");
                                tempEnum = int.Parse(Console.ReadLine());
                            } while (tempEnum < 1 || tempEnum > 5);

                            hostingUnit.Area = (BE.AreaEnum)(tempEnum - 1);

                            Console.Write("hosting unit sub area = ");
                            hostingUnit.SubArea = Console.ReadLine();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\tHosting unit NO: " + bl.addHostingUnit(hostingUnit) + " added successfully!");
                            Console.ForegroundColor = ConsoleColor.White;

                            break;
                        case 4:
                            BE.Order order = new BE.Order();

                            Console.Write("Please enter the key of the hosting unit: ");
                            order.HostingUnitKey = int.Parse(Console.ReadLine());
                            Console.Write("Please enter the key of the guest Request: ");
                            order.GuestRequestKey = int.Parse(Console.ReadLine());

                            Console.Write("order create date: ");
                            order.CreateDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Email Delivery Date to Customer: ");
                            order.OrderDate = DateTime.Parse(Console.ReadLine()); ;

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\torder NO: " + bl.addOrder(order) + " added successfully!");
                            Console.ForegroundColor = ConsoleColor.White;

                            break;

                        case 5:
                            Console.Write("Enter hosting unit key to delete: ");
                            key = int.Parse(Console.ReadLine());

                            bl.deleteHostingUnit(key);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\thosting unit NO: " + key + " deleted successfully!");
                            Console.ForegroundColor = ConsoleColor.White;

                            break;

                        case 6:

                            Console.Write("Enter key of the order: ");
                            key = int.Parse(Console.ReadLine());

                            BE.OrderStatusEnum status;

                            do
                            {
                                Console.Write("Please enter the Status of the order:" +
                                "\n\t1:Not yet addressed" +
                               "\n\t2:mail has been sent" +
                               "\n\t3:Closes out of customer disrespect" +
                               "\n\t4:Closes with customer response\n");
                                Console.Write("your choice (1-4): ");
                                tempEnum = int.Parse(Console.ReadLine());
                            } while (tempEnum < 1 || tempEnum > 4);

                            status = (BE.OrderStatusEnum)(tempEnum - 1);

                            bl.updateOrder(key, status);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\tOrder status updated successfully!");
                            Console.ForegroundColor = ConsoleColor.White;

                            break;

                        case 7:

                            Console.Write("Enter key of the host: ");
                            key = int.Parse(Console.ReadLine());

                            host= bl.GetHost(key);

                            Console.Write("Host fhone number = ");
                            host.FhoneNumber = int.Parse(Console.ReadLine());
                            Console.Write("Host bank account number = ");
                            host.BankAccountNumber = int.Parse(Console.ReadLine());
                            Console.Write("Host private name = ");
                            host.PrivateName = Console.ReadLine();
                            Console.Write("Host family name = ");
                            host.FamilyName = Console.ReadLine();
                            Console.Write("Host Mail address = ");
                            host.MailAddress = Console.ReadLine();
                            Console.Write("Is Collection Clearance? [0 = no, 1 = yes] = ");
                            host.CollectionClearance = (int.Parse(Console.ReadLine()) == 1);

                            do
                            {
                                Console.WriteLine("Please select from the following list who is your bank branch");
                                int count = 1;
                                foreach (var bankBranchs in bl.getListBankBranchs())
                                {

                                    Console.WriteLine("------------------------");
                                    Console.WriteLine("           " + (count++));
                                    Console.WriteLine("------------------------");
                                    Console.WriteLine(bankBranchs.ToString());
                                }
                                Console.WriteLine("------------------------");
                                Console.Write("Your bank branch is (1-5): ");

                                num = int.Parse(Console.ReadLine());
                            } while (num < 1 || num > 5);

                            host.BankBranchDetails = bl.getListBankBranchs()[num - 1];

                            bl.updateHost(host);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\tHost updated successfully!");
                            Console.ForegroundColor = ConsoleColor.White;

                            break;

                        case 8:

                            Console.Write("Enter key of the hosting unit: ");
                            key = int.Parse(Console.ReadLine());

                            hostingUnit = bl.GetHostingUnit(key);

                            Console.Write("host ID: ");
                            ID = int.Parse(Console.ReadLine());

                            hostingUnit.Owner = bl.GetHost(ID);

                            Console.Write("Hosting unit name = ");
                            hostingUnit.HostingUnitName = Console.ReadLine();

                            do
                            {
                                Console.Write("Please select an area: " +
                               "\n\t1:All Israel" +
                               "\n\t2:north Israel" +
                               "\n\t3:Central Israel" +
                               "\n\t4:South Israel" +
                               "\n\t5:Jerusalem area\n");
                                Console.Write("your choice (1-5): ");
                                tempEnum = int.Parse(Console.ReadLine());
                            } while (tempEnum < 1 || tempEnum > 5);

                            hostingUnit.Area = (BE.AreaEnum)(tempEnum - 1);

                            Console.Write("hosting unit sub area = ");
                            hostingUnit.SubArea = Console.ReadLine();

                            bl.updateHostingUnit(hostingUnit);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\tHosting unit updated successfully!");
                            Console.ForegroundColor = ConsoleColor.White;

                            break;

                        case 9:

                            Console.Write("Enter key of the guest request: ");
                            key = int.Parse(Console.ReadLine());

                            request = bl.GetGuestRequest(key);

                            Console.Write("PrivateName = ");
                            request.PrivateName = Console.ReadLine();
                            Console.Write("FamilyName = ");
                            request.FamilyName = Console.ReadLine();
                            Console.Write("MailAddress = ");
                            request.MailAddress = Console.ReadLine();
                            Console.Write("RegistrationDate [D/M/Y]= ");
                            request.RegistrationDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("EntryDate [D/M/Y]= ");
                            request.EntryDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("ReleaseDate [D/M/Y]= ");
                            request.ReleaseDate = DateTime.Parse(Console.ReadLine());
                            do
                            {
                                Console.Write("Please select an area: " +
                               "\n\t1:All Israel" +
                               "\n\t2:north Israel" +
                               "\n\t3:Central Israel" +
                               "\n\t4:South Israel" +
                               "\n\t5:Jerusalem area\n");
                                Console.Write("your choice (1-5): ");
                                tempEnum = int.Parse(Console.ReadLine());
                            } while (tempEnum < 1 || tempEnum > 5);

                            request.Area = (BE.AreaEnum)(tempEnum - 1);

                            Console.Write("subArea = ");
                            request.SubArea = Console.ReadLine();

                            do
                            {
                                Console.Write("Please select an hosting unit type: " +
                                "\n\t1:Hotel" +
                                "\n\t2:Zimmer " +
                                "\n\t3:Villa" +
                                "\n\t4:South\n");
                                Console.Write("your choice (1-4): ");
                                tempEnum = int.Parse(Console.ReadLine());
                            } while (tempEnum < 1 || tempEnum > 4);

                            request.Type = (BE.GuestReqTypeEnum)(tempEnum - 1);

                            Console.Write("Amount of children = ");
                            request.Children = int.Parse(Console.ReadLine());
                            Console.Write("Amount of adults = ");
                            request.Adults = int.Parse(Console.ReadLine());

                            do
                            {
                                Console.Write("Please select the pool options: " +
                                "\n\t1:necessary" +
                                "\n\t2:Possible " +
                                "\n\t3:not interested\n");
                                Console.Write("your choice (1-3): ");
                                tempEnum = int.Parse(Console.ReadLine());
                            } while (tempEnum < 1 || tempEnum > 3);

                            request.Pool = (BE.optionsEnum)(tempEnum - 1);

                            do
                            {
                                Console.Write("Please select the jacuzzi options: " +
                                "\n\t1:necessary" +
                                "\n\t2:Possible " +
                                "\n\t3:not interested\n");
                                Console.Write("your choice (1-3): ");
                                tempEnum = int.Parse(Console.ReadLine());
                            } while (tempEnum < 1 || tempEnum > 3);

                            request.Jacuzzi = (BE.optionsEnum)(tempEnum - 1);

                            do
                            {
                                Console.Write("Please select the garden options: " +
                                "\n\t1:necessary" +
                                "\n\t2:Possible " +
                                "\n\t3:not interested\n");
                                Console.Write("your choice (1-3): ");
                                tempEnum = int.Parse(Console.ReadLine());
                            } while (tempEnum < 1 || tempEnum > 3);

                            request.Garden = (BE.optionsEnum)(tempEnum - 1);

                            do
                            {
                                Console.Write("Please select the ChildrensAttractions options: " +
                                "\n\t1:necessary" +
                                "\n\t2:Possible " +
                                "\n\t3:not interested\n");
                                Console.Write("your choice (1-3): ");
                                tempEnum = int.Parse(Console.ReadLine());
                            } while (tempEnum < 1 || tempEnum > 3);

                            request.ChildrensAttractions = (BE.optionsEnum)(tempEnum - 1);

                            bl.updateGuestReq(request);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\tguest request updated successfully!");
                            Console.ForegroundColor = ConsoleColor.White;

                            break;

                        case 10:

                            Console.Write("Enter the host ID to print [To print all hosting units please enter 0]: ");
                            key = int.Parse(Console.ReadLine());

                            if (key == 0)
                                foreach (var item in bl.getListHosts())
                                {
                                    Console.WriteLine("------------------------------------------------------");
                                    Console.WriteLine(item.ToString());
                                }
                            else
                                Console.WriteLine(bl.GetHost(key));

                            break;

                        case 11:

                            Console.Write("Enter the hosting unit key to print [To print all hosting units please enter 0]: ");
                            key = int.Parse(Console.ReadLine());

                            if (key == 0)
                                foreach (var item in bl.getListHostingUnit())
                                {
                                    Console.WriteLine("------------------------------------------------------");
                                    Console.WriteLine(item.ToString());
                                }
                            else
                                Console.WriteLine(bl.GetHostingUnit(key));

                            break;

                        case 12:

                            Console.Write("Enter the guest request key to print [To print all guest requests please enter 0]: ");
                            key = int.Parse(Console.ReadLine());

                            if (key == 0)
                                foreach (var item in bl.getListGuestRequest())
                                {
                                    Console.WriteLine("------------------------------------------------------");
                                    Console.WriteLine(item.ToString());
                                }
                            else
                                Console.WriteLine(bl.GetGuestRequest(key));

                            break;

                        case 13:

                            Console.Write("Enter the orders key to print [To print all orders please enter 0]: ");
                            key = int.Parse(Console.ReadLine());

                            if (key == 0)
                                foreach (var item in bl.getListOrders())
                                {
                                    Console.WriteLine("------------------------------------------------------");
                                    Console.WriteLine(item.ToString());
                                }
                            else
                                Console.WriteLine(bl.GetOrder(key));

                            break;

                        case 14:

                            foreach (var item in bl.getListBankBranchs())
                            {
                                Console.WriteLine("------------------------------------------------------");
                                Console.WriteLine(item.ToString());
                            }

                            break;

                        case 15:

                            Console.Write("Enter the first day [D/M/Y]: ");
                            DateTime date = DateTime.Parse(Console.ReadLine());
                            Console.Write("Please enter the length of stay: ");
                            int length = int.Parse(Console.ReadLine());

                            if (bl.GetAvailableHostingUnits(date, length).Count() == 0)
                                Console.WriteLine("\n\tThere are no available hosting units on the requested date\n");

                            foreach (var item in bl.GetAvailableHostingUnits(date, length))
                            {
                                Console.WriteLine("Hosting unit NO: " + item.HostingUnitKey + "\n");
                            }

                            break;

                        case 16:

                            Console.WriteLine("Enter the amount of days");

                            length = int.Parse(Console.ReadLine());

                            foreach (var item in bl.ordersBiggestThan(length))
                            {
                                Console.WriteLine("order unit NO: " + item.OrderKey + "\n");
                            }

                            break;

                        case 17:

                            Console.Write("Enter guest request key: ");
                            key = int.Parse(Console.ReadLine());

                            Console.WriteLine("The amount of orders sent to the customer is " + bl.getSumOrders(bl.GetGuestRequest(key)));

                            break;

                        case 18:
                            Console.Write("Enter hosting unit key: ");
                            key = int.Parse(Console.ReadLine());

                            Console.WriteLine("The amount of orders that accepted " + bl.SumOfApprovedOrder(bl.GetHostingUnit(key)));

                            break;

                        case 19:

                            foreach (var items in bl.GetGuestRequestsAreaByGroups())
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine(items.Key);
                                Console.ForegroundColor = ConsoleColor.White;

                                foreach (var n in items)
                                    Console.WriteLine(n + "\n");
                                Console.WriteLine("\n");

                            }

                            break;

                        case 20:

                            foreach (var items in bl.GetGuestRequestsSumOfPeoplesByGroups())
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine(items.Key);
                                Console.ForegroundColor = ConsoleColor.White;

                                foreach (var n in items)
                                    Console.WriteLine(n + "\n");
                                Console.WriteLine("\n");
                            }

                            break;

                        case 21:
                            foreach (var items in bl.GetHostsNumOfUnitsByGroups())
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine(items.Key);
                                Console.ForegroundColor = ConsoleColor.White;

                                foreach (var n in items)
                                    Console.WriteLine(n + "\n");
                                Console.WriteLine("\n");
                            }

                            break;

                        case 22:

                            foreach (var items in bl.GetHostingUnitsAreaByGroups())
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine(items.Key);
                                Console.ForegroundColor = ConsoleColor.White;

                                foreach (var n in items)
                                    Console.WriteLine(n + "\n");
                                Console.WriteLine("\n");
                            }
                           
                            break;

                        default:
                            Console.WriteLine("ERORR");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\tERORR: " + ex.Message + "\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }


            } while (coise != 0);
        }
    }
}
