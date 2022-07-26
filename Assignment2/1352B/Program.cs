int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase > 0)
{
    int[] inputs = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    int sumOfK = inputs[0];
    int k = inputs[1];

    if (isNoCase(sumOfK, k))
    {
        Console.WriteLine("NO");
    }
    else if (k == sumOfK)
    {
        Console.WriteLine("YES");
        printValues(sumOfK, k, 1);
    }
    else if (!isOdd(sumOfK) && isOdd(k))
    {
        Console.WriteLine("YES");
        printValues(sumOfK, k, 2);
    }
    else if (!isOdd(sumOfK) && !isOdd(k) || isOdd(sumOfK) && isOdd(k))
    {
        Console.WriteLine("YES");
        printValues(sumOfK, k, 1);
    }
    numberOfTestCase--;
}

bool isNoCase(int sumOfK, int k) => k > sumOfK || !isOdd(sumOfK) && isOdd(k) && sumOfK / 2 < k || isOdd(sumOfK) && !isOdd(k);
bool isOdd(int value) => value % 2 != 0;
void printValues(int sumOfK, int k, int initialValue)
{
    IEnumerable<int> values = Enumerable.Repeat(initialValue, k - 1).Append(sumOfK - initialValue * (k - 1));
    Console.WriteLine(string.Join(' ', values));
}

// problem-link -> https://codeforces.com/problemset/problem/1352/B
// time-complexity -> O(n)