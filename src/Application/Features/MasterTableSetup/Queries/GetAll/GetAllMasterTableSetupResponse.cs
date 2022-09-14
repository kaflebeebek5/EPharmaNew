using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Features.MasterTableSetup.Queries.GetAll
{
    public class GetAllMasterTableSetupResponse
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string ColumnId { get; set; }
        public string ColumnName { get; set; }
    }
}
