﻿using System;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.DevTools;
using System.Threading;
using SeleniumExtras.WaitHelpers;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

namespace dawlance_test_automation
{
    [TestClass]
    public class dawlance
    {
        CultureInfo Info = new CultureInfo("tr-TR");
        ChromeOptions options = new ChromeOptions();
        ChromeDriverService service = ChromeDriverService.CreateDefaultService();
        IWebDriver Webdriver = null;
        IWebElement element = null;
        string orderNumber = null;
        string BasketLastPrice = null;
        double BasketLastPriceINT = 0;
        string HybrisStatus = null;
        string HybrisUnitPrice = null;
        double HybrisUnitPriceINT = 0;
        string HybrisDiscount = null;
        double HybrisDiscountINT = 0;
        double HybrisLastPrice = 0;
        double BasketLastPrice_M_CardINT = 0;
        string HybrisSAP_FI_No = null;
        string HybrisSAP_FI_No_2 = null;
        string Hybris_Payment_Mode = null;
        int product_type = 0; //1=payments 2=5000tl 3=499tl 4=%10 5=promo_paro 6=bundle 7=bundle
        string scrollElementIntoMiddle = "var viewPortHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);" + "var elementTop = arguments[0].getBoundingClientRect().top;" + "window.scrollBy(0, elementTop-(viewPortHeight/2));";


        public void _Start()
        {
            service.HideCommandPromptWindow = true;
            options.AddArgument("--start-maximized");
            options.AddArgument("--incognito");
            Webdriver = new ChromeDriver(service, options);
            Webdriver.Navigate().GoToUrl("https://dawlance-astra.c1m0wu3z2z-arcelikas1-s1-public.model-t.cc.commerce.ondemand.com/");
            _wait2();
        }

        //*******************************  User Operations Start

        //*******************************  User Operations End



        //*******************************  PLP Start

        //*******************************  PLP End



        //*******************************  PDP Start

        //*******************************  PLP End



        //*******************************  HomePage Start

