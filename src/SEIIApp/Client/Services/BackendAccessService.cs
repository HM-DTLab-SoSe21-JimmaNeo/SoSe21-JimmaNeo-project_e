using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SEIIApp.Shared.DomainDto;

namespace SEIIApp.Client.Services
{
    public class BackendAccessService
    {
      private HttpClient HttpClient { get; set; }

      public BackendAccessService(HttpClient httpClient)
      {
          this.HttpClient = httpClient;
      }

      private string GetUrlWithId(int id)
      {
          return $"api/users/{id}";
      }

      public async Task<StudentDto> GetStudentById(int id)
      {
          return await HttpClient.GetFromJsonAsync<StudentDto>(/*GetUrlWithId(id)*/$"api/users/{id}");
      }
    }
}