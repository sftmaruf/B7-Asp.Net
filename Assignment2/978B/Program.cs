int length = Convert.ToInt32(Console.ReadLine());
string str = Console.ReadLine()!;

int countX = 0;
int removedCount = 0;
foreach(var ch in str.ToCharArray())
{
    if(ch == 'x') countX++;
    else
    {
        if (countX >= 3) removedCount += countX - 2;
        countX = 0;
    }
}

if(countX >= 3) removedCount += countX - 2;
Console.WriteLine(removedCount);

// problem-link -> https://codeforces.com/problemset/problem/978/B
// time-complexity -> O(n)