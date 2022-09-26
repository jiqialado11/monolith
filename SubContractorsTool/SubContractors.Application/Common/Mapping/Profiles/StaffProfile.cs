using AutoMapper;
using SubContractors.Application.Handlers.Invoices.Queries.GetReferralListQuery;
using SubContractors.Application.Handlers.Staff.Commands.CreateStaff;
using SubContractors.Application.Handlers.Staff.Queries.GetInternalStaffListQuery;
using SubContractors.Application.Handlers.Staff.Queries.GetPmStaffQuery;
using SubContractors.Application.Handlers.Staff.Queries.GetStaffListQuery;
using SubContractors.Application.Handlers.Staff.Queries.GetStaffQuery;
using SubContractors.Application.Handlers.Staff.Queries.GetStaffStatusesQuery;
using SubContractors.Application.Handlers.Staff.Queries.SearchPmStaffQuery;
using SubContractors.Common.Extensions;
using SubContractors.Domain.SubContractor.Staff;
using System.Linq;

namespace SubContractors.Application.Common.Mapping.Profiles
{
    public class StaffProfile: Profile
    {
        public StaffProfile()
        {
            CreateMap<Staff, GetReferralListDto>()
                .ForMember(dest => dest.ReferralId, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.FirstName, o => o.MapFrom(source => source.FirstName))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(source => source.StartDate))
                .ForMember(dest => dest.LastName, o => o.MapFrom(source => source.LastName));

            CreateMap<Staff, GetStaffListDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.FirstName, o => o.MapFrom(source => source.FirstName))
                .ForMember(dest => dest.LastName, o => o.MapFrom(source => source.LastName))
                .ForMember(dest => dest.PmId, o => o.MapFrom(source => source.PmId))
                .ForMember(dest => dest.IsNdaSigned, o => o.MapFrom(source => source.IsNdaSigned))
                .ForMember(dest => dest.ExpirationDate, o => o.MapFrom(source => source.CannotLoginAfter))
                .ForMember(dest => dest.Position, o => o.MapFrom(source => source.Position))
                .ForMember(dest => dest.StatusId, o => o.MapFrom(source => (int)source.Status))
                .ForMember(dest => dest.Status, o => o.MapFrom(source => source.Status.GetDescription()));

            CreateMap<Staff, GetStaffDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Firstname, o => o.MapFrom(source => source.FirstName))
                .ForMember(dest => dest.Lastname, o => o.MapFrom(source => source.LastName))
                .ForMember(dest => dest.Email, o => o.MapFrom(source => source.Email))
                .ForMember(dest => dest.Skype, o => o.MapFrom(source => source.Skype))
                .ForMember(dest => dest.CannotLoginAfter, o => o.MapFrom(source => source.CannotLoginAfter))
                .ForMember(dest => dest.CannotLoginBefore, o => o.MapFrom(source => source.CannotLoginBefore))
                .ForMember(dest => dest.CellPhone, o => o.MapFrom(source => source.CellPhone))
                .ForMember(dest => dest.DepartmentName, o => o.MapFrom(source => source.DepartmentName))
                .ForMember(dest => dest.DomainLogin, o => o.MapFrom(source => source.DomainLogin))
                .ForMember(dest => dest.IsNdaSigned, o => o.MapFrom(source => source.IsNdaSigned))
                .ForMember(dest => dest.RealLocation, o => o.MapFrom(source => source.RealLocation))
                .ForMember(dest => dest.Qualification, o => o.MapFrom(source => source.Qualifications))
                .ForMember(dest => dest.PmId, o => o.MapFrom(source => source.PmId));

            CreateMap<Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.StaffList.Staff, SearchPmStaffDto>()
                .ForMember(dest => dest.PmId, o => o.MapFrom(source => source.StaffId))
                .ForMember(dest => dest.Firstname, o => o.MapFrom(source => source.FirstName))
                .ForMember(dest => dest.Lastname, o => o.MapFrom(source => source.LastName));

            CreateMap<Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.StaffDetails.Data, GetPmStaffDto>()
                .ForMember(dest => dest.PmId, o => o.MapFrom(source => source.Staff.StaffDetails.Id))
                .ForMember(dest => dest.Firstname, o => o.MapFrom(source => source.Staff.FirstName))
                .ForMember(dest => dest.Lastname, o => o.MapFrom(source => source.Staff.LastName))
                .ForMember(dest => dest.Email, o => o.MapFrom(source => source.Contacts.MainEmail))
                .ForMember(dest => dest.Skype, o => o.MapFrom(source => source.Contacts.Skype))
                .ForMember(dest => dest.Position, o => o.MapFrom(source => source.Staff.Job))
                .ForMember(dest => dest.CannotLoginBefore, o => o.MapFrom(source => source.Staff.StaffFirstDate))
                .ForMember(dest => dest.CannotLoginAfter, o => o.MapFrom(source => source.Staff.StaffLastDate))
                .ForMember(dest => dest.RealLocation, o => o.MapFrom(source => source.Staff.RealLocation))
                .ForMember(dest => dest.CellPhone,
                    o => o.MapFrom(source =>
                        source.Phones != null && source.Phones.Any()
                            ? source.Phones.FirstOrDefault().PhoneNumber
                            : string.Empty))
                .ForMember(dest => dest.IsNdaSigned, o => o.MapFrom(source => source.Staff.NdaSigned))
                .ForMember(dest => dest.DepartmentName, o => o.MapFrom(source => source.Staff.Department))
                .ForMember(dest => dest.DomainLogin, o => o.MapFrom(source => source.Staff.DomainLogin))
                .ForMember(dest => dest.Qualification,
                    o => o.Ignore());


            CreateMap<Staff, GetInternalStaffListDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Firstname, o => o.MapFrom(source => source.FirstName))
                .ForMember(dest => dest.Lastname, o => o.MapFrom(source => source.LastName));

            CreateMap<Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.StaffDetails.Data, CreateStaff>()
                .ForMember(dest => dest.PmId, o => o.MapFrom(source => source.Staff.StaffDetails.Id))
                .ForMember(dest => dest.SubContractorId, o => o.Ignore())
                .ForMember(dest => dest.FirstName, o => o.MapFrom(source => source.Staff.FirstName))
                .ForMember(dest => dest.LastName, o => o.MapFrom(source => source.Staff.LastName))
                .ForMember(dest => dest.Email, o => o.MapFrom(source => source.Contacts.MainEmail))
                .ForMember(dest => dest.Skype, o => o.MapFrom(source => source.Contacts.Skype))
                .ForMember(dest => dest.Position, o => o.MapFrom(source => source.Staff.Job))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(source => source.Staff.StaffFirstDate))
                .ForMember(dest => dest.EndDate, o => o.MapFrom(source => source.Staff.StaffLastDate))
                .ForMember(dest => dest.Qualifications, o => o.Ignore())
                .ForMember(dest => dest.RealLocation, o => o.MapFrom(source => source.Staff.RealLocation))
                .ForMember(dest => dest.CellPhone, o => o.MapFrom(source => source.Phones != null && source.Phones.Any() ?
                    source.Phones.FirstOrDefault().PhoneNumber : string.Empty))
                .ForMember(dest => dest.IsNdaSigned, o => o.MapFrom(source => source.Staff.NdaSigned))
                .ForMember(dest => dest.DepartmentName, o => o.MapFrom(source => source.Staff.Department))
                .ForMember(dest => dest.DomainLogin, o => o.MapFrom(source => source.Staff.DomainLogin));

            CreateMap<StaffStatus, GetStaffStatusesDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => (int)source))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.GetDescription()));

        }
    }
}
