int numberOfInteger = Convert.ToInt32(Console.ReadLine());
int[] integers = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
int length = 1, tempLength = 1;

for(int i = 1; i < numberOfInteger; i++)
{
    if(integers[i] > integers[i - 1]) tempLength++;
    else
    {
        if (length < tempLength) length = tempLength;
        tempLength = 1;
    }
}
if (length < tempLength) length = tempLength;

Console.WriteLine(length);

// problem-link -> https://codeforces.com/problemset/problem/702/A
// time-complexity -> O(n)