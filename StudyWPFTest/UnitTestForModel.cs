using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyWPF.Models;

namespace StudyWPFTest
{
    [TestClass]
    public class UnitTestForModel
    {
        [TestMethod]
        public void TestMethodAdd()
        {
            var appContext = new StudyWPF.Models.AppContext();

            var calc = appContext.Calc;
            calc.Lhs = 10;
            calc.Rhs = 3;
            calc.OperatorType = OperatorType.Add;
            calc.Execute();
            Assert.AreEqual(13, calc.Answer);
        }

        [TestMethod]
        public void TestMethodSub()
        {
            var appContext = new StudyWPF.Models.AppContext();

            var calc = appContext.Calc;
            calc.Lhs = 10;
            calc.Rhs = 3;
            calc.OperatorType = OperatorType.Sub;
            calc.Execute();
            Assert.AreEqual(7, calc.Answer);
        }

        [TestMethod]
        public void TestMethodMul()
        {
            var appContext = new StudyWPF.Models.AppContext();

            var calc = appContext.Calc;
            calc.Lhs = 10;
            calc.Rhs = 3;
            calc.OperatorType = OperatorType.Mul;
            calc.Execute();
            Assert.AreEqual(30, calc.Answer);
        }

        [TestMethod]
        public void TestMethodDiv()
        {
            var appContext = new StudyWPF.Models.AppContext();

            var calc = appContext.Calc;
            calc.Lhs = 100;
            calc.Rhs = 2;
            calc.OperatorType = OperatorType.Div;
            calc.Execute();
            Assert.AreEqual(50, calc.Answer);
        }
    }
}
