// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;

Console.WriteLine("Code to demonstrate Lambdas with collections");

var heroes = new List<Hero>{
    new("Wade", "Wilson", "Deadpool", false),
    new(string.Empty, string.Empty, "HomeLander", true),
    new("Bruce", "Wayne", "Batman", false),
    new(string.Empty, string.Empty, "StormFront", true),
};

var result = Filter(heroes, x => x.CanFly == true);
var heroesWhoCanFly =  string.Join(",", result);
Console.WriteLine("Heroes Who Can Fly are as follows");
Console.WriteLine(heroesWhoCanFly);

var result1 = Filter(new string[] {"lalit","mohan","nailwal"}, x => x.StartsWith("l"));
var result2 = Filter(new int[] {1,2,3,4}, x=> x % 2 == 0);

IEnumerable<T> Filter<T>(IEnumerable<T> heroes, Filter<T> f) //This is a generic filter capable of filtering any type of object for any condition
{
    foreach(var hero in heroes)
    {
        if(f(hero))
             yield return hero;  //yield helps us to get rid of cration of new list thus save on memory footprints
    }
}

delegate bool Filter<T>(T hero);

record Hero(string FistName, string LastName, string HeroName, bool CanFly);