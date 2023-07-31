# Notes

## Higher Order Function
* value types always go on stack and reference type fo on heap

* when we enters a function variables are put on stack and as soon as we the function is compleed variables are removed from the stack

* A higher order function is a function that does not return a simple data type, but a function that return a funciton or that gets a function as a parameter

* A higher order function represent a fatory pattern where a function returns a function(receipe) to create something rather that exact value


## Closures
* So in case if any variable is been used in returned function that variable is been promoted form stack to heap by C#, becaue lifetime of a variable inside a funciton is just limited to a stack. and this is what technically called a closure i:e binding the lifetime of a variable with the returned method

*Imagine a higher order function (a function that returns a function) and if the return function uses some variable it becomes a closure (i:e binding the lifetime of a variable with the returned method)