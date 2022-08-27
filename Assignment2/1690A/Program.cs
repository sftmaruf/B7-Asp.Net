int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase-- > 0)
{
    int totalBlocks = Convert.ToInt32(Console.ReadLine());
    int[] heights = new int[3];
    heights[0] = heights[1] = heights[2] = totalBlocks / 3;

    if (heights[0] * 3 != totalBlocks)
    {
        if (totalBlocks - (heights[0] * 3) == 2) heights[0]++;
        heights[1] += 2;
        heights[2]--;
    }
    else
    {
        heights[1]++;
        heights[2]--;
    }

    Console.WriteLine(string.Join(" ", heights));
}

// problem-link -> https://codeforces.com/problemset/problem/1690/A
// time-complexity -> O(1)