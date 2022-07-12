using Newtonsoft.Json;

using MakeMyPizza.Data.Models;
using MakeMyPizza.Data.Repository;

namespace MakeMyPizza.Data.Seeder;
public static class DrinkSeeder
{
    public static void SeedDrink(this PizzaOrderManagementDbContext context, string fileName = "DrinkData.json")
    {

        var jsonString = File.ReadAllText(fileName);

        var drinks = JsonConvert.DeserializeObject<List<Drink>>(jsonString);
        if (drinks?.Any() ?? false)
        {
            foreach (var drink in drinks)
            {
                context.Drinks.Add(drink);
            }

            context.SaveChanges();
        }
    }
}