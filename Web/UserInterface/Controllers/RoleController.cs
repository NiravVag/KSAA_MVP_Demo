using KSAA.Domain.Common;
using KSAA.Domain.Entities;
using KSAA.User.Application.DTOs.Role;
using KSAA.User.Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : Controller
    {
        // GET: Roles
        public async Task<IActionResult> RoleList()
        {
            IEnumerable<RoleListModel> roles = null;

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

                    roles = data.Data;
                    //readTask.Wait();
                }
                else //web api sent error response 
                {
                    //log response status here..

                    roles = Enumerable.Empty<RoleListModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(roles);
        }

        public IActionResult RoleAdd()
        {
            RoleViewModel roleViewModel = new RoleViewModel();
            return View(roleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RoleAdd(RoleViewModel roleViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7146/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/Role/CreateRole", roleViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    //return RedirectToAction("RoleList");
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<RoleViewModel>>(readTask);

                    roleViewModel = data.Data;
                }
                //else
                //{
                //    var error = JsonConvert.DeserializeObject<ErrorResponse>(await postTask.Content.ReadAsStringAsync());

                //    return BadRequest(error);
                //}
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(roleViewModel);
        }

        public async Task<IActionResult> GetRoleById(int id)
        {
            RoleViewModel roles = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7146/");
                //HTTP GET
                var responseTask = await client.GetAsync("api/Role/GetRoleById?id=" + id.ToString());
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<RoleViewModel>>(readTask);
                    //readTask.Wait();

                    roles = data.Data;
                }
            }
            return View("~/Views/Role/RoleUpdate.cshtml", roles);
        }

        public async Task<IActionResult> UpdateRole(RoleViewModel roleViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7146/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                //HTTP POST
                var responseTask = await client.PutAsJsonAsync("api/Role/UpdateRoleById", roleViewModel);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    //return RedirectToAction("RoleList", roleViewModel);
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<RoleViewModel>>(readTask);

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
                var deleteTask = await client.DeleteAsync("api/Role/Delete?id=" + id.ToString());
                //deleteTask.Wait();

                var result = deleteTask;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("RoleList");
                }
            }

            return RedirectToAction("RoleList");
        }
    }
}
