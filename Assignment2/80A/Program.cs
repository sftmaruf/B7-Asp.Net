int[] inputs = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

bool isNextPrime = true;
if (inputs[1] != 2 && inputs[1] % 2 == 0) isNextPrime = false;
else
{
    isNextPrime = isPrime(2, inputs[1]);

    if (isNextPrime)
    {
        for (int dividend = inputs[0] + 1; dividend < inputs[1]; dividend++)
        {
            if (isPrime(2, dividend))
            {
                isNextPrime = false;
                break;
            }
        }
    }
}
Console.WriteLine(isNextPrime ? "YES" : "NO");

bool isPrime(int divisor, int dividend)
{
    while (divisor != dividend)
    {
        if (dividend % divisor == 0) return false;
        divisor++;
    }
    return true;
}

// problem-link -> https://codeforces.com/problemset/problem/80/A
// time-complexity -> O(m*n)
