using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace WebChangeDetector
{
    public class Checker
    {
        private string lastValue = null;
        public string TargetUrl { get; private set; }
        public Action<string> OnDetectionCallback { get; private set; }
        public Func<RemoteWebDriver, string> ValueFinder {get; private set;}

        public Checker(string url, Func<RemoteWebDriver, string> valueFinder, Action<string> onDetectionCallback)
        {
            this.TargetUrl = url;
            this.OnDetectionCallback = onDetectionCallback;
            this.ValueFinder = valueFinder;
        }

        public void RunWithIntervall(int intervall)
        {
            while (true)
            {
                DoCheck();
                System.Threading.Thread.Sleep(intervall);
            }
        }

        void DoCheck()
        {
            var options = new ChromeOptions();
            options.AddArguments("headless");
            using (var driver = new ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(TargetUrl);
                var value = this.ValueFinder(driver);
                if (lastValue != null && !lastValue.Equals(value))
                {
                    this.OnDetectionCallback(value);
                }
                lastValue = value;                
            }
        }
    }
}
