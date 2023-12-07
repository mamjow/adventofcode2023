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
                var allXLocations = partNumbers
                    .Where(x => x.Value.ToString() == partNumber)
                    .Select(x => x.Index);

                foreach (var xloc in allXLocations)
                {
                    var listToScan = GetAllAdjacentItems(input, partNumber, xloc, yLoc);
                    var h = string.Join(string.Empty, listToScan);
                    if (Regex.Match(h, "[^.\\d]").Success)
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
        var list = new List<(int, int)>();
        for (int yLoc = 0; yLoc < input.Length; yLoc++)
        {
            var lineSchema = input[yLoc];
            var symbols = Regex.Matches(lineSchema, "[*]");

            foreach (var symbol in symbols.Cast<Match>())
            {
                // if a number been seen more than 1 time?
                var power = ReadAdjacentNumber(input, symbol.Index, yLoc);
                list.Add(power);
            }
        }
        return list.Select(x => x.Item2 * x.Item1).Sum().ToString();
    }

    /// <summary>
    /// it wil find all numberr adjacent  to a * symbol
    /// xxxxxxxxxxxx
    /// xxxxxx*xxxxx
    /// xxxxxxxxxxxx
    /// </summary>
    /// <param name="input"></param>
    /// <param name="xloc"></param>
    /// <param name="yLoc"></param>
    /// <param name="totalMathces"></param>
    /// <returns></returns>
    private static (int, int) ReadAdjacentNumber(string[] input, int xloc, int yLoc, int totalMathces = 2)
    {

        // read horizontal numbers
        // forward
        var rightNumber = "";
        var pointer = xloc + 1;

        var list = new List<string>();
        if (pointer < input[yLoc].Length)
        {
            var numberMatch = Regex.Match(input[yLoc][xloc + 1].ToString(), "^[\\d]+$");
            while (numberMatch.Success && pointer < input[yLoc].Length)
            {
                rightNumber = numberMatch.Value;
                pointer++;
                var extendedString = input[yLoc][(xloc + 1)..pointer];
                numberMatch = Regex.Match(extendedString, "^[\\d]+$");
            }
            rightNumber = numberMatch.Success ? numberMatch.Value : rightNumber; ;
            if (!string.IsNullOrWhiteSpace(rightNumber))
            {
                list.Add(rightNumber);
            }
        }


        var leftNumber = "";
        pointer = xloc - 1;
        if (pointer >= 0)
        {
            var numberMatch = Regex.Match(input[yLoc][pointer..xloc], "^[\\d]+$");
            while (numberMatch.Success && pointer > 0)
            {
                leftNumber = numberMatch.Value;
                pointer--;
                var extendedString = input[yLoc][pointer..xloc];
                numberMatch = Regex.Match(extendedString, "^[\\d]+$");
            }
            leftNumber = numberMatch.Success ? numberMatch.Value : leftNumber; ;
            if (!string.IsNullOrWhiteSpace(leftNumber))
            {
                list.Add(leftNumber);
            }
        }

        // bot 
        var botRowPointer = yLoc + 1;
        if (botRowPointer < input.Length)
        {
            var numberMatch = Regex.Match(input[botRowPointer][xloc].ToString(), "^[\\d]+$");
            //  ?x?
            //  ???
            // center is not digit so there might be 2 number neighbering from bot
            if (!numberMatch.Success)
            {
                var rightside = "";
                var leftside = "";
                //forward
                var forwardPointer = xloc + 1;

                if (forwardPointer < input[botRowPointer].Length)
                {
                    /// in mushkel dare
                    var startIndex = forwardPointer;
                    numberMatch = Regex.Match(input[botRowPointer][startIndex].ToString(), "^[\\d]+$");
                    while (numberMatch.Success && forwardPointer + 1 < input[yLoc].Length)
                    {
                        rightside = numberMatch.Value;
                        forwardPointer++;
                        var extendedString = input[botRowPointer][startIndex..(forwardPointer + 1)];
                        numberMatch = Regex.Match(extendedString, "^[\\d]+$");
                    }
                    rightside = numberMatch.Success ? numberMatch.Value : rightside;
                }

                //backward
                var backwardPointer = xloc - 1;

                if (backwardPointer >= 0)
                {
                    numberMatch = Regex.Match(input[botRowPointer][backwardPointer].ToString(), "^[\\d]+$");
                    while (numberMatch.Success && backwardPointer > 0)
                    {
                        leftside = numberMatch.Value;
                        backwardPointer--;
                        var extendedString = input[botRowPointer][backwardPointer..xloc];
                        numberMatch = Regex.Match(extendedString, "^[\\d]+$");
                    }
                    leftside = numberMatch.Success ? numberMatch.Value : leftside;
                }

                if (!string.IsNullOrWhiteSpace(rightside))
                {
                    list.Add(rightside);
                }
                if (!string.IsNullOrWhiteSpace(leftside))
                {
                    list.Add(leftside);
                }
            }
            else
            {
                // center is a digit so there might relative to diagonal ones
                var rightHalf = input[yLoc + 1][xloc].ToString(); ;
                var leftHalf = "";
                //forward
                var forwardPointer = xloc;
                if (forwardPointer < input[yLoc].Length)
                {
                    numberMatch = Regex.Match(input[yLoc + 1][forwardPointer].ToString(), "^[\\d]+$");
                    while (numberMatch.Success && forwardPointer + 1 < input[yLoc].Length)
                    {
                        rightHalf = numberMatch.Value;
                        forwardPointer++;
                        var extendedString = input[yLoc + 1][xloc..(forwardPointer + 1)];
                        numberMatch = Regex.Match(extendedString, "^[\\d]+$");
                    }
                    rightHalf = numberMatch.Success ? numberMatch.Value : rightHalf;
                }
                //backward
                var backwPointer = xloc - 1;
                if (backwPointer >= 0)
                {
                    numberMatch = Regex.Match(input[yLoc + 1][backwPointer].ToString(), "^[\\d]+$");
                    while (numberMatch.Success && backwPointer > 0)
                    {
                        leftHalf = numberMatch.Value;
                        backwPointer--;
                        var extendedString = input[yLoc + 1][backwPointer..xloc];
                        numberMatch = Regex.Match(extendedString, "^[\\d]+$");
                    }
                    leftHalf = numberMatch.Success ? numberMatch.Value : leftHalf;
                }
                if (!string.IsNullOrWhiteSpace($"{leftHalf}{rightHalf}"))
                {
                    list.Add($"{leftHalf}{rightHalf}");
                }
            }
        }

        var topRowPointer = yLoc - 1;
        if (topRowPointer >= 0)
        {
            var numberMatch = Regex.Match(input[topRowPointer][xloc].ToString(), "^[\\d]+$");
            //  ???
            //  ?x?
            // center is not digit so there might be 2 number neighbering from bot
            if (!numberMatch.Success)
            {
                var rightside = "";
                var leftside = "";
                //forward
                var forwardPointer = xloc + 1;

                if (forwardPointer < input[topRowPointer].Length)
                {
                    var startIndex = forwardPointer;
                    numberMatch = Regex.Match(input[topRowPointer][startIndex].ToString(), "^[\\d]+$");
                    while (numberMatch.Success && forwardPointer + 1 < input[yLoc].Length)
                    {
                        rightside = numberMatch.Value;
                        forwardPointer++;
                        var extendedString = input[topRowPointer][startIndex..(forwardPointer + 1)];
                        numberMatch = Regex.Match(extendedString, "^[\\d]+$");
                    }
                    rightside = numberMatch.Success ? numberMatch.Value : rightside;
                }

                //backward
                var backwardPointer = xloc - 1;

                if (backwardPointer >= 0)
                {
                    numberMatch = Regex.Match(input[topRowPointer][backwardPointer].ToString(), "^[\\d]+$");
                    while (numberMatch.Success && backwardPointer > 0)
                    {
                        leftside = numberMatch.Value;
                        backwardPointer--;
                        var extendedString = input[topRowPointer][backwardPointer..xloc];
                        numberMatch = Regex.Match(extendedString, "^[\\d]+$");
                    }
                    leftside = numberMatch.Success ? numberMatch.Value : leftside;
                }

                if (!string.IsNullOrWhiteSpace(rightside))
                {
                    list.Add(rightside);
                }
                if (!string.IsNullOrWhiteSpace(leftside))
                {
                    list.Add(leftside);
                }
            }
            else
            {
                // center is a digit so there might relative to diagonal ones
                var rightHalf = input[topRowPointer][xloc].ToString(); ;
                var leftHalf = "";
                //forward
                var forwardPointer = xloc;
                if (forwardPointer < input[topRowPointer].Length)
                {
                    numberMatch = Regex.Match(input[topRowPointer][forwardPointer].ToString(), "^[\\d]+$");
                    while (numberMatch.Success && forwardPointer + 1 < input[yLoc].Length)
                    {
                        rightHalf = numberMatch.Value;
                        forwardPointer++;
                        var extendedString = input[topRowPointer][xloc..(forwardPointer + 1)];
                        numberMatch = Regex.Match(extendedString, "^[\\d]+$");
                    }
                    rightHalf = numberMatch.Success ? numberMatch.Value : rightHalf;
                }
                //backward
                var backwPointer = xloc - 1;
                if (backwPointer >= 0)
                {
                    numberMatch = Regex.Match(input[topRowPointer][backwPointer].ToString(), "^[\\d]+$");
                    while (numberMatch.Success && backwPointer > 0)
                    {
                        leftHalf = numberMatch.Value;
                        backwPointer--;
                        var extendedString = input[topRowPointer][backwPointer..xloc];
                        numberMatch = Regex.Match(extendedString, "^[\\d]+$");
                    }
                }
                if (!string.IsNullOrWhiteSpace($"{leftHalf}{rightHalf}"))
                {
                    list.Add($"{leftHalf}{rightHalf}");
                }
            }
        }
        if (list.Count == totalMathces)
        {
            _ = int.TryParse(list[0], out int numbA);
            _ = int.TryParse(list[1], out int numbB);
            return (numbA, numbB);
        }
        return (0, 0);
    }

    private static List<string> GetAllAdjacentItems(string[] schema, string coreString, int xloc, int yLoc)
    {
        var initialXloc = xloc - 1;
        var lentghOfIteration = coreString.Length + 2;
        var closeIndex = initialXloc + lentghOfIteration;
        // what if it was close to vertical edges? out of index
        // left side
        if (initialXloc <= 0)
        {
            initialXloc = 0;
        }

        // right side
        var with1Margin = "";
        if (closeIndex >= schema[yLoc].Length)
        {
            with1Margin = schema[yLoc][initialXloc..];
            closeIndex -= 1;

        }
        else
        {
            with1Margin = schema[yLoc][initialXloc..closeIndex];
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


        var lists = with1Margin.ToString().Split(new string[] { coreString }, StringSplitOptions.None).ToList();
        lists.Add(topRow);
        lists.Add(botRow);
        return lists;
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
    private static int ScanTopAndBotBorderForPattern(string[] schema, string partNumber, int xLoc, int yLoc, string pattern)
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
        // var res = Regex.Match(botRow, "[^.\\d\n]").Success || Regex.Match(topRow, "[^.\\d\n]").Success;
        // collect it with its intial coordinate

        // var topMathces =  Regex.Matches(topRow, pattern)
        //             .Where(x => x.Value.ToString() == partNumber)
        //             .Select(x => x.Index);

        var count = Regex.Matches(botRow, pattern).Count + Regex.Matches(topRow, pattern).Count;
        return count;
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
    private static int ScanSideBordersForPattern(string[] schema, string partNumber, int xLoc, int yLoc, string pattern)
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
        var count = Regex.Matches(with1Margin, pattern).Count;
        return count;
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
