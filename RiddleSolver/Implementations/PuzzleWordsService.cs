using Newtonsoft.Json;
using RiddleSolver.Dto;
using RiddleSolver.Interfaces;
using System.Text;

namespace RiddleSolver.Implementations
{
    public class PuzzleWordsService : IPuzzleWordsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public PuzzleWordsService(IHttpClientFactory httpClientFactory, string httpClientName)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient(httpClientName);
        }


        public async Task<PuzzleWordsDto> GetPuzzleWords(string token)
        {
            var result = await _httpClient.GetAsync($"blocks?token={token}");
            var content = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PuzzleWordsDto>(content) ?? new PuzzleWordsDto();
        }

        public async Task<WordOrderResponseDto> AreTwoWordsConsecutive(string firstWord, string secondWord, string token)
        {
            var dto = new WordOrderOutDto();
            dto.Blocks = new string[] { firstWord, secondWord };

            var json = JsonConvert.SerializeObject(dto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"check?token={token}", data);
            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<WordOrderResponseDto>(result) ?? new WordOrderResponseDto();
        }

        public async Task<WordOrderResponseDto> CheckPuzzleSolution(string[] puzzleSolution, string token)
        {
            var dto = new PuzzleSolutionOutDto();
            dto.Solution = string.Join("", puzzleSolution);

            var json = JsonConvert.SerializeObject(dto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"check?token={token}", data);
            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<WordOrderResponseDto>(result) ?? new WordOrderResponseDto();
        }
    }
}