using Manager.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Create(UserDTO userDTO);
        Task<UserDTO> Update(UserDTO userDTO);
        Task Remove(int id);
        Task<UserDTO> Get(int id);
        Task<List<UserDTO>> Get();
        Task<List<UserDTO>> SearchByName(string name);
        Task<List<UserDTO>> SearchByEmail(string email);
        Task<UserDTO> GetByEmail(string email);
    }
}
