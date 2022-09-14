using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EPharma.Shared.Constants.Permission
{
    public static class Permissions
    {
        public static class Genders
        {
            public const string View = "Genders.View";
            public const string Create = "Genders.Create";
            public const string Edit = "Genders.Edit";
            public const string Delete = "Genders.Delete";
            public const string Export = "Genders.Export";
            public const string Search = "Genders.Search";
        }
        public static class DoctorSetup
        {
            public const string View = "DoctorSetup.View";
            public const string Create = "DoctorSetup.Create";
            public const string Edit = "DoctorSetup.Edit";
            public const string Delete = "DoctorSetup.Delete";
            public const string Export = "DoctorSetup.Export";
            public const string Search = "DoctorSetup.Search";
        }
        public static class UserMedicine
        {
            public const string View = "UserMedicine.View";
            public const string Create = "UserMedicine.Create";
            public const string Edit = "UserMedicine.Edit";
            public const string Delete = "UserMedicine.Delete";
            public const string Export = "UserMedicine.Export";
            public const string Search = "UserMedicine.Search";
        }
        public static class MedicineSetup
        {
            public const string View = "MedicineSetup.View";
            public const string Create = "MedicineSetup.Create";
            public const string Edit = "MedicineSetup.Edit";
            public const string Delete = "MedicineSetup.Delete";
            public const string Export = "MedicineSetup.Export";
            public const string Search = "MedicineSetup.Search";
        }
        public static class BillEntry
        {
            public const string View = "BillEntry.View";
            public const string Create = "BillEntry.Create";
            public const string Edit = "BillEntry.Edit";
            public const string Delete = "BillEntry.Delete";
            public const string Export = "BillEntry.Export";
            public const string Search = "BillEntry.Search";
        }


        public static class Castes
        {
            public const string View = "Castes.View";
            public const string Create = "Castes.Create";
            public const string Edit = "Castes.Edit";
            public const string Delete = "Castes.Delete";
            public const string Export = "Castes.Export";
            public const string Search = "Castes.Search";
        }

        public static class Communities
        {
            public const string View = "Communities.View";
            public const string Create = "Communities.Create";
            public const string Edit = "Communities.Edit";
            public const string Delete = "Communities.Delete";
            public const string Export = "Communities.Export";
            public const string Search = "Communities.Search";
        }
        public static class MasterTableSetup
        {
            public const string View = "MasterTableSetup.View";
            public const string Create = "MasterTableSetup.Create";
            public const string Edit = "MasterTableSetup.Edit";
            public const string Delete = "MasterTableSetup.Delete";
            public const string Export = "MasterTableSetup.Export";
            public const string Search = "MasterTableSetup.Search";
        }
        public static class TableSetup
        {
            public const string View = "TableSetup.View";
            public const string Create = "TableSetup.Create";
            public const string Edit = "TableSetup.Edit";
            public const string Delete = "TableSetup.Delete";
            public const string Export = "TableSetup.Export";
            public const string Search = "TableSetup.Search";
        }
        public static class BranchSetup
        {
            public const string View = "BranchSetup.View";
            public const string Create = "BranchSetup.Create";
            public const string Edit = "BranchSetup.Edit";
            public const string Delete = "BranchSetup.Delete";
            public const string Export = "BranchSetup.Export";
            public const string Search = "BranchSetup.Search";
        }
        public static class Documents
        {
            public const string View = "Documents.View";
            public const string Create = "Documents.Create";
            public const string Edit = "Documents.Edit";
            public const string Delete = "Documents.Delete";
            public const string Search = "Documents.Search";
        }

        public static class DocumentTypes
        {
            public const string View = "DocumentTypes.View";
            public const string Create = "DocumentTypes.Create";
            public const string Edit = "DocumentTypes.Edit";
            public const string Delete = "DocumentTypes.Delete";
            public const string Export = "DocumentTypes.Export";
            public const string Search = "DocumentTypes.Search";
        }

        public static class DocumentExtendedAttributes
        {
            public const string View = "DocumentExtendedAttributes.View";
            public const string Create = "DocumentExtendedAttributes.Create";
            public const string Edit = "DocumentExtendedAttributes.Edit";
            public const string Delete = "DocumentExtendedAttributes.Delete";
            public const string Export = "DocumentExtendedAttributes.Export";
            public const string Search = "DocumentExtendedAttributes.Search";
        }
        public static class Users
        {
            public const string View = "Users.View";
            public const string Create = "Users.Create";
            public const string Edit = "Users.Edit";
            public const string Delete = "Users.Delete";
            public const string Export = "Users.Export";
            public const string Search = "Users.Search";
        }
        public static class Roles
        {
            public const string View = "Roles.View";
            public const string Create = "Roles.Create";
            public const string Edit = "Roles.Edit";
            public const string Delete = "Roles.Delete";
            public const string Search = "Roles.Search";
        }
        public static class RoleClaims
        {
            public const string View = "RoleClaims.View";
            public const string Create = "RoleClaims.Create";
            public const string Edit = "RoleClaims.Edit";
            public const string Delete = "RoleClaims.Delete";
            public const string Search = "RoleClaims.Search";
        }
        public static class MenuRole
        {
            public const string View = "MenuRole.View";
            public const string Create = "MenuRole.Create";
            public const string Edit = "MenuRole.Edit";
            public const string Delete = "MenuRole.Delete";
            public const string Search = "MenuRole.Search";
        }
        public static class MenuList
        {
            public const string View = "MenuList.View";
            public const string Create = "MenuList.Create";
            public const string Edit = "MenuList.Edit";
            public const string Delete = "MenuList.Delete";
            public const string Search = "MenuList.Search";
        }
        public static class Categories
        {
            public const string View = "Categories.View";
            public const string Create = "Categories.Create";
            public const string Edit = "Categories.Edit";
            public const string Delete = "Categories.Delete";
            public const string Export = "Categories.Export";
            public const string Search = "Categories.Search";
        }

        public static class Communication
        {
            public const string Chat = "Communication.Chat";
        }

        public static class Preferences
        {
            public const string ChangeLanguage = "Preferences.ChangeLanguage";

            //TODO - add permissions
        }
        public static class Dashboards
        {
            public const string View = "Dashboards.View";
        }

        public static class Hangfire
        {
            public const string View = "Hangfire.View";
        }

        public static class AuditTrails
        {
            public const string View = "AuditTrails.View";
            public const string Export = "AuditTrails.Export";
            public const string Search = "AuditTrails.Search";
        }

        public static class HolidayFor
        {
            public const string View = "HolidayFor.View";
            public const string Create = "HolidayFor.Create";
            public const string Edit = "HolidayFor.Edit";
            public const string Delete = "HolidayFor.Delete";
            public const string Export = "HolidayFor.Export";
            public const string Search = "HolidayFor.Search";
        }

        public static class HolidayTypes
        {
            public const string View = "HolidayTypes.View";
            public const string Create = "HolidayTypes.Create";
            public const string Edit = "HolidayTypes.Edit";
            public const string Delete = "HolidayTypes.Delete";
            public const string Export = "HolidayTypes.Export";
            public const string Search = "HolidayTypes.Search";
        }

        public static class HolidaySetup
        {
            public const string View = "HolidaySetup.View";
            public const string Create = "HolidaySetup.Create";
            public const string Edit = "HolidaySetup.Edit";
            public const string Delete = "HolidaySetup.Delete";
            public const string Export = "HolidaySetup.Export";
            public const string Search = "HolidaySetup.Search";
        }

        public static class Calender
        {
            public const string View = "Calender.View";
            public const string Edit = "Calender.Edit";
            public const string Export = "Calender.Export";
            public const string Search = "Calender.Search";
        }
        public static class StaticVariable
        {
            public const string View = "StaticVariable.View";
        }
        public static class Branch
        {
            public const string View = "Branch.View";
            public const string Create = "Branch.Create";
            public const string Edit = "Branch.Edit";
            public const string Delete = "Branch.Delete";
            public const string Export = "Branch.Export";
            public const string Search = "Branch.Search";
        }

        public static class BankSetup
        {
            public const string View = "BankSetup.View";
            public const string Create = "BankSetup.Create";
            public const string Edit = "BankSetup.Edit";
            public const string Delete = "BankSetup.Delete";
            public const string Export = "BankSetup.Export";
            public const string Search = "Branch.Search";
        }
        public static class LiabilityAssets
        {
            public const string View = "LiabilityAssets.View";
            public const string Create = "LiabilityAssets.Create";
            public const string Edit = "LiabilityAssets.Edit";
            public const string Delete = "LiabilityAssets.Delete";
            public const string Export = "LiabilityAssets.Export";
            public const string Search = "LiabilityAssets.Search";
        }

        public static class IncomeExpenses
        {
            public const string View = "IncomeExpenses.View";
            public const string Create = "IncomeExpenses.Create";
            public const string Edit = "IncomeExpenses.Edit";
            public const string Delete = "IncomeExpenses.Delete";
            public const string Export = "IncomeExpenses.Export";
            public const string Search = "IncomeExpenses.Search";
        }

        public static class GLSettings
        {
            public const string View = "GLSettings.View";
            public const string Create = "GLSettings.Create";
            public const string Edit = "GLSettings.Edit";
            public const string Delete = "GLSettings.Delete";
            public const string Export = "GLSettings.Export";
            public const string Search = "GLSettings.Search";
        }
        public static class SalaryGL
        {
            public const string View = "SalaryGL.View";
            public const string Create = "SalaryGL.Create";
            public const string Edit = "SalaryGL.Edit";
            public const string Delete = "SalaryGL.Delete";
            public const string Export = "SalaryGL.Export";
            public const string Search = "SalaryGL.Search";
        }
        public static class StaffSetup
        {
            public const string View = "StaffSetup.View";
            public const string Create = "StaffSetup.Create";
            public const string Edit = "StaffSetup.Edit";
            public const string Delete = "StaffSetup.Delete";
            public const string Export = "StaffSetup.Export";
            public const string Search = "StaffSetup.Search";
        }
        public static class District
        {
            public const string View = "District.View";
        }
        public static class LocalBodies
        {
            public const string View = "LocalBodies.View";
        }

        /// <summary>
        /// Returns a list of 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRegisteredPermissions()
        {
            var permssions = new List<string>();
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                    permssions.Add(propertyValue.ToString());
            }
            return permssions;
        }
    }
}