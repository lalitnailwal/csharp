// See https://aka.ms/new-console-template for more information
Console.WriteLine("Code to Illustrate Concept of closure");

Action a = () => Console.WriteLine("hi");
a();

Action<int> a2 = (n) => Console.WriteLine(n*n);
a2(12);

Action<string,string> a3 = (s1, s2) => Console.WriteLine(s1 + s2);
a3("Foo", "Bar");

Func<int> f = () => 42;;
Console.WriteLine(f());

Func<int, int> f2  = (n) => n * n;
Console.WriteLine(f2(12));

Func<int, int, bool> f3 = (n1, n2) => n1 == n2;
Console.WriteLine(f3(12, 12));