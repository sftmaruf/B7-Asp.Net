int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase-- > 0)
{
    int length = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(string.Join(" ", Enumerable.Repeat(1, length).ToList()));
}

// problem-link -> https://codeforces.com/problemset/problem/1372/A
// time-complexity -> O(n)