int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(numberOfTestCase-- > 0)
{
    int length = Convert.ToInt32(Console.ReadLine());
    string solvedProblem = Console.ReadLine()!;
    int total = 0;

    var freq = solvedProblem.GroupBy(p => p).Select(p => p.Count());
    foreach (var count in freq)
    {
        if(count == 1) total += 2;
        else total += count + 1;
    }
    Console.WriteLine(total);
}

// problem-link -> https://codeforces.com/problemset/problem/1703/B
// time-complexity -> O(n)
