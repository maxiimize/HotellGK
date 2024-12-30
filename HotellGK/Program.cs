using Microsoft.Extensions.DependencyInjection;

namespace HotellGK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new HotelDbContext())
            {
                context.Database.EnsureCreated();
            }



        }
    }
}
