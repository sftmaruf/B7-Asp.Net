int NumberOfTestCase = Convert.ToInt32(Console.ReadLine());

while (NumberOfTestCase-- > 0)
{ 
    int rating = Convert.ToInt32(Console.ReadLine());
    if (1900 <= rating) Console.WriteLine("Division 1");
    else if (1600 <= rating) Console.WriteLine("Division 2");
    else if (1400 <= rating) Console.WriteLine("Division 3");
    else Console.WriteLine("Division 4");
}


// problem-link -> https://codeforces.com/problemset/problem/1669/A
// time-complexity -> O(1)