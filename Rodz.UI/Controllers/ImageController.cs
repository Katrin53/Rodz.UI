using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Rodz.UI.Data;
using System.Security.Claims;




namespace Rodz.UI.Controllers
{
    public class ImageController(UserManager<ApplicationUser>userManager) : Controller
    {
        public async Task<IActionResult> GetImage()
        {
            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            if (user.Avatar != null)
                return File(user.Avatar, user.MimeType);
            var imagePath = Path.Combine("Images", "default-profile-picture.png");
            return File(imagePath, "image/png");
        }
    }
}

