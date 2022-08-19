int numberOfTestCase = Convert.ToInt32(Console.ReadLine());

while(numberOfTestCase-- > 0)
{
    int[] positions = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    int dis = Math.Abs(positions[0] - positions[1]);
    int max = positions.Take(2).Max();
    int min = positions.Take(2).Min();

    if(dis*2 >= max && dis*2 >= positions[2])
    {
        if(max - dis == min)
        {
            if(dis >= positions[2]) Console.WriteLine(positions[2] + dis);
            else Console.WriteLine(positions[2] - dis);
            continue;
        }
    }
    Console.WriteLine("-1");
}

// problem-link - https://codeforces.com/problemset/problem/1560/B
// time-complexity -> O(1)