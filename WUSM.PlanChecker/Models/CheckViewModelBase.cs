using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUSM.PlanChecker.Models
{
    public class CheckViewModelBase : ICheckViewModelBase
    {
        public string Title { get; set; }
        public ObservableCollection<CheckTableModel> PlanChecks { get; set; }

        public void GetChecks()
        {
            
        }
    }
}
