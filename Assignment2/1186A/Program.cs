int[] inputs = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
Console.WriteLine(Math.Min(inputs[1], inputs[2]) >= inputs[0] ? "Yes" : "No");

// problem-link -> https://codeforces.com/problemset/problem/1186/A
// time-complexity -> O(1)