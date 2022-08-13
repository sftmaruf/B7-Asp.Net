int NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(NumberOfTestCase-- > 0)
{
    int length = Convert.ToInt32(Console.ReadLine());
    int[] sequence = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    
    List<int> favoriteSequence = new List<int>();
    for(int i = 0, j = length - 1; i < (int) Math.Ceiling(length/2.0); i++, j--)
    {
        favoriteSequence.Add(sequence[i]);
        if(i != j) favoriteSequence.Add(sequence[j]);
    }
    Console.WriteLine(String.Join(' ', favoriteSequence));
}

// problem-link -> https://codeforces.com/problemset/problem/1462/A
// time-complexity -> O(n/2)