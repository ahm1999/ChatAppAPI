using ChatApp.Data.Entities;
using ChatApp.DTOs;

namespace ChatApp.Services
{
    public interface IMessegeService
    {
        Task<Messege> AddMessage(AddMessegesDTO addMessegesDTO);

        Task<List<Messege>> GetMessagesBetween(string user1Id,string user2Id );
    }
}
