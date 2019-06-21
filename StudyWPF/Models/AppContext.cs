using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWPF.Models
{
    public class AppContext : BindableBase
    {
        private string message;
        public string Message
        {
            get { return this.message; }
            set { this.SetProperty(ref this.message, value); }
        }
        public Calc Calc { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AppContext()
        {
            this.Calc = new Calc(this);
        }
    }
}
