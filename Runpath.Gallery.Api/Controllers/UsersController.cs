using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Runpath.Gallery.Api.Models;
using Runpath.Gallery.Api.Repository;
using Runpath.Gallery.Domain.Entities;

namespace Runpath.Gallery.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _usersRepository;

        public UsersController(IRepository<User> usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> Get()
        {
            var users = _usersRepository
                .GetAll();

            if (!users.Any()) return NotFound();

            var usersDto = Mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetSingle(int id)
        {
            var user = _usersRepository.FindBy(x => x.Id == id)
                .FirstOrDefault();

            if (user == null) return NotFound();

            var userDto = Mapper.Map<UserDto>(user);

            return Ok(userDto);
        }
    }
}
