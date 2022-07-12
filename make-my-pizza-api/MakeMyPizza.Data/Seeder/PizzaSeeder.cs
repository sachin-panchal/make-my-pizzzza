using Newtonsoft.Json;

using MakeMyPizza.Data.Models;
using MakeMyPizza.Data.Repository;

namespace MakeMyPizza.Data.Seeder;
public static class PizzaSeeder
{
    public static void SeedPizza(this PizzaOrderManagementDbContext context, string fileName = "PizzaData.json")
    {

        var jsonString = File.ReadAllText(fileName);

        var pizzas = JsonConvert.DeserializeObject<List<Pizza>>(jsonString);
        if (pizzas?.Any() ?? false)
        {
            foreach (var pizza in pizzas)
            {
                context.Pizzas.Add(pizza);
            }

            context.SaveChanges();
        }
    }
}