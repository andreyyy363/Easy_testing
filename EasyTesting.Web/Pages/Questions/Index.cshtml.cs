using EasyTesting.Core.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EasyTesting.Web.Pages.Questions
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public List<QuestionDTO> Questions { get; set; } = new();
        public List<SubjectDTO> Subjects { get; set; } = new();

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            Questions = await client.GetFromJsonAsync<List<QuestionDTO>>("api/v1/questions") ?? new();
            Subjects = await client.GetFromJsonAsync<List<SubjectDTO>>("api/v1/subjects") ?? new();
        }
    }
}
