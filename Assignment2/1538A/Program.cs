int NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (NumberOfTestCase-- > 0)
{
    int numberOfStones = Convert.ToInt32(Console.ReadLine());
    List<int> powers = Console.ReadLine()!.Split().Select(int.Parse).ToList();
    int minMoves = numberOfStones;

    int minPos = powers.ToList().IndexOf(powers.Min());
    int maxPos = powers.ToList().IndexOf(powers.Max());

    int countFromStart = minPos > maxPos ? minPos + 1 : maxPos + 1;
    int countFromEnd = numberOfStones - minPos > numberOfStones - maxPos
        ? numberOfStones - minPos 
        : numberOfStones - maxPos;
    int countFromStartAndEnd = (minPos + 1) + (numberOfStones - maxPos) 
        < (maxPos + 1) + (numberOfStones - minPos)
        ? (minPos + 1) + (numberOfStones - maxPos)
        : (maxPos + 1) + (numberOfStones - minPos);

    if (countFromStart < minMoves) minMoves = countFromStart;
    if (countFromEnd < minMoves) minMoves = countFromEnd;
    if (countFromStartAndEnd < minMoves) minMoves = countFromStartAndEnd;
    Console.WriteLine(minMoves);
}

// problem-link -> https://codeforces.com/problemset/problem/1538/A
// time-complexity -> O(n)