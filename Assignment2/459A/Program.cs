int[] inputs = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

if (inputs[0] == inputs[2] && inputs[1] != inputs[3])
{
    int xAxis = Subtraction(inputs[3], inputs[1]);
    Console.WriteLine($"{xAxis + inputs[0]} {inputs[1]} {inputs[2] + xAxis} {inputs[3]}");
    return;
}

if (inputs[1] == inputs[3] && inputs[0] != inputs[2])
{
    int yAxis = Subtraction(inputs[2], inputs[0]);
    Console.WriteLine($"{inputs[0]} {inputs[1] + yAxis} {inputs[2]} {inputs[3] + yAxis}");
    return;
}

if (inputs[0] != inputs[2] && inputs[1] != inputs[3])
{
    if (Math.Abs(Subtraction(inputs[3], inputs[1])) != Math.Abs(Subtraction(inputs[2], inputs[0])))
    {
        Console.WriteLine(-1);
        return;
    }

    Console.WriteLine($"{inputs[0]} {inputs[3]} {inputs[2]} {inputs[1]}");
}

bool IsFirstOneMax(int value1, int value2) => value1 > value2;
int Subtraction(int value1, int value2)
{
    if(value1 < 0 && value2 < 0)
    {
        if (IsFirstOneMax(value1, value2)) return value2 - value1;
        else return value1 - value2;
    }

    if(IsFirstOneMax(value1, value2)) return value1 - value2;
    return value2 - value1;
}

// problem-link -> https://codeforces.com/problemset/problem/459/A
// time-complexity -> O(1)