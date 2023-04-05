using Practice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Common
{
    public class GenerateRandom
    {
        public static List<ChefModelDTO> GenerateRandomChefs(int count)
        {
            List<ChefModelDTO> randomChefs = new List<ChefModelDTO>();

            string[] firstNames = { "Ivo", "Miro", "Janika", "Lenka", "Senka", "Mirogojka", "Suncica", "Sanjiva" };
            string[] lastNames = { "Memic", "Santava", "Licanin", "Torbica", "Bijelkic", "Crvenkic", "Buzdovan" };

            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                var chef = new ChefModelDTO
                {
                    FirstName = firstNames[random.Next(firstNames.Length)],
                    LastName = lastNames[random.Next(lastNames.Length)],
                    PhoneNumber = $"098-{random.Next(100, 1000)}-{random.Next(1000, 10000)}",
                    HomeAddress = $"Ulica {lastNames[random.Next(lastNames.Length)]} 123",
                    Certified = random.Next(2) == 1, // Randomly set true or false
                    OIB = $"{random.Next(10000000, 100000000)}",
                    HireDate = DateTime.Now.AddDays(-random.Next(0, 365))
                };

                randomChefs.Add(chef);
            }

            return randomChefs;
        }
    }
}
