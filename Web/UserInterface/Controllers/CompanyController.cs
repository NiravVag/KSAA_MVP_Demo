using KSAA.Master.Application.DTOs.Master;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers
{
    public class CompanyController : Controller
    {
        public async Task<IActionResult> CompanyList()
        {
            IEnumerable<CompanyViewModel> Companys = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync("api/Company/GetAllCompany", requestContent);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    //readTask.Wait();
                    RootCompany myDeserializedClass = JsonConvert.DeserializeObject<RootCompany>(readTask);
                    var users1 = myDeserializedClass.data.ToList();
                    Companys = users1.Select(x => new CompanyViewModel
                    {
                        Id = x.id,
                        Company_Code = x.company_Code,
                        Company_Name = x.company_Name,
                        Company_Address = x.company_Address,
                        IP = x.iP,
                        BrowserCase = x.browserCase,
                        IsActive = x.isActive
                    }).Where(x => x.IsActive != IsActive.Delete);

                }
                else //web api sent error response 
                {
                    //log response status here..

                    Companys = Enumerable.Empty<CompanyViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(Companys);
        }
        public IActionResult CompanyAdd()
        {
            CompanyViewModel CompanyViewModel = new CompanyViewModel();
            return View(CompanyViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CompanyAdd(CompanyViewModel CompanyViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/Company/CreateCompany", CompanyViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("CompanyList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(CompanyViewModel);
        }

        public async Task<IActionResult> CompanyEdit(CompanyViewModel CompanyViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PutAsJsonAsync("api/Company/UpdateCompanyById", CompanyViewModel);

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("CompanyList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(CompanyViewModel);
        }

        public async Task<IActionResult> GetCompany(long id)
        {
            CompanyViewModel CompanyViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.GetAsync("api/Company/GetCompanyById/" + id);
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    var responseString = await getDocumentTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<CompanyViewModel>>(responseString);
                    CompanyViewModel = response.Data;
                }
            }

            return View("CompanyEdit", CompanyViewModel);
        }

        public async Task<IActionResult> DeleteCompany(long id)
        {
            CompanyViewModel CompanyViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.DeleteAsync("api/Company/DeleteCompanyById/" + id);
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("CompanyList");
                }
            }

            return View("DeleteCompanys", CompanyViewModel);
        }
    }
}