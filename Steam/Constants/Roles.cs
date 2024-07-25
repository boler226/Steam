﻿namespace Steam.Constants
{
    public static class Roles
    {
        public static List<string> All = new()
        {
            Admin,
            User,
            Developer
        };
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Developer = "Developer";
    }
}
