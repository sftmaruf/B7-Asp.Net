int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase != 0)
{
    int numberOfElements = Convert.ToInt32(Console.ReadLine());
    int[] elements = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    long sum = 0;
    int max = 0, min = 0, cursor = 0;

    for (int i = 0; i < numberOfElements;)
    {
        if (elements[i] > 0)
        {
            while (elements[cursor] > 0 && cursor != numberOfElements)
            {
                if (elements[cursor] > max) max = elements[cursor];
                cursor++;
                if (cursor == numberOfElements) break;
            }
            sum += max;
            max = 0;
            i = cursor;
        }
        else if (elements[i] < 0)
        {
            if (min == 0) min = elements[i];
            while (elements[cursor] < 0 && cursor != numberOfElements)
            {
                if (elements[cursor] > min) min = elements[cursor];
                cursor++;
                if (cursor == numberOfElements) break;
            }
            sum += min;
            min = 0;
            i = cursor;
        }
    }
    Console.WriteLine(sum);
    numberOfTestCase--;
}

// problem-link -> https://codeforces.com/problemset/problem/1343/C
// time-complexity -> O(m * n)