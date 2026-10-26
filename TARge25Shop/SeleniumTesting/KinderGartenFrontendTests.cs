using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace SeleniumTesting
{
    public class KinderGartenFrontendTests
    {
        public static string thisRunGuid { get; set; }
        [Fact]
        public void Should_NavigateToCreate_AddKinderGarten_WithCorrectData_ReturnToIndex()
        {
            //ülesseade
            IWebDriver driver = SetupAndNavigateSIndex();

            //tegevus
            IWebElement createInIndex = driver.FindElement(By.Id("CreateInIndex"));
            createInIndex.Click();

            //sisestatavad andmed
            InsertKinderGartenData(driver, null, false);

            IWebElement cu_CreateKinderGarten = driver.FindElement(By.Id("CU_CreateKinderGarten"));
            cu_CreateKinderGarten.Click();

            ////arvuti tudub
            //Thread.Sleep(1000);

            //andmete kogumine pärast reloadi
            IWebElement indexGroupNameKinderGarten = driver.FindElement(By.Id("IndexGroupNameKinderGarten"));
            var kinderGartenGroupNameData = indexGroupNameKinderGarten.Text;
            IWebElement indexChildrenCountKinderGarten = driver.FindElement(By.Id("IndexChildrenCountKinderGarten"));
            var kinderGartenChildrenCountData = indexChildrenCountKinderGarten.Text;
            IWebElement indexKinderGartenNameKinderGarten = driver.FindElement(By.Id("IndexKinderGartenNameKinderGarten"));
            var kinderGartenKinderGartenNameData = indexKinderGartenNameKinderGarten.Text;

            //kontroll
            Assert.Equal(kinderGartenGroupNameData, "i add name for kindergarten group");
            Assert.True(kinderGartenChildrenCountData == "12");
            Assert.Equal(kinderGartenKinderGartenNameData, "i add kindergarten name");
        }

        private IWebDriver SetupAndNavigateSIndex()
        {
            throw new NotImplementedException();
        }

        private void InsertKinderGartenData(IWebDriver driver, object value, bool v)
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Should_NavigateToDetails_OfAKinderGarten_WithPreviouslyCorrectData()
        {
            //ülesseade
            IWebDriver driver = SetupAndNavigateSIndex();

            //otsime üles kõik tabeli read, rida htmli tabelis tähistatakse "tr"iga
            //ICollection<IWebElement> table = driver.FindElements(By.TagName("tr"));
            var table = driver.FindElement(By.Id("IndexTable"));
            List<IWebElement> rows = table.FindElements(By.TagName("tr")).ToList();
            rows.RemoveAt(0);
            //tsükkel käib kõik read läbi

            foreach (var row in rows)
            {

                var locatedelement = row.FindElement(By.Id("IndexGroupNameKinderGarten"));
                if (locatedelement.Text == "i add name for kindergarten")
                {
                    //siis vajuta selles reas asuvat details nuppu
                    var rowelement = row.FindElement(By.Id("IndexActionsKinderGarten"));
                    var button = rowelement.FindElement(By.Id("IndexKinderGartenDetails"));
                    button.Click();
                    //pärast õiget vajutust, tsükkel katkestatakse
                    //arvuti tudub
                    Thread.Sleep(500);
                    break;
                }
            }
            //andmete kogumine pärast reloadi
            IWebElement details_KinderGartenId = driver.FindElement(By.Id("Details_KinderGartenId"));
            var details_id = details_KinderGartenId.Text;

            IWebElement details_KinderGartenGroupName = driver.FindElement(By.Id("Details_KinderGartenGroupName"));
            var details_groupName = details_KinderGartenGroupName.Text;

            IWebElement details_KinderGartenChildrenCount = driver.FindElement(By.Id("Details_KinderGartenChildrenCount"));
            var details_childrenCount = details_KinderGartenChildrenCount.Text;

            IWebElement details_KinderGartenName = driver.FindElement(By.Id("Details_KinderGartenName"));
            var details_kinderGartenName = details_KinderGartenName.Text;

            IWebElement details_KinderGartenTeacherName = driver.FindElement(By.Id("Details_KinderGartenTeacherName"));
            var details_teacherName = details_KinderGartenTeacherName.Text;
            //kontroll
            Assert.NotNull(details_id);
            //Assert.True(details_id.Contains("-") && (details_id.Count('-') == 4));
            //Assert.True(details_id.Substring(0, 9).EndsWith("-"));
            Assert.Equal("i add group name for kindergarten", details_groupName);
            Assert.Equal("12", details_childrenCount);
            Assert.Equal("i add name for kindergarten", details_kinderGartenName);
            Assert.Equal("i add name for teacher", details_teacherName);
        }

        [Fact]
        public void Should_UpdateDetails_OfAKinderGarten_WithNewData()
        {
            //ülesseade
            IWebDriver driver = SetupAndNavigateSIndex();

            //otsime üles kõik tabeli read, rida htmli tabelis tähistatakse "tr"iga
            //ICollection<IWebElement> table = driver.FindElements(By.TagName("tr"));
            var table = driver.FindElement(By.Id("IndexTable"));
            List<IWebElement> rows = table.FindElements(By.TagName("tr")).ToList();
            rows.RemoveAt(0);
            //tsükkel käib kõik read läbi

            foreach (var row in rows)
            {

                var locatedelement = row.FindElement(By.Id("IndexGroupNameKinderGarten"));
                if (locatedelement.Text == "i add group name for kindergarten")
                {
                    //siis vajuta selles reas asuvat details nuppu
                    var rowelement = row.FindElement(By.Id("IndexActionsKinderGarten"));
                    var button = rowelement.FindElement(By.Id("IndexKinderGartenUpdate"));
                    button.Click();
                    //pärast õiget vajutust, tsükkel katkestatakse
                    //arvuti tudub
                    Thread.Sleep(500);
                    break;
                }
            }
            //sisestatavad andmed
            InsertKinderGartenData(driver, null, true);

            IWebElement cu_CreateKinderGarten = driver.FindElement(By.Id("CU_UpdateKinderGarten"));
            cu_CreateKinderGarten.Click();

            ////arvuti tudub
            //Thread.Sleep(1000);
        }

        [Fact]
        public void Should_DeleteKinderGarten_FromAppVIADeleteView()
        {
            //ülesseade
            IWebDriver driver = SetupAndNavigateSIndex();

            //otsime üles kõik tabeli read, rida htmli tabelis tähistatakse "tr"iga
            //ICollection<IWebElement> table = driver.FindElements(By.TagName("tr"));
            var table = driver.FindElement(By.Id("IndexTable"));
            List<IWebElement> rows = table.FindElements(By.TagName("tr")).ToList();
            rows.RemoveAt(0);
            //tsükkel käib kõik read läbi

            foreach (var row in rows)
            {

                var locatedelement = row.FindElement(By.Id("IndexGroupNameKinderGarten"));
                if (locatedelement.Text == "DISISENJUNAEM")
                {
                    //siis vajuta selles reas asuvat details nuppu
                    var rowelement = row.FindElement(By.Id("IndexActionsKinderGarten"));
                    var button = rowelement.FindElement(By.Id("IndexKinderGartenDelete"));
                    button.Click();
                    //pärast õiget vajutust, tsükkel katkestatakse
                    //arvuti tudub
                    Thread.Sleep(500);
                    break;
                }
            }
            var clickthisbutton = driver.FindElement(By.Id("Delete_Delete"));
            clickthisbutton.Click();

            int rowCount = table.FindElements(By.TagName("tr")).ToList().Count();
            Assert.True(rowCount < 2);
        }

        [Fact]
        public void Should_NavigateToCreate_AddKinderGarten_WithCorrectData_ReturnToIndex_Proper()
        {
            //ülesseade
            IWebDriver driver = SetupAndNavigateSIndex();

            //tegevus
            IWebElement createInIndex = driver.FindElement(By.Id("CreateInIndex"));
            createInIndex.Click();

            //sisestatavad andmed, siin tagastub ka kasutatud nimi
            string concreteGroupName = InsertKinderGartenData_ReturnGroupName(driver, false);

            IWebElement cu_CreateKinderGarten = driver.FindElement(By.Id("CU_CreateKinderGarten"));
            cu_CreateKinderGarten.Click();

            ////arvuti tudub
            //Thread.Sleep(1000);

            //andmete kogumine pärast reloadi
            var tableOfIndex = driver.FindElement(By.Id("IndexTable"));
            List<IWebElement> rowsOfTable = tableOfIndex.FindElements(By.TagName("tr")).ToList();
            rowsOfTable.RemoveAt(0);
            bool isObjectFound = false;
            string kinderGartenGroupNameData = "";
            string kinderGartenChildrenCountData = "";
            string kinderGartenNameData = "";
            string kinderGartenTeacherNameData = "";
            //IWebElement indexNameSpaceship = driver.FindElement(By.Id("IndexNameSpaceship"));
            //var spaceShipNameData = indexNameSpaceship.Text;
            //IWebElement indexTypeSpaceship = driver.FindElement(By.Id("IndexTypeSpaceship"));
            //var spaceShipTypeData = indexTypeSpaceship.Text;
            //IWebElement indexCrewSpaceship = driver.FindElement(By.Id("IndexCrewSpaceship"));
            //var spaceShipCrewData = indexCrewSpaceship.Text;
            bool doesItExist = false;
            foreach (var row in rowsOfTable)
            {
                if (row.FindElement(By.Id("IndexGroupNameSpaceship")).Text == thisRunGuid)
                {
                    doesItExist = true;
                    kinderGartenGroupNameData = thisRunGuid;
                    kinderGartenChildrenCountData = row.FindElement(By.Id("IndexChildrenCountKinderGarten")).Text;
                    kinderGartenKinderGartenNameData = row.FindElement(By.Id("IndexKinderGartenNameKinderGarten")).Text;
                    kinderGartenTeacherNameData = row.FindElement(By.Id("IndexTeacherNameKinderGarten")).Text;
                    break;

                }
            }


            //kui on doesitexist on true

            driver.Quit();
            //kontroll
            //Assert.True(concreteName == indexNameSpaceship.Text);
            Assert.True(true == doesItExist);
            Assert.Equal(spaceShipNameData, thisRunGuid);
            Assert.True(spaceShipTypeData == "i add ship type for spaceship");
            Assert.Equal(spaceShipCrewData, "12345");
            Assert.True((thisRunGuid == spaceShipNameData) && ("i add name for spaceship" == spaceShipTypeData) && ("12345" == spaceShipCrewData));
        }
    }
}
