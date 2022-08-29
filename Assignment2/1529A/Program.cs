int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (numberOfTestCase-- > 0)
{
    int length = Convert.ToInt32(Console.ReadLine());
    int[] ints = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    int maxDeletedElement = 0;
    
    Array.Sort(ints);
    if (ints.First() != ints.Last())
    {
        int minCount = ints.Count(i => i == ints[0]);
        maxDeletedElement = length - minCount;
    }
    Console.WriteLine(maxDeletedElement);
}

// problem-link -> https://codeforces.com/problemset/problem/1529/A
// time-complexity - O(nlogn)