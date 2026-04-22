using AutoMapper;
using AymanStore.BLL.Interfaces;
using AymanStore.DAL.BaseEntity;
using AymanStore.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AymanStore.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersManagement : Controller
    {
        private readonly ILogger<UsersManagement> logger;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        private readonly IConfiguration configuration;

        public UsersManagement(ILogger<UsersManagement> logger, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            Mapper = mapper;
            this.logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Manage Users

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await unitOfWork.UserManager.Users.Where(a => a.Email != "ayman.fathy.elbatata@gmail.com")
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.FirstName,
                    u.LastName,
                    u.UserName,
                    u.IsDeleted,
                    u.IsActivated
                })
                .ToListAsync();

            var UserViewDTOs = new List<UserViewDTO>();
            foreach (var user in users)
            {
                var usermodel = new UserViewDTO();
                usermodel.Email = user.Email;
                usermodel.FirstName = user.FirstName;
                usermodel.LastName = user.LastName;
                usermodel.UserName = user.UserName;
                usermodel.IsDeleted = user.IsDeleted;
                usermodel.isActivated = user.IsActivated;
                usermodel.Id = user.Id;
                var UserinRules = unitOfWork.UserManager.Users.FirstOrDefault(u => u.Email == user.Email);
                usermodel.Roles = (List<string>)unitOfWork.UserManager.GetRolesAsync(UserinRules).Result ?? new List<string>();
                UserViewDTOs.Add(usermodel);
            }

            return Json(UserViewDTOs);
        }

        // GET: Users/GetRoles
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await unitOfWork.RoleManager.Roles
                .Select(r => new { Id = r.Id, Name = r.Name })
                .ToListAsync();

            var RolesModels = new List<RoleViewModel>();
            foreach (var user in roles)
            {
                var roleModel = new RoleViewModel();
                roleModel.Id = user.Id;
                roleModel.Name = user.Name;
                RolesModels.Add(roleModel);
            }

            return Json(RolesModels);
        }

        // POST: Users/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserInputModel model)
        {
            if (ModelState.IsValid)
            {
                if (await unitOfWork.UserManager.FindByEmailAsync(model.Email) != null)
                {
                    return Json(new { success = false, error = "Email is already registered" });
                }
                var user = new AppUser
                {
                    UserName = model.FirstName + "." + model.LastName + "-" + unitOfWork.MySPECIALGUID.GetUniqueKey(6),
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = "Default Address",
                    Phone = "0123456789",
                    CountryTBLId = 1,
                    GenderTBLId = 1,
                    ActivationCode = unitOfWork.MySPECIALGUID.GetUniqueKey(12),
                    IsActivated = true,
                };
                var result = await unitOfWork.UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    if (model.SelectedRoles != null && model.SelectedRoles.Any())
                    {
                        await unitOfWork.UserManager.AddToRolesAsync(user, model.SelectedRoles);
                    }
                    return Json(new { success = true });
                }
                return Json(new { success = false, errors = result.Errors.Select(e => e.Description) });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }

        // GET: Users/GetUser/{id}
        [HttpGet]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserInputModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                isDeleted = user.IsDeleted,
                isActivated = user.IsActivated,
                SelectedRoles = (await unitOfWork.UserManager.GetRolesAsync(user)).ToList()
            };

            return Json(model);
        }

        // POST: Users/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(string id, [FromBody] UserInputModelUpdate model)
        {
            if (ModelState.IsValid)
            {
                var user = await unitOfWork.UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return Json(new { success = false, error = "User not found" });
                }

                if (unitOfWork.UserManager.FindByEmailAsync(model.Email)?.Result?.Id != id)
                {
                    return Json(new { success = false, error = "Email is already registered for another user" });
                }
                else if (unitOfWork.UserManager.FindByNameAsync(model.UserName)?.Result?.Id != id)
                {
                    return Json(new { success = false, error = "Username is already registered for another user" });
                }

                user.Email = model.Email;
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.IsActivated = model.isActivated;
                var result = await unitOfWork.UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    var currentRoles = await unitOfWork.UserManager.GetRolesAsync(user);
                    await unitOfWork.UserManager.RemoveFromRolesAsync(user, currentRoles);

                    if (model.SelectedRoles != null && model.SelectedRoles.Any())
                    {
                        await unitOfWork.UserManager.AddToRolesAsync(user, model.SelectedRoles);
                    }
                    await unitOfWork.UserManager.UpdateSecurityStampAsync(user);

                    return Json(new { success = true });
                }
                return Json(new { success = false, errors = result.Errors.Select(e => e.Description) });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }

        // POST: Users/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(id);
            if (user != null && user.IsDeleted)
            {
                return Json(new { success = false, error = "User was deleted before!" });
            }
            else if (user != null && !user.IsDeleted)
            {
                user.IsDeleted = true;
                var result = await unitOfWork.UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false, errors = result.Errors.Select(e => e.Description) });
            }
            return Json(new { success = false, error = "User not found!" });
        }

        #endregion

    }
}
