using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RecipeApp.Core.Models.Mail;
using RecipeApp.Core.Models.Moderator;
using RecipeApp.Entity.DTOs;
using RecipeApp.Entity.Entities;
using RecipeApp.Service.Contracts;
using RecipeApp.Web.Controllers.Base;

namespace RecipeApp.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class ModeratorController : BaseAdminController
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly MailSettings _mailSettings;

        public ModeratorController(UserManager<User> userManager,
                                   RoleManager<Role> roleManager,
                                   IMapper mapper,
                                   IMailService mailService,
                                   IOptions<MailSettings> mailSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _mailService = mailService;
            _mailSettings = mailSettings.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.GetUsersInRoleAsync("moderator");
            return View(users);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(NewModeratorModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "moderator");
                var content = $"<p><b>Kullanıcı Adı:</b> {model.Username}</p>" +
                              $"<p><b>Parola:</b> {model.Password}</p>";

                var mail = new MailDTO
                {
                    Subject = "Yeni Moderatör",
                    Content = content,
                    To = new List<string> { model.Email },
                    From = _mailSettings.Username
                };

                // await _mailService.SendAsync(mail);

                TempData["UserCreated"] = "Bilgiler kullanıcıya e-posta ile gönderildi.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                result.Errors.ToList().ForEach(error => ModelState.AddModelError(error.Code, error.Description));
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int moderatorId)
        {
            var user = await _userManager.FindByIdAsync(moderatorId.ToString());
            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var users = (await _userManager.GetUsersInRoleAsync("moderator")).ToList();
                var data = users.Select(x => _mapper.Map<UserDTO>(x));
                return Ok(data);
            }

            return BadRequest();
        }
    }
}