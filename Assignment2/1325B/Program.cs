int NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(NumberOfTestCase-- > 0)
{
    int length = Convert.ToInt32(Console.ReadLine());
    int[] elements = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    Console.WriteLine(elements.Distinct().Count());
}

// problem-link -> https://codeforces.com/problemset/problem/1325/B
// time-complexity -> O(n)