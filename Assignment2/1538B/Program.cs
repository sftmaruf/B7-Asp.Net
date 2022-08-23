int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase-- > 0)
{
    int nOfFriends = Convert.ToInt32(Console.ReadLine());
    int[] candies = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

    int sum = candies.Sum();
    if (sum % nOfFriends != 0) Console.WriteLine("-1");
    else
    {
        int mean = sum / nOfFriends;
        Console.WriteLine(candies.Count(c => c > mean));
    }
}

// problem-link -> https://codeforces.com/problemset/problem/1538/B
// time-complexity -> O(n)