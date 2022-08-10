int NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(NumberOfTestCase-- > 0)
{
    var chars = Console.ReadLine()!.ToCharArray();
    bool isSquare = true;

    if (chars.Length % 2 != 0) isSquare = false;
    else
    {
        int len = chars.Length / 2;
        for (int i = 0; i < len; i++)
        {
            if (chars[i] != chars[len + i])
            {
                isSquare = false;
                break;
            }
        }
    }
    Console.WriteLine(isSquare ? "YES" : "NO");
}

// problem-link -> https://codeforces.com/problemset/problem/1619/A
// time-complexity -> O(n/2)