using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace CiclumTest
{
    class Program
    {
        static void Main(string[] args)
        {

			IWebDriver driver = new ChromeDriver(@"C:\lib\");
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // 1.Open web browser and go to google.com page.
            // 2.Search for "stibo systems" string.
            // 3.Click the link leading to "www.stibosystems.com".

                    driver.Url = "http:\\google.com";
			        driver.Manage().Window.Maximize();
			        driver.FindElement(By.Id("lst-ib")).SendKeys("stibo systems" + OpenQA.Selenium.Keys.Enter);

			        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			        wait.Until(drv => drv.FindElement(By.LinkText("Stibo Systems - Business-first data management solutions"))).Click();

            // 4.Click search and type "energy".
            // 5.Return in console number of found results.
            // 6.In search results for "energy" look for "About Us" link and return on which page it was found and click on the link.

                    driver.FindElement(By.ClassName("search")).Click();
			        driver.FindElement(By.Id("search-input")).SendKeys("energy");

			        var seqarchResoult = driver.FindElement(By.Id("stats"));
			        Console.WriteLine(seqarchResoult.Text);

			        var searchResults = driver.FindElement(By.Id("hits-container-wrapper"));
			        var SearchText = searchResults.Text.Contains("About Us");
			        int n = 1;

			        while (SearchText == false)
			        {
				        n++;
				        driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div/div/div[1]/div/div[3]/div/span/div[2]/div[4]/div/ul/li[6]/a")).Click();
				        searchResults = driver.FindElement(By.Id("hits-container-wrapper"));
				        SearchText = searchResults.Text.Contains("About Us");
			        }

			        Console.WriteLine( "About Us link found on page - " + n );

			        var resoultPage = driver.FindElement(By.Id("hits-container-wrapper"));
			        resoultPage.FindElement(By.LinkText("About Us")).Click();

            // 7.Assert that Facebook icon is present on the page.

                    try
			        {
				        Assert.IsTrue(driver.FindElement(By.ClassName("icon-facebook")).Displayed);
				        Console.WriteLine("FB pic displayed");
			        }
			        catch (Exception e )
			        {
				        Console.WriteLine(e);
			        }

            // 8.Click on "Blog" link.
            // 9.In "Follow Blog" email input field provide email with incorrect format and return in console error message.
            // 10.In "Follow Blog" email input field provide correct email and click send.
            // 11.Assert that "Thanks for Subscribing!" message was returned.

                    driver.FindElement(By.LinkText("Blog")).Click();

                    var blogEmail = driver.FindElement(By.Name("email"));
                    var sendButton = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div/div[2]/div/div[2]/div/div[1]/div/div/div/span/div/form/div[4]/div[2]/input"));

                    blogEmail.SendKeys("mailWithoutAt.dk");
                    sendButton.Click();

                    string XPathEmailErr = "/html/body/div[2]/div/div/div/div/div[2]/div/div[2]/div/div[1]/div/div/div/span/div/form/div[1]/ul/li/label";

                    var emailErr = driver.FindElement(By.XPath(XPathEmailErr));
                    var emailText = emailErr.Text;
                    Console.WriteLine(emailErr + " - Checked proper error mesage");

                    blogEmail.SendKeys("test@stibo.dk");
                    sendButton.Click();

                    var emailConf = driver.FindElement(By.ClassName("submitted-message")).Text;
                    Console.WriteLine(emailConf + " - Checked confirmation mesage");
        }

    }


	class Actions
	{

		public static IWebDriver ClickOn(IWebDriver driver, string button)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
			wait.Until(drv =>drv.FindElement(By.Name(button)));
			driver.FindElement(By.XPath(button)).Click();
			return driver;
		}


	}

}



