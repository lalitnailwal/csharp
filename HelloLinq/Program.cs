// See https://aka.ms/new-console-template for more information
Console.WriteLine("Code to illustrate IEnumberable and Linq concepts");

var result = GenerateNumbers(12)
        .Where(n => 
        {
            return n % 2 == 0;
        })
        .Select(n =>
        {
            return n * 3;
        });

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

