using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMS.be_together.Data.ViewModels;

namespace TMS.be_together.Data.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private UserViewModel _viewModel;

        [TestInitialize]
        public void TestInitialize()
        {
            _viewModel = new UserViewModel();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _viewModel.Dispose();
            _viewModel = null;
        }

        [TestMethod]
        public void GetUser_tvo()
        {
            _viewModel.Load("tvo");

            Assert.AreEqual(_viewModel.Values.Count(), 1);
        }
    }
}
