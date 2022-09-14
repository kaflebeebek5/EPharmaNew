using EPharma.Application.Requests;
using EPharma.Application.Responses;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Interfaces.DoctorSetup
{
   public interface IDoctorSetupService
   {
        Task<Result<MessageResponse>> SaveDoctor(DoctorSetupRequestModel Request);
        Task<Result<List<DoctoSetupResponse>>> GetAll();
        Task<Result<MessageResponse>> DeleteDoctor(int Id);
   }
}
