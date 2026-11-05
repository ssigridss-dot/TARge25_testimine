using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TARge25Shop.SeleniumTesting
{
    [TestCaseOrderer("TARge25Shop.SeleniumTesting.KinderGartenPriorityOrderer", "TARge25Shop.SeleniumTesting")]
    public class KinderGartenPriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
            where TTestCase : ITestCase
        {
            return testCases.OrderBy(test =>
            {
                var method = test.TestMethod.Method.GetCustomAttributes(typeof(OrderAttribute)).FirstOrDefault();
                if (method == null)
                {
                    return int.MaxValue;
                }
                else
                {
                    return method.GetNamedArgument<int>(nameof(OrderAttribute.Value));
                }
            }).ToList();
        }
    }
    public class KinderGartenFrontendTests
    {
        public static string thisRunGuid { get; set; }
        [Fact]
        [Order(1)]
        // See test kontrollib Create funktsiooniga lasteaia loomist ja et see kuvatakse
        // pärast õigete andmetega index lehel.
        public void Should_NavigateToCreate_AddKinderGarten_WithCorrectData_ReturnToIndex()
        {
            // ülesseade
            IWebDriver driver = SetupAndNavigateKinderGartenIndex();

            // tegevus
            IWebElement createInIndex = driver.FindElement(By.Id("CreateInIndex"));
            createInIndex.Click();

            thisRunGuid = Guid.NewGuid().ToString();
            InsertKinderGartenData(driver, thisRunGuid, false);
            IWebElement cu_CreateKinderGarten = driver.FindElement(By.Id("CU_CreateKinderGarten"));
            cu_CreateKinderGarten.Click();
            Thread.Sleep(1000);

            var table = driver.FindElement(By.Id("IndexTable"));
            List<IWebElement> rows = table.FindElements(By.TagName("tr")).ToList();
            rows.RemoveAt(0);

            bool doesItExist = false;
            string groupNameData = "";
            string childrenCountData = "";
            string kinderGartenNameData = "";
            string teacherNameData = "";

            foreach (var row in rows)
            {
                if (row.FindElement(By.Id("IndexNameKinderGarten")).Text == thisRunGuid)
                {
                    doesItExist = true;
                    groupNameData = row.FindElement(By.Id("IndexGroupNameKinderGarten")).Text;
                    childrenCountData = row.FindElement(By.Id("IndexChildrenCountKinderGarten")).Text;
                    kinderGartenNameData = row.FindElement(By.Id("IndexNameKinderGarten")).Text;
                    teacherNameData = row.FindElement(By.Id("IndexTeacherNameKinderGarten")).Text;
                    break;
                }
            }
            driver.Quit();
            // kontroll
            Assert.True(doesItExist);
            Assert.Equal("Sipsikud", groupNameData);
            Assert.Equal("20", childrenCountData);
            Assert.Equal(thisRunGuid, kinderGartenNameData);
            Assert.Equal("Aino", teacherNameData);
        }

        [Fact]
        [Order(2)]
        // See test kontrollib, et lasteaedade details nupp töötab ja eelmises testis loodud
        // lasteaia andmed kuvatakse detailide lehel õigesti.
        public void Should_NavigateToDetails_OfAKinderGarten_WithCorrectData()
        {
            // ülesseade
            IWebDriver driver = SetupAndNavigateKinderGartenIndex();

            // otsime üles kõik tabeli read
            var table = driver.FindElement(By.Id("IndexTable"));
            List<IWebElement> rows = table.FindElements(By.TagName("tr")).ToList();
            rows.RemoveAt(0);

            // käime läbi kõik read ja otsime üles eelmise testiga loodud lasteaia
            foreach (var row in rows)
            {
                var locatedelement = row.FindElement(By.Id("IndexNameKinderGarten"));
                if (locatedelement.Text == thisRunGuid)
                {
                    // leiab ja vajutab Detaili nuppu
                    var button = row.FindElement(By.Id("IndexKinderGartenDetails"));
                    button.Click();
                    Thread.Sleep(1000);
                    break;
                }
            }

            // andmete kogumine pärast reloadi
            IWebElement details_KinderGartenId = driver.FindElement(By.Id("Details_KinderGartenId"));
            var details_id = details_KinderGartenId.Text;

            IWebElement details_KinderGartenGroupName = driver.FindElement(By.Id("Details_GroupNameKinderGarten"));
            var details_groupName = details_KinderGartenGroupName.Text;

            IWebElement details_KinderGartenChildrenCount = driver.FindElement(By.Id("Details_ChildrenCountKinderGarten"));
            var details_childrenCount = details_KinderGartenChildrenCount.Text;

            IWebElement details_KinderGartenName = driver.FindElement(By.Id("Details_KinderGartenName"));
            var details_kinderGartenName = details_KinderGartenName.Text;

            IWebElement details_KinderGartenTeacherName = driver.FindElement(By.Id("Details_TeacherNameKinderGarten"));
            var details_teacherName = details_KinderGartenTeacherName.Text;

            driver.Quit();

            // kontroll
            Assert.NotNull(details_id);
            Assert.Equal("Sipsikud", details_groupName);
            Assert.Equal("20", details_childrenCount);
            Assert.Equal(thisRunGuid, details_kinderGartenName);
            Assert.Equal("Aino", details_teacherName);
        }

        [Fact]
        [Order(3)]
        // See test kontrollib, et Update töötab. Tabelis oleva lasteaia andmed
        // muudetakse ja kuvatakse õigesti index lehel.
        public void Should_UpdateKinderGarten_WithNewData()
        {
            // ülesseade
            IWebDriver driver = SetupAndNavigateKinderGartenIndex();

            // otsime üles kõik tabeli read
            var table = driver.FindElement(By.Id("IndexTable"));
            List<IWebElement> rows = table.FindElements(By.TagName("tr")).ToList();
            rows.RemoveAt(0);

            // käime läbi kõik read
            foreach (var row in rows)
            {
                var locatedelement = row.FindElement(By.Id("IndexNameKinderGarten"));
                if (locatedelement.Text == thisRunGuid)
                {
                    // leiab ja vajutab Update nuppu
                    var button = row.FindElement(By.Id("IndexKinderGartenUpdate"));
                    button.Click();
                    Thread.Sleep(1000);
                    break;
                }
            }

            // uued andmed
            InsertKinderGartenData(driver, "Mesilane", true);
            IWebElement cu_UpdateKinderGarten = driver.FindElement(By.Id("CU_UpdateKinderGarten"));
            cu_UpdateKinderGarten.Click();
            Thread.Sleep(1000);

            // andmete kogumine pärast reloadi
            IWebElement details_KinderGartenGroupName = driver.FindElement(By.Id("Details_GroupNameKinderGarten"));
            var details_groupName = details_KinderGartenGroupName.Text;

            IWebElement details_KinderGartenChildrenCount = driver.FindElement(By.Id("Details_ChildrenCountKinderGarten"));
            var details_childrenCount = details_KinderGartenChildrenCount.Text;

            IWebElement details_KinderGartenName = driver.FindElement(By.Id("Details_KinderGartenName"));
            var details_kinderGartenName = details_KinderGartenName.Text;

            IWebElement details_KinderGartenTeacherName = driver.FindElement(By.Id("Details_TeacherNameKinderGarten"));
            var details_teacherName = details_KinderGartenTeacherName.Text;

            driver.Quit();

            // kontroll
            Assert.Equal("Mürakarud", details_groupName);
            Assert.Equal("25", details_childrenCount);
            Assert.Equal("Mesilane", details_kinderGartenName);
            Assert.Equal("Kati", details_teacherName);
        }

        private IWebDriver SetupAndNavigateKinderGartenIndex()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "https://localhost:7227/";
            IWebElement kinderGartenLink = driver.FindElement(By.LinkText("KinderGarten"));
            kinderGartenLink.Click();

            return driver;
        }
        private void InsertKinderGartenData(IWebDriver driver, string name, bool isUpdate)
        {
            IWebElement groupName = driver.FindElement(By.Id("CU_GroupNameKinderGarten"));
            groupName.Clear();
            groupName.SendKeys(isUpdate ? "Mürakarud" : "Sipsikud");
            IWebElement childrenCount = driver.FindElement(By.Id("CU_ChildrenCountKinderGarten"));
            childrenCount.Clear();
            childrenCount.SendKeys(isUpdate ? "25" : "20");
            IWebElement kinderGartenName = driver.FindElement(By.Id("CU_KinderGartenNameKinderGarten"));
            kinderGartenName.Clear();
            kinderGartenName.SendKeys(name);
            IWebElement teacherName = driver.FindElement(By.Id("CU_TeacherNameKinderGarten"));
            teacherName.Clear();
            teacherName.SendKeys(isUpdate ? "Kati" : "Aino");
        }
    }
}