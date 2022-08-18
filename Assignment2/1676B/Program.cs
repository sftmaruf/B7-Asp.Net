int NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (NumberOfTestCase-- > 0)
{
    int numberOfBox = Convert.ToInt32(Console.ReadLine());
    int[] quantities = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

    int min = quantities.Min();
    var extraQuantities = quantities.SkipWhile(quantity => quantity == min);
    int removedQuantity = extraQuantities.Sum() - (extraQuantities.Count() * min);
    Console.WriteLine(removedQuantity);
}

// problem-link -> https://codeforces.com/problemset/problem/1676/B
// time-complexity -> O(n)