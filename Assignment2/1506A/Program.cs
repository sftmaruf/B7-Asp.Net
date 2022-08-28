int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase-- > 0)
{
    ulong[] inputs = Console.ReadLine()!.Split().Select(ulong.Parse).ToArray();
    ulong position;

    if (inputs[0] * inputs[1] == inputs[2]) position = inputs[2];
    else
    {
        ulong column = (ulong) Math.Ceiling(inputs[2] / (double) inputs[0]);
        ulong row = inputs[2] % inputs[0]; 
        if (row == 0) row = inputs[0];
        position = (row - 1) * inputs[1] + column;
    }
    Console.WriteLine(position);
}

// problem-link -> https://codeforces.com/problemset/problem/1506/A
// time-complexity -> O(1)