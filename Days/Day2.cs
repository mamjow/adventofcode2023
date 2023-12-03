using System.Collections;
using System.Text.RegularExpressions;
using app;
namespace Days;

public class Day2 : ISolve
{
        Bag Bag = new Bag(12, 13, 14);
        public string SolvePartOne(string[] input)
        {
                var totalValidGames = 0;
                for (int i = 0; i < input.Length; i++)
                {
                        totalValidGames += Bag.GameWasValid(input[i]);
                }
                return totalValidGames.ToString();
        }

        public string SolvePartTwo(string[] input)
        {
                var gamesPowers = 0;
                for (int i = 0; i < input.Length; i++)
                {
                        gamesPowers += Bag.GetGamePower(input[i]);
                }
                return gamesPowers.ToString();
        }
}

public class Bag
{
        public Dictionary<string, int> Container { get; set; }

        public Bag(int red, int green, int blue)
        {
                this.Container = new Dictionary<string, int>
                {
                    { "red", red },
                    { "green", green },
                    { "blue", blue }
                };
        }


        public int GameWasValid(string gameAndPlayes)
        {
                var gameIdentifier = Regex.Matches(gameAndPlayes, "Game (\\d+):");
                _ = int.TryParse(gameIdentifier[0].Groups[1].Value, out int gameIdentifierNormalized);
                var rounds = gameAndPlayes.Split(':')[1].Split(';');
                foreach (var round in rounds)
                {

                        if (IsValidCubes(round))
                        {
                                continue;
                        }
                        else return 0;
                }
                return gameIdentifierNormalized;
        }

        public int GetGamePower(string game)
        {
                var rounds = game.Split(':')[1].Split(';');
                var leastInContainerToWin = new Dictionary<string, int>();
                foreach (var round in rounds)
                {
                        var cubes = Regex.Matches(game, "(\\d+) (\\w+)");
                        foreach (var cube in cubes)
                        {
                                _ = int.TryParse(((Match)cube).Groups[1].Value, out int number);
                                var color = ((Match)cube).Groups[2].Value;
                                var foundFirst = leastInContainerToWin.TryGetValue(color, out int latestRecord);
                                if (foundFirst)
                                {
                                        leastInContainerToWin[color] = latestRecord <= number ? number : latestRecord;
                                }
                                else
                                {
                                        leastInContainerToWin.Add(color, number);
                                }
                        }

                }
                var gamepower = leastInContainerToWin.Values.Aggregate((a, x) => a * x);
                return gamepower;
        }
        private bool IsValidCubes(string game)
        {
                var cubes = Regex.Matches(game, "(\\d+) (\\w+)");
                foreach (var cube in cubes)
                {
                        _ = int.TryParse(((Match)cube).Groups[1].Value, out int number);
                        var color = ((Match)cube).Groups[2].Value;
                        Container.TryGetValue(color, out int totalPossible);
                        if (totalPossible < number)
                        {
                                return false;
                        }
                }
                return true;
        }
}
