using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace YahooSmokeTest
{
    [TestClass]
    public class UnitTest1
    {

        IWebDriver driver;



        ExtentHtmlReporter reporter = new ExtentHtmlReporter("C:\\Users\\Satyanarayan\\source\\git\\YahooSmokeTest\\YahooSmokeTest\\Reports\\prod\\" + FunctionLibrary.Genaratedate() + "\\yahoo.html");
        ExtentReports extent = new ExtentReports();


        [TestMethod]
      
        public void prod() 
        {

            IWebDriver driver = new ChromeDriver();


            extent.AttachReporter(reporter);
            var test = extent.CreateTest("yahoolocal");

            try
            {
              

                driver.Url = "https://cricket.yahoo.net";

                driver.Manage().Window.Maximize();



                for (int i = 2; i <= 11; i++)
                {

                    string title = driver.FindElement(By.XPath("//*[@class='site-nav']/ul/li[" + i + "]/a")).GetAttribute("title");


                    string actTitle = FunctionLibrary.ReadDataExcel(1, 1, i);

                    title.Contains(actTitle);

                    test.Log(Status.Info, "Title verified");

                    FunctionLibrary.waitForElement(driver, "//*[@class='site-nav']/ul/li[" + i + "]");


                    FunctionLibrary.clickAction(driver, "//*[@class='site-nav']/ul/li[" + i + "]", "xpath");



                    Console.WriteLine(title);

                   

                    test.Log(Status.Pass, title  +  "Test case Passsed");



                    if (title.Contains("Series"))
                    {


                        driver.Navigate().GoToUrl("https://cricket.yahoo.net/series/big-bash-league-2019-20-1182");

                        Thread.Sleep(2000);

                        for (int j = 2; j <= 10; j++)
                        {
                            FunctionLibrary.waitForElement(driver, "//*[@class='quick-links-menu swiper-container']/div/a[" + j + "]");

                            FunctionLibrary.clickAction(driver, "//*[@class='quick-links-menu swiper-container']/div/a[" + j + "]", "xpath");

                            test.Log(Status.Pass, driver.Title +  "Test case Passsed");


                            string expSerTitle = driver.Title;
                                
                            string actSerTitile = FunctionLibrary.ReadDataExcel(1, 2, j);

                            actSerTitile.Contains(expSerTitle);

                            test.Log(Status.Info, "Title verified");

                            Console.WriteLine(driver.Title);



                        }


                    }




                }
                driver.Close();
            }



            catch(Exception)
            {

                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();

                ss.SaveAsFile(@"C:\Users\Satyanarayan\source\git\YahooSmokeTest\YahooSmokeTest\Screenshot\\" + FunctionLibrary.Genaratedate()+ ".png");

                test.Log(Status.Fail,driver.Title + "Test case failed");
            }

           

            extent.Flush();

        }


      
    }
    
}
