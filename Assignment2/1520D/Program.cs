int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(numberOfTestCase > 0)
{
    int numberOfInteger = Convert.ToInt32(Console.ReadLine());
    int[] integers = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    Dictionary<int, int> result = new Dictionary<int, int>();
    long count = 0;

    for(int i = 0; i < numberOfInteger; i++)
    {
        integers[i] = integers[i] - i;
        if (!result.ContainsKey(integers[i]))
        {
            result.Add(integers[i], 1);
        }
        else
        {
            result[integers[i]]++;
        }
    }

    foreach(var value in result.Values)
    {
        if (value > 1)
        {
            int tempValue = value - 1;
            count += (long)((tempValue / 2.0) * (1 + tempValue));
        }
    }
    Console.WriteLine(count);
    numberOfTestCase--;
}

// problem-link -> https://codeforces.com/problemset/problem/1520/D
// time-complexity -> O(n)