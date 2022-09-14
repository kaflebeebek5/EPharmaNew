using EPharma.Application.Requests;
using EPharma.Application.Responses;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.DoctorSetup
{
   public interface IDoctorSetupManager:IManager
    {
        Task<IResult<MessageResponse>> SaveDoctor(DoctorSetupRequestModel Request);
        Task<IResult<List<DoctoSetupResponse>>> GetAll();
        Task<IResult<MessageResponse>> DeleteDoctor(int Id);
    }
}
