﻿namespace TaskTracker.Identity.Model.Dto
{
    public class RegistrationRequestDto
    {
        public string Email { get; set; }

        public string UserName { get; set; }
        
        public string Name { get; set; }

        public string Surname { get; set; }
        
        public string? Patronymic { get; set; }
        
        public string Password { get; set; }
    }
}
