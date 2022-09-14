using EPharma.Application.Responses.Identity;
using EPharma.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPharma.Application.Interfaces.Chat;
using EPharma.Application.Models.Chat;

namespace EPharma.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}