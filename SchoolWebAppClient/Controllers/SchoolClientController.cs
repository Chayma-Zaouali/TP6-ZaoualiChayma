using Microsoft.AspNetCore.Mvc;
using SchoolWebAppClient.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SchoolWebAppClient.Controllers
{
    public class SchoolClientController : Controller
    {
        private readonly HttpClient _client;

        public SchoolClientController(HttpClient client)
        {
            client.BaseAddress ??= new Uri("https://localhost:7018/"); // Port de l'API
            _client = client;
        }

        // 1. GetAllSchools (Index)
        public async Task<IActionResult> Index()
        {
            var schools = await _client.GetFromJsonAsync<List<SchoolClient>>("api/SchoolsRepo/list-schools");
            return View(schools);
        }

        // 2. GetSchoolById
        public async Task<IActionResult> GetSchoolById(int id)
        {
            var response = await _client.GetAsync($"api/SchoolsRepo/get-school-by-id/{id}");

            if (response.IsSuccessStatusCode)
            {
                var school = await response.Content.ReadFromJsonAsync<SchoolClient>();
                return View(school);
            }

            return NotFound();
        }

        // 3. GetSchoolByName
        public async Task<IActionResult> GetSchoolByName(string name)
        {
            var response = await _client.GetAsync($"api/SchoolsRepo/search-by-name/{name}");

            if (response.IsSuccessStatusCode)
            {
                var schools = await response.Content.ReadFromJsonAsync<IEnumerable<SchoolClient>>();
                return View(schools);
            }

            return View(new List<SchoolClient>());
        }

        // 4. CreateSchool - GET
        [HttpGet]
        public IActionResult CreateSchool()
        {
            return View();
        }

        // 4. CreateSchool - POST
        [HttpPost]
        public async Task<IActionResult> CreateSchool(SchoolClient school)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/SchoolsRepo/add-school", school);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // 5. EditSchool - GET
        [HttpGet]
        public async Task<IActionResult> EditSchool(int id)
        {
            var response = await _client.GetAsync($"api/SchoolsRepo/get-school-by-id/{id}");

            if (response.IsSuccessStatusCode)
            {
                var school = await response.Content.ReadFromJsonAsync<SchoolClient>();
                return View(school);
            }

            return NotFound();
        }

        // 5. EditSchool - POST
        [HttpPost]
        public async Task<IActionResult> EditSchool(SchoolClient school)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/SchoolsRepo/edit-school/{school.Id}", school);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(school);
        }

        // 6. DeleteSchool - GET
        [HttpGet]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var response = await _client.GetAsync($"api/SchoolsRepo/get-school-by-id/{id}");

            if (response.IsSuccessStatusCode)
            {
                var school = await response.Content.ReadFromJsonAsync<SchoolClient>();
                return View(school);
            }

            return NotFound();
        }

        // 6. DeleteSchool - POST
        [HttpPost]
        public async Task<IActionResult> DeleteSchool(SchoolClient school)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/SchoolsRepo/delete-school/{school.Id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(school);
        }
    }
}

