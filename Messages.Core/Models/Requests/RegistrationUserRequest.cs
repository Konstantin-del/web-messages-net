﻿namespace Messages.Core.Models.Requests
{
    public class RegistrationUserRequest
    {
        public string Name { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}