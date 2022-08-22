int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(numberOfTestCase-- > 0)
{
    int length = Convert.ToInt32(Console.ReadLine());
    int[] ints = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

    int sum = ints.Sum();
    if (sum == length) Console.WriteLine(0);
    else if (sum > length) Console.WriteLine(sum - length);
    else Console.WriteLine(1);
}

// problem-link -> https://codeforces.com/problemset/problem/1537/A
// time-complexity -> O(1)