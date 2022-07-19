byte numberOfInteger = Convert.ToByte(Console.ReadLine());
byte[] numbers = Console.ReadLine()!.Split().Select(byte.Parse).ToArray();

byte numberOfZero, numberOfOne;
numberOfZero = numberOfOne = 0;

for (byte n = 0; n < numbers.Length; n++)
{
    if (numbers[n] == 0) numberOfZero++;
    if (numbers[n] == 1) numberOfOne++;
}

if(numberOfOne == numberOfInteger)
{
    Console.WriteLine(numberOfOne - 1);
    return;
}

if(numberOfZero == numberOfInteger)
{
    Console.WriteLine(numberOfInteger);
    return;
}

bool isZero = false;
byte countZero = 0, maxZero = 0;

for (byte i = 0; i < numbers.Length; i++)
{
    if (numbers[i] == 0)
    {
        countZero++;
        if (countZero > maxZero) maxZero = countZero;
        if(!isZero) isZero = true;
    }
    else if (isZero && countZero > 0 && numbers[i] == 1) countZero--;
}

Console.WriteLine(maxZero + numberOfOne);

// problem-link -> https://codeforces.com/problemset/problem/327/A
// time-complexity -> O(n)