using System.Text;

int rows = Convert.ToInt32(Console.ReadLine());

bool isPossible = false;
StringBuilder busConfig = new StringBuilder();

for(int i = 0; i < rows; i++)
{
    string seatConfig = Console.ReadLine()!;
    if (!isPossible)
    {
        isPossible = seatConfig.Contains("OO");
        if(isPossible)
        {
            string[] seats = seatConfig.Split('|');
            if (seats[0].Contains("OO"))
            {
                seatConfig = seatConfig.Replace("OO|", "++|");
            }
            else
            {
                seatConfig = seatConfig.Replace("|OO", "|++");
            }
        }
    }
    busConfig.Append(seatConfig).Append('\n');
}
Console.WriteLine(isPossible ? "YES" : "NO");
if(isPossible) Console.WriteLine(busConfig.ToString());

// problem-link -> https://codeforces.com/problemset/problem/711/A
// time-complexity -> O(n)