using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SEIIApp.Shared.DomainDto;
using SEIIApp.Shared.DomainDto.StatusDto;

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

        private string GetUrlBasic()
        {
            return "api/users";
        }

        public async Task<StudentDto> GetStudentById(int id)
        {
            return await HttpClient.GetFromJsonAsync<StudentDto>(GetUrlWithId(id));
        }

        public async Task<UserDto> GetUserByNameAndPw(string name, string pw)
        {
            var response = await HttpClient.GetAsync($"api/users?name={name}&password={pw}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDto>();
            }

            return null;
        }

        public async Task<bool> UploadContentFile(ContentDto entryFile)
        {
            var response = await HttpClient.PutAsJsonAsync("api/content", entryFile);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<ContentDto> GetContentById(int id)
        {
            return await HttpClient.GetFromJsonAsync<ContentDto>($"api/content/{id}");
        }

        public async Task<ContentDto[]> GetAllContent()
        {
            return await HttpClient.GetFromJsonAsync<ContentDto[]>("api/content");
        }

        public async Task<CourseStatusDto[]> GetAllEnrolledCourses(int id)
        {
            var response = await HttpClient.GetAsync($"api/coursestatus?studentId={id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CourseStatusDto[]>();
            }

            return null;
        }

        public async Task<CourseDto[]> GetAllCourses()
        {
            var response = await HttpClient.GetAsync("api/course");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CourseDto[]>();
            }

            return null;
        }

        public async Task<StudentDto[]> GetAllStudents()
        {
            var response = await HttpClient.GetAsync("api/users/allstudents");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StudentDto[]>();
            }

            return null;
        }

        private string GetQuizUrl()
        {
            return "api/quizdefinitions";
        }

        private string GetQuizUrlWithId(int id)
        {
            return $"{GetQuizUrl()}/{id}";
        }

        /// <summary>
        /// Returns a certain quiz by id
        /// </summary>
        public async Task<QuizDto> GetQuizById(int id)
        {
            return await HttpClient.GetFromJsonAsync<QuizDto>(GetQuizUrlWithId(id));
        }

        /// <summary>
        /// Returns all quizzes stored on the backend
        /// </summary>
        public async Task<QuizDto[]> GetQuizOverview()
        {
            return await HttpClient.GetFromJsonAsync<QuizDto[]>(GetQuizUrl());
        }

        /// <summary>
        /// Adds or updates a quiz on the backend. Returns the quiz if successful else null
        /// </summary>
        public async Task<QuizDto> AddOrUpdateQuiz(QuizDto dto)
        {
            var response = await HttpClient.PutAsJsonAsync(GetQuizUrl(), dto);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.DeserializeResponseContent<QuizDto>();
            }
            else return null;
        }

        /// <summary>
        /// Deletes a quiz and returns true if successful
        /// </summary>
        public async Task<bool> DeleteQuiz(int quizId)
        {
            var response = await HttpClient.DeleteAsync(GetQuizUrlWithId(quizId));
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}