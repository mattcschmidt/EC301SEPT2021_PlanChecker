using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using WUSM.PlanChecker.Models;
using WUSM.PlanChecker.Services;

namespace WUSM.PlanChecker.ViewModels
{
    public class PlanChecksViewModel 
    {
        private ESAPIWorker _esapiWorker;
        private PlanSetup _plan;

        public ObservableCollection<CheckTableModel> PlanChecks { get; set; }
        public string Title { get; set; }
        public PlanChecksViewModel(ESAPIWorker eSAPIWorker,PlanSetup planSetup)
        {
            _esapiWorker = eSAPIWorker;
            _plan = planSetup;
            Title = "Plan Checks";
            PlanChecks = new ObservableCollection<CheckTableModel>();
            GetChecks();
        }
        public void GetChecks()
        {
            CheckValidDose();
            CheckDoseInsideTarget();

        }

        private void CheckDoseInsideTarget()
        {
            var doseInTarget = new CheckTableModel();
            Structure target = null;
            DoseValue maxDose = new DoseValue();
            bool inTarget = false;

            _esapiWorker.Run(xapp =>
            {
                target = _plan.StructureSet.Structures.FirstOrDefault(p => p.Id == _plan.TargetVolumeID);
                maxDose = _plan.Dose.DoseMax3D;
                if (target != null)
                {
                    inTarget = target.IsPointInsideSegment(_plan.Dose.DoseMax3DLocation);
                }
                doseInTarget.Property = "Max Dose";
                doseInTarget.Value = maxDose.ToString();
                doseInTarget.Result = inTarget ? Evaluation.Pass : Evaluation.Flagged;
                PlanChecks.Add(doseInTarget);
            });
            
        }

        private void CheckValidDose()
        {
            var doseValidCheck = new CheckTableModel();
            doseValidCheck.Property = "Is Dose Valid";
            bool doseValid = false;
            _esapiWorker.Run(xapp =>
            {
                doseValid = _plan.IsDoseValid;
                doseValidCheck.Value = doseValid ? "Dose is Valid" : "Dose is Not Valid";
                doseValidCheck.Result = doseValid ? Evaluation.Pass : Evaluation.Flagged;
                PlanChecks.Add(doseValidCheck);
            });
            
        }
    }
}
