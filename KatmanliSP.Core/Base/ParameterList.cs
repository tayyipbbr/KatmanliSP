using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.ResponseMessages;

namespace KatmanliSP.Core.Base
{
    public class ParameterList : IEnumerable<Parameter>
    {
        public List<Parameter> Parameters { get; set; }
        public ParameterList() 
        {
            Parameters = new List<Parameter>();
        }
        public IEnumerator<Parameter> GetEnumerator()
        {
            return Parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Reset()
        {
            Parameters = new List<Parameter>();
        }

        public void Add(string name, object value)
        {
            if(Parameters.Any(p => p.Name == name))
            {

            }
            Parameters.Add(new Parameter() {Name = name, Value = value});
        }
    }
}
