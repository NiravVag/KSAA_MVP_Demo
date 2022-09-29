using KSAA.Domain.Entities;
using KSAA.User.Application.DTOs.User;
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
            IEnumerable<UserViewModel> users = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7146/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync("api/User/GetAllUsers",requestContent);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    //readTask.Wait();
                    Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(readTask);
                    var users1 = myDeserializedClass.data.ToList();
                  users = users1.Select(x => new UserViewModel
                  { 
                      Id=x.id,
                      FirstName=x.firstName,
                      LastName=x.lastName,
                      Email=x.email,
                      Password= (string)x.password,
                  });

                    //users = readTask;
                    // users = (IEnumerable<UserViewModel>?)myDeserializedClass;
                    // var listuser = JsonSerializer.Deserialize<UserViewModel>(readTask);
                    //users = (IEnumerable<UserViewModel>?)listuser;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    users = Enumerable.Empty<UserViewModel>();

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
        public async Task<IActionResult> UserAdd([FromBody] UserViewModel userViewModel)
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
                    return RedirectToAction("UserList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(userViewModel);
        }
    }
}
