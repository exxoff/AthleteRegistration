using AthleteAdmin.Interfaces;
using AthleteAdmin.UserTypes;
using AthleteAdmin.ViewModels;
using AthleteMessageService.UserTypes;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteAdminTests
{
    [TestFixture]
    public class AthleteAdminTests
    {


        [Test]
        public void MainViewModel_HostInfo_DatabaseFile_Is_Valid_()
        {

            Mock<IDialogService> dService = new Mock<IDialogService>();
            dService.Setup(ds => ds.DisplayYesNoMessageBoxDialog(
                It.IsAny<string>(),
                It.IsAny<string>(),
                false)).Returns(true);

            dService.Setup(ds => ds.FileOpenDialog()).Returns("C:\\Test\\Test.aReg");
            MainViewModel model = new MainViewModel(new HostInfo(),dService.Object);

            Assert.AreEqual("C:\\Test\\Test.aReg", model.hostInfo.DatabaseFile);

        }
    }
}
