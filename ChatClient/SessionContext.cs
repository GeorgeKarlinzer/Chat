using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatData;

namespace ChatClient
{
    internal class SessionContext
    {
        private static SessionContext instance;
        public static SessionContext Instance => instance??= new SessionContext();
        private SessionContext() { }

        public User CurrentUser { get; set; }
    }
}
