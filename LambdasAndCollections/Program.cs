// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;

Console.WriteLine("Code to demonstrate Lambdas with collections");

var heroes = new List<Hero>{
    new("Wade", "Wilson", "Deadpool", false),
    new(string.Empty, string.Empty, "HomeLander", true),
    new("Bruce", "Wayne", "Batman", false),
    new(string.Empty, string.Empty, "StormFront", true),
};

var result = FilterHeroesWhoCanFly(heroes);
var heroesWhoCanFly =  string.Join(",", result);
Console.WriteLine("Heroes Who Can Fly are as follows");
Console.WriteLine(heroesWhoCanFly);

var results = FilterHeroesWhoesLastNameIsUnknown(heroes);
var heroesWhoesLastNameIsUnknown =  string.Join(",", result);
Console.WriteLine("Heroes Whoes Last Name is Unknown");
Console.WriteLine(heroesWhoesLastNameIsUnknown);

List<Hero> FilterHeroesWhoCanFly(List<Hero> heroes)
{
    var resultList = new List<Hero>();
    foreach(var hero in heroes)
    {
        if(hero.CanFly)
            resultList.Add(hero);        
    }
    return resultList;
}

List<Hero> FilterHeroesWhoesLastNameIsUnknown(List<Hero> heroes)
{
    var resultList = new List<Hero>();
    foreach(var hero in heroes)
    {
        if(string.IsNullOrEmpty(hero.LastName))
            resultList.Add(hero);        
    }
    return resultList;
}

record Hero(string FistName, string LastName, string HeroName, bool CanFly);