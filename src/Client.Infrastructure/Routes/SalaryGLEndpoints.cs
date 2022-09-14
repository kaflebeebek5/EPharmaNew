using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Routes
{
    class SalaryGLEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/SalaryGL/export";
        public static string GetAll = "api/v1/SalaryGL";
        public static string GetById = "api/v1/SalaryGL";
        public static string Delete = "api/v1/SalaryGL";
        public static string Save = "api/v1/SalaryGL";
        
    }
}
