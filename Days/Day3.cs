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
            var partNumbers = Regex.Matches(lineSchema, "\\d+");

            var partNumbersDistinct = partNumbers.Select(x => x.Value).Distinct();
            foreach (var partNumber in partNumbersDistinct)
            {
                // if a number been seen more than 1 time?
                var allXLocations = Regex.Matches(lineSchema, @$"\d+")
                    .Where(x => x.Value.ToString() == partNumber)
                    .Select(x => x.Index);

                foreach (var xloc in allXLocations)
                {
                    if (ScanTopAndBotBorderForSymbol(input, partNumber, xloc, yLoc) || ScanSideBordersForSymbol(input, partNumber, xloc, yLoc))
                    {
                        list.Add(partNumber);
                    }
                }
            }
        }

        return list.Select(x =>
        {
            _ = long.TryParse(x, out var dig);
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
    private static bool ScanTopAndBotBorderForSymbol(string[] schema, string partNumber, int xLoc, int yLoc)
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
        var res = Regex.Match(botRow, "[^.\\d\n]").Success || Regex.Match(topRow, "[^.\\d\n]").Success;
        // if(res){
        //     var s = Regex.Match(botRow, "[^.0-9]").Success ? botRow : "" ;
        //     var s1 = Regex.Match(topRow, "[^.0-9]").Success ? topRow : "" ;
        //     Console.WriteLine($"{yLoc} - {partNumber} , {s1}  ANDBOT   {s}");
        // }
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
    private static bool ScanSideBordersForSymbol(string[] schema, string partNumber, int xLoc, int yLoc)
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
        if (closeIndex >= schema[yLoc].Length)
        {
            closeIndex -= 1;

        }
        var with1Margin = schema[yLoc][initialXloc..closeIndex];
        var res = Regex.Match(with1Margin, "[^.\\d\n]").Success;
        return res;
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
