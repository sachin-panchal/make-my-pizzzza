using Newtonsoft.Json;

using MakeMyPizza.Data.Models;
using MakeMyPizza.Data.Repository;

namespace MakeMyPizza.Data.Seeder;
public static class NonPizzaSeeder
{
    public static void SeedNonPizza(this PizzaOrderManagementDbContext context, string fileName = "NonPizzaData.json")
    {

        var jsonString = File.ReadAllText(fileName);

        var nonPizzas = JsonConvert.DeserializeObject<List<NonPizza>>(jsonString);
        if (nonPizzas?.Any() ?? false)
        {
            foreach (var nonPizza in nonPizzas)
            {
                context.NonPizzas.Add(nonPizza);
            }

            context.SaveChanges();
        }
    }
}