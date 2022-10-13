using KSAA.Master.Application.DTOs.Master;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers
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
                    //readTask.Wait();
                    RootCustomerCode myDeserializedClass = JsonConvert.DeserializeObject<RootCustomerCode>(readTask);
                    var users1 = myDeserializedClass.data.ToList();
                    CustomerCodes = users1.Select(x => new CustomerCodeViewModel
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
        public async Task<IActionResult> CustomerCodeAdd(CustomerCodeViewModel CustomerCodeViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/CustomerCode/CreateCustomerCode", CustomerCodeViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("CustomerCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(CustomerCodeViewModel);
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
                    return RedirectToAction("CustomerCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(CustomerCodeViewModel);
        }

        public async Task<IActionResult> GetCustomerCode(long id)
        {
            CustomerCodeViewModel CustomerCodeViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.GetAsync("api/CustomerCode/GetCustomerCodeById/" + id);
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    var responseString = await getDocumentTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<CustomerCodeViewModel>>(responseString);
                    CustomerCodeViewModel = response.Data;
                }
            }

            return View("CustomerCodeEdit", CustomerCodeViewModel);
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