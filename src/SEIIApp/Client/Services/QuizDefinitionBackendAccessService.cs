using SEIIApp.Shared.DomainTdo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SEIIApp.Client.Services {

    public class QuizDefinitionBackendAccessService {

        private HttpClient HttpClient { get; set; }
        public QuizDefinitionBackendAccessService(HttpClient client) {
            this.HttpClient = client;
        }

        private string GetQuizDefinitionUrl() {
            return "api/quizdefinitions";
        }

        private string GetQuizDefinitionUrlWithId(int id) {
            return $"{GetQuizDefinitionUrl()}/{id}";
        }

        /// <summary>
        /// Returns a certain quiz by id
        /// </summary>
        public async Task<QuizDefinitionDto> GetQuizById(int id) {
            return await HttpClient.GetFromJsonAsync<QuizDefinitionDto>(GetQuizDefinitionUrlWithId(id));
        }

        /// <summary>
        /// Returns all quizzes stored on the backend
        /// </summary>
        public async Task<QuizDefinitionBaseDto[]> GetQuizOverview() {
            return await HttpClient.GetFromJsonAsync<QuizDefinitionBaseDto[]>(GetQuizDefinitionUrl());
        }

        /// <summary>
        /// Adds or updates a quiz on the backend. Returns the quiz if successful else null
        /// </summary>
        public async Task<QuizDefinitionDto> AddOrUpdateQuiz(QuizDefinitionDto dto) {
            var response = await HttpClient.PutAsJsonAsync(GetQuizDefinitionUrl(), dto);
            if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                return await response.DeserializeResponseContent<QuizDefinitionDto>();
            }
            else return null;
        }

        /// <summary>
        /// Deletes a quiz and returns true if successful
        /// </summary>
        public async Task<bool> DeleteQuiz(int quizId) {
            var response = await HttpClient.DeleteAsync(GetQuizDefinitionUrlWithId(quizId));
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

    }
}
