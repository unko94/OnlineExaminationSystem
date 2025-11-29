using DataAccess.Repositery.IRepositery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Models.Model;
using Models.ViewModel;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utility;




namespace OnlineExaminationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ManagUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager; 

        public ManagUsersController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
             IEmailSender emailSender,
             IUnitOfWork unitOfWork,
             RoleManager<IdentityRole> roleManager
             )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
        }
        public async Task<IActionResult>  Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roleuser =await _userManager.GetRolesAsync(user);

            var jsonstring = System.IO.File.ReadAllText("C:\\Users\\HP\\Desktop\\Online Examination System\\OnlineExaminationSystem\\OnlineExaminationSystem\\wwwroot\\PhoneExtentions.json");

            var extentions = JsonSerializer.Deserialize<List<Extention>>(jsonstring);
            var selectlist = extentions.Select(e => new SelectListItem
            {
                Text = $"{e.name} ({e.dial_code})",
                Value = e.dial_code
            }).ToList();


            IEnumerable<SelectListItem> rolesList = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()

            });

            if (user !=null  &&  roleuser !=null) 
            {
                string[] splitphoneandextention = new string[2];
                if (user.PhoneNumber !=null)
                {
                      splitphoneandextention = user.PhoneNumber.Split();
                }
             
                var model = new EditUser()
                {
                    id = user.Id,
                    fullName = user.FullName,
                    userName = user.UserName,
                    email = user.Email,
                    lockoutEnabled = user.LockoutEnabled,
                    lockoutEnd = user.LockoutEnd?.DateTime,
                    emailConfirmed = user.EmailConfirmed,
                    accessFailedCount = user.AccessFailedCount,
                    roleid = roleuser.FirstOrDefault(),
                    roles =  rolesList,
                    extentions = selectlist,
                    extention = splitphoneandextention[0] ,
                    phoneNumber = splitphoneandextention[1],
                    confirmPhoneNumber = user.PhoneNumberConfirmed,

                };
                return View(model);
            }
            return RedirectToAction();
           // return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUser model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.id);
                var currentRole = await _userManager.GetRolesAsync(user);
                var rolefromModel = await _roleManager.FindByIdAsync(model.roleid);

                if (currentRole.FirstOrDefault() != rolefromModel.Name)
                {
                    if (currentRole  != null)
                    {
                        await _userManager.RemoveFromRoleAsync(user,currentRole.FirstOrDefault());
                    }
                    if (!string.IsNullOrEmpty(rolefromModel.Name))
                    {
                        await _userManager.AddToRoleAsync(user, rolefromModel.Name);
                    }
                }
                user.FullName = model.fullName;
                user.UserName = model.userName;
                user.AccessFailedCount = model.accessFailedCount;
                user.Email = model.email;
                user.PhoneNumber = model.extention+" "+model.phoneNumber;
                user.LockoutEnabled = model.lockoutEnabled;
                user.LockoutEnd = model.lockoutEnd;
                user.PhoneNumber = model.phoneNumber;
                user.PhoneNumberConfirmed = model.confirmPhoneNumber;
                user.EmailConfirmed = model.emailConfirmed;

                await _userManager.UpdateAsync(user);
            
                return RedirectToAction("UsersMangment");
            }

            var jsonstring = System.IO.File.ReadAllText("C:\\Users\\HP\\Desktop\\Online Examination System\\OnlineExaminationSystem\\OnlineExaminationSystem\\wwwroot\\PhoneExtentions.json");

            var extentions = JsonSerializer.Deserialize<List<Extention>>(jsonstring);
            var selectlist = extentions.Select(e => new SelectListItem
            {
                Text = $"{e.name} ({e.dial_code})",
                Value = e.dial_code
            }).ToList();


            IEnumerable<SelectListItem> rolesList = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()

            });
            model.roles = rolesList;
            model.extentions = selectlist;
          
            return View(model);
        }
       
        [HttpPost]
         
         public async Task<IActionResult> LockOut(string id, DateTime lockoutEnd)
        {
            
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            await _userManager.SetLockoutEnabledAsync(user,true);
            await _userManager.SetLockoutEndDateAsync(user,lockoutEnd);

            return RedirectToAction("UsersMangment");
        }
        [HttpPost]
        public async Task<IActionResult> Unlock(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }

            await _userManager.SetLockoutEndDateAsync(user,null);
            await _userManager.SetLockoutEnabledAsync(user,false);

            return RedirectToAction("UsersMangment");
        }



        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            Console.WriteLine("Delete Action Hitted");

            var user = await _userManager.FindByIdAsync(id);
           
            if (user != null)
            {
                 var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    TempData["Error"] = "User not found";
                }
                return RedirectToAction("UsersMangment");
            }
 
            return RedirectToAction("UsersMangment");

        }

        public IActionResult UsersMangment()
        {
            return View();
        }
        public IActionResult CreateUser()
        {
            var jsonstring = System.IO.File.ReadAllText("C:\\Users\\HP\\Desktop\\Online Examination System\\OnlineExaminationSystem\\OnlineExaminationSystem\\wwwroot\\PhoneExtentions.json");

            var extentions = JsonSerializer.Deserialize<List<Extention>>(jsonstring);
            var selectlist = extentions.Select(e => new SelectListItem
            {
                Text = $"{e.name} ({e.dial_code})",
                Value = e.dial_code
            }).ToList();
            

            IEnumerable<SelectListItem> rolesList = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
                
            });
            
           

            CreateUserVM model = new()
            {
                 roles =  rolesList,
                 extentions = selectlist
            };
            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserVM model)
        {
            var userE =  await _userManager.FindByEmailAsync(model.email);
            var userU = await _userManager.FindByNameAsync(model.username);
            var jsonstring = System.IO.File.ReadAllText("C:\\Users\\HP\\Desktop\\Online Examination System\\OnlineExaminationSystem\\OnlineExaminationSystem\\wwwroot\\PhoneExtentions.json");

            var extentions = JsonSerializer.Deserialize<List<Extention>>(jsonstring);
            var selectlist = extentions.Select(e => new SelectListItem
            {
                Text = $"{e.name} ({e.dial_code})",
                Value = e.dial_code
            }).ToList();

            IEnumerable<SelectListItem> rolesList = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            });
            if (userE != null || userU != null)
            {
                TempData["Error"] = " Email or username is already exists";
             

                
                model.roles = rolesList;
                model.extentions = selectlist;
                return View(model);
            }
            ApplicationUser user = new()
            {
                FullName = model.firstname + " " + model.lastname,
                Email = model.email,
                UserName = model.username
            };
            if (!ModelState.IsValid)
            {
                TempData["Error"] = " model is not valid";
      
                model.roles = rolesList;
                model.extentions = selectlist;
                return View(model);
            }
            var result =  await _userManager.CreateAsync(user,model.password);
            if (!result.Succeeded) 
            {
                TempData["Error"] = " Create user failed";

                model.roles = rolesList;
                model.extentions = selectlist;
                return View(model);
            }
            var role = await _roleManager.FindByIdAsync(model.roleid);
            if (role == null)
            {
                TempData["Error"] = "Role not exists";

                model.roles = rolesList;
                model.extentions = selectlist;
                return View(model);

            }
            var rolename = await _roleManager.GetRoleNameAsync(role);
            await _userManager.AddToRoleAsync(user, rolename);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodetoken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var confirmationlink = Url.ActionLink("ConfirmEmail", "Account" , new { area = "RigAndLog", userId = user.Id ,token =  encodetoken });
            await _emailSender.SendEmailAsync(user.Email,"Confirm Email", $"Please confirm your account by <a href='{confirmationlink}'>click here</a> ");
            return RedirectToAction("UsersMangment");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateField(string id, string field, string value)
        {
            var user =  await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            switch (field)
            {
                case "fullName":
                    user.FullName = value;
                    break;
                case "email":
                    user.Email = value;
                    break;

                case "role":
                    var role = await _userManager.GetRolesAsync(user);
                    var currrole = role.FirstOrDefault();
                   await _userManager.RemoveFromRoleAsync(user,currrole);
                    var isexistrole = await _roleManager.RoleExistsAsync(value);
                    if (!isexistrole)
                    {
                        return BadRequest("role is not exists");
                    }
                    await _userManager.AddToRoleAsync(user,value);
                    break;
                case "userName":
                    user.UserName = value;
                    break;


                case "lockoutEnabled":
                    user.LockoutEnabled = (value.ToLower() == "true") ? true : false;   
                    break;

                case "lockoutEnd":
                    
                    break;

                default:
                    return BadRequest("Invalid field");
            }
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Update Failed");
            
        }

        public async Task<IActionResult> RemoveSelected(List<string> ides)
        {
            List<string> Errors = new List<string>();

            foreach (var id in ides)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    Errors.Add("User with this " + id + " not exists");
                }

                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {

                    foreach (var error in result.Errors)
                    {
                        Errors.Add($"User {user.UserName}: {error.Description}");
                    }
                }

            }
            if (Errors.Count > 0)
            {
                TempData["Errors"] = JsonSerializer.Serialize(Errors);
            }
            return Json(new { success = true });
        }
        //this for send email verfication and modify the feilds
        public IActionResult SaveAll(CreateUserVM createUserVM)
        {
            return RedirectToAction("UsersMangment");
        }
        public string RandomPassword()
        {
            int count = 0;
            //  List<char> letters = new List<char>();
            string password = "";
            char charletter;
            var random = new Random();
            while (count < 8)
            {
           
                 
                if (count == 0)
                {
             
                    charletter = Convert.ToChar(random.Next(65,90));
                    password += charletter;

                }
                if (count == 2)
                {

                    charletter = Convert.ToChar(random.Next(33, 47));
                    password += charletter;

                }
                if (count == 3)
                {
                    charletter = Convert.ToChar(random.Next(48, 57));
                    password += charletter;

                }
                int letter = random.Next(97, 122);
                charletter = Convert.ToChar(letter);
                password += charletter;
                count++;
                
            }
            return password;
        }
        
        #region
        public async Task<IActionResult> GetAll(string? role = null)
        {
            
            var users = _userManager.Users.ToList();
          
            List<UsersVM> usersVM = new List<UsersVM>();
            foreach (var user in users)
            {
         
                var  roles = await _userManager.GetRolesAsync(user);
                
                    if (!string.IsNullOrEmpty(role) && role != null && roles.FirstOrDefault() != null && role != roles.FirstOrDefault())
                    {
                        continue;
                    }

                

                usersVM.Add(new UsersVM()
                {
                    id = user.Id,
                    userName = user.UserName,
                    fullName = user.FullName,
                    email = user.Email,
                    emailConfirmed = user.EmailConfirmed,
                    lockoutEnabled = user.LockoutEnabled,
                    //lockoutEnd = (DateTime)(user.LockoutEnd?.DateTime),
                    //lockoutEnd = (user.LockoutEnd?.DateTime?? DateTime.MinValue).ToString("yyyy-MM-dd HH-mm-ss"),
                    lockoutEnd = user.LockoutEnd?.DateTime.ToString("yyyy-MM-ddTHH:mm:ss") ?? "",
                    accessFailedCount = user.AccessFailedCount,
                    role = roles.FirstOrDefault()
                });
            }
            
            return Json(usersVM);
        }
     
        #endregion


    }
}
