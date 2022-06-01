using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Options
{
    public class Settings
    {
        public string JwtKey { get; set; }
        public string AppUrl { get; set; }
        public string DocksiteUrl { get; set; }
        public int AccessTokenLifetime { get; set; }
    }
}
