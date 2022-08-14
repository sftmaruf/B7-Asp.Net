int NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(NumberOfTestCase-- > 0)
{
    int length = Convert.ToInt32(Console.ReadLine());
    int[] integers = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    int oddCount = 0, evenCount = 0;

    foreach(int i in integers)
    {
        if (i % 2 != 0) oddCount++;
        else evenCount++;
    }

    if (oddCount % 2 != 0 || oddCount!=0 && evenCount!=0) Console.WriteLine("Yes");
    else Console.WriteLine("NO");
}

// problem-link -> https://codeforces.com/problemset/problem/1296/A
// time-complexity -> O(n)