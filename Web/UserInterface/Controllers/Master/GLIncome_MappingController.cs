using KSAA.Domain.Common;
using KSAA.Master.Application.DTOs.Master.DocumentTypeDTOs;
using KSAA.Master.Application.DTOs.Master.GLIncome_MappingDTOs;
using KSAA.Master.Application.DTOs.Master.TBTaggingDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers.Master
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
                    var data = JsonConvert.DeserializeObject<CommonResponse<IEnumerable<GLIncome_MappingViewModel>>>(readTask);
                    GLIncome_Mappings = data.Data;
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
        public async Task<IActionResult> GLIncome_MappingAdd(GLIncome_MappingViewModel glIncome_MappingViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/GLIncome_Mapping/CreateGLIncome_Mapping", glIncome_MappingViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<GLIncome_MappingViewModel>>(readTask);

                    glIncome_MappingViewModel = data.Data;
                    return RedirectToAction("GLIncome_MappingList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(glIncome_MappingViewModel);
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
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<GLIncome_MappingViewModel>>(readTask);

                    return Json(data);
                    //return RedirectToAction("GLIncome_MappingList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(GLIncome_MappingViewModel);
        }

        public async Task<IActionResult> GetGLIncome_Mapping(long id)
        {
            GLIncome_MappingViewModel glIncome_MappingViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.GetAsync("api/GLIncome_Mapping/GetGLIncome_MappingById/" + id);
                var result = getDocumentTask;
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    /*var responseString = await getDocumentTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<GLIncome_MappingViewModel>>(responseString);
                    GLIncome_MappingViewModel = response.Data;*/

                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<GLIncome_MappingViewModel>>(readTask);
                    //readTask.Wait();

                    glIncome_MappingViewModel = data.Data;
                }
            }

            return View("GLIncome_MappingEdit", glIncome_MappingViewModel);
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