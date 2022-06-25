using Newtonsoft.Json;

namespace RiddleSolver.Dto
{
    public class WordOrderResponseDto
    {
        [JsonProperty("message")]
        public bool IsCorrect { get; set; }
    }
}
