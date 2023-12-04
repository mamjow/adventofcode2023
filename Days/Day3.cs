using System.Text.RegularExpressions;
using App;
namespace Days;

public class Day3 : ISolve
{
    public string SolvePartOne(string[] input)
    {
        var list = new List<string>();
        for (int yLoc = 0; yLoc < input.Length; yLoc++)
        {
            var lineSchema = input[yLoc];
            var partNumbers = Regex.Matches(lineSchema, "[0-9]+");

            var partNumbersDistinct = partNumbers.Select(x => x.Value).Distinct();
            foreach (var partNumber in partNumbersDistinct)
            {
                // if a number been seen more than 1 time?
                var allXLoccation = lineSchema.AllIndexesOf(partNumber.ToString());

                foreach (var xloc in allXLoccation)
                {
                    if (ScanTopAndBotBorderForPart(input, partNumber.ToString(), xloc, yLoc) || ScanSideBordersForPart(input, partNumber.ToString(), xloc, yLoc))
                    {
                        list.Add(partNumber.ToString());
                    }
                }
            }
        }

        return list.Select(x =>
        {
            _ = int.TryParse(x, out int dig);
            return dig;
        }).Sum().ToString();
    }

    public string SolvePartTwo(string[] input)
    {
        return "";
    }

    /// <summary>
    /// chechs bot and top border with margin od diagonal ones from mid lane 
    /// 
    /// checking questions marks
    /// ??????? 
    ///  xxxxx
    /// ???????
    /// 
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="partNumber"></param>
    /// <param name="xLoc"></param>
    /// <param name="yLoc"></param>
    /// <returns></returns>
    private static bool ScanTopAndBotBorderForPart(string[] schema, string partNumber, int xLoc, int yLoc)
    {
        var initialXloc = xLoc - 1;
        var lentghOfIteration = partNumber.Length + 2;
        var closeIndex = initialXloc + lentghOfIteration;

        // what if it was close to vertical edges? out of range index
        // left side
        if (initialXloc < 0)
        {
            initialXloc = 0;
        }

        // right side
        if (closeIndex > schema[yLoc].Length)
        {
            closeIndex -= 1;
        }

        var botRow = "";
        var topRow = "";
        // so its not last row
        if (schema.Length > yLoc + 1)
        {
            botRow = schema[yLoc + 1][initialXloc..closeIndex];
        }
        // its not firstio row
        if (yLoc != 0)
        {
            topRow = schema[yLoc - 1][initialXloc..closeIndex];
        }
        var res = Regex.Match(botRow, "[^.0-9\n]").Success || Regex.Match(topRow, "[^.0-9\n]").Success;
        return res;
    }

    /// <summary>
    ///  so sidways , the diagonal will be check in v
    ///  
    ///  xxxxx
    ///  ?<n>?
    ///  xxxxx
    ///  
    /// </summary>
    /// <returns></returns>
    private static bool ScanSideBordersForPart(string[] schema, string partNumber, int xLoc, int yLoc)
    {
        var initialXloc = xLoc - 1;
        var lentghOfIteration = partNumber.Length + 2;
        var closeIndex = initialXloc + lentghOfIteration;
        // what if it was close to vertical edges? out of index
        // left side
        if (initialXloc <= 0)
        {
            initialXloc = 0;
        }

        // right side
        if (closeIndex > schema[yLoc].Length)
        {
            closeIndex -= 1;

        }
        var with1Margin = schema[yLoc][initialXloc..closeIndex];
        return Regex.Match(with1Margin, "[^.0-9\n]").Success;
    }

}

public static class IndexHelper
{
    public static IEnumerable<int> AllIndexesOf(this string str, string searchstring)
    {
        int minIndex = str.IndexOf(searchstring);
        while (minIndex != -1)
        {
            yield return minIndex;
            minIndex = str.IndexOf(searchstring, minIndex + searchstring.Length);
        }
    }
}
