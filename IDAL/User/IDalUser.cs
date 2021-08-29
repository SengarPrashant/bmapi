using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models.Common;

namespace IDAL.User
{
    public interface IDalUser
    {
        Task<Users> GetUserLoginDetail(Users user);
        Task<Users> RegisterUserAsync(Users user);
        public Task UpdateLoginAsync(Users users);
        public Task<School> CreateUpdateSchoolAsyc(School school);
        Task<School> GetSchollDetailAsync(School school);
        Task<List<School>> GetAllSchoolAsync(School school);
        Task<Users> UpdatePasswordAsync(Users users);
    }
}
