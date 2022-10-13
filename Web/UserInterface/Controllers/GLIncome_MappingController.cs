using KSAA.Master.Application.DTOs.Master;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers
{
    public class GLIncome_MappingController : Controller
    {
        public async Task<IActionResult> GLIncome_MappingList()
        {
            IEnumerable<GLIncome_MappingViewModel> GLIncome_Mappings = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync("api/GLIncome_Mapping/GetAllGLIncome_Mapping", requestContent);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    //readTask.Wait();
                    RootGLIncome_Mapping myDeserializedClass = JsonConvert.DeserializeObject<RootGLIncome_Mapping>(readTask);
                    var users1 = myDeserializedClass.data.ToList();

                    GLIncome_Mappings = users1.Select(x => new GLIncome_MappingViewModel
                    {
                        Id = x.id,
                        GLIncomeCode = x.glIncomeCode,
                        GLIncomeDescription = x.glIncomeDescription,
                        IP = x.iP,
                        BrowserCase = x.browserCase,
                        IsActive = x.isActive
                    }).Where(x => x.IsActive != IsActive.Delete);

                }
                else
                {
                    //log response status here..

                    GLIncome_Mappings = Enumerable.Empty<GLIncome_MappingViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(GLIncome_Mappings);
        }

        public IActionResult GLIncome_MappingAdd()
        {
            GLIncome_MappingViewModel GLIncome_MappingViewModel = new GLIncome_MappingViewModel();
            return View(GLIncome_MappingViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GLIncome_MappingAdd(GLIncome_MappingViewModel GLIncome_MappingViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/GLIncome_Mapping/CreateGLIncome_Mapping", GLIncome_MappingViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GLIncome_MappingList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(GLIncome_MappingViewModel);
        }

        public async Task<IActionResult> GLIncome_MappingEdit(GLIncome_MappingViewModel GLIncome_MappingViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PutAsJsonAsync("api/GLIncome_Mapping/UpdateGLIncome_MappingById", GLIncome_MappingViewModel);

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GLIncome_MappingList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(GLIncome_MappingViewModel);
        }

        public async Task<IActionResult> GetGLIncome_Mapping(long id)
        {
            GLIncome_MappingViewModel GLIncome_MappingViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.GetAsync("api/GLIncome_Mapping/GetGLIncome_MappingById/" + id);
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    var responseString = await getDocumentTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<GLIncome_MappingViewModel>>(responseString);
                    GLIncome_MappingViewModel = response.Data;
                }
            }

            return View("GLIncome_MappingEdit", GLIncome_MappingViewModel);
        }

        public async Task<IActionResult> DeleteGLIncome_MappingById(long id)
        {
            GLIncome_MappingViewModel GLIncome_MappingViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.DeleteAsync("api/GLIncome_Mapping/DeleteGLIncome_MappingById/" + id);
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("GLIncome_MappingList");
                }
            }

            return View("DeleteDocument", GLIncome_MappingViewModel);
        }
    }
}