        [TestMethod]
        public void HomePage_01_Search()
        {
            _Start();
            Webdriver.FindElement(By.XPath("//div[@class='placeholder']")).Click();
            Sleep();
            Webdriver.FindElement(By.Id("searchText")).SendKeys("Refrigerators");
            Webdriver.FindElements(By.CssSelector("svg[class='icon icon-search']"))[2].Click();
            _wait2();
            int COUNT_PROD = Webdriver.FindElement(By.XPath("//div[@class='products productgridcomponent-page']")).FindElements(By.CssSelector("div[class='prd product-item']")).Count();
            if (COUNT_PROD > 0)
            {
                Sleep();
                Webdriver.Quit();
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HomePage_02_Technologies()
        {
            _Start();
            Webdriver.FindElement(By.CssSelector("a[href='/technologies']")).Click();
            _wait2();
            int COUNT_ITEM = Webdriver.FindElement(By.CssSelector("div[class='tab-content banner-list list-col-3']")).FindElements(By.CssSelector("div[class='banner bnr-static bnr-left js-filter-media-item']")).Count();
            if (COUNT_ITEM > 0)
            {
                Sleep();
                Webdriver.Quit();
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HomePage_03_Promotions()
        {
            _Start();
            Webdriver.FindElement(By.CssSelector("a[href='/promotions']")).Click();
            _wait2();
            int COUNT_ITEM = Webdriver.FindElement(By.CssSelector("div[class='tab-content']")).FindElements(By.CssSelector("div[data-category='all']")).Count();
            if (COUNT_ITEM > 0)
            {
                element = Webdriver.FindElements(By.CssSelector("a[title='Shop Now']"))[0];
                ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
                element.Click();
                _wait2();
                Webdriver.Quit();
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HomePage_04_Store()
        {
            _Start();
            Webdriver.FindElement(By.CssSelector("a[href='/store-finder']")).Click();
            _wait2();
            Webdriver.FindElement(By.Id("cityCode")).FindElement(By.CssSelector("option[value='926']")).Click();
            Sleep();
            Webdriver.FindElement(By.Id("townCode")).FindElement(By.CssSelector("option[value='926.18']")).Click();
            Sleep();
            Webdriver.FindElement(By.Id("neighborhood")).FindElement(By.CssSelector("option[value='152593']")).Click();
            Sleep();
            Webdriver.FindElement(By.CssSelector("a[title='Search Store']")).Click();
            _wait2();
            element = Webdriver.FindElement(By.CssSelector("div[class='srv-list']"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            int COUNT_STORE = element.FindElements(By.CssSelector("div[class='srv-item srv-item-pk']")).Count();
            if (COUNT_STORE > 0)
            {
                Sleep();
                Webdriver.Quit();
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HomePage_05_Support()
        {
            _Start();
            Webdriver.FindElement(By.CssSelector("a[href='/support']")).Click();
            _wait2();
            element = Webdriver.FindElements(By.Id("searchText"))[1];
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            element.Click();
            element.SendKeys("Refrigerators");
            Sleep();
            element.SendKeys(Keys.Enter);
            _wait2();
            int RELEVANT_COUNT = Webdriver.FindElement(By.Id("articles")).FindElements(By.CssSelector("div[class='spt-list-item filter-code-Refrigerators']")).Count();
            if (RELEVANT_COUNT > 0)
            {
                Webdriver.FindElement(By.CssSelector("button[href='#popular-articles']")).Click();
                Sleep();
                int POPULAR_COUNT = Webdriver.FindElement(By.Id("popular-articles")).FindElements(By.CssSelector("div[class='spt-list-item filter-code-populer']")).Count();
                if (POPULAR_COUNT > 0)
                {
                    Webdriver.FindElement(By.CssSelector("a[href='/support']")).Click();
                    _wait2();
                }
            }
            string CATEGORIES = Webdriver.FindElements(By.CssSelector("ul[class='yCmsContentSlot ul-clear category-list']"))[0].Text.Trim();
            if (CATEGORIES == "Air Conditioners\r\nWater Dispensers\r\nSmall Domestic Appliances\r\nCooking Appliances\r\nWashing Machines\r\nRefrigerators and freezers")
            {
                element = Webdriver.FindElement(By.CssSelector("a[href='/authorized-services']"));
                ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
                Sleep();
                element.Click();
                _wait2();
                Webdriver.FindElement(By.Id("cityCode")).FindElement(By.CssSelector("option[value='926']")).Click();
                Sleep();
                Webdriver.FindElement(By.Id("townCode")).FindElement(By.CssSelector("option[value='926.9']")).Click();
                Sleep();
                Webdriver.FindElement(By.Id("neighborhood")).FindElement(By.CssSelector("option[value='152581']")).Click();
                Sleep();
                Webdriver.FindElement(By.CssSelector("a[title='Search']")).Click();
                _wait2();
                element = Webdriver.FindElement(By.CssSelector("div[class='srv-list']"));
                ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
                int COUNT_STORE = element.FindElements(By.CssSelector("div[class='srv-item']")).Count();
                if (COUNT_STORE > 0)
                {
                    Sleep();
                    Webdriver.Quit();
                }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void HomePage_06_Products_01_Freezers()
        {
            _Start();
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            Actions ac = new Actions(Webdriver);
            element = Webdriver.FindElement(By.CssSelector("a[href='/refrigerators-and-freezers']"));
            ac.MoveToElement(element).Perform();
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
                _titles.Add(title);
                element = Webdriver.FindElement(By.CssSelector("a[title='" + _titles[i] + "']"));
                ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
                ac.Reset();
            }
            for (int j = COUNT; j > 0; j--)
            {
                string error = null;
                string error_2 = null;
                Webdriver.SwitchTo().Window(Webdriver.WindowHandles[j]);
                error_2 = Webdriver.Title;
                if (error_2 == "Server Error")
                {
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Assert.Fail();
                    }
                }
                catch (Exception)
                {
                    _wait2();
                    Webdriver.Close();
                }
            }
            Webdriver.Quit();
        }


        [TestMethod]
        public void HomePage_06_Products_02_WashingMachines()
        {
            _Start();
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            Actions ac = new Actions(Webdriver);
            element = Webdriver.FindElement(By.CssSelector("a[href='/washing-machines']"));
            ac.MoveToElement(element).Perform();
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
                _titles.Add(title);
                element = Webdriver.FindElement(By.CssSelector("a[title='" + _titles[i] + "']"));
                ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
                ac.Reset();
            }
            for (int j = COUNT; j > 0; j--)
            {
                string error = null;
                string error_2 = null;
                Webdriver.SwitchTo().Window(Webdriver.WindowHandles[j]);
                error_2 = Webdriver.Title;
                if (error_2 == "Server Error")
                {
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Assert.Fail();
                    }
                }
                catch (Exception)
                {
                    _wait2();
                    Webdriver.Close();
                }
            }
            Webdriver.Quit();
        }


        [TestMethod]
        public void HomePage_06_Products_03_Cooking()
        {
            _Start();
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            Actions ac = new Actions(Webdriver);
            element = Webdriver.FindElement(By.CssSelector("a[href='/cooking-appliances']"));
            ac.MoveToElement(element).Perform();
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
                _titles.Add(title);
                element = Webdriver.FindElement(By.CssSelector("a[title='" + _titles[i] + "']"));
                ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
                ac.Reset();
            }
            for (int j = COUNT; j > 0; j--)
            {
                string error = null;
                string error_2 = null;
                Webdriver.SwitchTo().Window(Webdriver.WindowHandles[j]);
                error_2 = Webdriver.Title;
                if (error_2 == "Server Error")
                {
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Assert.Fail();
                    }
                }
                catch (Exception)
                {
                    _wait2();
                    Webdriver.Close();
                }
            }
            Webdriver.Quit();
        }


        [TestMethod]
        public void HomePage_06_Products_04_Dishwashers()
        {
            _Start();
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            Actions ac = new Actions(Webdriver);
            element = Webdriver.FindElement(By.CssSelector("a[href='/dishwashers']"));
            ac.MoveToElement(element).Perform();
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
                _titles.Add(title);
                element = Webdriver.FindElement(By.CssSelector("a[title='" + _titles[i] + "']"));
                ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
                ac.Reset();
            }
            for (int j = COUNT; j > 0; j--)
            {
                string error = null;
                string error_2 = null;
                Webdriver.SwitchTo().Window(Webdriver.WindowHandles[j]);
                error_2 = Webdriver.Title;
                if (error_2 == "Server Error")
                {
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Assert.Fail();
                    }
                }
                catch (Exception)
                {
                    _wait2();
                    Webdriver.Close();
                }
            }
            Webdriver.Quit();
        }


        [TestMethod]
        public void HomePage_06_Products_05_AirCondition()
        {
            _Start();
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            Actions ac = new Actions(Webdriver);
            element = Webdriver.FindElement(By.CssSelector("a[href='/air-conditioners']"));
            ac.MoveToElement(element).Perform();
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
                _titles.Add(title);
                element = Webdriver.FindElement(By.CssSelector("a[title='" + _titles[i] + "']"));
                ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
                ac.Reset();
            }
            for (int j = COUNT; j > 0; j--)
            {
                string error = null;
                string error_2 = null;
                Webdriver.SwitchTo().Window(Webdriver.WindowHandles[j]);
                error_2 = Webdriver.Title;
                if (error_2 == "Server Error")
                {
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Assert.Fail();
                    }
                }
                catch (Exception)
                {
                    _wait2();
                    Webdriver.Close();
                }
            }
            Webdriver.Quit();
        }


        [TestMethod]
        public void HomePage_06_Products_06_WaterDispenser()
        {
            _Start();
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            Actions ac = new Actions(Webdriver);
            element = Webdriver.FindElement(By.CssSelector("a[href='/water-dispensers']"));
            ac.MoveToElement(element).Perform();
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
                _titles.Add(title);
                element = Webdriver.FindElement(By.CssSelector("a[title='" + _titles[i] + "']"));
                ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
                ac.Reset();
            }
            for (int j = COUNT; j > 0; j--)
            {
                string error = null;
                string error_2 = null;
                Webdriver.SwitchTo().Window(Webdriver.WindowHandles[j]);
                error_2 = Webdriver.Title;
                if (error_2 == "Server Error")
                {
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Assert.Fail();
                    }
                }
                catch (Exception)
                {
                    _wait2();
                    Webdriver.Close();
                }
            }
            Webdriver.Quit();
        }


