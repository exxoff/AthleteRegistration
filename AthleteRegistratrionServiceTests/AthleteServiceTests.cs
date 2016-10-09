
using AthleteRegistrationService;
using AthleteRegistrationService.Interfaces;
using Moq;
using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AthleteRegistrationServiceTests
{
    [TestFixture]
    public class AthleteServiceTests
    {

        
        public IAthleteService service;

        [SetUp]
        public void SetUp()
        {
            service = new AthleteService();
        }

        [Test]
        
        public void Adding_a_null_Athlete_will_throw_ArgumentNullException()
        {


            //IAthleteService service = new AthleteService();

            Assert.That(() => service.StoreAthlete(null), Throws.TypeOf<ArgumentNullException>());

        }

        [Test]
        public void Adding_a_valid_athlete_returns_true()
        {
            //IAthleteService service = new AthleteService();

            bool ret = service.StoreAthlete(new AthleteDto());

            Assert.IsTrue(ret);
        }

        [Test]
        public void Getting_all_athletes_async_return_0_when_there_are_no_athletes_in_collection()
        {

            List<AthleteDto> returnList = new List<AthleteDto>();

    
            Mock<IDbClient> dbClientMock = new Mock<IDbClient>();
            dbClientMock.Setup(x => x.GetAllAthletes(false)).Returns(() => returnList);

            //IAthleteService service = new AthleteService();

            Assert.AreEqual(0, (service.TempGetAllAthletesAsync(dbClientMock.Object, false).Result).Count());
        }

        [Test]
        public void Get_all_athletes_with_crew_returns_3()
        {
            List<AthleteDto> returnList = new List<AthleteDto>()
            {
                new AthleteDto() {Bib=1 },
                new AthleteDto() {Bib=2 },
                new AthleteDto() {Bib=-1 }
            };

            Mock<IDbClient> dbClientMock = new Mock<IDbClient>();
            dbClientMock.Setup(x => x.GetAllAthletes(true)).Returns(() => returnList);

            //IAthleteService service = new AthleteService();

            Assert.AreEqual(3, (service.TempGetAllAthletesAsync(dbClientMock.Object, true).Result).Count());
        }

        [Test]
        public void Get_Existing_Crew_Returns_null()
        {
            Mock<IDbClient> dbClientMock = new Mock<IDbClient>();
            dbClientMock.Setup(x => x.GetAthlete(-1)).Returns(() => null);


            Assert.IsNull(service.TempExistingAthleteAsync(dbClientMock.Object, -1).Result);
        }

        [Test]
        public void Get_Existing_Athlete_With_BIB_0_Returns_null()
        {
            Mock<IDbClient> dbClientMock = new Mock<IDbClient>();
            dbClientMock.Setup(x => x.GetAthlete(0)).Returns(() => null);


            Assert.IsNull(service.TempExistingAthleteAsync(dbClientMock.Object, -1).Result);

        }
    }
}
