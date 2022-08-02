using System.Text;

int TestCase = Convert.ToInt32(Console.ReadLine());

StringBuilder output1 = new StringBuilder();
StringBuilder output2 = new StringBuilder();

while (TestCase > 0)
{
    int Length = Convert.ToInt32(Console.ReadLine());
    char[] TernaryNumber = Console.ReadLine()!.ToCharArray();

    bool IsEqual = true;
    foreach (char c in TernaryNumber)
    {
        if (c == '2')
        {
            if (IsEqual)
            {
                output1.Append('1');
                output2.Append('1');
            }
            else
            {
                output1.Append('0');
                output2.Append('2');
            }
        }
        else if (c == '1')
        {
            if (IsEqual)
            {
                output1.Append('1');
                output2.Append('0');
                IsEqual = false;
            }
            else
            {
                output1.Append('0');
                output2.Append('1');
            }
        }
        else
        {
            output1.Append(0);
            output2.Append(0);
        }
    }

    Console.WriteLine(output1.ToString());
    Console.WriteLine(output2.ToString());
    output1.Clear();
    output2.Clear();
    TestCase--;
}

