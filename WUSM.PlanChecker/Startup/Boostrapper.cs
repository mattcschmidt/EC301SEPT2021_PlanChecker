using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using WUSM.PlanChecker.Services;
using WUSM.PlanChecker.ViewModels;
using WUSM.PlanChecker.Views;

namespace WUSM.PlanChecker.Startup
{
    public class Boostrapper
    {
        public IContainer Bootstrap(ESAPIWorker eSAPIWorker, Patient patient,Course course, PlanSetup planSetup)
        {
            var container = new ContainerBuilder();
            //esapi stuff
            container.RegisterInstance<ESAPIWorker>(eSAPIWorker);
            container.RegisterInstance<Patient>(patient);
            container.RegisterInstance<PlanSetup>(planSetup);
            container.RegisterInstance<Course>(course);

            //register view
            container.RegisterType<MainView>().AsSelf();

            //register viewmodels.
            container.RegisterType<MainViewModel>().AsSelf();
            container.RegisterType<PlanChecksViewModel>().AsSelf();
            container.RegisterType<DoseChecksViewModel>().AsSelf();

            return container.Build();
        }
    }
}
