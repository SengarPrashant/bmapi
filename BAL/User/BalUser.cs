using IBAL.User;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using IDAL.User;

namespace BAL.User
{
    public class BalUser : IBalUser
    {
        private readonly IDalUser _dalUser;
        public BalUser(IDalUser dalUser)
        {
            _dalUser = dalUser;
        }
        public Task<Users> GetUserLoginDetail(Users user)
        {
            return _dalUser.GetUserLoginDetail(user);
        }

        public Task<Users> RegisterUserAsync(Users user)
        {
            return _dalUser.RegisterUserAsync(user);
        }

        public Task UpdateLoginAsync(Users users)
        {
            throw new NotImplementedException();
        }

        public Task<School> CreateUpdateSchoolAsyc(School school)
        {
            return _dalUser.CreateUpdateSchoolAsyc(school);
        }
        public Task<School> GetSchollDetailAsync(School school)
        {
            return _dalUser.GetSchollDetailAsync(school);
        }
        public Task<List<School>> GetAllSchoolAsync(School school)
        {
            return _dalUser.GetAllSchoolAsync(school);
        }
        public Task<Users> UpdatePasswordAsync(Users users)
        {
            return _dalUser.UpdatePasswordAsync(users);
        }
    }
}
