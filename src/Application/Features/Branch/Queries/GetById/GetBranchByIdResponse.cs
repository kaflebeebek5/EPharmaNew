using System;

namespace EPharma.Application.Features.Branch.Queries.GetById
{
    public class GetBranchByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameNepali { get; set; }
        public string Code { get; set; }
        public string NRBCode { get; set; }
        public DateTime? OperationDate { get; set; }
        public int? ParentBranchId { get; set; }
        public string ParentBranch { get; set; }
        public int? BranchTypeId { get; set; }
        public string BranchType { get; set; }
        public int? ProvinceId { get; set; }
        public string Province { get; set; }
        public int? DistrictId { get; set; }
        public string District { get; set; }
        public int? LocalBodiesId { get; set; }
        public string LocalBodies { get; set; }
        public string Locality { get; set; }
        public int? WardNo { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
    }
}