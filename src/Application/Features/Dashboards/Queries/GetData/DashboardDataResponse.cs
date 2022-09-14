using System.Collections.Generic;

namespace EPharma.Application.Features.Dashboards.Queries.GetData
{
    public class DashboardDataResponse
    {
        public int DocumentCount { get; set; }
        public int DocumentTypeCount { get; set; }
        public int DocumentExtendedAttributeCount { get; set; }
        public int UserCount { get; set; }
        public int RoleCount { get; set; }
        public List<ChartSeries> DataEnterBarChart { get; set; } = new();
    }

    public class ChartSeries
    {
        public ChartSeries() { }

        public string Name { get; set; }
        public double[] Data { get; set; }
    }

}