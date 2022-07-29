using System.Text;

char[] integers = Console.ReadLine()!.ToCharArray();
StringBuilder output = new StringBuilder();

for (int cursor = 0; cursor < integers.Length; cursor++)
{
    int numInt = (int)char.GetNumericValue(integers[cursor]);
    int invert = 9 - numInt;
    if (invert < numInt && (cursor != 0 || invert > 0)) output.Append(invert);
    else output.Append(numInt);
}
Console.WriteLine(output.ToString());

// problem-link -> https://codeforces.com/problemset/problem/514/A
// time-complexity -> O(n)