using System.Collections.ObjectModel;

namespace WUSM.PlanChecker.Models
{
    public interface ICheckViewModelBase
    {
        ObservableCollection<CheckTableModel> PlanChecks { get; set; }
        string Title { get; set; }
         void GetChecks();
    }
}