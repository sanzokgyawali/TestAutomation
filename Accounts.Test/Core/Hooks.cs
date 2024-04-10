using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using ITsutra.Api.CommonStepDefs;
using ITsutra.Api.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace Accounts.Test.Core
{
    [Binding]
    public class Hooks
    {
        public static AventStack.ExtentReports.ExtentReports extent = new AventStack.ExtentReports.ExtentReports();
        public static ExtentTest test;
        public static ExtentTest scenario;

        [BeforeScenario]
        public void BeforeScenario()
        {
            test = extent.CreateTest(Regex.Replace(TestContext.CurrentContext.Test.Name, "(\\B[A-Z])", " $1"));
            scenario = test.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        [AfterStep]
        public void InsertScenarioAfterStep()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepType + " " + ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(stepType + " " + ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(stepType + " " + ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(stepType + " " + ScenarioStepContext.Current.StepInfo.Text);
            }
            else if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepType + " " + ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                else if (stepType == "When")
                    scenario.CreateNode<When>(stepType + " " + ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(stepType + " " + ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            }
            foreach(var testLog in TestLog.output)
            {
                testLog.Type = testLog.Type.ToLower();
                if(testLog.Type=="pass")
                    test.Log(Status.Pass, testLog.Output);
                if (testLog.Type == "fail")
                    test.Log(Status.Fail, testLog.Output);
                else
                    test.Log(Status.Info, testLog.Output);

            }
            TestLog.output.Clear();
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var htmlReporter = new ExtentHtmlReporter(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Reports\" + DateTime.Now.Date.ToString() + " - ExtentReport.html");
            htmlReporter.Config.DocumentTitle = "ITSutra";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent.AttachReporter(htmlReporter);
            CommonApiStepDefs.AuthorizeApi("No");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush();
        }
    }
}