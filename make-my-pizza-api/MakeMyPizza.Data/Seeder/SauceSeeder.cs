using MakeMyPizza.Data.Models;
using MakeMyPizza.Data.Repository;
using Newtonsoft.Json;

namespace MakeMyPizza.Data.Seeder;
public static class SauceSeeder
{
    public static void SeedSauce(this PizzaOrderManagementDbContext context, string fileName = "SauceData.json")
    {

        var jsonString = File.ReadAllText(fileName);

        var sauces = JsonConvert.DeserializeObject<List<Sauce>>(jsonString);
        if (sauces?.Any() ?? false)
        {
            foreach (var sauce in sauces)
            {
                context.Sauces.Add(sauce);
            }

            context.SaveChanges();
        }
    }
}