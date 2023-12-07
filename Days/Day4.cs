
using System.Text.RegularExpressions;
using App;
namespace Days;

public class Day4 : ISolve
{
    public string SolvePartOne(string[] input)
    {
        double points = 0;
        foreach (var data in input)
        {
            var cards = Regex.Match(data, "^Card\\s+\\d+: (.+)$");
            // var winingCards = card.
            var allCards = cards.Groups[1].Value.Split("|").Select(
                x => x.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)
            )).ToList();

            var countOfWiningCards = allCards[1].Count(x => allCards[0].Contains(x));
            double point = countOfWiningCards <= 1 ? countOfWiningCards : Math.Pow(2, (countOfWiningCards - 1));
            points += point;
        }

        return points.ToString();
    }

    public string SolvePartTwo(string[] input)
    {
        double points = 0;
        var totalCopies = new List<int>();
        for (int i = 0; i < input.Length; i++)
        {
            var cards = Regex.Match(input[i], "^Card\\s+\\d+: (.+)$");
            var CardDeck = i + 1;
            // var winingCards = card.
            var allCards = cards.Groups[1].Value.Split("|").Select(
                x => x.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)
            )).ToList();

            var countOfWiningCards = allCards[1].Count(x => allCards[0].Contains(x));

            var instancesOfCard = totalCopies.Count(x => x == CardDeck) + 1;

            for (int nextDeck = 1; nextDeck <= countOfWiningCards; nextDeck++)
            {
                var nd = CardDeck + nextDeck;
                totalCopies.AddRange(Enumerable.Repeat(nd, instancesOfCard));
            }
        }
        return (input.Length + totalCopies.Count()).ToString();
    }
}