using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUSM.PlanChecker.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(PlanChecksViewModel planChecksViewModel, DoseChecksViewModel doseChecksViewModel)
        {
            PlanChecksViewModel = planChecksViewModel;
            DoseChecksViewModel = doseChecksViewModel;
        }

        public PlanChecksViewModel PlanChecksViewModel { get; }
        public DoseChecksViewModel DoseChecksViewModel { get; }
    }
}
