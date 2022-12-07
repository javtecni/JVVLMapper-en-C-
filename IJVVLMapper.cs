using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVVLMAPPER
{
    public interface IJVVLMapper
    {

        T? mapper_JVVL<T>(object objeto, object objeto2, bool all_Or_Emty, bool same);

    }
}
