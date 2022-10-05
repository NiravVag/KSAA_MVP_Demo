using KSAA.Master.Application.DTOs.Master;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers
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
                        RootPlantCode myDeserializedClass = JsonConvert.DeserializeObject<RootPlantCode>(readTask);
                        var plantCode1 = myDeserializedClass.data.ToList();
                        plantCodes = plantCode1.Select(x => new PlantCodeViewModel
                        {
                            Id = x.id,
                            Location = x.location,
                            Address = x.address,
                            Plant_Code = x.plant_Code,
                            GSTRegistrationNo = x.gstregistrationNo,
                            TypeOfUnit = x.typeOfUnit,
                            ProductsManufactured = x.productsManufactured,
                            ProductsTraded = x.productsTraded,
                            ServicesProvided = x.servicesProvided,
                            RegistrationType = x.registrationType,
                            IP = x.iP,
                            BrowserCase = x.browserCase,
                            IsActive = x.isActive
                        }).Where(x => x.IsActive != IsActive.Delete);

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
        public async Task<IActionResult> PlantCodeAdd(PlantCodeViewModel PlantCodeViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/PlantCode/CreatePlantCode", PlantCodeViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("PlantCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(PlantCodeViewModel);
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
                    return RedirectToAction("PlantCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(PlantCodeViewModel);
        }

        public async Task<IActionResult> GetPlantCode(long id)
        {
            PlantCodeViewModel PlantCodeViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.GetAsync("api/PlantCode/GetPlantCodeById/" + id);
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    var responseString = await getDocumentTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<PlantCodeViewModel>>(responseString);
                    PlantCodeViewModel = response.Data;
                }
            }

            return View("PlantCodeEdit", PlantCodeViewModel);
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