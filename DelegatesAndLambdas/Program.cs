Console.WriteLine("This example is to illustrate Delegates");

/*delegate in below allows us to write an anonymous function, The annonymous function do have a name but
that name is been provided by compilet at run time, which can be seen using tools like dnSpy etc */
CalculateAndPrint(25, 30, delegate(int x, int y) { return (x+y);}); 

/*This function knows how to calculate and print but don't know 
what operation to perform (The Operation to perform is passed via delegate)*/
static void CalculateAndPrint(int x, int y, Mathop f)
{
    int result = f(x,y);
    Console.WriteLine(result);
}

delegate int Mathop(int a, int b); //delegate here is a typed definition for a category of funcitons