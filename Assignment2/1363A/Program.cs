int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase > 0)
{
    int[] inputs = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    int[] elements = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

    int length = inputs[0];
    int neededElement = inputs[1];

    (int oddCount, int evenCount) = getQuantityOfOddAndEven(elements);

    if (evenCount == 0 && !isOdd(neededElement) || oddCount == 0 || Sum(evenCount, oddCount) < neededElement)
    {
        Console.WriteLine("No");
    }
    else if (isOdd(oddCount))
    {
        if (Sum(evenCount, oddCount) >= neededElement)
            Console.WriteLine("Yes");
    }
    else
    {
        oddCount--;
        if (Sum(evenCount, oddCount) >= neededElement) Console.WriteLine("Yes");
        else Console.WriteLine("No");
    }

    numberOfTestCase--;
}

bool isOdd(int value) => value % 2 != 0;
int Sum(int value1, int value2) => value1 + value2;

(int, int) getQuantityOfOddAndEven(int[] elements)
{
    int odd = 0, even = 0;
    for (int i = 0; i < elements.Length; i++)
        if (elements[i] % 2 == 0) even++;
        else odd++;
    return (odd, even);
}

// problem-link -> https://codeforces.com/problemset/problem/1363/A
// time-complexity -> O(1)