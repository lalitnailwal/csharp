Console.WriteLine("This example is to illustrate Delegates");

CalculateAndPrint<int>(25, 30, (int x, int y) =>  x * y); 
CalculateAndPrint("A", "B", (x, y) => x + y);
CalculateAndPrint(true, false, (x, y) => x && y);

static void CalculateAndPrint<T>(T x, T y, Combine<T> f)
{
    var result = f(x, y);
    Console.WriteLine(result);
}

delegate T Combine<T>(T a, T b);