﻿namespace E_Commerce_API.DTOs.UserDTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime AccountCreated { get; set; }
    }
}
