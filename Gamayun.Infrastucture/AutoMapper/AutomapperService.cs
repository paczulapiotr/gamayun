using AutoMapper;
using System.Linq;
using System.Reflection;

namespace Gamayun.Infrastucture.Mapper
{
    public static class AutomapperService
    {
        public static MapperConfiguration Initialize()
        {
            var mapperConfigs = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.IsSubclassOf(typeof(Profile)))
                .ToList();

            return new MapperConfiguration(cfg => mapperConfigs.ForEach(a => cfg.AddProfile(a)));
        }
    }
}
