using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Routes
{
    public class BankSetupEndPoints
    {
        public static string ExportFilterd(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/banksetup/export";
        public static string GetAll = "api/v1/banksetup/GetAll";
        public static string Delete = "api/v1/banksetup";
        public static string Save = "api/v1/banksetup";
        public static string GetCount = "api/v1/banksetup/count";
        public static string GetParentItem = "api/v1/banksetup";
    }
}
