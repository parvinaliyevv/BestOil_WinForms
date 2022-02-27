using System.Collections.Generic;
using homework.Models;

namespace homework.Database
{
    public static class DB
    {
        public static List<Fuel> Fuels { get; } = new();

        static DB()
        {
            Fuels.Add(new Fuel("AI-92", 0.90));
            Fuels.Add(new Fuel("AI-95", 1.40));
            Fuels.Add(new Fuel("AI-98", 1.55));
        }
    }
}
