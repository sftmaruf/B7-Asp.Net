int NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(NumberOfTestCase-- > 0)
{
    string str = Console.ReadLine()!;
    Console.WriteLine(str.ToLower() == "yes" ? "YES" : "NO");
}

// problem-link -> https://codeforces.com/problemset/problem/1703/A
// time-complexity -> O(n)