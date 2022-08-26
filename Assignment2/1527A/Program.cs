int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase-- > 0)
{
    int number = Convert.ToInt32(Console.ReadLine());
    int required;

    if (number <= 2) required = number;
    else
    {
        required = 4;
        while(number >= required) required *= 2;
        required /= 2;
    }
    Console.WriteLine(required - 1);
}

// problem-link -> https://codeforces.com/problemset/problem/1527/A
// time-complexity -> O(logn)