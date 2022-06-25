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

            await FindSolution(puzzleWords, token);

            return puzzleWords;
        }

        private async Task FindSolution(string[] puzzleWords, string token)
        {
            int firstWordIndex = 0;
            //Since the first word is in correct order, start looking for the consecutive to that one
            for (int consecutiveWordIndex = 1; consecutiveWordIndex < puzzleWords.Length; consecutiveWordIndex++)
            {
                var word = puzzleWords[firstWordIndex];
                var consecutiveWord = puzzleWords[consecutiveWordIndex];

                //The last two words should be in order already, no need to verify, for the rest we have to find which is the consecutive word
                if (firstWordIndex + 2 < puzzleWords.Length)
                {
                    bool areConsecutive = (await _puzzleWordsService.AreTwoWordsConsecutive(word, consecutiveWord, token)).IsCorrect;

                    if (areConsecutive)
                    {
                        ++firstWordIndex;

                        //If it is bigger than 0 it means that we have to move the consecutive word to it's correct position
                        //If the operation is equal to 0 it means they are already in order
                        if (consecutiveWordIndex - firstWordIndex > 0)
                        {
                            int correctWordPosition = firstWordIndex;
                            string wrongOrderWord = puzzleWords[correctWordPosition];
                            puzzleWords[correctWordPosition] = consecutiveWord;
                            puzzleWords[consecutiveWordIndex] = wrongOrderWord;
                        }

                        //We move the index to start looking the consecutive word for the consecutive word we just found
                        consecutiveWordIndex = firstWordIndex;
                    }
                }
            }
        }
    }
}