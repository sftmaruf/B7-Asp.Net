var NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(NumberOfTestCase-- > 0)
{
    var length = Convert.ToInt32(Console.ReadLine());
    var arr = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    Console.WriteLine(arr.Max() - arr.Min());
}

// problem-link -> https://codeforces.com/problemset/problem/1624/A
// time-complexity -> O(1)