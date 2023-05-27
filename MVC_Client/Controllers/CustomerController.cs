using BusinessObject.Model;
using Microsoft.AspNetCore.Mvc;
using MVC_Client.Helper;
using MVC_Client.Models;
using System.Net;
using System.Net.Http.Headers;

namespace MVC_Client.Controllers
{
    [Route("customer")]
    public class CustomerController : Controller
    {
        private readonly HttpClient client;
        private string api;

        public CustomerController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            contentType.Parameters.Add(new NameValueHeaderValue("odata.metadata", "none"));
            client.DefaultRequestHeaders.Accept.Add(contentType);
            api = "https://localhost:7196/odata/customers";
        }

        #region Index
        /**
         * [GET]
         * Index view
        */
        public async Task<IActionResult> Index()
        {
            var list = await client.GetApi<IEnumerable<CustomerViewModel>>(api);

            return View(list);
        }
        #endregion

        #region Create
        /**
         * [GET]
         * Create View
        */
        [HttpGet("add", Name = "add")]
        public IActionResult Create()
        {
            return View();
        }

        /**
         * [POST]
         * Craete View
        */
        [HttpPost("add")]
        public async Task<IActionResult> Create(string gender, CustomerViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var cus = new Customer();

                if (gender == "Male")
                    cus.Gender = Gender.Male;
                else
                    cus.Gender = Gender.Female;

                cus.Username = obj.Username;
                cus.Password = obj.Password;
                cus.Fullname = obj.Fullname!;
                cus.Address = obj.Address!;
                cus.Birthday = DateTime.Parse(obj.Birthday!).ToString("dd/MM/yyyy");

                HttpResponseMessage res = await client.PostApi(cus, $"{api}/add");
                if (res.StatusCode == HttpStatusCode.Created)
                {
                    return Redirect("/customer");
                }
            }
            return View(obj);
        }
        #endregion

        #region Edit
        /**
         * [GET]
         * Update View
        */
        [HttpGet("edit/{id}", Name = "edit")]
        public async Task<IActionResult> Update(int id)
        {
            var cus = await client.GetApi<CustomerViewModel>($"{api}/{id}");

            return View(cus);
        }

        /**
         * [POST]
         * Update View
        */
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Update(int id, string gender, CustomerViewModel obj)
        {
            var cus = await client.GetApi<CustomerViewModel>($"{api}/{id}");
            if (ModelState.IsValid)
            {
                obj.Gender = gender;

                if (obj.Birthday != null)
                    obj.Birthday = DateTime.Parse(obj.Birthday).ToString("dd/MM/yyyy");
                else
                    obj.Birthday = cus.Birthday;

                HttpResponseMessage res = await client.PatchApi(obj, $"{api}/{id}");
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    return Redirect("/customer");
                }
            }
            return View(cus);

        }
        #endregion

        #region Delete
        /**
         * [GET]
         * Delete View
        */
        [HttpGet("delete/{id}", Name = "delete")]
        public async Task<IActionResult> Delete(CustomerViewModel obj)
        {
            var cus = await client.GetApi<CustomerViewModel>($"{api}/{obj.Id}");

            return View(cus);
        }

        /**
         * [POST]
         * Delete View
        */
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage res = await client.DeleteAsync($"{api}/{id}");
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return Redirect("/customer");
            }
            return View();
        }
        #endregion
    }
}
