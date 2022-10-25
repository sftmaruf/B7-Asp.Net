using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.Migration
{
    public class Migration
    {
        private IDecoder _decoder;

        public Migration(IDecoder decoder)
        {
            _decoder = decoder;
        }

        public async Task Run(Type type)
        {
            if(type == null) throw new ArgumentNullException("type");
            await _decoder.Decode(type);
        }
    }
}
