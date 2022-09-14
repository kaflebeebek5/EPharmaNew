using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Routes
{
   public class CasteEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/castes/export";

        public static string GetAll = "api/v1/castes";
        public static string Delete = "api/v1/castes";
        public static string Save = "api/v1/castes";
        public static string GetCount = "api/v1/castes/count";
    }
}
