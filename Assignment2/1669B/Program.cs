int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(numberOfTestCase-- > 0)
{
    int length = Convert.ToInt32(Console.ReadLine());
    int[] arr = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

    int[] result = arr.GroupBy(e => e).Where(e => e.Count() >= 3)
        .Select(e => e.Key).ToArray();
    Console.WriteLine(result.Length >= 1 ? result[0] : -1);
}

// problem-linl -> https://codeforces.com/problemset/problem/1669/B
// time-complexity -> O(n)