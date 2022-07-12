using Newtonsoft.Json;

using MakeMyPizza.Data.Models;
using MakeMyPizza.Data.Repository;

namespace MakeMyPizza.Data.Seeder;
public static class PizzaPriceSeeder
{
    public static void SeedPizzaPrice(this PizzaOrderManagementDbContext context, string fileName = "PizzaPriceData.json")
    {

        var jsonString = File.ReadAllText(fileName);

        var pizzaPrices = JsonConvert.DeserializeObject<List<PizzaPrice>>(jsonString);
        if (pizzaPrices?.Any() ?? false)
        {
            foreach (var pizzaPrice in pizzaPrices)
            {
                context.PizzaPrice.Add(pizzaPrice);
            }

            context.SaveChanges();
        }
    }
}