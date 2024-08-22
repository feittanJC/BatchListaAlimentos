using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace Common.Settings
{
    public class ConfigurationLib : IConfigurationLib
    {
        public IConfiguration Configuration { get; set; }

        public ConfigurationLib(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string ConnectionString => Configuration.GetSection("ConnectionStrings")["myconn"];



        public string UrlBase => Configuration.GetSection("BackOfficeService")["urlBase"];
        public string PrefixBackOffice => Configuration.GetSection("BackOfficeService")["prefix"];
        public string GenerateIndexController => Configuration.GetSection("BackOfficeService")["generateIndexController"];
        public string GenerateListController => Configuration.GetSection("BackOfficeService")["generateListController"];
        public string GeneratePDFController => Configuration.GetSection("BackOfficeService")["generatePDFController"];

        public string FoodPath => Configuration.GetSection("FilesFolder")["FoodPath"];
        public string FilesPath => Configuration.GetSection("FilesFolder")["FilesPath"];
    }
}
