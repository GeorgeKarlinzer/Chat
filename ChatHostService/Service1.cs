using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ChatHostService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            // TODO: Hosting a wtc
            // TODO: Communicate with db with ef
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {

        }
    }
}
