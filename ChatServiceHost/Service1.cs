using ChatWCFService;
using System.ServiceModel;
using System.ServiceProcess;

namespace ChatServiceHost
{
    public partial class ChatService : ServiceBase
    {
        ServiceHost host;
        public ChatService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            host = new ServiceHost(typeof(Service));
            host.Open();
        }

        protected override void OnStop()
        {
            host.Close();
        }
    }
}
