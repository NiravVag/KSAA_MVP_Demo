using KSAA.Domain.Common;
using KSAA.Domain.Entities;
using KSAA.User.Application.DTOs.User;
using KSAA.User.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult UserAdd()
        {
            UserViewModel userViewModel = new UserViewModel();
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
