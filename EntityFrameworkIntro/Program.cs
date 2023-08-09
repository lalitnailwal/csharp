// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

Console.WriteLine("Code to Illustrate Entity Framework");

var factory = new CookBookContextFactory();
using var context = factory.CreateDbContext(args);

Console.WriteLine("Add Porridge for breakfast");
var porridge = new Dish{ Title ="Breakfast Porridge", Notes = "This is sooooo good", Stars = 4 };
context.Dishes.Add(porridge);
await context.SaveChangesAsync();
Console.WriteLine($"Added Porridge (id = {porridge.Id}) Successfully");

Console.WriteLine("Checking Stars for Porridge");
var dishes =  await context.Dishes.Where(d => d.Title.Contains("Porridge"))
.ToListAsync(); // Linq => sql
if(dishes.Count != 1) Console.Error.WriteLine("Something really bad happens. Porridge disappeared :-(");
Console.WriteLine($"Porridge has {dishes[0].Stars} stars");

Console.WriteLine("Changing Porridge stars to 5");
porridge.Stars = 5;
await context.SaveChangesAsync();
Console.WriteLine("Changed Stars");

Console.WriteLine("Removing Porridge from database");
context.Dishes.Remove(porridge);
await context.SaveChangesAsync();
Console.WriteLine("Porridge Removed");


//Crate the Model Class
class Dish
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Title { get; set; }   = string.Empty;

    [MaxLength(1000)]
    public string? Notes { get; set; }

    public int? Stars { get; set; }

    List<DishIngredient> Ingredients { get; set; } = new();
}
 
class DishIngredient
{
     public int Id { get; set; }

     [MaxLength(100)]
    public string Description { get; set; }   = string.Empty;

    [MaxLength(50)]
    public string UnitOFMeasure { get; set; } = string.Empty;

    [Column(TypeName ="decimal(5, 2)")]
    public double   Amount { get; set; }

    public Dish? Dish{ get; set; }

    public int DishId { get; set; }
}

class CookBookContext : DbContext
{
    public DbSet<Dish> Dishes {get; set;}

    public DbSet<DishIngredient> DishIngredients {get; set;}

    public CookBookContext(DbContextOptions<CookBookContext> options) : base(options)
    {

    }
}

class CookBookContextFactory : IDesignTimeDbContextFactory<CookBookContext>
{
    public CookBookContext CreateDbContext(string[]? args = null)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder< CookBookContext>();
        optionsBuilder
            // Uncomment the following line if you want to print generated
            // SQL statements on the console.
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        return new  CookBookContext(optionsBuilder.Options);
    }
}