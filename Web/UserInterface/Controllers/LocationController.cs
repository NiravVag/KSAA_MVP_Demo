using KSAA.Master.Application.DTOs.Master;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers
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
                        RootLocation myDeserializedClass = JsonConvert.DeserializeObject<RootLocation>(readTask);
                        var Location1 = myDeserializedClass.data.ToList();
                        Locations = Location1.Select(x => new LocationViewModel
                        {
                            Id = x.id,
                            Location_Code = x.location_Code,
                            Address = x.address,
                            GSTRegistrationNo = x.gstregistrationNo,
                            TypeOfUnit = x.typeOfUnit,
                            ProductsManufactured = x.productsManufactured,
                            ProductsTraded = x.productsTraded,
                            TypeOfServicesProvided = x.typeofservicesProvided,
                            IP = x.iP,
                            BrowserCase = x.browserCase,
                            IsActive = x.isActive
                        }).Where(x => x.IsActive != IsActive.Delete);

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
            LocationViewModel LocationViewModel = new LocationViewModel();
            return View(LocationViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> LocationAdd(LocationViewModel LocationViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/Location/CreateLocation", LocationViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("LocationList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(LocationViewModel);
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
                    return RedirectToAction("LocationList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(LocationViewModel);
        }

        public async Task<IActionResult> GetLocation(long id)
        {
            LocationViewModel LocationViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.GetAsync("api/Location/GetLocationById/" + id);
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    var responseString = await getDocumentTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<LocationViewModel>>(responseString);
                    LocationViewModel = response.Data;
                }
            }

            return View("LocationEdit", LocationViewModel);
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