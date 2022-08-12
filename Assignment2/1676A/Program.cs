int NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (NumberOfTestCase-- > 0)
{ 
    int[] integers = Console.ReadLine()!.Select(i => Convert.ToInt32(i)).ToArray();
    if (integers[..3].Sum() == integers[3..].Sum()) Console.WriteLine("YES");
    else Console.WriteLine("NO");
}

// problem-link -> https://codeforces.com/problemset/problem/1676/A
// time-complexity -> O(1)