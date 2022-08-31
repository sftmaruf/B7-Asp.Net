int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase-- > 0)
{
    int result;
    Console.ReadLine();
    int[] cellAPosition = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    int[] cellBPosition = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    int[] cellCPosition = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

    int[] cols = { cellAPosition[0], cellBPosition[0], cellCPosition[0] };
    int[] rows = { cellAPosition[1], cellBPosition[1], cellCPosition[1] };

    Array.Sort(cols);
    Array.Sort(rows);

    int colCount = cols.Distinct().Count();
    int rowCount = rows.Distinct().Count();

    if (colCount == 1)
    {
        result = Math.Abs(cellAPosition[1] - cellBPosition[1]);
        if (rows[1] == cellCPosition[1]) result += 2;
    }
    else if (rowCount == 1)
    {
        result = Math.Abs(cellAPosition[0] - cellBPosition[0]);
        if (cols[1] == cellCPosition[0]) result += 2;
    }
    else
    {
        result = Math.Abs(cellAPosition[0] - cellBPosition[0])
            + Math.Abs(cellAPosition[1] - cellBPosition[1]);
    }

    Console.WriteLine(result);
}

// problem-link -> https://codeforces.com/problemset/problem/1547/A
// time-complexity -> O(1)
