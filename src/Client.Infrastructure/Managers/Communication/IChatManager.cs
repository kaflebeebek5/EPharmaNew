using EPharma.Application.Models.Chat;
using EPharma.Application.Responses.Identity;
using EPharma.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPharma.Application.Interfaces.Chat;

namespace EPharma.Client.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
    }
}