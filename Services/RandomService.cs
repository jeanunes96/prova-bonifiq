using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class RandomService
    {
        private readonly TestDbContext _ctx;
        private static readonly Random _random = new();

        public RandomService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> GetRandomAsync()
        {
            int number = _random.Next(100);
            _ctx.Numbers.Add(new RandomNumber { Number = number });
            await _ctx.SaveChangesAsync();
            return number;
        }
    }
}
