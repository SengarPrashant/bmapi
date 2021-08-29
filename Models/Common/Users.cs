using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Common
{
    public class Users
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }

        public string PasswordSalt { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public int DesignationId { get; set; }
        public string LoginIP { get; set; }
        public string LoginDevice { get; set; }
        public int SchId { get; set; }
        public Boolean IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<Roles> UserRoles { get; set; }
    }

    public class Login
    {
        [Required(ErrorMessage ="User Id is required")]
        public string UserID { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string LoginIP { get; set; }
        public string LoginDevice { get; set; }
    }

    public class Roles
    {
        public int Id { get; set; }
        public int SchId { get; set; }
        public string RoleName { get; set; }
        public Boolean IsActive { get; set; }
    }

    public class SharedModel
    {
        public int SchId { get; set; }
        public Boolean IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

    }

}
