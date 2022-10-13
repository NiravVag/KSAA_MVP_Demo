using KSAA.Master.Application.DTOs.Master;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers
{
    public class VendorCodeController : Controller
    {
        public async Task<IActionResult> VendorCodeList()
            {
            IEnumerable<VendorCodeViewModel> VendorCodes = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync("api/VendorCode/GetAllVendorCode", requestContent);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    //readTask.Wait();
                    RootVendorCode myDeserializedClass = JsonConvert.DeserializeObject<RootVendorCode>(readTask);
                    var users1 = myDeserializedClass.data.ToList();
                    VendorCodes = users1.Select(x => new VendorCodeViewModel
                    {
                        Id = x.id,
                        Customer_Code = x.customer_Code,
                        GSTN = x.gstn,
                        Name = x.name,
                        Location = x.location,
                        Address = x.address,
                        IP = x.iP,
                        BrowserCase = x.browserCase,
                        IsActive = x.isActive
                    }).Where(x => x.IsActive != IsActive.Delete);

                }
                else //web api sent error response 
                {
                    //log response status here..

                    VendorCodes = Enumerable.Empty<VendorCodeViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(VendorCodes);
        }
        public IActionResult VendorCodeAdd()
        {
            VendorCodeViewModel VendorCodeViewModel = new VendorCodeViewModel();
            return View(VendorCodeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> VendorCodeAdd(VendorCodeViewModel VendorCodeViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/VendorCode/CreateVendorCode", VendorCodeViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("VendorCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(VendorCodeViewModel);
        }

        public async Task<IActionResult> VendorCodeEdit(VendorCodeViewModel VendorCodeViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PutAsJsonAsync("api/VendorCode/UpdateVendorCodeById", VendorCodeViewModel);

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("VendorCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(VendorCodeViewModel);
        }

        public async Task<IActionResult> GetVendorCode(long id)
        {
            VendorCodeViewModel VendorCodeViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.GetAsync("api/VendorCode/GetVendorCodeById/" + id);
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    var responseString = await getDocumentTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<VendorCodeViewModel>>(responseString);
                    VendorCodeViewModel = response.Data;
                }
            }

            return View("VendorCodeEdit", VendorCodeViewModel);
        }

        public async Task<IActionResult> DeleteVendorCodes(long id)
        {
            VendorCodeViewModel VendorCodeViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.DeleteAsync("api/VendorCode/DeleteVendorCodeById/" + id);
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("VendorCodeList");
                }
            }

            return View("DeleteVendorCodes", VendorCodeViewModel);
        }
    }
}