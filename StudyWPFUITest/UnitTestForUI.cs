using System;
using System.Diagnostics;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StudyWPFUITest
{
    [TestClass]
    public class UnitTestForUI
    {
        private Process process;
        private WindowsAppFriend app;
        private MainWindowDriver driver;

        [TestInitialize]
        public void Initialize()
        {
            this.process = Process.Start(@"StudyWPF.exe");
            this.app = new WindowsAppFriend(this.process);
            this.driver = new MainWindowDriver(app.Type("System.Windows.Application").Current.MainWindow);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.app.Dispose();
            this.process.Kill();
        }

        [TestMethod]
        public void TestMethodAdd()
        {
            this.driver.Lhs.EmulateChangeText("10");
            this.driver.Rhs.EmulateChangeText("3");
            this.driver.OperatorType.EmulateChangeSelectedIndex(0); // +
            this.driver.Execute.EmulateClick();
            Assert.AreEqual("13", this.driver.Answer.Text);
        }

        [TestMethod]
        public void TestMethodSub()
        {
            this.driver.Lhs.EmulateChangeText("10");
            this.driver.Rhs.EmulateChangeText("3");
            this.driver.OperatorType.EmulateChangeSelectedIndex(1); // -
            this.driver.Execute.EmulateClick();
            Assert.AreEqual("7", this.driver.Answer.Text);
        }

        [TestMethod]
        public void TestMethodMul()
        {
            this.driver.Lhs.EmulateChangeText("10");
            this.driver.Rhs.EmulateChangeText("3");
            this.driver.OperatorType.EmulateChangeSelectedIndex(2); // X
            this.driver.Execute.EmulateClick();
            Assert.AreEqual("30", this.driver.Answer.Text);
        }

        [TestMethod]
        public void TestMethodDiv()
        {
            this.driver.Lhs.EmulateChangeText("100");
            this.driver.Rhs.EmulateChangeText("2");
            this.driver.OperatorType.EmulateChangeSelectedIndex(3); // X
            this.driver.Execute.EmulateClick();
            Assert.AreEqual("50", this.driver.Answer.Text);
        }

        [TestMethod]
        public void TestMethodErrorValue()
        {
            this.driver.Lhs.EmulateChangeText("aaa");
            this.driver.Rhs.EmulateChangeText("3");
            this.driver.OperatorType.EmulateChangeSelectedIndex(0); // Add
            this.driver.Execute.EmulateClick();
            Assert.AreEqual("0", this.driver.Answer.Text);
        }

    }
}
