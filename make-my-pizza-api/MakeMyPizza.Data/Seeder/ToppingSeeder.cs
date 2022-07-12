using Newtonsoft.Json;

using MakeMyPizza.Data.Models;
using MakeMyPizza.Data.Repository;

namespace MakeMyPizza.Data.Seeder;
public static class ToppingSeeder
{
    public static void SeedTopping(this PizzaOrderManagementDbContext context, string fileName = "ToppingData.json")
    {

        var jsonString = File.ReadAllText(fileName);

        var toppings = JsonConvert.DeserializeObject<List<Topping>>(jsonString);
        if (toppings?.Any() ?? false)
        {
            foreach (var topping in toppings)
            {
                context.Toppings.Add(topping);
            }

            context.SaveChanges();
        }
    }
}