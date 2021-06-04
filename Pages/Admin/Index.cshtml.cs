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

        public IQueryable<WebbkursProvUser> Users { get; set; }


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
            Users = _userManager.Users;

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

            //await _userManager.AddToRoleAsync(CurrentUser, "Admin");

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            Users = _userManager.Users;
            if (RoleName != null)
            {
                await CreateRole(RoleName);
            }
            return RedirectToPage("./index");
        }

        public async Task CreateRole(string roleName)
        {
            bool exist = await _roleManager.RoleExistsAsync(roleName);
            if (!exist)
            {
                // first we create Admin role 
                var role = new IdentityRole
                {
                    Name = roleName
                };
                await _roleManager.CreateAsync(role);
            }
        }
    }
}
