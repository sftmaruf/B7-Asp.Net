int numberOfPiles = Convert.ToInt32(Console.ReadLine());
int[] worms = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
int numberOfJuicyWorms = Convert.ToInt32(Console.ReadLine());
int[] labelsOfJuicyWorms = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

for (int i = 1; i < numberOfPiles; i++)
{
    worms[i] = worms[i] + worms[i - 1];
}

foreach (var labeledWorm in labelsOfJuicyWorms)
{
    int position = findJuicyWorm(labeledWorm);
    Console.WriteLine(position);
}

int findJuicyWorm(int juicyWorm)
{
    int left = 0, right = numberOfPiles - 1;

    while (left <= right)
    {
        int mid = (left + right) / 2;

        if (juicyWorm == worms[mid]) return mid + 1;
        if (mid == 0 && juicyWorm < worms[mid]) return mid + 1;

        if (juicyWorm < worms[mid] && juicyWorm > worms[mid - 1]) return mid + 1;
        else if (juicyWorm > worms[mid]) left = mid + 1;
        else right = mid - 1;
    }
    return 0;
}