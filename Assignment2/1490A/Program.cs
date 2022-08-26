int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase-- > 0)
{
    int length = Convert.ToInt32(Console.ReadLine());
    int[] ints = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

    int requiredQuantity = 0;
    for (int i = 1; i < length;)
    {
        int max = Math.Max(ints[i], ints[i - 1]);
        int min = Math.Min(ints[i], ints[i - 1]);
        if (max / (double)min > 2.0)
        {
            if (ints[i] > ints[i - 1]) ints[i - 1] = min * 2;
            if (ints[i] < ints[i - 1]) ints[i - 1] = (int) Math.Ceiling(max / 2.0);
            requiredQuantity++;
        }
        else i++;
    }
    Console.WriteLine(requiredQuantity);
}

// problem-link -> https://codeforces.com/problemset/problem/1490/A
// time-complexity -> O(n)
