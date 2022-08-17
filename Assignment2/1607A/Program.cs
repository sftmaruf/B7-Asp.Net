int NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (NumberOfTestCase-- > 0)
{
    string lettersInKeyboard = Console.ReadLine()!;
    string lettersInString = Console.ReadLine()!;

    int sum = 0;
    for (int i = 1; i < lettersInString.Length; i++)
    {
        sum += Math.Abs(
            lettersInKeyboard.IndexOf(lettersInString[i])
            - lettersInKeyboard.IndexOf(lettersInString[i - 1])
            );
    }
    Console.WriteLine(sum);
}

// problem-link -> https://codeforces.com/problemset/problem/1607/A
// time-complexity -> O(n)