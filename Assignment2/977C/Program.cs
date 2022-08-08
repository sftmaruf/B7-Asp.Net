var inputs = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
var length = inputs[0];
var integer = inputs[1];
var integers = Console.ReadLine()!.Split().Select(long.Parse)
    .OrderBy(integer => integer).ToArray();

if(integer == 0)
{
    Console.WriteLine(integers[0] - 1 == 0 ? -1 : integers[0] - 1);
    return;
}

if(length == 1)
{
    if(integer == 1) Console.WriteLine(integers[0]);
    else Console.WriteLine(-1);
    return;
}

for(int i = 0; i < length; i++)
{
    if(i == integer-1)
    {
        if(i != length - 1 && integers[i + 1] == integers[i]) Console.WriteLine(-1);
        else Console.WriteLine(integers[i]);
        break;
    }
}

// problem-link -> https://codeforces.com/problemset/problem/977/C
// time-complexity -> O(1)[best case] / O(n)[worst case]