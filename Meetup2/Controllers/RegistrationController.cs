using Meetup.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MeetupServices.IRepository;
using MeetupInfrastructure.Entity;

namespace Meetup2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        IUserInterface userInterface;
        public RegistrationController(IUserInterface _userInterface)
        {
            userInterface = _userInterface;
        }
        /// <summary>
        /// This API To save the registration detail into database
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RegistrationResponseModel> UserRegistration(Registration registration)
        {
            RegistrationResponseModel registrationResponse = new RegistrationResponseModel();
            if (registration != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var Sucess = await userInterface.AddPost(registration);
                        if (Sucess.StatusCode == 200)
                        {
                            registrationResponse.Status = true;
                            registrationResponse.StatusCode = 200;
                        }
                        else
                        {
                            registrationResponse.Status = false;
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        registrationResponse.Status = false;
                        
                    }
                }
                else
                {
                    registrationResponse.Status = false;
                    
                }
               
            }
            return registrationResponse;
        }
        /// <summary>
        /// This API to update the user details
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdatePost")]
        public async Task<IActionResult> UpdatePost([FromBody] Registration registration)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await userInterface.UpdatePost(registration);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }
            return BadRequest();
        }
        /// <summary>
        /// This API to fetch the list  of user  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPosts")]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var posts = await userInterface.GetPosts();
                if (posts == null)
                {
                    return NotFound();
                }

                return Ok(posts);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// This API fetching the id wise detail to update
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPost")]
        public async Task<IActionResult> GetPost(int? postId)
        {
            if (postId == null)
            {
                return BadRequest();
            }

            try
            {
                var post = await userInterface.GetPost(postId);

                if (post == null)
                {
                    return NotFound();
                }

                return Ok(post);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
