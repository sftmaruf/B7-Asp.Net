int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase-- > 0)
{
    int[] ints = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    IList<int> results = new List<int>(ints.Take(2))
    {
        ints.Last() - (ints[0] + ints[1])
    };
    Console.WriteLine(string.Join(" ", results));
}

// problem-link -> https://codeforces.com/problemset/problem/1618/A
// time-complexity -> O(1)