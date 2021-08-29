using Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBAL.User
{
    public interface IBalUser
    {
        Task<Users> GetUserLoginDetail(Users user);
        Task<Users> RegisterUserAsync(Users user);
        Task UpdateLoginAsync(Users users);
        Task<School> CreateUpdateSchoolAsyc(School school);
        Task<School> GetSchollDetailAsync(School school);
        Task<List<School>> GetAllSchoolAsync(School school);
        Task<Users> UpdatePasswordAsync(Users users);
    }
}
