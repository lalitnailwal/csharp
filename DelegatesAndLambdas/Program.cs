Console.WriteLine("This example is to illustrate Delegates");

static int Add(int x, int y)
{
    int result = x + y;
    return result;
}

static int Subtract(int a, int b)
{
    int result = a - b;
    return result;
}

Mathop f = Add;
Console.WriteLine(f(10,20));
f = Subtract;
Console.WriteLine(f(10,20));

CalculateAndPrint(25,30,f);

/*This function knows how to calculate and print but don't know 
what operation to perform (The Operation to perform is passed via delegate)*/
static void CalculateAndPrint(int x, int y, Mathop f)
{
    int result = f(x,y);
    Console.WriteLine(result);
}

delegate int Mathop(int a, int b);

