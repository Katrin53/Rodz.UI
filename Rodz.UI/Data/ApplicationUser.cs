﻿using Microsoft.AspNetCore.Identity;

namespace Rodz.UI.Data
{
    public class ApplicationUser: IdentityUser
    {
        public byte[] Avatar { get; set; }
        public string MimeType { get; set; } = string.Empty;
    }
}
