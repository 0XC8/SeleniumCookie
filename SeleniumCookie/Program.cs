using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace SeleniumCookie
{
    class Program
    {
        /* NuGet：
         *   -- Selenium.WebDriver
         *   -- Selenium.WebDriverBackedSelenium
         *   -- Selenium.WebDriver.IEDriver（一般的网站推荐这个，如果网站不兼容IE，则采用Firefox,Chrome）
         *   -- // Selenium.WebDriver.XXDriver (支持IE,Chrome,Firefox...，选择对应的驱动即可. 如果是chrome，要注意chrome的版本与驱动的版本搭配)
         *
         *  根据项目经验，推荐使用chrome驱动，因为ie浏览器不太稳定。
         *
         */
        static void Main(string[] args)
        {
            Console.WriteLine("\r\n>> 打开浏览器...");
            Cookie[] cookies = GetCookies();
            Console.WriteLine("\r\n>> 读取Cookie：");
            if (cookies == null || cookies.Length == 0)
            {
                Console.WriteLine("No Cookie");
            }
            else
            {
                foreach (var cookie in cookies)
                {
                    Console.WriteLine($"{cookie.Domain.PadLeft(20,' ')}\t{cookie.Name.PadLeft(20, ' ')}\t{cookie.Value.PadLeft(20, ' ')}");
                }
            }
            Console.WriteLine(">>回车退出");
            Console.ReadLine();
        }

        private static Cookie[] GetCookies()
        {
            // remark: 如果使用Chrome浏览器，则使用类‘ChromeDriver’

            using (IWebDriver webDriver = new InternetExplorerDriver())
            {
                webDriver.Navigate().GoToUrl("https://www.baidu.com");
                /*
                 * 运行权限注意：
                 *   此处需要管理员权限运行，否则无法获取到打开后的浏览器窗口对象了。
                 *   可查看程序清单文件‘app.manifest’。
                 */

                /* 经过测试，这里无需等待，Selenium好像会在网页load完成才继续执行
                IWebElement inputkw = null;
                while (inputkw == null)
                {
                    inputkw = webDriver.FindElement(By.XPath("//input[@id='kw']"));
                    // inputkw.SendKeys("12306"); // 赋值
                    Console.WriteLine("waiting...");
                    Thread.Sleep(200);
                }
                 */

                var cookies = webDriver.Manage().Cookies.AllCookies;
                webDriver.Close();
                webDriver.Quit();
                return cookies?.ToArray();
            }
        }
    }
}
