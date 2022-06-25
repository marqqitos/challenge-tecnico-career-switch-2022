using Newtonsoft.Json;

namespace RiddleSolver.Dto
{
    public class PuzzleSolutionOutDto
    {
        [JsonProperty("encoded")]

        public string Solution { get; set; }
    }
}
