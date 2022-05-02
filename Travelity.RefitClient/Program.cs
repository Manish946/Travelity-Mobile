using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.RefitAbstractions;

namespace Travelity.RefitClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Calling API using Refit");
            var client = RestService.For<ITravelityApiClient>("http://164.68.120.109:8010/api");
            var users = await client.GetUsers();
            Console.WriteLine($"Currently {users.Count()} Users in the Database");
            Console.WriteLine("Creating new User");
            //await client.CreateUser(new User
            //{
            //   FirstName = "Manihs"
            //});
          
        }
    }
}
