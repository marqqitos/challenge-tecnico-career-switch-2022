using RiddleSolver.Interfaces;

namespace RiddleSolver.Implementations
{
    public class RiddleSolverService : IRiddleSolverService
    {
        private IPuzzleWordsService _puzzleWordsService;

        public RiddleSolverService(IPuzzleWordsService puzzleWordsService)
        {
            _puzzleWordsService = puzzleWordsService;
        }

        public async Task<string[]> Check(string[] puzzleWords, string token)
        {
            if (puzzleWords is null || puzzleWords.Length is 0)
            {
                return new string[] { };
            }

            int firstWordIndex = 0;
            for (int consecutiveWordIndex = 1; consecutiveWordIndex < puzzleWords.Length; consecutiveWordIndex++)
            {
                var firstWord = puzzleWords[firstWordIndex];
                var consecutiveWord = puzzleWords[consecutiveWordIndex];

                bool areConsecutive = (await _puzzleWordsService.AreTwoWordsConsecutive(firstWord, consecutiveWord, token)).IsCorrect;

                if (areConsecutive && firstWordIndex + 1 < puzzleWords.Length)
                {
                    int correctWordPosition = ++firstWordIndex;
                    string wrongOrderWord = puzzleWords[correctWordPosition];
                    puzzleWords[correctWordPosition] = consecutiveWord;
                    puzzleWords[consecutiveWordIndex] = wrongOrderWord;
                    consecutiveWordIndex = firstWordIndex;
                }
            }

            return puzzleWords;
        }
    }
}