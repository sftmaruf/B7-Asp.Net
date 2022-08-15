int NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(NumberOfTestCase-- > 0)
{
    var chars = Console.ReadLine()!.ToCharArray();
    int numberOfA = 0, numberOfB = 0, numberOfC = 0;
    foreach(var c in chars)
    {
        if (c == 'A') numberOfA++;
        if (c == 'B') numberOfB++;
        if (c == 'C') numberOfC++;
    }

    if ((numberOfB - numberOfC) - numberOfA == 0) Console.WriteLine("YES");
    else Console.WriteLine("NO");
}

// problem-link -> https://codeforces.com/problemset/problem/1579/A
// time-complexity -> O(n)