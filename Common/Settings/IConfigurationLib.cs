using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Settings
{
    public interface IConfigurationLib
    {
        string ConnectionString { get; }
        string UrlBase { get; }
        string PrefixBackOffice { get; }
        string GenerateIndexController { get; }
        string GenerateListController { get; }
        string GeneratePDFController { get; }
    }
}
