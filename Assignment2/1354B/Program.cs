var NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(NumberOfTestCase-- > 0)
{
    var tracker = new int[3];
    for (int i = 0; i < 3; i++) tracker[i] = -1;

    var characters = Console.ReadLine()!.ToCharArray();
    var length = characters.Length;
    for(var i = 0; i < characters.Length; i++)
    {
        tracker[(int)char.GetNumericValue(characters[i]) - 1] = i;
        if(tracker[0] != -1 && tracker[1] != -1 && tracker[2] != -1)
        {
            length = Math.Min((tracker.Max() - tracker.Min()) + 1, length);
        }
    }
    
    if(tracker[0] == -1 || tracker[1] == -1 || tracker[2] == -1) Console.WriteLine(0);
    else Console.WriteLine(length);
}

// problem-link -> https://codeforces.com/problemset/problem/1354/B
// time-complexity -> O(n)