        [TestMethod]
        public void HomePage_06_Products_07_SmallDomesticAppliances()
        {
            _Start();
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            Actions ac = new Actions(Webdriver);
            element = Webdriver.FindElement(By.CssSelector("a[href='/small-domestic-appliances']"));
            ac.MoveToElement(element).Perform();
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
                _titles.Add(title);
                element = Webdriver.FindElement(By.CssSelector("a[title='" + _titles[i] + "']"));
                ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
                ac.Reset();
            }
            for (int j = COUNT; j > 0; j--)
            {
                string error = null;
                string error_2 = null;
                Webdriver.SwitchTo().Window(Webdriver.WindowHandles[j]);
                error_2 = Webdriver.Title;
                if (error_2 == "Server Error")
                {
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Assert.Fail();
                    }
                }
                catch (Exception)
                {
                    _wait2();
                    Webdriver.Close();
                }
            }
            Webdriver.Quit();
        }



        [TestMethod]
        public void HomePage_06_Products_08_PersonelCare()
        {
            _Start();
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            Actions ac = new Actions(Webdriver);
            element = Webdriver.FindElement(By.CssSelector("a[href='/personal-care']"));
            ac.MoveToElement(element).Perform();
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT - 4; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
                _titles.Add(title);
                element = Webdriver.FindElement(By.CssSelector("a[title='" + _titles[i] + "']"));
                ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
                ac.Reset();
            }
            for (int j = COUNT - 4; j > 0; j--)
            {
                string error = null;
                string error_2 = null;
                Webdriver.SwitchTo().Window(Webdriver.WindowHandles[j]);
                error_2 = Webdriver.Title;
                if (error_2 == "Server Error")
                {
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Assert.Fail();
                    }
                }
                catch (Exception)
                {
                    _wait2();
                    Webdriver.Close();
                }
            }
            Webdriver.Quit();
        }


        [TestMethod]
        public void HomePage_07_Banner()
        {
            _Start();
            _wait2();
            //Main Slider Right
            Webdriver.FindElements(By.XPath("//div[@aria-label='Next slide']"))[0].Click();
            Sleep();
            //Main Slider Left
            Webdriver.FindElements(By.XPath("//div[@aria-label='Previous slide']"))[0].Click();
            Sleep();
            string title = Webdriver.FindElements(By.ClassName("bnr-title"))[9].Text.Trim();
            if (title == "Dawlance")
            {
                element = Webdriver.FindElement(By.CssSelector("a[title='Discover']"));
                ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
                element.Click();
                _wait2();
                Webdriver.Quit();
            }
            else
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void HomePage_08_SliderCategories()
        {
            _Start();
            Actions ac = new Actions(Webdriver);
            element = Webdriver.FindElements(By.XPath("//div[@aria-label='Next slide']"))[2];
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            Webdriver.FindElements(By.XPath("//div[@aria-label='Next slide']"))[2].Click();
            Sleep();
            //Main Slider Left
            Webdriver.FindElements(By.XPath("//div[@aria-label='Previous slide']"))[2].Click();
            Sleep();
            element = Webdriver.FindElement(By.Id("category-swiper")).FindElement(By.CssSelector("div[data-order='0']"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            int COUNT = Webdriver.FindElement(By.Id("category-swiper")).FindElements(By.CssSelector("div[data-order]")).Count();
            if (COUNT > 0)
            {
                for (int i = 0; i < COUNT; i++)
                {
                    element = Webdriver.FindElement(By.Id("category-swiper")).FindElement(By.CssSelector("div[data-order='" + i + "']"));
                    ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
                    ac.Reset();
                }
                for (int j = COUNT; j > 0; j--)
                {
                    string error = null;
                    string error_2 = null;
                    Webdriver.SwitchTo().Window(Webdriver.WindowHandles[j]);
                    error_2 = Webdriver.Title;
                    if (error_2 == "Server Error")
                    {
                        Assert.Fail();
                    }
                    try
                    {
                        error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                        if (error.Contains("404"))
                        {
                            Assert.Fail();
                        }
                    }
                    catch (Exception)
                    {
                        _wait2();
                        Webdriver.Close();
                    }
                }
                Webdriver.Quit();
            }
            else
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void HomePage_09_PopularProducts()
        {
            _Start();
            Actions ac = new Actions(Webdriver);
            element = Webdriver.FindElement(By.Id("popular-products"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            int COUNT = Webdriver.FindElement(By.Id("popular-products")).FindElements(By.CssSelector("div[data-order]")).Count();
            if (COUNT > 0)
            {
                for (int i = 0; i < COUNT -7; i++)
                {
                    element = Webdriver.FindElement(By.Id("popular-products")).FindElements(By.CssSelector("a[title='Review']"))[i];
                    ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
                    ac.Reset();
                }
                for (int j = COUNT -7; j > 0; j--)
                {
                    string error = null;
                    string error_2 = null;
                    Webdriver.SwitchTo().Window(Webdriver.WindowHandles[j]);
                    error_2 = Webdriver.Title;
                    if (error_2 == "Server Error")
                    {
                        Assert.Fail();
                    }
                    try
                    {
                        error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                        if (error.Contains("404"))
                        {
                            Assert.Fail();
                        }
                    }
                    catch (Exception)
                    {
                        _wait2();
                        Webdriver.Close();
                    }
                }
                Webdriver.Quit();
            }
            else
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void HomePage_10_BestSeller()
        {
            _Start();
            Actions ac = new Actions(Webdriver);
            element = Webdriver.FindElements(By.CssSelector("a[title='Review']"))[10];
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            for (int i = 10; i < 13; i++)
            {
                element = Webdriver.FindElements(By.CssSelector("a[title='Review']"))[i];
                ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
                ac.Reset();
            }
            for (int j = 3; j > 0; j--)
            {
                string error = null;
                string error_2 = null;
                Webdriver.SwitchTo().Window(Webdriver.WindowHandles[j]);
                error_2 = Webdriver.Title;
                if (error_2 == "Server Error")
                {
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Assert.Fail();
                    }
                }
                catch (Exception)
                {
                    _wait2();
                    Webdriver.Close();
                }
            }
            Webdriver.Quit();
        }

        //*******************************  HomePage End



        //*******************************  Payment Start

        //*******************************  Payment End



        //*******************************  BasketPage Start

        //*******************************  BasketPage End



        //*******************************  Discount Start

        //*******************************  Discount End




        //*******************************  Methods



        public void cookie()
        {
            WebDriverWait wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("onetrust-accept-btn-handler")));
            Webdriver.FindElement(By.Id("onetrust-accept-btn-handler")).Click();
        }


        public void _wait2()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Webdriver;
            string result = js.ExecuteScript("return document.readyState").ToString();
            do
            {
                Sleep();
            }
            while (result != "complete");
        }

        public void Sleep()
        {
            Random random = new Random();
            int _random = random.Next(4000, 6000);
            Thread.Sleep(_random);
        }
    }
}
