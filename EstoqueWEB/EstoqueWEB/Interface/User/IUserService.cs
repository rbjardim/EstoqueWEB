using EstoqueWEB.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstoqueWEB.Interface.Service
{
    public interface IUserService
    {
        Task<List<AplicationUser>> GetAllUsersAsync();
        Task<AplicationUser> GetUserByIdAsync(string id);
        Task DeleteUserAsync(string id);
    }
}
