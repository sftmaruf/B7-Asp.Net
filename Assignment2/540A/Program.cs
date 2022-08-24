int numberOfDisks = Convert.ToInt32(Console.ReadLine());
int[] diskPositions = Console.ReadLine()!.Select(x => (int) char.GetNumericValue(x)).ToArray();
int[] secretPositions = Console.ReadLine()!.Select(x => (int)char.GetNumericValue(x)).ToArray();

int totalMoves = 0;
for (int i = 0; i < numberOfDisks; i++)
{
    int dis = Math.Abs(diskPositions[i] - secretPositions[i]);
    if(dis > 5)
    {
        dis = 9 - Math.Max(diskPositions[i], secretPositions[i]);
        dis += Math.Min(diskPositions[i], secretPositions[i]);
        dis++;
    }
    totalMoves += dis;
}
Console.WriteLine(totalMoves);

// problem-link -> https://codeforces.com/problemset/problem/540/A
// time-complexity -> O(n)