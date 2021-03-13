using Meetup.Models;
using MeetupInfrastructure.Entity;
using MeetupInfrastructure.Migration;
using MeetupServices.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MeetupServices.Repository
{
    public class UserDAL : IUserInterface
    {
        ApplicationDBContext dBContext;
        public UserDAL(ApplicationDBContext _dBContext)
        {
            dBContext = _dBContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        public async Task<RegistrationResponseModel> AddPost(Registration registrationRequest)
        {
            RegistrationResponseModel registrationResponse = new RegistrationResponseModel();
            if (registrationRequest != null)
            {
                await dBContext.registrations.AddAsync(registrationRequest);
                await dBContext.SaveChangesAsync();
                registrationResponse.StatusCode = 200;
            }
            return registrationResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public async Task UpdatePost(Registration registration)
        {
            if (dBContext != null)
            {
                //Delete that post
                dBContext.registrations.Update(registration);

                //Commit the transaction
                await dBContext.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public async Task<RegistrationViewModel> GetPost(int? postId)
        {
            if (dBContext != null)
            {
                return  (from p in dBContext.registrations.AsQueryable()
                              where p.Id == postId
                              select new RegistrationViewModel
                              {
                                  Name = p.Name,
                                  DOB=p.DOB,
                                  Age = p.Age,
                                  Profession = p.Profession,
                                  Address = p.Address,
                                  NumberOfGuest = p.NumberOfGuest
                              }).FirstOrDefault();
            }

            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<RegistrationViewModel>> GetPosts()
        {
            if (dBContext != null)
            {
                return  (from p in dBContext.registrations
                              select new RegistrationViewModel
                              {
                                  Name = p.Name,
                                  DOB=p.DOB,
                                  Age = p.Age,
                                  Profession = p.Profession,
                                  Address = p.Address,
                                  NumberOfGuest = p.NumberOfGuest
                              }).ToList();
            }

            return null;
        }


    }
}
