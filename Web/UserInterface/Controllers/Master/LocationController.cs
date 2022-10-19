using KSAA.Domain.Common;
using KSAA.Domain.Entities;
using KSAA.Master.Application.DTOs.Master.DocumentTypeDTOs;
using KSAA.Master.Application.DTOs.Master.GLIncome_MappingDTOs;
using KSAA.Master.Application.DTOs.Master.LocationDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers.Master
{
    public class LocationController : Controller
    {
        public async Task<IActionResult> LocationList()
        {

            IEnumerable<LocationViewModel> Locations = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync("api/Location/GetAllLocation", requestContent);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = await result.Content.ReadAsStringAsync();
                        //readTask.Wait();
                        var data = JsonConvert.DeserializeObject<CommonResponse<List<LocationViewModel>>>(readTask);
                        Locations = data.Data;
                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        Locations = Enumerable.Empty<LocationViewModel>();


                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
            }
            return View(Locations);
        }
        public IActionResult LocationAdd()
        {
            LocationViewModel locationViewModel = new LocationViewModel();
            return View(locationViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> LocationAdd(LocationViewModel locationViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/Location/CreateLocation", locationViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<LocationViewModel>>(readTask);

                    locationViewModel = data.Data;
                    //return RedirectToAction("LocationList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(locationViewModel);
        }

        public async Task<IActionResult> LocationEdit(LocationViewModel LocationViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PutAsJsonAsync("api/Location/UpdateLocationById", LocationViewModel);

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<LocationViewModel>>(readTask);

                    return Json(data);
                    //return RedirectToAction("LocationList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(LocationViewModel);
        }

        public async Task<IActionResult> GetLocation(long id)
        {
            LocationViewModel locationViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.GetAsync("api/Location/GetLocationById/" + id);
                var result = getDocumentTask;
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    /*var responseString = await getDocumentTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<LocationViewModel>>(responseString);
                    LocationViewModel = response.Data;*/

                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<LocationViewModel>>(readTask);
                    //readTask.Wait();

                    locationViewModel = data.Data;
                }
            }

            return View("LocationEdit", locationViewModel);
        }

        public async Task<IActionResult> DeleteLocation(long id)
        {
            LocationViewModel LocationViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.DeleteAsync("api/Location/DeleteLocationById/" + id);
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("LocationList");
                }
            }

            return View("DeleteLocations", LocationViewModel);
        }
    }
}