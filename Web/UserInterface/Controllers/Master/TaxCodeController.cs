using KSAA.Domain.Common;
using KSAA.Domain.Entities;
using KSAA.Master.Application.DTOs.Master.DocumentTypeDTOs;
using KSAA.Master.Application.DTOs.Master.GLIncome_MappingDTOs;
using KSAA.Master.Application.DTOs.Master.PlantCodeDTOs;
using KSAA.Master.Application.DTOs.Master.TaxCodeDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers.Master
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
                    var data = JsonConvert.DeserializeObject<CommonResponse<IEnumerable<TaxCodeViewModel>>>(readTask);
                    taxCodes = data.Data;
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
        public async Task<IActionResult> TaxCodeAdd(TaxCodeViewModel taxCodeViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/TaxCode/CreateTaxCode", taxCodeViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<TaxCodeViewModel>>(readTask);

                    taxCodeViewModel = data.Data;
                    //return RedirectToAction("TaxCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(taxCodeViewModel);
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
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<TaxCodeViewModel>>(readTask);

                    return Json(data);
                    //return RedirectToAction("TaxCodeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(TaxCodeViewModel);
        }

        public async Task<IActionResult> GetTaxCode(long id)
        {
            TaxCodeViewModel taxCodeViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getTaxCodeTask = await client.GetAsync("api/TaxCode/GetTaxCodeById/" + id);
                var result = getTaxCodeTask;
                if (getTaxCodeTask.IsSuccessStatusCode)
                {
                    /*var responseString = await getDocumentTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<TaxCodeViewModel>>(responseString);
                    TaxCodeViewModel = response.Data;*/

                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<TaxCodeViewModel>>(readTask);
                    //readTask.Wait();

                    taxCodeViewModel = data.Data;
                }
            }

            return View("TaxCodeEdit", taxCodeViewModel);
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