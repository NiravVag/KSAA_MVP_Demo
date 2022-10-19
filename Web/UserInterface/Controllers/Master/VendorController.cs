using KSAA.Domain.Common;
using KSAA.Domain.Entities;
using KSAA.Master.Application.DTOs.Master.DocumentTypeDTOs;
using KSAA.Master.Application.DTOs.Master.GLIncome_MappingDTOs;
using KSAA.Master.Application.DTOs.Master.TBTaggingDTOs;
using KSAA.Master.Application.DTOs.Master.VendorCodeDTOs;
using KSAA.User.Application.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers.Master
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
                    var data = JsonConvert.DeserializeObject<CommonResponse<IEnumerable<VendorCodeViewModel>>>(readTask);
                    VendorCodes = data.Data;
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
        public async Task<IActionResult> VendorCodeAdd(VendorCodeViewModel vendorCodeViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/VendorCode/CreateVendorCode", vendorCodeViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<VendorCodeViewModel>>(readTask);

                    vendorCodeViewModel = data.Data;
                    //return RedirectToAction("VendorCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(vendorCodeViewModel);
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
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<VendorCodeViewModel>>(readTask);

                    return Json(data);
                    //return RedirectToAction("VendorCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(VendorCodeViewModel);
        }

        public async Task<IActionResult> GetVendorCode(long id)
        {
            VendorCodeViewModel vendorCodeViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getVendorCodeTask = await client.GetAsync("api/VendorCode/GetVendorCodeById/" + id);
                var result = getVendorCodeTask;
                if (getVendorCodeTask.IsSuccessStatusCode)
                {
                    /*var responseString = await getDocumentTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<VendorCodeViewModel>>(responseString);
                    VendorCodeViewModel = response.Data;*/

                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<VendorCodeViewModel>>(readTask);
                    //readTask.Wait();

                    vendorCodeViewModel = data.Data;
                }
            }

            return View("VendorCodeEdit", vendorCodeViewModel);
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