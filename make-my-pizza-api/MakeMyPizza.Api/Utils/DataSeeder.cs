using MakeMyPizza.Data.Repository;
using MakeMyPizza.Data.Seeder;

namespace MakeMyPizza.Api.Utils;
public static class DataSeeder
{
    public static IApplicationBuilder SeedData(this IApplicationBuilder app, IConfiguration config)
    {
        var seedDir = config.GetSection("AppSettings:SeedFileLocation")?.Get<string>();

        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<PizzaOrderManagementDbContext>();

            if(context is null)
                throw new Exception("Unable to resolve PizzaOrderManagementDbContext");

            context.SeedPizza($"{seedDir}/PizzaData.json");
            context.SeedPizzaPrice($"{seedDir}/PizzaPriceData.json");
            context.SeedNonPizza($"{seedDir}/NonPizzaData.json");
            context.SeedUser($"{seedDir}/UserData.json");
            context.SeedDrink($"{seedDir}/DrinkData.json");
            context.SeedSauce($"{seedDir}/SauceData.json");
            context.SeedTopping($"{seedDir}/ToppingData.json");
        }

        return app;
    }
}