int numberOfTestCase = Convert.ToInt32(Console.ReadLine()); 

while(numberOfTestCase-- > 0)
{
    int[] quantities = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    if (quantities[0] == 0) Console.WriteLine(1);
    else Console.WriteLine(quantities[0] + (quantities[1]*2) + 1);
}

// problem-link -> https://codeforces.com/problemset/problem/1660/A
// time-complexity -> O(1)