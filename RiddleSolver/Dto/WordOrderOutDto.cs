using Newtonsoft.Json;

namespace RiddleSolver.Dto
{
    public class WordOrderOutDto
    {
        [JsonProperty("blocks")]
        public string[] Blocks { get; set; }
    }
}
