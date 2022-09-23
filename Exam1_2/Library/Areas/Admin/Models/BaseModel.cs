using Autofac;
using Infrastructure.Services;
using static System.Formats.Asn1.AsnWriter;

namespace Library.Areas.Admin.Models
{
    public abstract class BaseModel
    {
        protected ILifetimeScope _scope;

        public BaseModel()
        {

        }

        public virtual void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
        }
    }
}
