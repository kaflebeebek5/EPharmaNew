namespace EPharma.Application.Requests.Settings
{
    public class GetAllPagedCalenderRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }
}