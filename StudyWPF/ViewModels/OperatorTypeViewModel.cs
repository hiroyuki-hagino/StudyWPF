using StudyWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWPF.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class OperatorTypeViewModel
    {
        public OperatorType OperatorType { get; private set; }
        public string Label { get; private set; }

        public OperatorTypeViewModel(string label, OperatorType operatorType)
        {
            this.Label = label;
            this.OperatorType = operatorType;
        }

        public static OperatorTypeViewModel[] OperatorTypes = new[]
        {
            new OperatorTypeViewModel("+", OperatorType.Add),
            new OperatorTypeViewModel("-", OperatorType.Sub),
            new OperatorTypeViewModel("X", OperatorType.Mul),
            new OperatorTypeViewModel("/", OperatorType.Div),
        };
    }
}
