using DynaCore.Application.Models.Chat;
using DynaCore.Application.Responses.Identity;
using DynaCore.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynaCore.Application.Interfaces.Chat;

namespace DynaCore.Client.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
    }
}