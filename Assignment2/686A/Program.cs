long[] inputs = Console.ReadLine()!.Split().Select(long.Parse).ToArray();

long distressedKids = 0;
while (inputs[0]-- > 0)
{
    string[] signWithQuantity = Console.ReadLine()!.Split();
    long quantity = long.Parse(signWithQuantity[1]);

    if (signWithQuantity[0] == "+") inputs[1] += quantity;
    else
    {
        if (inputs[1] < quantity) distressedKids++;
        else inputs[1] -= quantity;
    }
}
Console.WriteLine($"{inputs[1]} {distressedKids}");

// problem-link -> https://codeforces.com/problemset/problem/686/A
// time-complexity -> O(n)
