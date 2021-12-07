using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.API.Domain.Dtos.User;
using Vehicles.API.Domain.Entities;
using Vehicles.API.Domain.Interfaces;
using Vehicles.API.Domain.Interfaces.Services.User;

namespace Vehicles.API.Service.Services
{
    public class UserService : IUserService
    {
        private IRepository<User> _respository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public UserService(IRepository<User> respository, IMapper mapper, IHttpContextAccessor accessor)
        {
            this._respository = respository;
            this._mapper = mapper;
            this._accessor = accessor;
        }

        public async Task<UserDto> Get(Guid id)
        {
            var entity = await _respository.SelectAsync(id);

            return _mapper.Map<UserDto>(entity);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var listEntity = await _respository.SelectAsync();

            return _mapper.Map<IEnumerable<UserDto>>(listEntity);
        }

        public async Task<UserDto> Post(UserDtoCreate user)
        {
            var entity = _mapper.Map<User>(user);
            var result = await _respository.InsertAsync(entity);

            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> Put(UserDtoUpdate user)
        {
            var entity = _mapper.Map<User>(user);
            var result = await _respository.UpdateAsync(entity);

            return _mapper.Map<UserDto>(result);
        }

        public async Task<bool> Delete(Guid id)
        {
            var userEmail = this._accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("unique_name", StringComparison.InvariantCultureIgnoreCase));
            return await _respository.DeleteAsync(id);
        }
    }
}
