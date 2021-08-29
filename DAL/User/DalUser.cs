using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IDAL.User;
using Models.Common;
using MySql.Data.MySqlClient;
using DAL.Common;
using Dapper;
using System.Data;
using System.Transactions;
using System.Linq;

namespace DAL.User
{
    public class DalUser : IDalUser
    {
        public async Task<Users> GetUserLoginDetail(Users user)
        {
            try
            {
                var sqlQuery = "";
                var p = new DynamicParameters();
                if (user.ID > 0)
                {
                    sqlQuery = @"select Id, SchId,FName,MName,LName,UserId,Password,PasswordSalt,DesignationId,IsActive,CreatedBy
                                ,UpdatedBy,CreatedDate,UpdatedDate
                                from schusers where id = @id; Select ur.Id, ur.UserId, ur.RoleId, r.RoleName from schuserroles ur 
                                inner join schroles r on ur.RoleId=r.id where userid = @id";
                    p.Add("@id", user.ID);
                }
                else
                {
                    sqlQuery = @"select Id, SchId,FName,MName,LName,UserId,Password,PasswordSalt,DesignationId,IsActive,CreatedBy
                                ,UpdatedBy,CreatedDate,UpdatedDate from schusers where UserId = @UserId; 
                                Select ur.Id, ur.UserId, ur.RoleId, r.RoleName from schuserroles ur 
                                inner join schroles r on ur.RoleId=r.id 
                                inner join schusers u on u.UserId=@UserId ";
                    p.Add("@UserId", user.UserID);
                }
                using MySqlConnection conn = new MySqlConnection(DalConstants.ConString);
                using (var multi = conn.QueryMultipleAsync(sqlQuery, p, commandType: CommandType.Text))
                {
                    user = multi.Result.Read<Users>().FirstOrDefault();
                    if(user != null)
                    {
                        user.UserRoles = new List<Roles>();
                        user.UserRoles = multi.Result.Read<Roles>().ToList();
                    }
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Users> UpdatePasswordAsync(Users users)
        {
            using MySqlConnection conn = new MySqlConnection(DalConstants.ConString);
            conn.Open();
            try
            {
                string sql = @"Update schusers set Password=@Password, PasswordSalt=@PasswordSalt, UpdatedDate=CURRENT_TIMESTAMP(),
                                UpdatedBy=@UpdatedBy
                                where id=@id and UserId=@UserId";
                var p = new DynamicParameters();
                p.Add("@Password", users.Password);
                p.Add("@PasswordSalt", users.PasswordSalt);
                p.Add("@UpdatedBy", users.UpdatedBy);
                p.Add("@id", users.ID);
                p.Add("@UserId", users.UserID);
                await conn.ExecuteScalarAsync<int>(sql, p, commandType: CommandType.Text);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                throw;
            }
            return users;
        }

        public Task UpdateLoginAsync(Users users)
        {
            throw new NotImplementedException();
        }

        public async Task<Users> RegisterUserAsync(Users user) 
        {
            using MySqlConnection conn = new MySqlConnection(DalConstants.ConString);
            conn.Open();
            try
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = @"
                            Insert into
                              schusers(SchId,FName,MName,LName,UserId,Password,PasswordSalt,DesignationId,IsActive,CreatedBy,UpdatedBy)
                            VALUES(@SchId,@FName,@MName,@LName,@UserId,@Password,@PasswordSalt,@DesignationId,@IsActive,@CreatedBy,@UpdatedBy);
                            select LAST_INSERT_ID();
                        ";
                        var p = new DynamicParameters();
                        p.Add("@SchId", user.SchId);
                        p.Add("@FName", user.FName); p.Add("@MName", user.MName); p.Add("@LName", user.LName);
                        p.Add("@UserId", user.UserID); p.Add("@Password", user.Password); p.Add("@PasswordSalt", user.PasswordSalt);
                        p.Add("@DesignationId", user.DesignationId); p.Add("@IsActive", user.IsActive);
                        p.Add("@CreatedBy", user.CreatedBy); p.Add("@UpdatedBy", user.UpdatedBy);
                        user.ID = await conn.ExecuteScalarAsync<int>(sql, p, commandType: CommandType.Text);

                        foreach (var role in user.UserRoles)
                        {
                            sql = @"Insert into schuserroles(UserId,RoleId,IsActive,CreatedBy,UpdatedBy)
                                VALUES(@UserId,@RoleId,@IsActive,@CreatedBy,@UpdatedBy);";
                            p = new DynamicParameters();
                            p.Add("@UserId", user.ID); p.Add("@RoleId", role.Id);
                            p.Add("@IsActive", role.IsActive);
                            p.Add("@CreatedBy", user.CreatedBy);
                            p.Add("@UpdatedBy", user.UpdatedBy);
                            await conn.ExecuteScalarAsync<int>(sql, p, commandType: CommandType.Text);
                        }
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                      await  transaction.RollbackAsync();
                      throw ex;
                    }
                    
                }
                return user;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        

        public async Task<School> CreateUpdateSchoolAsyc(School school)
        {
            using MySqlConnection conn = new MySqlConnection(DalConstants.ConString);
            conn.Open();
            try
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        if (school.Id > 0)
                        {
                            string sql = @"Update schools set SchoolName=@SchoolName, SchoolCode=@SchoolCode, AddressLine1=@AddressLine1,
                                AddressLine2=@AddressLine2, Country=@Country, state=@state, CityDistrict= @CityDistrict,
                                PinCode=@PinCode, GeoLocation=@GeoLocation,Contact=@Contact, SchoolLogo=@SchoolLogo, SessionStart=@SessionStart, SessionEnd=@SessionEnd,
                                IsActive=@IsActive where id=@id ";
                            var p = new DynamicParameters();
                            p.Add("@id", school.Id);
                            p.Add("@SchoolName", school.SchoolName); p.Add("@SchoolCode", school.SchoolCode); p.Add("@AddressLine1", school.AddressLine1);
                            p.Add("@AddressLine2", school.AddressLine2); p.Add("@Country", school.Country); p.Add("@state", school.State);
                            p.Add("@CityDistrict", school.CityDistrict); p.Add("@PinCode", school.PinCode);
                            p.Add("@GeoLocation", school.GeoLocation); p.Add("@Contact", school.Contact);
                            p.Add("@SchoolLogo", school.SchoolLogo); p.Add("@SessionStart", school.SessionStart);
                            p.Add("@SessionEnd", school.SessionEnd); p.Add("@IsActive", school.IsActive);
                            await conn.ExecuteScalarAsync<int>(sql, p, commandType: CommandType.Text);
                        }
                        else
                        {
                            string sql = @" Insert into
                              schools(SchoolName,SchoolCode,AddressLine1,AddressLine2,Country,state,CityDistrict,PinCode,GeoLocation,Contact,SchoolLogo,SessionStart,SessionEnd)
                              VALUES(@SchoolName,@SchoolCode,@AddressLine1,@AddressLine2,@Country,@state,@CityDistrict,@PinCode,
                                     @GeoLocation,@Contact,@SchoolLogo,@SessionStart,@SessionEnd);
                            select LAST_INSERT_ID();  ";
                            var p = new DynamicParameters();
                            p.Add("@SchoolName", school.SchoolName); p.Add("@SchoolCode", school.SchoolCode); p.Add("@AddressLine1", school.AddressLine1);
                            p.Add("@AddressLine2", school.AddressLine2); p.Add("@Country", school.Country); p.Add("@state", school.State);
                            p.Add("@CityDistrict", school.CityDistrict); p.Add("@PinCode", school.PinCode);
                            p.Add("@GeoLocation", school.GeoLocation); p.Add("@Contact", school.Contact);
                            p.Add("@SchoolLogo", school.SchoolLogo); p.Add("@SessionStart", school.SessionStart);
                            p.Add("@SessionEnd", school.SessionEnd);
                            school.Id = await conn.ExecuteScalarAsync<int>(sql, p, commandType: CommandType.Text);
                        }
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw ex;
                    }
                }
                conn.Close();
                return school;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public async Task<School> GetSchollDetailAsync(School school)
        {
            try
            {
                var sqlQuery = "";
                var p = new DynamicParameters();
                var result = new School();
                if (school.Id > 0)
                {
                    sqlQuery = @"select Id, SchoolName,SchoolCode,AddressLine1,AddressLine2,Country,state,CityDistrict,PinCode
                                 ,SchoolLogo,SessionStart,SessionEnd, IsActive, CreatedDate
                                from schools where id = @id;";
                    p.Add("@id", school.Id);
                }
                else
                {
                    sqlQuery = @"select Id, SchoolName,SchoolCode,AddressLine1,AddressLine2,Country,State,CityDistrict,PinCode
                                 ,SchoolLogo,SessionStart,SessionEnd, IsActive, CreatedDate
                                from schools where SchoolCode = @SchoolCode; ";
                    p.Add("@SchoolCode", school.SchoolCode);
                }
                using MySqlConnection conn = new MySqlConnection(DalConstants.ConString);
                using (var multi = await conn.QueryMultipleAsync(sqlQuery, p, commandType: CommandType.Text))
                {
                    result = multi.Read<School>().FirstOrDefault();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<School>> GetAllSchoolAsync(School school)
        {
            try
            {
               var sqlQuery = @"select Id, SchoolName,SchoolCode,AddressLine1,AddressLine2,Country,state,CityDistrict,PinCode
                                 ,SchoolLogo,SessionStart,SessionEnd, IsActive, CreatedDate
                                from schools";
                using MySqlConnection conn = new MySqlConnection(DalConstants.ConString);
                var res = new List<School>();
                using (var multi = await conn.QueryMultipleAsync(sqlQuery, null, commandType: CommandType.Text))
                {
                    res = multi.Read<School>().ToList();
                }
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
