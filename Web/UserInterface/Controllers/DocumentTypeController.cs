using KSAA.Master.Application.DTOs.Master;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KSAA.UserInterface.Web.Controllers
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
                    Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(readTask);
                    var users1 = myDeserializedClass.data.ToList();

                    documentTypes = users1.Select(x => new DocumentTypeViewModel
                    {


                        Id = x.id,
                        BillType = x.billType,
                        TransactionType = x.transactionType,
                        OurSoftwareProcessing = x.ourSoftwareProcessing,
                        IP = x.iP,
                        BrowserCase = x.browserCase,
                        IsActive = x.isActive
                    }).Where(x => x.IsActive != IsActive.Delete);

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
                    return RedirectToAction("DocumentTypeList");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(documentTypeViewModel);
        }

        public async Task<IActionResult> DocomentTypeEdit(DocumentTypeViewModel documentTypeViewModel)
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
                    return RedirectToAction("DocumentTypeList");
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
                if (getDocumentTask.IsSuccessStatusCode)
                {
                    var responseString = await getDocumentTask.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response<DocumentTypeViewModel>>(responseString);
                    documentTypeViewModel = response.Data;
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