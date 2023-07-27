Console.WriteLine("This example is to illustrate Delegates");

// CalculateAndPrint(25, 30, delegate(int x, int y) { return (x+y);});

/* Below is a lambda function, A lambda function is just a delegate, 
a function with less code i:e less letter and digits, here fat arrow is a symbol to write code quickly with less words*/
CalculateAndPrint(25, 30, (x, y) =>  x + y);
CalculateAndPrint(25, 30, (x, y) =>  x - y);
CalculateAndPrint(25, 30, (x, y) =>  x * y); 

/*This function knows how to calculate and print but don't know 
what operation to perform (The Operation to perform is passed via delegate)*/
static void CalculateAndPrint(int x, int y, Mathop f)
{
    int result = f(x,y);
    Console.WriteLine(result);
}

delegate int Mathop(int a, int b); //delegate here is a typed definition for a category of funcitons