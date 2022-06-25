namespace RiddleSolver.Interfaces
{
    public interface IRiddleSolverService
    {
        public Task<string[]> Check(string[] puzzleWords, string token);
    }
}
