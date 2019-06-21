using Codeer.Friendly.Windows.Grasp;
using RM.Friendly.WPFStandardControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWPFUITest
{
    /// <summary>
    /// 
    /// </summary>
    public class MainWindowDriver
    {
        public WPFTextBox Lhs { get; }
        public WPFTextBox Rhs { get; }
        public WPFComboBox OperatorType { get; }
        public WPFButtonBase Execute { get; }
        public WPFTextBlock Answer { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        public MainWindowDriver(dynamic window)
        {
            var w = new WindowControl(window);
            this.Lhs = new WPFTextBox(w.LogicalTree().ByBinding("Lhs").Single());
            this.Rhs = new WPFTextBox(w.LogicalTree().ByBinding("Rhs").Single());
            this.OperatorType = new WPFComboBox(w.LogicalTree().ByBinding("OperatorTypes").Single());
            this.Execute = new WPFButtonBase(w.LogicalTree().ByBinding("ExecuteCommand").Single());
            this.Answer = new WPFTextBlock(w.LogicalTree().ByBinding("Answer").Single());
        }
    }
}
