using Luval.Coach.Data;

namespace Luval.Coach.Services
{
    public class MetricService
    {

        public MetricService(ICoachDbContext context)
        {
            Context = context;
        }

        public ICoachDbContext Context { get; private set; }


    }
}