var NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (NumberOfTestCase-- > 0)
{
    var length = Convert.ToInt32(Console.ReadLine());
    var integers = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    var erasedCount = 0;

    if (length == 1)
    {
        Console.WriteLine(erasedCount);
        continue;
    }

    bool flag = false;
    for (var i = integers.Length - 2; i >= 0; i--)
    {
        if (!flag && integers[i] < integers[i + 1])
        {
            flag = true;
            continue;
        }

        if (flag && integers[i] > integers[i + 1])
        {
            erasedCount = i + 1;
            break;
        }
    }
    Console.WriteLine(erasedCount);
}