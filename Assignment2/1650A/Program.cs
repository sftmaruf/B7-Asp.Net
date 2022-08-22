int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase-- > 0)
{
    string str = Console.ReadLine()!;
    char c = Convert.ToChar(Console.ReadLine()!);
    bool isConvertible = false;

    if (str.Length % 2 != 0) 
    {
        for (int i = 0; i < str.Length; i++)
        {
            if(str[i] == c && i % 2 == 0)
            {
                isConvertible = true;
                break;
            }
        }
    }
    Console.WriteLine(isConvertible ? "Yes" : "No");
}

// problem-link -> https://codeforces.com/problemset/problem/1650/A
// time-complexity -> O(n)