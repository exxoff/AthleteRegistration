using AthleteAdmin.Interfaces;
using AthleteAdmin.UserTypes;
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
        public void OpenFileDialogReturnsString()
        {
            Mock<IDialogService> dsMock = new Mock<IDialogService>();

            dsMock.Setup(ds => ds.FileOpenDialog(It.IsAny<string>())).Returns("string");
            //IDialogService ds = new DialogService();

            string returnPath = dsMock.Object.FileOpenDialog("");
            // TODO: Add your test code here
            Assert.IsInstanceOf<string>(returnPath);
        }
    }
}
