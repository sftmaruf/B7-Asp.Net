int NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(NumberOfTestCase-- > 0)
{
    int length = Convert.ToInt32(Console.ReadLine());
    int[] arr = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

    int oddCount = arr.Count(item => item % 2 != 0);
    Console.WriteLine(oddCount == length ? "YES" : "NO");
}

// problem-link -> https://codeforces.com/problemset/problem/1542/A
// time-complexity -> O(n)