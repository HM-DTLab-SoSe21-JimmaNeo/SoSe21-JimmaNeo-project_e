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

        public async Task<bool> UploadContentFile(PdfContentDto entryFile)
        {
            var response = await HttpClient.PutAsJsonAsync("api/pdfcontent", entryFile);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<PdfContentDto> GetContentById(int id)
        {
            return await HttpClient.GetFromJsonAsync<PdfContentDto>($"api/pdfcontent/{id}");
        }

        public async Task<PdfContentDto[]> GetAllContent()
        {
            return await HttpClient.GetFromJsonAsync<PdfContentDto[]>("api/pdfcontent");
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


        public async Task<QuestionStatusDto[]> GetAllQuestionsForRepetition(int id)
        {
            var response = await HttpClient.GetAsync($"api/questionstatus?userId={id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<QuestionStatusDto[]>();
            }

            return null;
        }

        public async Task<QuestionStatusDto> GetQuestionStatusByQuestionAndUser(int userId, int questionId)
        {
            var response =
                await HttpClient.GetAsync(
                    $"/api/questionstatus/byQuestionAndUser?userId={userId}&questionId={questionId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<QuestionStatusDto>();
            }

            return null;
        }

        public async Task<QuestionStatusDto> AddOrUpdateQuestionStatus(questionStatusTransfer qst)
        {
            var response = await HttpClient.PutAsJsonAsync("api/questionstatus", qst);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.DeserializeResponseContent<QuestionStatusDto>();
            }
            else return null;
        }

        public async Task<QuizDto> GetQuizById(int id)
        {
            return await HttpClient.GetFromJsonAsync<QuizDto>($"api/quiz/{id}");
        }

        public async Task<CourseDto> GetCourseById(int id)
        {
            return await HttpClient.GetFromJsonAsync<CourseDto>($"api/course/{id}");
        }

        public async Task<ChapterDto> GetChapterById(int id)
        {
            return await HttpClient.GetFromJsonAsync<ChapterDto>($"api/chapter/{id}");
        }

        public async Task<CourseDto> PutCourse(CourseDto courseDto)
        {
            var response = await HttpClient.PutAsJsonAsync("api/course", courseDto);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.DeserializeResponseContent<CourseDto>();
            }
            else return null;
        }

        public async Task<CourseStatusDto> AddOrUpdateCourseStatus(CourseStatusTransfer courseStatusTransfer)
        {
            var response = await HttpClient.PutAsJsonAsync("api/coursestatus", courseStatusTransfer);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.DeserializeResponseContent<CourseStatusDto>();
            }
            else return null;
        }

        public async Task<ChapterStatusDto> AddOrUpdateChapterStatus(chapterStatusTransfer chapterStatusTransfer)
        {
            var response = await HttpClient.PutAsJsonAsync("api/chapterstatus", chapterStatusTransfer);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.DeserializeResponseContent<ChapterStatusDto>();
            }
            else return null;
        }

        public async Task<CourseDto> GetCourseByName(string name)
        {
            return await HttpClient.GetFromJsonAsync<CourseDto>($"api/course/byname?name={name}");
        }

        public async Task<ChapterStatusDto> GetLastChapterWorkedOn(int userId)
        {
            var response = await HttpClient.GetFromJsonAsync<ChapterStatusDto>($"api/chapterstatus/getlast/{userId}");
            return response;
        }

        public async Task<ChapterDto[]> GetAllChapters()
        {
            var response = await HttpClient.GetFromJsonAsync<ChapterDto[]>($"api/chapter");
            return response;
        }

        public async Task<CourseDto> GetCourseByChapterId(int chapterId)
        {
            return await HttpClient.GetFromJsonAsync<CourseDto>($"api/course/bychapterid?chapterId={chapterId}");
        }

        public async Task<CourseStatusDto> GetLastCourseStatusWorkedOn(int userId)
        {
            var response = await HttpClient.GetFromJsonAsync<CourseStatusDto>($"api/coursestatus/getlast/{userId}");
            return response;
        }

        public async Task<QuizStatusDto> AddOrUpdateQuizStatus(QuizStatusTransfer quizStatusTransfer)
        {
            var response = await HttpClient.PutAsJsonAsync("api/quizstatus", quizStatusTransfer);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.DeserializeResponseContent<QuizStatusDto>();
            }
            else return null;
        }

        public async Task<ChapterDto> GetChapterByQuizId(int quizId)
        {
            return await HttpClient.GetFromJsonAsync<ChapterDto>($"api/chapter/byquiz/{quizId}");
        }

        public async Task<StudentDto> AddStudent(StudentDto studentDto)
        {
            var response = await HttpClient.PutAsJsonAsync("api/users/student", studentDto);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.DeserializeResponseContent<StudentDto>();
            }
            else return null;
        }

        public async Task<InstructorDto> AddInstrcutor(InstructorDto instructorDto)
        {
            var response = await HttpClient.PutAsJsonAsync("api/users/instructor", instructorDto);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.DeserializeResponseContent<InstructorDto>();
            }
            else return null;
        }

        public async Task<UserDto[]> GetAllUsers()
        {
            return await HttpClient.GetFromJsonAsync<UserDto[]>($"api/users/allusers");
        }
    }
}