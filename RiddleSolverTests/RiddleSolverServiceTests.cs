using FluentAssertions;
using Moq;
using RiddleSolver.Dto;
using RiddleSolver.Implementations;
using RiddleSolver.Interfaces;

namespace RiddleSolverTests
{
    public class Tests
    {
        private IRiddleSolverService _sut;
        private Mock<IPuzzleWordsService> _mockPuzzleWordsService;

        [SetUp]
        public void Setup()
        {
            _mockPuzzleWordsService = new Mock<IPuzzleWordsService>();
            _sut = new RiddleSolverService(_mockPuzzleWordsService.Object);
        }

        [Test]
        public async Task Chech_WhenPuzzleIsPassed_SolutionIsFound()
        {
            //Arrange
            var unorderedWords = new string[] { "abcd", "mnop", "qrst", "efgh", "uvwx", "ijkl" };
            var expectedResult = "abcdefghijklmnopqrstuvwx";
            var token = "token";

            var trueResponse = new WordOrderResponseDto() { IsCorrect = true };
            var falseResponse = new WordOrderResponseDto() { IsCorrect = false };

            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive(It.IsAny<string>(), It.IsAny<string>(), token)).ReturnsAsync(falseResponse);
            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive("abcd", "efgh", token)).ReturnsAsync(trueResponse);
            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive("efgh", "ijkl", token)).ReturnsAsync(trueResponse);
            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive("ijkl", "mnop", token)).ReturnsAsync(trueResponse);
            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive("mnop", "qrst", token)).ReturnsAsync(trueResponse);
            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive("qrst", "uvwx", token)).ReturnsAsync(trueResponse);

            //Act
            var result = await _sut.Check(unorderedWords, token);

            //Assert
            string.Join("", result).Should().Be(expectedResult);
        }

        [Test]
        public async Task Chech_WhenPuzzleIsPassedWhereAllButLastTwoElementAreInOrder_SolutionIsFound()
        {
            //Arrange
            var unorderedWords = new string[] { "abcd", "efgh", "ijkl", "mnop", "uvwx", "qrst" };
            var expectedResult = "abcdefghijklmnopqrstuvwx";
            var token = "token";

            var trueResponse = new WordOrderResponseDto() { IsCorrect = true };
            var falseResponse = new WordOrderResponseDto() { IsCorrect = false };

            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive(It.IsAny<string>(), It.IsAny<string>(), token)).ReturnsAsync(falseResponse);
            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive("abcd", "efgh", token)).ReturnsAsync(trueResponse);
            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive("efgh", "ijkl", token)).ReturnsAsync(trueResponse);
            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive("ijkl", "mnop", token)).ReturnsAsync(trueResponse);
            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive("mnop", "qrst", token)).ReturnsAsync(trueResponse);
            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive("qrst", "uvwx", token)).ReturnsAsync(trueResponse);

            //Act
            var result = await _sut.Check(unorderedWords, token);

            //Assert
            string.Join("", result).Should().Be(expectedResult);
        }

        [Test]
        public async Task Chech_WhenPuzzleIsPassedWhereAllElementAreInOrder_SolutionIsFound()
        {
            //Arrange
            var unorderedWords = new string[] { "abcd", "efgh", "ijkl", "mnop", "qrst", "uvwx" };
            var expectedResult = "abcdefghijklmnopqrstuvwx";
            var token = "token";

            var trueResponse = new WordOrderResponseDto() { IsCorrect = true };
            var falseResponse = new WordOrderResponseDto() { IsCorrect = false };

            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive(It.IsAny<string>(), It.IsAny<string>(), token)).ReturnsAsync(falseResponse);
            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive("abcd", "efgh", token)).ReturnsAsync(trueResponse);
            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive("efgh", "ijkl", token)).ReturnsAsync(trueResponse);
            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive("ijkl", "mnop", token)).ReturnsAsync(trueResponse);
            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive("mnop", "qrst", token)).ReturnsAsync(trueResponse);
            _mockPuzzleWordsService.Setup(s => s.AreTwoWordsConsecutive("qrst", "uvwx", token)).ReturnsAsync(trueResponse);

            //Act
            var result = await _sut.Check(unorderedWords, token);

            //Assert
            string.Join("", result).Should().Be(expectedResult);
        }
    }
}