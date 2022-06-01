using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Helpers
{
    public static class ConvertHelper
    {
        public static double ConvertBytesToMegaBytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}
