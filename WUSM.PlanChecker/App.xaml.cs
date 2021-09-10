using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WUSM.PlanChecker.Services;
using VMS.TPS.Common.Model.API;
using System.Windows.Threading;
using WUSM.PlanChecker.Startup;
using System.Reflection;
using System.IO;
using Autofac;
using WUSM.PlanChecker.Views;
using WUSM.PlanChecker.ViewModels;

namespace WUSM.PlanChecker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private string _patientId;
        private string _courseId;
        private string _planId;
        private Patient _patient;
        private Course _course;
        private PlanSetup _plan;
        private IContainer container;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                using (VMS.TPS.Common.Model.API.Application app = VMS.TPS.Common.Model.API.Application.CreateApplication())
                {
                    if (e.Args.Count() > 0)
                    {
                        _patientId = e.Args.First().Split(';').First();
                        _courseId = e.Args.First().Split(';').ElementAt(1);
                        _planId = e.Args.First().Split(';').Last();
                        if (!String.IsNullOrEmpty(_patientId))
                        {
                            _patient = app.OpenPatientById(_patientId);
                            if (_patient != null)
                            {
                                _course = _patient.Courses.FirstOrDefault(p => p.Id == _courseId);
                                if (_course != null)
                                {
                                    _plan = _course.PlanSetups.FirstOrDefault(p => p.Id == _planId);
                                }
                            }
                        }
                    }
                    // The ESAPI worker needs to be created in the main thread
                    var esapiWorker = new ESAPIWorker(app);
                    Boostrapper bootstrapper = new Boostrapper();
                    container = bootstrapper.Bootstrap(esapiWorker, _patient, _course, _plan);
                    // This new queue of tasks will prevent the script
                    // for exiting until the new window is closed
                    DispatcherFrame frame = new DispatcherFrame();

                    RunOnNewStaThread(() =>
                    {
                            // This method won't return until the window is closed
                            InitializeAndStartMainWindow(esapiWorker);

                            // End the queue so that the script can exit
                            frame.Continue = false;
                    });

                    // Start the new queue, waiting until the window is closed
                    Dispatcher.PushFrame(frame);

                }
            }
            catch (Exception ex)
            {
                var path = Path.Combine(Assembly.GetExecutingAssembly().Location, "Errors.txt");
                File.WriteAllText(path, System.Environment.NewLine + ex.Message);
            }
        }

        private void RunOnNewStaThread(Action a)
        {
            Thread thread = new Thread(() => a());
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }



        private void InitializeAndStartMainWindow(ESAPIWorker esapiWorker)
        {
           
            var mv = container.Resolve<MainView>();
            mv.DataContext = container.Resolve<MainViewModel>();
            mv.ShowDialog();
        }
    }
}
