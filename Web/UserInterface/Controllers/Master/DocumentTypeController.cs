using KSAA.Domain.Common;
using KSAA.Master.Application.DTOs.Master.CustomerCodeDTOs;
using KSAA.Master.Application.DTOs.Master.DocumentTypeDTOs;
using KSAA.User.Application.DTOs.Role;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers.Master
{
    public class DocumentTypeController : Controller
    {
        public async Task<IActionResult> DocumentTypeList()
        {
            IEnumerable<DocumentTypeViewModel> documentTypes = null;    

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync("api/DocumentType/GetAllDocumentType", requestContent);
                //responseTask.Wait();

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    //readTask.Wait();

                    var data = JsonConvert.DeserializeObject<CommonResponse<List<DocumentTypeViewModel>>>(readTask);
                    documentTypes = data.Data;
                }
                else
                {
                    //log response status here..

                    documentTypes = Enumerable.Empty<DocumentTypeViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(documentTypes);
        }

        public IActionResult DocumentTypeAdd()
        {
            DocumentTypeViewModel documentTypeViewModel = new DocumentTypeViewModel();
            return View(documentTypeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DocumentTypeAdd(DocumentTypeViewModel documentTypeViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync("api/DocumentType/CreateDocumentType", documentTypeViewModel);
                //postTask.Wait();

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<DocumentTypeViewModel>>(readTask);

                    documentTypeViewModel = data.Data;
                    //return RedirectToAction("DocumentTypeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(documentTypeViewModel);
        }

        public async Task<IActionResult> DocomentTypeEdits(DocumentTypeViewModel documentTypeViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PutAsJsonAsync("api/DocumentType/UpdateDocumentTypeById", documentTypeViewModel);

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<DocumentTypeViewModel>>(readTask);

                    return Json(data);
                    //return RedirectToAction("DocumentTypeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(documentTypeViewModel);
        }

        public async Task<IActionResult> GetDocument(long id)
        {
            DocumentTypeViewModel documentTypeViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.GetAsync("api/DocumentType/GetDocumentTypeById/" + id);
                var result = getDocumentTask;
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    /*var responseString = await getDocumentTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<DocumentTypeViewModel>>(responseString);
                    documentTypeViewModel = response.Data;*/

                    var readTask = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CommonResponse<DocumentTypeViewModel>>(readTask);
                    //readTask.Wait();

                    documentTypeViewModel = data.Data;
                }
            }

            return View("DocumentTypeEdit", documentTypeViewModel);
        }

        public async Task<IActionResult> DeleteDocument(long id)
        {
            DocumentTypeViewModel documentTypeViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7050/");
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var getDocumentTask = await client.DeleteAsync("api/DocumentType/DeleteDocumentTypeById/" + id);
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("DocumentTypeList");
                }
            }

            return View("DeleteDocument", documentTypeViewModel);
        }
    }
}