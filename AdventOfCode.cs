using App;

namespace AdventOfCode;
public class AdventOfCodeChallenge
{
    public static void SolveEventChallenge(ISolve solution)
    {
        var day = solution.GetType().Name;
        var gamesinput = System.IO.File.ReadAllLines($"./inputs/{day}.txt");
        Console.WriteLine($"Day: {day[3..]}");
        Console.WriteLine($"\tPart One: {solution.SolvePartOne(gamesinput)}");
        Console.WriteLine($"\tPart Two: {solution.SolvePartTwo(gamesinput)}");
    }
}