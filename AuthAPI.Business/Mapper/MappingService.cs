using AuthAPI.Commons.Models;
using AuthAPI.Commons.Requests;
using AuthAPI.Commons.Responses;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Business.Mapper
{
    public class MappingService : Profile
    {
        public MappingService()
        {

            #region Request
            CreateMap<PersonRequest, Person>();
            CreateMap<UserRequest, User>();
            CreateMap<RegisterRequest, User>();
            CreateMap<RoleRequest, Role>();
            CreateMap<CharacteristicRequest, Characteristic>();
            #endregion

            #region Response
            CreateMap<Role, RoleResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<Person, PersonResponse>();
            CreateMap<Device, DeviceResponse>();
            CreateMap<Characteristic, CharacteristicResponse>();
            #endregion

        }
    }
}
