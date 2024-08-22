using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Response
{
    public class EResponseBase<TEntity> : ICloneable where TEntity : class, new()
    {
        public int? Code { get; set; }
        public string Message { get; set; }
        public string MessageEN { get; set; }
        public bool IsResultList { get; set; } = false;
        public IEnumerable<TEntity> listado { get; set; }
        public TEntity objeto { get; set; }
        public List<TEntity> list { get; set; }
        public string dato { get; set; }
        public int? total { get; set; }
        public int? total1 { get; set; }
        public int? count { get; set; }
        public int? count1 { get; set; }
        public int? count2 { get; set; }
        public int? count3 { get; set; }
        //public Exception TechnicalErrors { get; set; }
        public List<string> FunctionalErrors { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("Response[Code: {0}, Message: {1},  listado: {2} , objeto {3}]", Code, Message, listado, objeto);
        }

    }
}
