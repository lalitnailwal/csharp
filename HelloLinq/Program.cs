// See https://aka.ms/new-console-template for more information
Console.WriteLine("Code to illustrate IEnumberable and Linq concepts");

var even = true;

var result = GenerateNumbers(10);
if (even)
{
    result = result.Where(n => n % 2 == 0);
}
result = result.Select(n => n * 3);

foreach (var item in result)
{
    Console.WriteLine(item);
}

IEnumerable<int> GenerateNumbers(int Maxvalue)
{   
    for(int i = 0; i < Maxvalue; i++)
    {
        yield return i;
    }
}