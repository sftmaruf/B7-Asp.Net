char[] CardOnTable = Console.ReadLine()!.ToCharArray();
string[] CardsOnHand = Console.ReadLine()!.Split().ToArray();

foreach(string card in CardsOnHand)
{
    if (card[0] == CardOnTable[0] || card[1] == CardOnTable[1])
    {
        Console.WriteLine("YES");
        return;
    }
}
Console.WriteLine("NO");

// problem-link -> https://codeforces.com/problemset/problem/1097/A
// time-complexity -> O(1)