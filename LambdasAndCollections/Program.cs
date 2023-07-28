// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;

Console.WriteLine("Code to demonstrate Lambdas with collections");

var heroes = new List<Hero>{
    new("Wade", "Wilson", "Deadpool", false),
    new(string.Empty, string.Empty, "HomeLander", true),
    new("Bruce", "Wayne", "Batman", false),
    new(string.Empty, string.Empty, "StormFront", true),
};

var result = FilterHeroes(heroes, x => x.CanFly == true);
var heroesWhoCanFly =  string.Join(",", result);
Console.WriteLine("Heroes Who Can Fly are as follows");
Console.WriteLine(heroesWhoCanFly);

List<Hero> FilterHeroes(List<Hero> heroes, Filter f)
{
    var resultList = new List<Hero>();
    foreach(var hero in heroes)
    {
        if(f(hero))
            resultList.Add(hero);        
    }
    return resultList;
}

delegate bool Filter(Hero hero);

record Hero(string FistName, string LastName, string HeroName, bool CanFly);