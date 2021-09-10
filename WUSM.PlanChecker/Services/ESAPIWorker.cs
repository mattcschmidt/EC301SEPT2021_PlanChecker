using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using VMS.TPS.Common.Model.API;

namespace WUSM.PlanChecker.Services
{
    public class ESAPIWorker
    {
        private readonly Application _app;
        private readonly Dispatcher _dispatcher;

        public ESAPIWorker(Application application)
        {
            _app = application;
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        public void Run(Action<Application> a)
        {
            _dispatcher.BeginInvoke(a, _app);
        }
    }
}
