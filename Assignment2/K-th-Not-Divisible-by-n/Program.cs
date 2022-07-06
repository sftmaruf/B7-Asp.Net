int testCase = Convert.ToInt32(Console.ReadLine());

while (testCase != 0)
{
    var values = Console.ReadLine()!.Split().Select(long.Parse).ToArray();
    long divisor = values[0];
    long position = values[1];
    Find(divisor, position);
    testCase--;
}

void Find(long divisor, long position)
{
    long left = 0, right = position * divisor;
    while (right >= left)
    {
        long mid = (left + right) / 2;
        long positionBasedOnMid = (long) Math.Ceiling(mid - (mid/(double)divisor));

        if (positionBasedOnMid == position && mid % divisor != 0)
        {
            Console.WriteLine(mid);
            break;
        }
        else if (positionBasedOnMid < position)
        {
            left = mid + 1;
        }
        else
        {
            right = mid - 1;
        }
    }
}

// problem-link -> https://codeforces.com/problemset/problem/1352/C
// time-complexity -> O(log n)