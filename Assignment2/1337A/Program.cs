int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(numberOfTestCase-- > 0)
{
    string[] ints = Console.ReadLine()!.Split();
    Console.WriteLine($"{ints[1]} {ints[2]} {ints[2]}");
}

// problem-link -> https://codeforces.com/problemset/problem/1337/A
// time-complexity -> O(1)