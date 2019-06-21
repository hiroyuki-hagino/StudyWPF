using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyWPF.ViewModels;

namespace StudyWPFTest
{
    [TestClass]
    public class UnitTestForViewModel
    {
        [TestMethod]
        public void TestMethodAdd()
        {
            var vm = new MainWindowViewModel();
            vm.Lhs = "10";
            vm.Rhs = "3";
            vm.SelectedOperatorType = vm.OperatorTypes[0]; // Add

            var command = vm.ExecuteCommand;
            Assert.AreEqual(true, command.CanExecute());
            command.Execute();
            Assert.AreEqual(13, vm.Answer);
        }

        // 以下同様に...
    }
}
