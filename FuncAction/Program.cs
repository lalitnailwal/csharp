
// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

Console.WriteLine("Code to illustate Func and Action Delegates");

MeasureTime(() => CountNearlytoInfinity());
Console.WriteLine($"The result is {MeasureTimeFunc(() => CalculateSomeResult())}");

static void MeasureTime(Action f)
{
    var watch = Stopwatch.StartNew();
    f();
    watch.Stop();
    Console.WriteLine(watch.Elapsed);
}

static int MeasureTimeFunc(Func<int> f)
{
    var watch = Stopwatch.StartNew();
    var result = f();
    watch.Stop();
    Console.WriteLine(watch.Elapsed);
    return result;
}

static void CountNearlytoInfinity()
{
    for(int i=0; i<1000000000; i++);
}

static int CalculateSomeResult()
{
    for(int i=0; i<1000000000; i++);
    return 36;
}