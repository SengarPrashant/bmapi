using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschool.Common
{
    public class UiConstants
    {
        public static string CommonSuccess { get; set; } = "Operation is successfully performed.";
        public static string CommonError { get; set; } = "Internal server error.";
        public static string UserAlreadyRegistered { get; set; } = "User id already registered.";
        public static string InvalidCreadentials { get; set; } = "Invalid User id or Password.";
        public static string WrongPassword { get; set; } = "Wrong Password.";
        public static string InvalidUserRegistrationDetails { get; set; } = "Invalid user details.";
        public static string baseUrl { get; set; } = "https://eduschool.in";
        public static string baseUrlLocal { get; set; } = "https://localhost:44342";

    }
}
