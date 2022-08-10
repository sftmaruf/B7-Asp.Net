int NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(NumberOfTestCase-- > 0)
{
    int[] distances = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    int count = 0;
    for (int i = 1; i < distances.Length; i++)
    {
        if (distances[0] < distances[i]) count++;
    }
    Console.WriteLine(count);
}

// problem-link -> https://codeforces.com/problemset/problem/1692/A
// time-complexity -> O(n)