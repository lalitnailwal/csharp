// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

Console.WriteLine("Code to Illustrate Advanced Concepts of Entity Framework");

var factory = new CookBookContextFactory();
using var dbContext = factory.CreateDbContext();

//An experiment...
var newDish = new Dish {Title = "foo", Notes= "Bar"};
dbContext.Dishes.Add(newDish);
await dbContext.SaveChangesAsync();
newDish.Notes = "Baz";
await dbContext.SaveChangesAsync();

await EntityState1(factory);
await ChangeTracking(factory);
await AttachEntities(factory);
await NoTracking(factory);
await RawSql(factory);
await Transactions(factory);
await ExpressionTree(factory);

static async Task ExpressionTree(CookBookContextFactory factory)
{
    using var dbContext = factory.CreateDbContext();
    var newDish = new Dish {Title = "foo", Notes= "Bar"};
    dbContext.Dishes.Add(newDish);
    await dbContext.SaveChangesAsync();

    var dishes = dbContext.Dishes
    .Where(d => d.Title.StartsWith("F"))
    .ToArrayAsync();

    Func<Dish, bool> f = d => d.Title.StartsWith("F");
    Expression<Func<Dish, bool>> ex = d => d.Title.StartsWith("F");

    var inMemoryDishes = new[] {        
        new Dish {Title = "foo", Notes= "Bar"},
        new Dish {Title = "foo", Notes= "Bar"}  
    };

    var dishes1 = inMemoryDishes
    .Where(d => d.Title.StartsWith("F"))
    .ToArray();

}

static async Task Transactions(CookBookContextFactory factory)
{
    using var dbContext = factory.CreateDbContext();

    using var transaction = await dbContext.Database.BeginTransactionAsync();
    try
    {
        dbContext.Dishes.Add(new Dish {Title = "foo", Notes= "Bar"});
        await dbContext.SaveChangesAsync();

        await dbContext.Database.ExecuteSqlRawAsync("Select 1/0 as Bad");
        await transaction.CommitAsync();
    }
    catch (SqlException ex)
    {        
        Console.Error.WriteLine($"Something Bad happend: {ex.Message}");
    }

}

static async Task RawSql(CookBookContextFactory factory)
{
    using var dbContext = factory.CreateDbContext();
    var dishes = await dbContext.Dishes.FromSqlRaw("Select * from Dishes").ToArrayAsync();

    var filter = "%z";

    dishes = await dbContext.Dishes.FromSqlInterpolated($"Select * from Dishes where Notes like {filter} ")
    .AsNoTracking()
    .ToArrayAsync();

    // BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD 
    // BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD 
    // BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD 
    // SQL INJECTION
    dishes = await dbContext.Dishes.FromSqlRaw("Select * from Dishes where Notes like '" + filter + "'")
    .ToArrayAsync();

    await dbContext.Database.ExecuteSqlRawAsync("DELETE From Dishes Where id NOT IN (SELECT DishId from DishIngredients)");

}

static async Task EntityState1(CookBookContextFactory factory)
{
    using var dbContext = factory.CreateDbContext();
    var newDish = new Dish {Title = "foo", Notes= "Bar"};
    var state = dbContext.Entry(newDish).State; // << Detached

    dbContext.Dishes.Add(newDish);
    state = dbContext.Entry(newDish).State; // << Added

    await dbContext.SaveChangesAsync();
    state = dbContext.Entry(newDish).State; // << Unchanged

    newDish.Notes = "Baz";
    state = dbContext.Entry(newDish).State; // << Modified

    await dbContext.SaveChangesAsync();

    dbContext.Dishes.Remove(newDish);
    state = dbContext.Entry(newDish).State; // << Deleted

    await dbContext.SaveChangesAsync();
    state = dbContext.Entry(newDish).State; // << Detached

}

static async Task ChangeTracking(CookBookContextFactory factory)
{
    using var dbContext = factory.CreateDbContext();

    var newDish = new Dish {Title = "foo", Notes= "Bar"};
    dbContext.Dishes.Add(newDish);
    await dbContext.SaveChangesAsync();

    newDish.Notes = "Baz";

    var entry = dbContext.Entry(newDish);
    var OriginalValue = entry.OriginalValues[nameof(Dish.Notes)].ToString();
    var dishFromDatabse = await dbContext.Dishes.SingleAsync(d => d.Id == newDish.Id);

     using var dbContext2 = factory.CreateDbContext();
     var dishFromDatabse2 = await dbContext2.Dishes.SingleAsync(d => d.Id == newDish.Id);

}

static async Task AttachEntities(CookBookContextFactory factory)
{
    using var dbContext = factory.CreateDbContext();

    var newDish = new Dish {Title = "foo", Notes= "Bar"};
    dbContext.Dishes.Add(newDish);
    await dbContext.SaveChangesAsync();

    //EF : Forget the "newDish" object
    dbContext.Entry(newDish).State =  EntityState.Detached;

    dbContext.Dishes.Update(newDish);
    await dbContext.SaveChangesAsync(); 
}

static async Task NoTracking(CookBookContextFactory factory)
{
    using var dbContext = factory.CreateDbContext();

    
    var dishes = await dbContext.Dishes.AsNoTracking().ToArrayAsync();
    var state = dbContext.Entry(dishes[0]).State; //Detached
}


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