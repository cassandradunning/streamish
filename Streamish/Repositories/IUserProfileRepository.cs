using Streamish.Models;
using System.Collections.Generic;

namespace Streamish.Repositories
{
    public interface IUserProfileRepository
    {
        public List<UserProfile> GetAll();
        public UserProfile GetById(int id);
        public void Add(UserProfile UserProfile);
        public void Update(UserProfile UserProfile);
        public void Delete(int id);
    }
}