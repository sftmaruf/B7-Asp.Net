var NumberOfStones = Convert.ToInt32(Console.ReadLine());
var Costs = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
var NumberOfQuestions = Convert.ToInt32(Console.ReadLine());
var SortedCosts = Costs.OrderBy(cost => cost).ToArray();

var SumOfCosts = new long[Costs.Length];
var SumOfSortedCosts = new long[Costs.Length];
SumOfCosts[0] = Costs[0];
SumOfSortedCosts[0] = SortedCosts[0];

for(int i = 1; i < NumberOfStones; i++)
{
    SumOfCosts[i] = SumOfCosts[i - 1] + Costs[i];
    SumOfSortedCosts[i] = SumOfSortedCosts[i - 1] + SortedCosts[i];
}

while (NumberOfQuestions > 0)
{
    var questions = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    bool isNegativeIndex = questions[1] - 2 < 0;
    if (questions[0] == 1) Solve(isNegativeIndex, questions[1], questions[2], SumOfCosts);
    else Solve(isNegativeIndex, questions[1], questions[2], SumOfSortedCosts);
    NumberOfQuestions--;
}

void Solve(bool isNegativeIndex, int lowerBound, int upperBound, long[] costs)
{
    Console.WriteLine(isNegativeIndex
        ? costs[upperBound - 1]
        : costs[upperBound - 1] - costs[lowerBound - 2]);
}

// problem-link -> https://codeforces.com/problemset/problem/433/B
// time-complexity -> O(nlogn)[best case] / O(n^2)[worst case]