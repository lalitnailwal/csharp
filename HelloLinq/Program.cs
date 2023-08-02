// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("Code to illustrate various linq queries");

var fileContents = await File.ReadAllTextAsync("data.json");
var cars = JsonSerializer.Deserialize<CarData[]>(fileContents);

////Print all cars with at least 4 doors
//var carsWithAtLeastFourDoors = cars.Where(car => car.NumberOfDoors >= 4);
//foreach (var car in carsWithAtLeastFourDoors)
//{
//    Console.WriteLine($"The car {car.Model} has {car.NumberOfDoors} doors");
//}

////Print All Mazda cars with at least four doors
//var mazdaCarsWithAtLeastFourDoors = cars.Where(car => car.Make == "Mazda" && car.NumberOfDoors >= 4);
//mazdaCarsWithAtLeastFourDoors = cars.Where(car => car.Make == "Mazda").Where(car => car.NumberOfDoors >= 4);
//foreach (var car in mazdaCarsWithAtLeastFourDoors)
//{
//    Console.WriteLine($"The mazda car {car.Model} has {car.NumberOfDoors} doors");
//}

////Print Make + Model for all Makes that starts with "M"
//cars.Where(car => car.Make.StartsWith("M"))
//    .Select(car => $"{car.Make} {car.Model}")
//    .ToList()
//    .ForEach(car => Console.WriteLine(car));

////Display a list of 10 most powerful cars (in terms of hp)
//cars.OrderByDescending(car => car.HP)
//    .Take(10)
//    .Select(car => $"{car.Make} {car.Model}")
//    .ToList()
//    .ForEach(car => Console.WriteLine(car));

////Display the number of models per make that appeared after 2008
//cars.Where(car => car.Year >= 2008)
//    .GroupBy(car => car.Make)    
//    .Select(c => new { c.Key, NumberOfModel = c.Count() })
//    .ToList()
//    .ForEach(item => Console.WriteLine($"{item.Key}: {item.NumberOfModel}"));

//Display the number of models per make that appeared after 2008
//Make should be displayed with a number of zero if there are no model after 2008

//Method 1
//cars.GroupBy(car => car.Make)
//    .Select(c => new { c.Key,
//        NumberOfModel = c.Where(car => car.Year >= 2008).Count() })
//    .ToList()
//    .ForEach(item => Console.WriteLine($"{item.Key}: {item.NumberOfModel}"));

////Method 2
//cars.GroupBy(car => car.Make)
//    .Select(c => new {
//        c.Key,
//        NumberOfModel = c.Count(car => car.Year >= 2008)})
//    .ToList()
//    .ForEach(item => Console.WriteLine($"{item.Key}: {item.NumberOfModel}"));

//Display a list of makes that have at least 2 models with >= 400hp

////Method 1
//cars.Where(car => car.HP >= 400)
//    .GroupBy(car => car.Make)
//    .Select(car => new { Make = car.Key, NumberofPowerFulCars = car.Count() })
//    .Where(car => car.NumberofPowerFulCars >= 2)
//    .ToList()
//    .ForEach(make => Console.WriteLine(make.Make));

////Method 2
//cars.Where(car => car.HP >= 400)
//    .GroupBy(car => car.Make)
//    .Select(car => new { Make = car.Key, NumberofPowerFulCars = car.Count() })    
//    .ToList()
//    .ForEach(make => 
//    {
//        if(make.NumberofPowerFulCars >=  2) Console.WriteLine(make.Make);
//    });

////Display the average hp per make
//cars.GroupBy(car => car.Make)
//    .Select(car => new { Make = car.Key, AverageHP = car.Average(car => car.HP)})
//    .ToList()
//    .ForEach(make => Console.WriteLine($"{make.Make}: {make.AverageHP}"));


//How many makes build cars with hp between 0..100, 101..200, 201..300, 301..400, 401..500
cars.GroupBy(car => car.HP switch
    {
        <= 100 => "0..100",
        <= 200 => "101..200",
        <= 300 => "201..300",
        <= 400 => "301..400",
        _ => "401..500"
    })
    .Select(car => new
    {
        HPCategory = car.Key,
        NumberOfMake = car.Select(c => c.Make).Distinct().Count()
    })
    .ToList()
    .ForEach(item => Console.WriteLine($"{item.HPCategory}: {item.NumberOfMake}"));

class CarData
{
    [JsonPropertyName("id")]
    public int ID { get; set; }

    [JsonPropertyName("car_make")]
    public string? Make { get; set; }

    [JsonPropertyName("car_models")]
    public string? Model { get; set; }

    [JsonPropertyName("car_year")]
    public int Year { get; set; }

    [JsonPropertyName("number_of_doors")]
    public int NumberOfDoors { get; set; }

    [JsonPropertyName("hp")]
    public int HP { get; set; }
}


