using KSAA.Domain.Common;
using KSAA.Domain.Entities;
using KSAA.Master.Application.DTOs.Master.CompanyDTOs;
using KSAA.Master.Application.DTOs.Master.CustomerCodeDTOs;
using KSAA.Master.Application.DTOs.Master.DocumentTypeDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers.Master
{
    public class CustomerCodeController : Controller
    {
        public async Task<IActionResult> CustomerCodeList()
        {
            IEnumerable<CustomerCodeViewModel> CustomerCodes = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync("api/CustomerCode/GetAllCustomerCode", requestContent);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    
                    var data = JsonConvert.DeserializeObject<CommonResponse<IEnumerable<CustomerCodeViewModel>>>(readTask);
                    CustomerCodes = data.Data;

                }
                else //web api sent error response 
                {
                    //log response status here..

                    CustomerCodes = Enumerable.Empty<CustomerCodeViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(CustomerCodes);
        }
        public IActionResult CustomerCodeAdd()
        {
            CustomerCodeViewModel CustomerCodeViewModel = new CustomerCodeViewModel();
            return View(CustomerCodeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerCodeAdd(CustomerCodeViewModel customerCodeViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/CustomerCode/CreateCustomerCode", customerCodeViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<CustomerCodeViewModel>>(readTask);

                    customerCodeViewModel = data.Data;
                    //return RedirectToAction("CustomerCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(customerCodeViewModel);
        }

        public async Task<IActionResult> CustomerCodeEdits(CustomerCodeViewModel CustomerCodeViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PutAsJsonAsync("api/CustomerCode/UpdateCustomerCodeById", CustomerCodeViewModel);

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<CustomerCodeViewModel>>(readTask);

                    return Json(data);
                    //return RedirectToAction("CustomerCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(CustomerCodeViewModel);
        }

        public async Task<IActionResult> GetCustomerCode(long id)
        {
            CustomerCodeViewModel customerCodeViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.GetAsync("api/CustomerCode/GetCustomerCodeById/" + id);
                var result = getDocumentTask;
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    /*var responseString = await getDocumentTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<CustomerCodeViewModel>>(responseString);
                    CustomerCodeViewModel = response.Data;*/

                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<CustomerCodeViewModel>>(readTask);
                    //readTask.Wait();

                    customerCodeViewModel = data.Data;
                }
            }

            return View("CustomerCodeEdit", customerCodeViewModel);
        }

        public async Task<IActionResult> DeleteCustomerCodes(long id)
        {
            CustomerCodeViewModel CustomerCodeViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.DeleteAsync("api/CustomerCode/DeleteCustomerCodeById/" + id);
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("CustomerCodeList");
                }
            }

            return View("DeleteCustomerCodes", CustomerCodeViewModel);
        }
    }
}