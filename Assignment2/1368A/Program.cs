int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase-- > 0)
{
    int[] inputs = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    int numberOfOperations = 0;

    while (inputs[0] <= inputs[2] && inputs[1] <= inputs[2])
    {
        if (inputs[0] > inputs[1]) inputs[1] += inputs[0];
        else inputs[0] += inputs[1];
        numberOfOperations++;
    }
    Console.WriteLine(numberOfOperations);
}

// problem-link -> https://codeforces.com/problemset/problem/1368/A
// time-complexity -> O(logn)
