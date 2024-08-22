using Common.Response;
using Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.HttpHelpers
{
    public class UtilitariesResponse<T> where T : class, new()
    {
        private IConfigurationLib ConfigurationLib = null;

        public UtilitariesResponse(IConfigurationLib _configurationLib)
        {
            ConfigurationLib = _configurationLib;
        }

      
        public EResponseBase<T> setResponseBaseForToMuchInformation()
        {
            EResponseBase<T> response = new EResponseBase<T>();
            response.Code = 555;
            response.Message = "Mucha Información";
            response.MessageEN = "To much information";
            response.listado = new List<T>();
            return response;
        }

    }
}
