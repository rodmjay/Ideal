using System.Collections.Generic;

namespace Ideal.Core.Model
{
    public class Form : DomainObject
    {
        public FormLayout Layout { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
    }
}