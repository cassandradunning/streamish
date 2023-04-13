using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Streamish.Models;
using Streamish.Repositories;

namespace Streamish.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _UserProfileRepository;
        public UserProfileController(IUserProfileRepository UserProfileRepository)
        {
            _UserProfileRepository = UserProfileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_UserProfileRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var UserProfile = _UserProfileRepository.GetById(id);
            if (UserProfile == null)
            {
                return NotFound();
            }
            return Ok(UserProfile);
        }


        //[HttpGet("GetUsersWithVideos")]
        //public IActionResult GetUsersWithVideos()
        //{
        //    var videos = _UserProfileRepository.GetAllUsersWithVideos();
        //    return Ok(videos);
        //}

        [HttpPost]
        public IActionResult Post(UserProfile UserProfile)
        {
            _UserProfileRepository.Add(UserProfile);
            return CreatedAtAction("Get", new { id = UserProfile.Id }, UserProfile);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserProfile UserProfile)
        {
            if (id != UserProfile.Id)
            {
                return BadRequest();
            }

            _UserProfileRepository.Update(UserProfile);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _UserProfileRepository.Delete(id);
            return NoContent();
        }
    }
}
