using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebbkursProv.Areas.Identity.Data;

namespace WebbkursProv.Pages.Admin
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string AddUserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RemoveUserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Role { get; set; }

        public WebbkursProvUser CurrentUser { get; set; }

        [BindProperty]
        public string RoleName { get; set; }

        public List<IdentityRole> Roles { get; set; }

        public List<string> UserRoles { get; set; }

        public bool isNy { get; set; }
        public bool isSkribent { get; set; }
        public bool isAdmin { get; set; }
        public bool AdminCheck { get; set; }

        public List<WebbkursProvUser> Users { get; set; }

        private readonly RoleManager<IdentityRole> _roleManager;
        public UserManager<WebbkursProvUser> _userManager;

        public IndexModel(RoleManager<IdentityRole> roleManager, UserManager<WebbkursProvUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Roles = _roleManager.Roles.ToList();
            Users = _userManager.Users.ToList();

            //Ändrar roll samt tar bort från andra roller
            if (AddUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(AddUserId);
                var roleresult = await _userManager.AddToRoleAsync(alterUser, Role);

                if (Role == "Ny")
                {
                    var xalterUser = await _userManager.FindByIdAsync(RemoveUserId);
                    var xroleresult = await _userManager.RemoveFromRoleAsync(alterUser, "Skribent");
                    var yalterUser = await _userManager.FindByIdAsync(RemoveUserId);
                    var yroleresult = await _userManager.RemoveFromRoleAsync(alterUser, "Admin");
                }
                else if (Role == "Skribent")
                {
                    var xalterUser = await _userManager.FindByIdAsync(RemoveUserId);
                    var xroleresult = await _userManager.RemoveFromRoleAsync(alterUser, "Ny");
                    var yalterUser = await _userManager.FindByIdAsync(RemoveUserId);
                    var yroleresult = await _userManager.RemoveFromRoleAsync(alterUser, "Admin");
                }
                else if (Role == "Admin")
                {
                    var xalterUser = await _userManager.FindByIdAsync(RemoveUserId);
                    var xroleresult = await _userManager.RemoveFromRoleAsync(alterUser, "Ny");
                    var yalterUser = await _userManager.FindByIdAsync(RemoveUserId);
                    var yroleresult = await _userManager.RemoveFromRoleAsync(alterUser, "Skribent");
                }
            }

            if (RemoveUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(RemoveUserId);
                var roleresult = await _userManager.RemoveFromRoleAsync(alterUser, Role);
            }


            CurrentUser = await _userManager.GetUserAsync(User);

            isNy = await _userManager.IsInRoleAsync(CurrentUser, "Ny");
            isSkribent = await _userManager.IsInRoleAsync(CurrentUser, "Skribent");
            isAdmin = await _userManager.IsInRoleAsync(CurrentUser, "Admin");

            // om Admin saknas blir den som loggar in på /admin ny Admin
            await CheckAdminAsync();

            return Page();
        }

        public async Task<IActionResult> CheckAdminAsync()
        {
            Users = _userManager.Users.ToList();

            foreach (var x in Users)
            {
                AdminCheck = await _userManager.IsInRoleAsync(x, "Admin");
                if (AdminCheck) break;
            }
            if (AdminCheck == false)
            {
                await _userManager.AddToRoleAsync(CurrentUser, "Admin");
                await _userManager.RemoveFromRoleAsync(CurrentUser, "Ny");
                await _userManager.RemoveFromRoleAsync(CurrentUser, "Skribent");
            }
            return Page();
        }

    }
}
