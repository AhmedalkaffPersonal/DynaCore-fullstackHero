using DynaCore.Application.Responses.Identity;
using DynaCore.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynaCore.Application.Interfaces.Chat;
using DynaCore.Application.Models.Chat;

namespace DynaCore.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}