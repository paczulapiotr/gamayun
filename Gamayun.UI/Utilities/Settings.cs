using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.UI.Utilities
{
    public interface ISettings
    {
        string RootUrl { get; }
    }
    public class Settings : ISettings
    {
        private readonly IConfiguration _configuration;

        public Settings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string RootUrl => _configuration[nameof(RootUrl)];
    }
}
