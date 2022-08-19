int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase-- > 0)
{
    int[] votes = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

    List<int> results = votes.Select(vote => votes.Max() - vote)
        .Select(vote => vote != 0 ? vote + 1 : vote).ToList();

    if (results.Count(r => r == 0) == 3)
    {
        results = results.Select(vote => vote + 1).ToList();
    }

    if (results.Count(r => r == 0) == 2)
    {
        results = results.Select(vote => vote == 0 ? vote + 1 : vote).ToList();
    }
    Console.WriteLine(string.Join(" ", results));
}

// problem-link -> https://codeforces.com/problemset/problem/1593/A
// time-complexity -> O(n)
