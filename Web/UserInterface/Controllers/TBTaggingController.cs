using KSAA.Master.Application.DTOs.Master;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;

namespace KSAA.UserInterface.Web.Controllers
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
                    RootTBTagging myDeserializedClass = JsonConvert.DeserializeObject<RootTBTagging>(readTask);
                    var users1 = myDeserializedClass.data.ToList();

                    TBTaggings = users1.Select(x => new TBTaggingViewModel
                    {
                        Id = x.id,
                        TBTaggingCode = x.tbTaggingCode,
                        GLCode = x.glCode,
                        GLName = x.glName,
                        Amount = x.amount,
                            TagCode = x.tagCode,
                            IP = x.iP,
                            BrowserCase = x.browserCase,
                        IsActive = x.isActive
                    }).Where(x => x.IsActive != IsActive.Delete);

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
        public async Task<IActionResult> TBTaggingAdd(TBTaggingViewModel TBTaggingViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/TBTagging/CreateTBTagging", TBTaggingViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("TBTaggingList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(TBTaggingViewModel);
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
                    return RedirectToAction("TBTaggingList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(TBTaggingViewModel);
        }

        public async Task<IActionResult> GetTBTagging(long id)
        {
            TBTaggingViewModel TBTaggingViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getTBTaggingTask = await client.GetAsync("api/TBTagging/GetTBTaggingById/" + id);
                if (getTBTaggingTask.IsSuccessStatusCode)
                {
                    var responseString = await getTBTaggingTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<TBTaggingViewModel>>(responseString);
                    TBTaggingViewModel = response.Data;
                }
            }

            return View("TBTaggingEdit", TBTaggingViewModel);
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