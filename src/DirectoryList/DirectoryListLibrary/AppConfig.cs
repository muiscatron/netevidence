using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryListLibrary
{
    public class AppConfig : IConfig
    {
        public string QueueName
        {
            get;
            set;
        }
    }
}
