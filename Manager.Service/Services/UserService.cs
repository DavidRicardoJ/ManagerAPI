using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Service.DTO;
using Manager.Service.Interfaces;
using Manager.Service.Cryptography;
using System.Collections.Generic;
using System.Threading.Tasks;
using Manager.Service.Cryptography.Interfaces;

namespace Manager.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IBCrypt _iBCrypt;
        
        public UserService(IMapper mapper, IUserRepository userRepository, IBCrypt bCrypt)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _iBCrypt = bCrypt;
        }

        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            var userExists = await _userRepository.GetByEmail(userDTO.Email);

            if (userExists != null)
                throw new DomainException("Já existe um usuário cadastrado com o email informado.");

            var user = _mapper.Map<User>(userDTO);
            user.Validate();
            user.ChangePassword(_iBCrypt.HashPassword(userDTO.Password));

            var userCreated = await _userRepository.Create(user);

            return _mapper.Map<UserDTO>(userCreated);
        }

        public async Task<UserDTO> Update(UserDTO userDTO)
        {
            var userExists = await _userRepository.Get(userDTO.Id);

            if (userExists == null)
                throw new DomainException("Não existe nenhum usuário com o id informado!");

            var user = _mapper.Map<User>(userDTO);
            user.Validate();
            user.ChangePassword(_iBCrypt.HashPassword(userDTO.Password));

            var userUpdated = await _userRepository.Update(user);

            return _mapper.Map<UserDTO>(userUpdated);
        }
        public async Task Remove(int id)
        {
            await _userRepository.Remove(id);
        }

        public async Task<UserDTO> Get(int id)
        {
            var user = await _userRepository.Get(id);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> Get()
        {
            var allUsers = await _userRepository.GetList();

            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<List<UserDTO>> SearchByName(string name)
        {
            var allUsers = await _userRepository.SearchByName(name);

            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<List<UserDTO>> SearchByEmail(string email)
        {
            var allUsers = await _userRepository.SearchByEmail(email);

            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);

            return _mapper.Map<UserDTO>(user);
        }
    }
}
