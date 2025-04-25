using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EasyTesting.Web.Pages.Subjects
{
    public class IndexModel : PageModel
    {
        public List<SubjectDTO> Subjects { get; set; } = new();

        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            Subjects = await client.GetFromJsonAsync<List<SubjectDTO>>("api/v1/subjects") ?? new();
        }
    }
}
