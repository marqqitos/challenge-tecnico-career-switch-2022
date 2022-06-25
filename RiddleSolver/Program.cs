
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiddleSolver.Implementations;
using RiddleSolver.Interfaces;

namespace RiddleSolver
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //setup our DI
            var serviceCollection = new ServiceCollection();
            var _serviceProvider = ConfigureServiceProvider(serviceCollection);

            var puzzleWordsService = _serviceProvider.GetService<IPuzzleWordsService>();
            var riddleSolverService = _serviceProvider.GetService<IRiddleSolverService>();

            var token = GetConfiguration().GetSection("token").Value;

            Console.WriteLine($"Getting words");
            var wordsDto = await puzzleWordsService.GetPuzzleWords(token);

            Console.WriteLine($"Solving the puzzle");
            var solution = await riddleSolverService.Check(wordsDto.Data, token);

            var isCorrect = (await puzzleWordsService.CheckPuzzleSolution(solution, token)).IsCorrect;

            if(isCorrect)
            {
                Console.WriteLine($"The solution is {string.Join("", solution)}");
            }
            else
            {
                Console.WriteLine($"The solution was not found");
            }

        }

        private static ServiceProvider ConfigureServiceProvider(ServiceCollection services)
        {
            IConfigurationRoot config = GetConfiguration();

            var urlBaseAddress = config.GetSection("URLBaseAddress").Value;
            var puzzleServiceClientHttpClientName = config.GetSection("PuzzleWordsServiceHttpClient").Value;
            services.AddHttpClient(puzzleServiceClientHttpClientName, c => c.BaseAddress = new System.Uri(urlBaseAddress));

            services.AddScoped<IPuzzleWordsService, PuzzleWordsService>(
                s => new PuzzleWordsService
                (
                    s.GetService<IHttpClientFactory>(),
                    puzzleServiceClientHttpClientName
                )
            );

            services.AddScoped<IRiddleSolverService, RiddleSolverService>();

            return services.BuildServiceProvider();
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("config.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}