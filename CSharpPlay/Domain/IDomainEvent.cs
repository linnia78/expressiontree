using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlay
{
    public interface IDomainEvent
    {
        DateTime DateTimeEventOccurred { get; }
    }
}
