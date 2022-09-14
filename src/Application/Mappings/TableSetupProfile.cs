using AutoMapper;
using EPharma.Application.Features.TableSetup.Commands.AddEdit;
using EPharma.Application.Features.TableSetup.Commands.Delete;
using EPharma.Application.Features.TableSetup.Queries.GetAll;
using EPharma.Application.Features.TableSetup.Queries.GetById;
using EPharma.Domain.Entities.Settings;

namespace EPharma.Application.Mappings
{
    class TableSetupProfile : Profile
    {
        public TableSetupProfile()
        {
            //Gender
            CreateMap<GetTableSetupByIdResponse, Gender>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, Gender>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, Gender>().ReverseMap();

            //Caste


            //Category


            //Community

            //HolidayFor




            //Designation


            //EmployeeStatus

            //MaritalStatus
            CreateMap<GetTableSetupByIdResponse, MaritalStatus>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, MaritalStatus>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, MaritalStatus>().ReverseMap();

            //BloodGroup
            CreateMap<GetTableSetupByIdResponse, BloodGroup>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, BloodGroup>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, BloodGroup>().ReverseMap();

            //Nationality
            CreateMap<GetTableSetupByIdResponse, Nationality>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, Nationality>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, Nationality>().ReverseMap();

            //Level
            CreateMap<GetTableSetupByIdResponse, Level>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, Level>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, Level>().ReverseMap();

            //Department


            //SubDepartment
            CreateMap<GetTableSetupByIdResponse, SubDepartment>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, SubDepartment>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, SubDepartment>().ReverseMap();

            //SubUnit
            CreateMap<GetTableSetupByIdResponse, SubUnit>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, SubUnit>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, SubUnit>().ReverseMap();

            //EmploymentType

            //LocalBodiesType
            CreateMap<GetTableSetupByIdResponse, LocalBodiesType>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, LocalBodiesType>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, LocalBodiesType>().ReverseMap();

            //Region
            CreateMap<GetTableSetupByIdResponse, Region>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, Region>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, Region>().ReverseMap();

            //BranchType
            CreateMap<GetTableSetupByIdResponse, BranchType>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, BranchType>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, BranchType>().ReverseMap();

            //Branch
            CreateMap<GetTableSetupByIdResponse, Branch>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, Branch>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, Branch>().ReverseMap();

            //Province
            CreateMap<GetTableSetupByIdResponse, Province>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, Province>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, Province>().ReverseMap();


            //District
            CreateMap<GetTableSetupByIdResponse, District>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, District>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, District>().ReverseMap();


            //LocalBodiesType
            CreateMap<GetTableSetupByIdResponse, LocalBodies>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, LocalBodies>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, LocalBodies>().ReverseMap();


            CreateMap<GetTableSetupByIdResponse, DoctorSetUp>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, DoctorSetUp>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, DoctorSetUp>().ReverseMap();


            CreateMap<GetTableSetupByIdResponse, TblMedicine>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, TblMedicine>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, TblMedicine>().ReverseMap();


            CreateMap<GetTableSetupByIdResponse, tblCategory>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, tblCategory>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, tblCategory>().ReverseMap();

            CreateMap<GetTableSetupByIdResponse, tblSubCategory>().ReverseMap();
            CreateMap<GetAllTableSetupResponse, tblSubCategory>().ReverseMap();
            CreateMap<AddEditTableSetupCommand, tblSubCategory>().ReverseMap();

        }
    }
}
