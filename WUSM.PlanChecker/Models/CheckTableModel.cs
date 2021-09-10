using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUSM.PlanChecker.Models
{
    public class CheckTableModel
    {
        public string Property { get; set; }
        public string Value { get; set; }
        public Evaluation Result { get; set; }

    }
    public enum Evaluation
    {
        None,
        Pass,
        Warning,
        Flagged
    }
}
