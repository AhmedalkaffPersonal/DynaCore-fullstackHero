﻿using System.ComponentModel.DataAnnotations;

namespace DynaCore.Application.Requests.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}