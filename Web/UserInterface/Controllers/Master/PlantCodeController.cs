using KSAA.Domain.Common;
using KSAA.Domain.Entities;
using KSAA.Master.Application.DTOs.Master.DocumentTypeDTOs;
using KSAA.Master.Application.DTOs.Master.GLIncome_MappingDTOs;
using KSAA.Master.Application.DTOs.Master.LocationDTOs;
using KSAA.Master.Application.DTOs.Master.PlantCodeDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers.Master
{
    public class PlantCodeController : Controller
    {
        public async Task<IActionResult> PlantCodeList()
        {

            IEnumerable<PlantCodeViewModel> plantCodes = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync("api/PlantCode/GetAllPlantCode", requestContent);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = await result.Content.ReadAsStringAsync();
                        //readTask.Wait();
                        var data = JsonConvert.DeserializeObject<CommonResponse<IEnumerable<PlantCodeViewModel>>>(readTask);
                        plantCodes = data.Data;

                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        plantCodes = Enumerable.Empty<PlantCodeViewModel>();


                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
            }
            return View(plantCodes);
        }
        public IActionResult PlantCodeAdd()
        {
            PlantCodeViewModel plantCodeViewModel = new PlantCodeViewModel();
            return View(plantCodeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PlantCodeAdd(PlantCodeViewModel plantCodeViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/PlantCode/CreatePlantCode", plantCodeViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<PlantCodeViewModel>>(readTask);

                    plantCodeViewModel = data.Data;
                    //return RedirectToAction("PlantCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(plantCodeViewModel);
        }

        public async Task<IActionResult> PlantCodeEdit(PlantCodeViewModel PlantCodeViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PutAsJsonAsync("api/PlantCode/UpdatePlantCodeById", PlantCodeViewModel);

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<PlantCodeViewModel>>(readTask);

                    return Json(data);
                    //return RedirectToAction("PlantCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(PlantCodeViewModel);
        }

        public async Task<IActionResult> GetPlantCode(long id)
        {
            PlantCodeViewModel plantCodeViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getPlantCodeTask = await client.GetAsync("api/PlantCode/GetPlantCodeById/" + id);
                var result = getPlantCodeTask;
                if (getPlantCodeTask.IsSuccessStatusCode)
                {
                    /*var responseString = await getDocumentTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<PlantCodeViewModel>>(responseString);
                    PlantCodeViewModel = response.Data;*/

                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<PlantCodeViewModel>>(readTask);
                    //readTask.Wait();

                    plantCodeViewModel = data.Data;
                }
            }

            return View("PlantCodeEdit", plantCodeViewModel);
        }

        public async Task<IActionResult> DeletePlantCodes(long id)
        {
            PlantCodeViewModel PlantCodeViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.DeleteAsync("api/PlantCode/DeletePlantCodeById/" + id);
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("PlantCodeList");
                }
            }

            return View("DeletePlantCodes", PlantCodeViewModel);
        }
    }
}