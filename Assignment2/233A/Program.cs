int size = Convert.ToInt32(Console.ReadLine());

if (size % 2 != 0) Console.WriteLine(-1);
else
{
    List<int> ints = new List<int>();
    for (int i = 2; i <= size; i += 2)
    {
        ints.Add(i);
        ints.Add(i - 1);
    }
    Console.WriteLine(string.Join(" ", ints));
}

// problem-link -> https://codeforces.com/problemset/problem/233/A
// time-complexity -> O(n)