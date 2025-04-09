using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EasyTesting.Web.Pages.Questions
{
    public class CreateModel : PageModel
    {
        public List<SubjectDto> Subjects { get; set; } = new();
        [BindProperty]
        public required CreateQuestionDto NewQuestion { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            Subjects = await client.GetFromJsonAsync<List<SubjectDto>>("api/v1/subjects") ?? new();
        }
    }
}
