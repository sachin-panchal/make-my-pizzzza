using Newtonsoft.Json;

using MakeMyPizza.Data.Models;
using MakeMyPizza.Data.Repository;

namespace MakeMyPizza.Data.Seeder;

public static class UserSeeder
{
    public static void SeedUser(this PizzaOrderManagementDbContext context, string fileName = "UserData.json")
    {

        var jsonString = File.ReadAllText(fileName);

        var users = JsonConvert.DeserializeObject<List<User>>(jsonString);
        if (users?.Any() ?? false)
        {
            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
        }
    }
}