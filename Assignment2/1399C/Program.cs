var NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(NumberOfTestCase-- > 0)
{
    var numberOfParticipants = Convert.ToInt32(Console.ReadLine());
    var weights = Console.ReadLine()!.Split().Select(int.Parse).ToList();

    if (numberOfParticipants == 1)
    {
        Console.WriteLine(0);
        continue;
    }

    var expectedWeight = 2 * numberOfParticipants;
    var maxTeamSize = 0;

    while(expectedWeight >= 2)
    {       
        var trackTeamSize = 0;
        var trackUsedWeight = new bool[numberOfParticipants];

        for (var i = 0; i < numberOfParticipants; i++)
        {
            for (var j = i + 1; j < numberOfParticipants; j++)
            {
                if (trackUsedWeight[i] || trackUsedWeight[j]) continue;

                if (weights[i] + weights[j] == expectedWeight)
                {
                    trackUsedWeight[i] = trackUsedWeight[j] = true;
                    trackTeamSize++;
                }
            }
        }
        maxTeamSize = Math.Max(trackTeamSize, maxTeamSize);
        expectedWeight--;
    }
    Console.WriteLine(maxTeamSize);
}

// problem-link -> https://codeforces.com/problemset/problem/1399/C
// time-complexity -> O(m * n^2)
