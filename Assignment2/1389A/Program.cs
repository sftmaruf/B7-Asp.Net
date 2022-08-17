int NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (NumberOfTestCase-- > 0)
{
    int[] inputs = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

    if(inputs[0] * 2 <= inputs[1]) Console.WriteLine($"{inputs[0]} {inputs[0] * 2}");
    else Console.WriteLine("-1 -1");
}

// problem-link -> https://codeforces.com/problemset/problem/1389/A
// time-complexity -> O(1)