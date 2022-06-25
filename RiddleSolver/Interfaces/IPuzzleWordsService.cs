using RiddleSolver.Dto;

namespace RiddleSolver.Interfaces
{
    public interface IPuzzleWordsService
    {
        public Task<PuzzleWordsDto> GetPuzzleWords(string token);
        public Task<WordOrderResponseDto> AreTwoWordsConsecutive(string firstWord, string secondWord, string token);
        public Task<WordOrderResponseDto> CheckPuzzleSolution(string[] puzzleSolution, string token);
    }
}