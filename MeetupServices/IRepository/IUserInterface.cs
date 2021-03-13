using Meetup.Models;
using MeetupInfrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetupServices.IRepository
{
    public interface IUserInterface
    {
        Task<RegistrationResponseModel> AddPost(Registration registrationRequest);
        Task UpdatePost(Registration registration);
        Task<RegistrationViewModel> GetPost(int? postId);
        Task<List<RegistrationViewModel>> GetPosts();
    }
}
