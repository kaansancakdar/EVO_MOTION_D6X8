using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetProcess
{

        public class MyProcess
        {
            Process _p;

            public MyProcess(Process p)
            {
                _p = p;
            }

            public override string ToString()
            {
                return _p?.ProcessName;
            }

            public Process P { get => _p; }
        }
    
}
