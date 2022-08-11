int NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(NumberOfTestCase-- > 0)
{
    int[] inputs = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    int nOfFloor = 1;
    if (inputs[0] > 2) nOfFloor += (int) Math.Ceiling((inputs[0] - 2.0) / inputs[1]);
    Console.WriteLine(nOfFloor);
}

// problem-link -> https://codeforces.com/problemset/problem/1426/A
// time-complexity -> O(1)