using System.IO;

// See https://aka.ms/new-console-template for more information

//Console.WriteLine("Code illustration for AsyncAwait concept");


//-------------------Synchronous Code-----------------------------------------------------------------------

// var lines = File.ReadAllLines("TextFile1.txt");

// foreach(var line in lines)
//     Console.WriteLine(line);

//-------------------Asynchronous Code Classical way, without using async and await---------------------------

// File.ReadAllLinesAsync("TextFile1.txt")
//     .ContinueWith(t => 
//     {

//         if(t.IsFaulted)
//             Console.WriteLine(t.Exception);

//         var lines = t.Result;
//         foreach(var line in lines)
//         {
//             Console.WriteLine(line);
//         }
//     });

// Console.ReadLine();


//--------------------Asynchronous Code using async and await----------------------------------------------------

await ReadFile();

static async Task ReadFile()
{
    var lines = await File.ReadAllLinesAsync("TextFile1.txt");

    foreach(var line in lines)
        Console.WriteLine(line);
}

var networkResult = await GetDataFromNetworkAsync();
Console.WriteLine(networkResult);

async Task<int> GetDataFromNetworkAsync(){
    await Task.Delay(150);
    var result = 42;
    return result;
}

//---------------------Asynchronous Code with asynch and await in lambda style---------------------------------------

Func<Task<int>> getDataFromNetworkViaLambda  = async () => {
    await Task.Delay(150);
    var result = 24;
    return result;
};

Console.WriteLine(await getDataFromNetworkViaLambda());