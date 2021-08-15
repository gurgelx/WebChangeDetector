using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;

namespace WebChangeDetector
{
    class Program
    {
        static string TargetUrl = "https://swedroid.se/forum/threads/fyndtipstraden-amazon-se-inga-diskussioner.186347/";
        static int CheckIntervall = 1000 * 10; //1000ms = 1s x 60 = 1min
        static void Main(string[] args)
        {
            Console.WriteLine("Running checks on " + TargetUrl);
            var checker = new Checker(TargetUrl, ElementFinder, OnChangeDetected);
            checker.RunWithIntervall(CheckIntervall);
        }

        static string ElementFinder(RemoteWebDriver driver)
        {
            return driver
                .FindElement(By.ClassName("pageNavLinkGroup"))
                .FindElement(By.TagName("nav"))
                .FindElements(By.XPath("./a"))[3].Text;

        }

        static void OnChangeDetected(string newValue)
        {
            var lastPage = "https://swedroid.se/forum/threads/fyndtipstraden-amazon-se-inga-diskussioner.186347/page-" + newValue;
            Process.Start(new ProcessStartInfo(lastPage) { UseShellExecute = true });
        }
    }
}
