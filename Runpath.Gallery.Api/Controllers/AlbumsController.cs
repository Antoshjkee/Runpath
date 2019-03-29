using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Runpath.Gallery.Api.Models;
using Runpath.Gallery.Api.Repository;
using Runpath.Gallery.Domain.Entities;

namespace Runpath.Gallery.Api.Controllers
{
    [Route("api/users/{userId}/albums")]
    [ApiController]
    public class AlbumsController  : ControllerBase
    {
        private readonly IRepository<Album> _albumRepository;

        public AlbumsController(IRepository<Album> albumRepository)
        {
            _albumRepository = albumRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlbumDto>> Get(int userId)
        {
            var albums = _albumRepository
                .FindBy(x => x.UserId == userId)
                .Include(x => x.Photos);

            if (!albums.Any()) return NotFound();

            var albumsDto = Mapper.Map<IEnumerable<AlbumDto>>(albums);
            return Ok(albumsDto);
        }

        [HttpGet("{id}")]
        public ActionResult<AlbumDto> GetSingle(int userId, int id)
        {
            var album = _albumRepository.FindBy(x => x.Id == id && x.UserId == userId)
                .Include(x => x.Photos)
                .FirstOrDefault();

            if (album == null)  return NotFound();
            var albumDto = Mapper.Map<AlbumDto>(album);

            return Ok(albumDto);
        }
    }
}
