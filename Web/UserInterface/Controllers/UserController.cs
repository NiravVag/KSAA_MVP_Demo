using KSAA.DAL;
using KSAA.Domain.Common;
using KSAA.Master.Application.DTOs.Master.CompanyDTOs;
using KSAA.User.Application.DTOs.Role;
using KSAA.User.Application.DTOs.User;
using KSAA.User.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace KSAA.UserInterface.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: Users
        public async Task<IActionResult> UserList()
        {
            IEnumerable<UserListModel> users = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7146/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync("api/User/GetAllUsers", requestContent);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    //readTask.Wait();

                    //  Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(readTask);
                    //  var users1 = myDeserializedClass.data.ToList();
                    //users = users1.Select(x => new UserListModel
                    //{ 
                    //    Id=x.id,
                    //    FirstName=x.firstName,
                    //    LastName=x.lastName,
                    //    Email=x.email,
                    //    UserType=x.userType,
                    //    UserTypeName=x.userTypeName,
                    //    CompanyName=x.companyName,
                    //    Company = x.company,
                    //    IsActive=x.isActive,
                    //    UserRoleName = x.userRoleName,
                    //}).Where(x => x.IsActive != IsActive.Delete);

                    var data = JsonConvert.DeserializeObject<CommonResponse<List<UserListModel>>>(readTask);

                    users = data.Data;

                }
                else //web api sent error response 
                {
                    //log response status here..

                    users = Enumerable.Empty<UserListModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(users);
        }

        public async Task<IActionResult> UserAdd()
        {
            UserViewModel userViewModel = new UserViewModel();

            //Company Dropdown
            List<CompanyViewModel> CompanysList = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync("api/Company/GetAllCompany", requestContent);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<List<CompanyViewModel>>>(readTask);

                    CompanysList = data.Data;

                    CompanysList.Insert(0, new CompanyViewModel() { Id = 0, Company_Name = "----- Select Company -----" });

                    ViewBag.Companys = CompanysList;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    CompanysList = Enumerable.Empty<CompanyViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            //User Role Dropdown
            List<RoleListModel> RolesList = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7146/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync("api/Role/GetAllRole", requestContent);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<List<RoleListModel>>>(readTask);

                    RolesList = data.Data;

                    RolesList.Insert(0, new RoleListModel() { Id = 0, Name = "----- Select Role -----" });

                    ViewBag.Roles = RolesList;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    RolesList = Enumerable.Empty<RoleListModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }


            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserAdd(UserViewModel userViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7146/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/User/CreateUser", userViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    //return RedirectToAction("UserList");
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<UserViewModel>>(readTask);

                    userViewModel = data.Data;
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(await postTask.Content.ReadAsStringAsync());

                    return BadRequest(error);
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(userViewModel);
        }

        public async Task<IActionResult> GetUserById(int id)
        {
            //Company Dropdown
            List<CompanyViewModel> CompanysList = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync("api/Company/GetAllCompany", requestContent);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<List<CompanyViewModel>>>(readTask);

                    CompanysList = data.Data;

                    CompanysList.Insert(0, new CompanyViewModel() { Id = 0, Company_Name = "----- Select Company -----" });

                    ViewBag.Companys = CompanysList;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    CompanysList = Enumerable.Empty<CompanyViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            //User Role Dropdown
            List<RoleListModel> RolesList = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7146/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync("api/Role/GetAllRole", requestContent);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<List<RoleListModel>>>(readTask);

                    RolesList = data.Data;

                    RolesList.Insert(0, new RoleListModel() { Id = 0, Name = "----- Select Role -----" });

                    ViewBag.Roles = RolesList;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    RolesList = Enumerable.Empty<RoleListModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            UserViewModel users = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7146/");
                //HTTP GET
                var responseTask = await client.GetAsync("api/User/GetUserById?id=" + id.ToString());
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    //var data = JsonConvert.DeserializeObject<UserViewModel>(readTask);
                    //readTask.Wait();

                    //Root1 myDeserializedClass = JsonConvert.DeserializeObject<Root1>(readTask);
                    //var users1 = myDeserializedClass.data;
                    //users =  new UserViewModel
                    //{
                    //    Id = users1.id,
                    //    FirstName = users1.firstName,
                    //    LastName = users1.lastName,
                    //    Email = users1.email,
                    //    Password = (string)users1.password,
                    //    UserType = users1.userType,
                    //    Company = users1.company,
                    //    IsActive = users1.isActive,
                    //    RoleId = users1.roleId
                    //};

                    var data = JsonConvert.DeserializeObject<CommonResponse<UserViewModel>>(readTask);

                    users = data.Data;
                }
            }
            return View("~/Views/User/UserUpdate.cshtml", users);
        }

        public async Task<IActionResult> UpdateUser(UserViewModel userViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7146/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                //HTTP POST
                var responseTask = await client.PutAsJsonAsync("api/User/UpdateUserById", userViewModel);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    //return RedirectToAction("UserList", userViewModel);
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<UserViewModel>>(readTask);

                    return Json(data);
                }

                else
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ErrorResponse>(readTask);
                    return BadRequest(data);
                }
            }

        }

        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7146/");

                //HTTP DELETE
                var deleteTask = await client.DeleteAsync("api/User/Delete?id=" + id.ToString());
                //deleteTask.Wait();

                var result = deleteTask;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("UserList");
                }
            }

            return RedirectToAction("UserList");
        }
    }
}
