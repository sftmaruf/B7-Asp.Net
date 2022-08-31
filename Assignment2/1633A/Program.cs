int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(numberOfTestCase-- > 0)
{
    int result;
    int number = Convert.ToInt32(Console.ReadLine());

    if (number % 7 == 0) result = number;
    else
    {
        int remainder = number % 7;
        if (number % 10 >= remainder) result = number - remainder;
        else result = number + (7 - remainder);
    }
    Console.WriteLine(result);
}

// problem-link -> https://codeforces.com/problemset/problem/1633/A
// time-complexity -> O(1)