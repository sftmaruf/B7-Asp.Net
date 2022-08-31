int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase-- > 0)
{
    int number = Convert.ToInt32(Console.ReadLine());

    if (number % 2 == 0) Console.WriteLine(0);
    else
    {
        int[] numberStr = number.ToString().ToCharArray()
            .Select(x => (int)char.GetNumericValue(x)).ToArray();
        bool isAllOddDigit = numberStr.Count(n => n % 2 == 0) == 0;

        if (isAllOddDigit) Console.WriteLine(-1);
        else
        {
            if (numberStr[0] % 2 == 0) Console.WriteLine(1);
            else Console.WriteLine(2);
        }
    }
}

// problem-link -> https://codeforces.com/problemset/problem/1611/A
// time-complexity -> O(n)