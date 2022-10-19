using KSAA.Domain.Common;
using KSAA.Domain.Entities;
using KSAA.Master.Application.DTOs.Master.DocumentTypeDTOs;
using KSAA.Master.Application.DTOs.Master.GLIncome_MappingDTOs;
using KSAA.Master.Application.DTOs.Master.TaxCodeDTOs;
using KSAA.Master.Application.DTOs.Master.TBTaggingDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;

namespace KSAA.UserInterface.Web.Controllers.Master
{
    public class TBTaggingController : Controller
    {
        public async Task<IActionResult> TBTaggingList()
        {
            IEnumerable<TBTaggingViewModel> TBTaggings = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync("api/TBTagging/GetAllTBTagging", requestContent);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    //readTask.Wait();
                    var data = JsonConvert.DeserializeObject<CommonResponse<IEnumerable<TBTaggingViewModel>>>(readTask);
                    TBTaggings = data.Data;
                }
                else
                {
                    //log response status here..

                    TBTaggings = Enumerable.Empty<TBTaggingViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(TBTaggings);
        }

        public IActionResult TBTaggingAdd()
        {
            TBTaggingViewModel TBTaggingViewModel = new TBTaggingViewModel();
            return View(TBTaggingViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> TBTaggingAdd(TBTaggingViewModel tbTaggingViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/TBTagging/CreateTBTagging", tbTaggingViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<TBTaggingViewModel>>(readTask);

                    tbTaggingViewModel = data.Data;
                    //return RedirectToAction("TBTaggingList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(tbTaggingViewModel);
        }

        public async Task<IActionResult> TBTaggingEdit(TBTaggingViewModel TBTaggingViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PutAsJsonAsync("api/TBTagging/UpdateTBTaggingById", TBTaggingViewModel);

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<TBTaggingViewModel>>(readTask);

                    return Json(data);
                    //return RedirectToAction("TBTaggingList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(TBTaggingViewModel);
        }

        public async Task<IActionResult> GetTBTagging(long id)
        {
            TBTaggingViewModel tbTaggingViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getTBTaggingTask = await client.GetAsync("api/TBTagging/GetTBTaggingById/" + id);
                var result = getTBTaggingTask;
                if (getTBTaggingTask.IsSuccessStatusCode)
                {
                    /*var responseString = await getTBTaggingTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<TBTaggingViewModel>>(responseString);
                    TBTaggingViewModel = response.Data;*/

                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<TBTaggingViewModel>>(readTask);
                    //readTask.Wait();

                    tbTaggingViewModel = data.Data;
                }
            }

            return View("TBTaggingEdit", tbTaggingViewModel);
        }

        public async Task<IActionResult> DeleteTBTaggingById(long id)
        {
            TBTaggingViewModel TBTaggingViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getTBTaggingTask = await client.DeleteAsync("api/TBTagging/DeleteTBTaggingById/" + id);
                if (getTBTaggingTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("TBTaggingList");
                }
            }

            return View("DeleteTBTagging", TBTaggingViewModel);
        }
    }
}