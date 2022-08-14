int[] charIndex = Console.ReadLine()!.Select(c => c - 97).ToArray();
int current = 0;
int movesCount = 0;

foreach (int index in charIndex)
{
    int distanceClockWise = Math.Abs(index - current);
    if (distanceClockWise <= 13) movesCount += distanceClockWise;
    else
    {
        if(index > current) movesCount += (26 - index) + current;
        else movesCount += (26 - current) + index;
    }
    current = index;
}
Console.WriteLine(movesCount);

// problem-link -> https://codeforces.com/problemset/problem/731/A
// time-complexity -> O(n)