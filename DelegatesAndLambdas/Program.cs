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

delegate int Mathop(int a, int b);

