using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace DAL
{
    static class Cloning
    {
        public static BE.GuestRequest Clone(this BE.GuestRequest original)
        {
            BE.GuestRequest target = new BE.GuestRequest();

            target.Adults = original.Adults;
            target.Area = original.Area;
            target.Children = original.Children;
            target.ChildrensAttractions = original.ChildrensAttractions;
            target.EntryDate = new DateTime(original.EntryDate.Year, original.EntryDate.Month, original.EntryDate.Day);
            target.FamilyName = original.FamilyName;
            target.Garden = original.Garden;
            target.GuestRequestKey = original.GuestRequestKey;
            target.Jacuzzi = original.Jacuzzi;
            target.MailAddress = original.MailAddress;
            target.Pool = original.Pool;
            target.PrivateName = original.PrivateName;
            target.RegistrationDate = new DateTime(original.RegistrationDate.Year, original.RegistrationDate.Month, original.RegistrationDate.Day); 
            target.ReleaseDate = new DateTime(original.ReleaseDate.Year, original.ReleaseDate.Month, original.ReleaseDate.Day);
            target.Status = original.Status;
            target.SubArea = original.SubArea;
            target.Type = original.Type;

            return target;
        }

        

        public static BE.BankBranch Clone(this BE.BankBranch original)
        {
            BE.BankBranch target = new BE.BankBranch();

            target.BankName = original.BankName;
            target.BankNumber = original.BankNumber;
            target.BranchAddress = original.BranchAddress;
            target.BranchCity = original.BranchCity;
            target.BranchNumber = original.BranchNumber;

            return target;
        }

        public static BE.Host Clone(this BE.Host original)
        {
            BE.Host target = new BE.Host();

            target.BankAccountNumber = original.BankAccountNumber;
            target.BankBranchDetails = original.BankBranchDetails.Clone();
            target.CollectionClearance = original.CollectionClearance;
            target.FamilyName = original.FamilyName;
            target.FhoneNumber = original.FhoneNumber;
            target.HostKey = original.HostKey;
            target.MailAddress = original.MailAddress;
            target.PrivateName = original.PrivateName;

            return target;
        }

        public static BE.HostingUnit Clone(this BE.HostingUnit original)
        {
            BE.HostingUnit target = new BE.HostingUnit();

            target.Area = original.Area;

            target.Diary = new bool[12, 31];

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    target.Diary[i,j] = original.Diary[i,j];
                }
            }

            target.HostingUnitKey = original.HostingUnitKey;
            target.HostingUnitName = original.HostingUnitName;
            target.Owner = original.Owner.Clone();
            target.SubArea = original.SubArea;

            return target;
        }

        public static BE.Order Clone(this BE.Order original)
        {
            BE.Order target = new BE.Order();

            target.cost = original.cost;
            target.CreateDate = new DateTime(original.CreateDate.Year, original.CreateDate.Month, original.CreateDate.Day);
            target.GuestRequestKey = original.GuestRequestKey;
            target.HostingUnitKey = original.HostingUnitKey;
            target.isClosed = original.isClosed;
            target.OrderDate = new DateTime(original.OrderDate.Year, original.OrderDate.Month, original.OrderDate.Day);
            target.OrderKey = original.OrderKey;
            target.Status = original.Status;

            return target;
        }

        public static IEnumerable<BE.GuestRequest> Clone(this IEnumerable<BE.GuestRequest> original)
        {
            return (from item in DS.DataSource.GuestRequestList
                    select item.Clone());
        }
        public static IEnumerable<BE.HostingUnit> Clone(this IEnumerable<BE.HostingUnit> original)
        {
            return (from item in DS.DataSource.HostingUnitList
                    select item.Clone());
        }
        public static IEnumerable<BE.Host> Clone(this IEnumerable<BE.Host> original)
        {
            return (from item in DS.DataSource.HostList
                    select item.Clone());
        }
        public static IEnumerable<BE.Order> Clone(this IEnumerable<BE.Order> original)
        {
            return (from item in DS.DataSource.OrderList
                    select item.Clone());
        }
    }
}
