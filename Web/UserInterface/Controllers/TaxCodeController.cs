using KSAA.Master.Application.DTOs.Master;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers
{
    public class TaxCodeController : Controller
    {
        public async Task<IActionResult> TaxCodeList()
        {
            IEnumerable<TaxCodeViewModel> taxCodes = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync("api/TaxCode/GetAllTaxCode", requestContent);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    //readTask.Wait();
                    RootTaxCode myDeserializedClass = JsonConvert.DeserializeObject<RootTaxCode>(readTask);
                    var users1 = myDeserializedClass.data.ToList();
                    taxCodes = users1.Select(x => new TaxCodeViewModel
                    {
                        Id = x.id,
                        Tax_Code = x.tax_Code,
                        SubtaxName = x.subtaxName,
                        TaxRate = x.taxRate,
                        Type = x.type,
                        IP = x.iP,
                        BrowserCase = x.browserCase,
                        IsActive = x.isActive
                    }).Where(x => x.IsActive != IsActive.Delete);

                    //users = readTask;
                    // users = (IEnumerable<UserViewModel>?)myDeserializedClass;
                    // var listuser = JsonSerializer.Deserialize<UserViewModel>(readTask);
                    //users = (IEnumerable<UserViewModel>?)listuser;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    taxCodes = Enumerable.Empty<TaxCodeViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(taxCodes);
        }
        public IActionResult TaxCodeAdd()
        {
            TaxCodeViewModel TaxCodeViewModel = new TaxCodeViewModel();
            return View(TaxCodeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> TaxCodeAdd(TaxCodeViewModel TaxCodeViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/TaxCode/CreateTaxCode", TaxCodeViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("TaxCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(TaxCodeViewModel);
        }

        public async Task<IActionResult> TaxCodeEdits(TaxCodeViewModel TaxCodeViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PutAsJsonAsync("api/TaxCode/UpdateTaxCodeById", TaxCodeViewModel);

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("TaxCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(TaxCodeViewModel);
        }

        public async Task<IActionResult> GetTaxCode(long id)
        {
            TaxCodeViewModel TaxCodeViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.GetAsync("api/TaxCode/GetTaxCodeById/" + id);
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    var responseString = await getDocumentTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<TaxCodeViewModel>>(responseString);
                    TaxCodeViewModel = response.Data;
                }
            }

            return View("TaxCodeEdit", TaxCodeViewModel);
        }

        public async Task<IActionResult> DeleteTaxCodes(long id)
        {
            TaxCodeViewModel TaxCodeViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.DeleteAsync("api/TaxCode/DeleteTaxCodeById/" + id);
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("TaxCodeList");
                }
            }

            return View("DeleteTaxCodes", TaxCodeViewModel);
        }
    }

    public class Response
    {
        public Response()
        {

        }
        public Response(string message)
        {
            Succeeded = true;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
    public class Response<T> : Response
    {

        public Response()
        {

        }
        public Response(T data, string message = null) : base(message)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}