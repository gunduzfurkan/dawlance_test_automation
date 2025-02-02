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

        [TestMethod]
        public void User_01_Login()
        {
            _Start();
            Webdriver.FindElement(By.CssSelector("a[href='/login']")).Click();
            _wait2();
            Webdriver.FindElement(By.Id("j_username")).SendKeys("grundigtest@outlook.com");
            Webdriver.FindElement(By.Id("j_password")).SendKeys("12345Grundig");
            element = Webdriver.FindElement(By.Id("g-recaptcha-btn-login"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            element.Click();
        }



        [TestMethod]
        public void User_02_Info()
        {
            User_01_Login();
            Sleep();
            _wait2();
            Webdriver.FindElement(By.XPath("//a[@data-selector='user-name']")).Click();
            _wait2();
            Webdriver.FindElement(By.Id("name")).Clear();
            Webdriver.FindElement(By.Id("name")).SendKeys("Test");
            Webdriver.FindElement(By.Id("surname")).Clear();
            Webdriver.FindElement(By.Id("surname")).SendKeys("Test");
            Webdriver.FindElement(By.Id("gender")).Click();
            Webdriver.FindElement(By.Id("gender")).FindElement(By.CssSelector("option[value='MALE']")).Click();
            element = Webdriver.FindElement(By.Id("birthDate"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            element.Clear();
            element.SendKeys("12/10/1978");
            Webdriver.FindElement(By.Id("phoneNumber")).Clear();
            Webdriver.FindElement(By.Id("phoneNumber")).SendKeys("000000002");
            Webdriver.FindElement(By.Id("g-recaptcha-btn-profile")).Click();
            _wait2();
            Webdriver.Quit();
        }



        [TestMethod]
        public void User_03_AddAddress()
        {
            User_01_Login();
            Sleep();
            _wait2();
            Webdriver.FindElement(By.XPath("//a[@data-selector='user-name']")).Click();
            _wait2();
            Webdriver.FindElement(By.CssSelector("button[title='Add New Address']")).Click();
            Sleep();
            Webdriver.FindElement(By.Id("fullName")).SendKeys("Deneme Deneme");
            Webdriver.FindElement(By.Id("phone")).SendKeys("1234567890");
            Webdriver.FindElement(By.Id("cityCode")).Click();
            Webdriver.FindElement(By.CssSelector("option[value='921']")).Click();
            Webdriver.FindElement(By.Id("townCode")).Click();
            Webdriver.FindElement(By.CssSelector("option[value='921.1']")).Click();
            Webdriver.FindElement(By.Id("neighborhood")).Click();
            Webdriver.FindElement(By.CssSelector("option[value='150000']")).Click();
            Webdriver.FindElement(By.Id("line1")).SendKeys("Deneme Ev Adresi");
            Webdriver.FindElement(By.Id("addressName")).Clear();
            Webdriver.FindElement(By.Id("addressName")).SendKeys("Yeni Ev Adresi");
            Webdriver.FindElement(By.Id("save-address-btn")).Click();
            Sleep();
            Webdriver.Quit();
        }



        [TestMethod]
        public void User_04_UpdateAddress()
        {
            User_01_Login();
            Sleep();
            _wait2();
            Webdriver.FindElement(By.XPath("//a[@data-selector='user-name']")).Click();
            _wait2();
            int _count = Webdriver.FindElement(By.CssSelector("div[class='usr-addresses']")).FindElements(By.CssSelector("div[class='item']")).Count();
            List<string> Listaddress = new List<string>();
            for (int i = 0; i < _count; i++)
            {
                string address = Webdriver.FindElement(By.CssSelector("div[class='usr-addresses']")).FindElements(By.CssSelector("div[class='t']"))[i].Text.Trim();
                Listaddress.Add(address);
            }
            int findIndex = Listaddress.FindIndex(x => x == "Yeni Ev Adresi");
            Webdriver.FindElement(By.CssSelector("div[class='usr-addresses']")).FindElements(By.CssSelector("div[class='item']"))[findIndex].FindElement(By.CssSelector("a[class='link-inline js-edit-address']")).Click();
            Sleep();
            Webdriver.FindElement(By.Id("cityCode")).Click();
            Webdriver.FindElement(By.CssSelector("option[value='922']")).Click();
            Webdriver.FindElement(By.Id("townCode")).Click();
            Webdriver.FindElement(By.CssSelector("option[value='922.2']")).Click();
            Webdriver.FindElement(By.Id("neighborhood")).Click();
            Webdriver.FindElement(By.CssSelector("option[value='150175']")).Click();
            Webdriver.FindElement(By.Id("line1")).SendKeys("AB");
            Webdriver.FindElement(By.Id("addressName")).Clear();
            Webdriver.FindElement(By.Id("addressName")).SendKeys("Düzenlenmiş Ev Adresi");
            Webdriver.FindElement(By.Id("save-address-btn")).Click();
            Sleep();
            Webdriver.Quit();
        }



        [TestMethod]
        public void User_05_DeleteAddress()
        {
            User_01_Login();
            Sleep();
            _wait2();
            Webdriver.FindElement(By.XPath("//a[@data-selector='user-name']")).Click();
            _wait2();
            int _count = Webdriver.FindElement(By.CssSelector("div[class='usr-addresses']")).FindElements(By.CssSelector("div[class='item']")).Count();
            List<string> Listaddress = new List<string>();
            for (int i = 0; i < _count; i++)
            {
                string address = Webdriver.FindElement(By.CssSelector("div[class='usr-addresses']")).FindElements(By.CssSelector("div[class='t']"))[i].Text.Trim();
                Listaddress.Add(address);
            }
            int findIndex = Listaddress.FindIndex(x => x == "Düzenlenmiş Ev Adresi");
            Webdriver.FindElement(By.CssSelector("div[class='usr-addresses']")).FindElements(By.CssSelector("div[class='item']"))[findIndex].FindElement(By.CssSelector("a[class='link-inline js-remove-address js-ov-trg mr-10']")).Click();
            Sleep();
            Webdriver.FindElement(By.CssSelector("button[title='Remove']")).Click();
            Sleep();
            Webdriver.Quit();
        }



        //[TestMethod]
        //public void User_06_AddressValidation()
        //{
        //    User_01_Login();
        //    Sleep();
        //    _wait2();
        //    Webdriver.FindElement(By.XPath("//a[@data-selector='user-name']")).Click();
        //    _wait2();
        //    Webdriver.FindElement(By.CssSelector("button[title='Add New Address']")).Click();
        //    Sleep();
        //    Webdriver.FindElement(By.CssSelector("button[class='btn btn-primary js-save-address']")).Click();
        //    Sleep();
        //    string name_validation = Webdriver.FindElements(By.CssSelector("div[class='form-error-msg']"))[0].Text.Trim();
        //    if (name_validation == "Please enter name and surname")
        //    {
        //        string phone_validation = Webdriver.FindElements(By.CssSelector("div[class='form-error-msg']"))[1].Text.Trim();
        //        if (phone_validation == "Please enter a valid phone number (e.g. +92-311-1111111)")
        //        {
        //            string village_validation = Webdriver.FindElements(By.CssSelector("div[class='form-error-msg']"))[2].Text.Trim();
        //            if (village_validation == "Please select a town")
        //            {
        //                string street_validation = Webdriver.FindElements(By.CssSelector("div[class='form-error-msg']"))[3].Text.Trim();
        //                if (street_validation == "Please choose a neighborhood.")
        //                {
        //                    string adresDetail_validation = Webdriver.FindElements(By.CssSelector("div[class='form-error-msg']"))[4].Text.Trim();
        //                    if (adresDetail_validation == "Please enter address")
        //                    {
        //                        string adresName_validation = Webdriver.FindElements(By.CssSelector("div[class='form-error-msg']"))[5].Text.Trim();
        //                        if (adresName_validation == "Please enter address name")
        //                        {
        //                            Sleep();
        //                            Webdriver.Quit();
        //                        }
        //                        else
        //                        {
        //                            Webdriver.Quit();
        //                            Assert.Fail();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Webdriver.Quit();
        //                        Assert.Fail();
        //                    }
        //                }
        //                else
        //                {
        //                    Webdriver.Quit();
        //                    Assert.Fail();
        //                }
        //            }
        //            else
        //            {
        //                Webdriver.Quit();
        //                Assert.Fail();
        //            }
        //        }
        //        else
        //        {
        //            Webdriver.Quit();
        //            Assert.Fail();
        //        }
        //    }
        //    else
        //    {
        //        Webdriver.Quit();
        //        Assert.Fail();
        //    }
        //}



        [TestMethod]
        public void User_06_AddCompanyAddress()
        {
            User_01_Login();
            _wait2();
            WebDriverWait wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/my-account/my-profile']"))).Click();
            _wait2();
            Webdriver.FindElement(By.CssSelector("button[title='Add New Address']")).Click();
            Sleep();
            Webdriver.FindElement(By.Id("sw_adr_2")).Click();
            Sleep();
            Webdriver.FindElement(By.Id("company")).SendKeys("test company");
            Webdriver.FindElement(By.Id("taxNumber")).SendKeys("4664540663");
            Webdriver.FindElement(By.Id("phone")).SendKeys("00000000000");
            Webdriver.FindElement(By.Id("cityCode")).Click();
            Webdriver.FindElement(By.CssSelector("option[value='921']")).Click();
            Webdriver.FindElement(By.Id("townCode")).Click();
            Webdriver.FindElement(By.CssSelector("option[value='921.1']")).Click();
            Webdriver.FindElement(By.Id("neighborhood")).Click();
            Webdriver.FindElement(By.CssSelector("option[value='150000']")).Click();
            Webdriver.FindElement(By.Id("line1")).SendKeys("Deneme Kurumsal Adres");
            Webdriver.FindElement(By.Id("addressName")).Clear();
            Webdriver.FindElement(By.Id("addressName")).SendKeys("Yeni Adres Kurumsal");
            Webdriver.FindElement(By.Id("save-address-btn")).Click();
            Sleep();
            Webdriver.Quit();
        }



        [TestMethod]
        public void User_07_UpdateCompanyAddress()
        {
            User_01_Login();
            _wait2();
            WebDriverWait wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/my-account/my-profile']"))).Click();
            _wait2();
            int _count = Webdriver.FindElement(By.CssSelector("div[class='usr-addresses']")).FindElements(By.CssSelector("div[class='item']")).Count();
            List<string> _adressList = new List<string>();
            for (int i = 0; i < _count; i++)
            {
                string adress = Webdriver.FindElement(By.CssSelector("div[class='usr-addresses']")).FindElements(By.CssSelector("div[class='item']"))[i].FindElement(By.CssSelector("div[class='t']")).Text.Trim();
                _adressList.Add(adress);
            }
            int getindex = _adressList.FindIndex(x => x == "Yeni Adres Kurumsal");
            Webdriver.FindElements(By.CssSelector("a[class='link-inline js-edit-address']"))[getindex].Click();
            Sleep();
            Webdriver.FindElement(By.Id("line1")).Clear();
            Sleep();
            Webdriver.FindElement(By.Id("line1")).SendKeys("Deneme Kurumsal Adres");
            Sleep();
            Webdriver.FindElement(By.Id("save-address-btn")).Click();
            Sleep();
            Webdriver.Quit();
        }



        [TestMethod]
        public void User_08_DeleteCompanyAddress()
        {
            User_01_Login();
            _wait2();
            WebDriverWait wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/my-account/my-profile']"))).Click();
            _wait2();
            int _count = Webdriver.FindElement(By.CssSelector("div[class='usr-addresses']")).FindElements(By.CssSelector("div[class='item']")).Count();
            List<string> _adressList = new List<string>();
            for (int i = 0; i < _count; i++)
            {
                string adress = Webdriver.FindElement(By.CssSelector("div[class='usr-addresses']")).FindElements(By.CssSelector("div[class='item']"))[i].FindElement(By.CssSelector("div[class='t']")).Text.Trim();
                _adressList.Add(adress);
            }
            int getindex = _adressList.FindIndex(x => x == "Yeni Adres Kurumsal");
            Webdriver.FindElements(By.CssSelector("a[class='link-inline js-remove-address js-ov-trg mr-10']"))[getindex].Click();
            Webdriver.FindElement(By.CssSelector("button[title='Remove']")).Click();
            Sleep();
            Webdriver.Quit();
        }




        //[TestMethod]
        //public void User_10_ValidationCompanyAddress()
        //{
        //    User_01_Login();
        //    Sleep();
        //    _wait2();
        //    Webdriver.FindElement(By.XPath("//a[@data-selector='user-name']")).Click();
        //    _wait2();
        //    Webdriver.FindElement(By.CssSelector("button[title='Add New Address']")).Click();
        //    Sleep();
        //    Webdriver.FindElement(By.Id("sw_adr_2")).Click();
        //    Sleep();
        //    Webdriver.FindElement(By.CssSelector("button[class='btn btn-primary js-save-address']")).Click();
        //    Sleep();
        //    string company_name = Webdriver.FindElements(By.CssSelector("div[class='form-error-msg']"))[0].Text.Trim();
        //    if (company_name == "Please enter valid company name")
        //    {
        //        string taxNumber = Webdriver.FindElements(By.CssSelector("div[class='form-error-msg']"))[1].Text.Trim();
        //        if (taxNumber == "National Tax Number should be entered")
        //        {
        //            string phone = Webdriver.FindElements(By.CssSelector("div[class='form-error-msg']"))[2].Text.Trim();
        //            if (phone == "Please enter a valid phone number (e.g. +92-311-1111111)")
        //            {
        //                string town = Webdriver.FindElements(By.CssSelector("div[class='form-error-msg']"))[3].Text.Trim();
        //                if (town == "Please select a town")
        //                {
        //                    string street = Webdriver.FindElements(By.CssSelector("div[class='form-error-msg']"))[4].Text.Trim();
        //                    if (street == "Please choose a neighborhood.")
        //                    {
        //                        string adress_detail = Webdriver.FindElements(By.CssSelector("div[class='form-error-msg']"))[5].Text.Trim();
        //                        if (adress_detail == "Please enter address")
        //                        {
        //                            string adress_name = Webdriver.FindElements(By.CssSelector("div[class='form-error-msg']"))[6].Text.Trim();
        //                            if (adress_name == "Please enter address name")
        //                            {
        //                                Sleep();
        //                                Webdriver.Quit();
        //                            }
        //                            else
        //                            {
        //                                Webdriver.Quit();
        //                                Assert.Fail();
        //                            }
        //                        }
        //                        else
        //                        {
        //                            Webdriver.Quit();
        //                            Assert.Fail();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Webdriver.Quit();
        //                        Assert.Fail();
        //                    }
        //                }
        //                else
        //                {
        //                    Webdriver.Quit();
        //                    Assert.Fail();
        //                }
        //            }
        //            else
        //            {
        //                Webdriver.Quit();
        //                Assert.Fail();
        //            }
        //        }
        //        else
        //        {
        //            Webdriver.Quit();
        //            Assert.Fail();
        //        }
        //    }
        //    else
        //    {
        //        Webdriver.Quit();
        //        Assert.Fail();
        //    }
        //}




        [TestMethod]
        public void User_09_ChangePassword() 
        {
            User_01_Login();
            Sleep();
            _wait2();
            Webdriver.FindElement(By.XPath("//a[@data-selector='user-name']")).Click();
            _wait2();
            element = Webdriver.FindElement(By.CssSelector("a[href='/my-account/update-password']"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Thread.Sleep(1000);
            element.Click();
            _wait2();
            Webdriver.FindElement(By.Id("newPassword")).SendKeys("12345Furkan");
            Webdriver.FindElement(By.Id("newPasswordVerification")).SendKeys("12345Furkan");
            Webdriver.FindElement(By.CssSelector("button[title='Save ']")).Click();
            _wait2();
            Webdriver.Quit();
        }



        [TestMethod]
        public void User_10_ForgotPassword()
        {
            try
            {
                _Start();
                _wait2();
                WebDriverWait wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(60));
                Webdriver.FindElement(By.XPath("//a[@title='Sign In']")).Click();
                _wait2();
                element = Webdriver.FindElement(By.XPath("//a[@href='/password/forgot-password']"));
                ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
                Sleep();
                element.Click();
                _wait2();
                element = Webdriver.FindElement(By.Id("email"));
                ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
                Sleep();
                element.SendKeys("grundigtest@outlook.com");
                Webdriver.FindElement(By.Id("g-recaptcha-btn-forgot")).Click();
                ((IJavaScriptExecutor)Webdriver).ExecuteScript("window.open();");
                Webdriver.SwitchTo().Window(Webdriver.WindowHandles.Last());
                Webdriver.Navigate().GoToUrl("https://login.live.com/login.srf?wa=wsignin1.0&rpsnv=13&ct=1652774846&rver=7.0.6737.0&wp=MBI_SSL&wreply=https%3a%2f%2foutlook.live.com%2fowa%2f%3fnlp%3d1%26RpsCsrfState%3d1701857e-6bf6-96f1-99dd-09c8c0527bfb&id=292841&aadredir=1&whr=outlook.com&CBCXT=out&lw=1&fl=dob%2cflname%2cwld&cobrandid=90015");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("input[type='email']"))).SendKeys("grundigtest@outlook.com");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("idSIButton9"))).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("input[type='password']"))).SendKeys("1234furkan");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("idSIButton9"))).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("idBtn_Back"))).Click();
                Thread.Sleep(45000);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Pivot81-Tab1"))).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div[class='hcptT']")));
                Webdriver.FindElements(By.CssSelector("div[class='hcptT']"))[0].Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@rel='noopener noreferrer']")));
                element = Webdriver.FindElements(By.XPath("//a[@rel='noopener noreferrer']"))[0];
                ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
                Sleep();
                element.Click();
                Webdriver.SwitchTo().Window(Webdriver.WindowHandles.Last());
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("password"))).SendKeys("12345Grundig");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("checkPassword"))).SendKeys("12345Grundig");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[title='Update My Password']"))).Click();
                Sleep();
                Webdriver.Quit();
            }
            catch (Exception)
            {
                Screenshot ss = ((ITakesScreenshot)Webdriver).GetScreenshot();
                ss.SaveAsFile("C:\\Users\\gundu\\Desktop\\s2_automations\\Dawlance_SS\\User_10_ForgotPassword.png",
                ScreenshotImageFormat.Png);
                Webdriver.Quit();
                Assert.Fail();
            }
        }



        [TestMethod]
        public void User_11_Wishlist()
        {
            User_01_Login();
            _wait2();
            WebDriverWait wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/my-account/my-profile']"))).Click();
            _wait2();
            Webdriver.FindElement(By.XPath("//a[@href='/my-account/favourites ']")).Click();
            _wait2();
            try
            {
                //if wishlist is full
                element = Webdriver.FindElement(By.XPath("//button[@title='Clear List']"));
                ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
                element.Click();
                _wait2();
                string CHECHK_WISHLIST = Webdriver.FindElement(By.XPath("//div[@class='t']")).Text.Trim();
                if (CHECHK_WISHLIST == "Your wishlist is empty")
                {
                    Webdriver.FindElement(By.XPath("//div[@class='logo']")).Click();
                    _wait2();
                }
            }
            catch (Exception)
            {
                //if wishlist is empty
                string CHECHK_WISHLIST = Webdriver.FindElement(By.XPath("//div[@class='t']")).Text.Trim();
                if (CHECHK_WISHLIST == "Your wishlist is empty")
                {
                    Webdriver.FindElement(By.XPath("//div[@class='logo']")).Click();
                    _wait2();
                }
            }
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            Actions ac = new Actions(Webdriver);
            element = Webdriver.FindElement(By.CssSelector("a[href='/refrigerators-and-freezers']"));
            ac.MoveToElement(element).Perform();
            ac.Reset();
            Webdriver.FindElement(By.XPath("//a[@href='/refrigerators']")).Click();
            _wait2();
            //PLP Wishlist
            element = Webdriver.FindElement(By.XPath("//button[@data-title='91999 Avante+ Ruby Red']"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            element.Click();
            Sleep();
            Webdriver.FindElement(By.XPath("//button[@class='menu-btn-star add-my-list']")).Click();
            Sleep();
            Webdriver.FindElements(By.XPath("//button[@class='btn-close']"))[3].Click();
            //PDP Wishlist
            element = Webdriver.FindElements(By.XPath("//a[@href='/double-door-refrigerator/9173wb-avante-sapphire-purple-refrigerators']"))[0];
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            element.Click();
            _wait2();
            Webdriver.FindElements(By.XPath("//button[@data-title='9173WB Avante+ Sapphire Purple']"))[1].Click();
            Sleep();
            Webdriver.FindElement(By.XPath("//button[@class='menu-btn-star add-my-list']")).Click();
            Sleep();
            Webdriver.FindElements(By.XPath("//button[@class='btn-close']"))[2].Click();
            Sleep();
            //Basket Wishlist
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            element = Webdriver.FindElement(By.CssSelector("a[href='/refrigerators-and-freezers']"));
            ac.MoveToElement(element).Perform();
            ac.Reset();
            Webdriver.FindElement(By.XPath("//a[@href='/refrigerators']")).Click();
            _wait2();
            element = Webdriver.FindElements(By.XPath("//a[@href='/double-door-refrigerator/91999-avante-sapphire-purple-refrigerators']"))[0];
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            element.Click();
            _wait2();
            Webdriver.FindElement(By.XPath("//button[@title='Add to Cart']")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[title='Go to Cart']"))).Click();
            _wait2();
            Webdriver.FindElement(By.XPath("//button[@class='link-inline add-my-list']")).Click();
            Sleep();
            //Go Profile
            Webdriver.FindElement(By.XPath("//a[@data-selector='user-name']")).Click();
            _wait2();
            Webdriver.FindElement(By.XPath("//a[@href='/my-account/favourites ']")).Click();
            _wait2();
            string FIRST_PROD = Webdriver.FindElements(By.XPath("//div[@class='fav-name']"))[0].Text.Trim();
            string SECOND_PROD = Webdriver.FindElements(By.XPath("//div[@class='fav-name']"))[1].Text.Trim();
            string THIRD_PROD = Webdriver.FindElements(By.XPath("//div[@class='fav-name']"))[2].Text.Trim();
            if (FIRST_PROD == "91999 Avante+ Sapphire Purple")
            {
                if (SECOND_PROD == "9173WB Avante+ Sapphire Purple")
                {
                    if (THIRD_PROD == "91999 Avante+ Ruby Red")
                    {
                        Sleep();
                        Webdriver.FindElement(By.XPath("//button[@class='btn btn-outline-dark remove-my-all-list']")).Click();
                        _wait2();
                        string EMPTY_FAVORITE = Webdriver.FindElement(By.XPath("//div[@class='t']")).Text.Trim();
                        if (EMPTY_FAVORITE == "Your wishlist is empty")
                        {
                            Webdriver.Quit();
                        }
                        else
                        {
                            Webdriver.Quit();
                            Assert.Fail();
                        }
                    }
                    else
                    {
                        Webdriver.Quit();
                        Assert.Fail();
                    }
                }
                else
                {
                    Webdriver.Quit();
                    Assert.Fail();
                }
            }
            else
            {
                Webdriver.Quit();
                Assert.Fail();
            }
        }
        //*******************************  User Operations End



        //*******************************  PLP Start

        [TestMethod]
        public void PLP_01_Highlight()
        {
            _Start();
            WebDriverWait wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(60));
            Actions ac = new Actions(Webdriver);
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            element = Webdriver.FindElement(By.CssSelector("a[href='/refrigerators-and-freezers']"));
            ac.MoveToElement(element).Perform();
            ac.Reset();
            Webdriver.FindElement(By.XPath("//a[@href='/refrigerators']")).Click();
            _wait2();
            element = Webdriver.FindElements(By.XPath("//div[@class='prd-features']"))[0];
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            string FIRST_PROD = Webdriver.FindElements(By.XPath("//div[@class='prd-features']"))[0].Text.Trim();
            string SECOND_PROD = Webdriver.FindElements(By.XPath("//div[@class='prd-features']"))[1].Text.Trim();
            string THIRD_PROD = Webdriver.FindElements(By.XPath("//div[@class='prd-features']"))[2].Text.Trim();
            if (FIRST_PROD == "Vitamin Fresh Technology : Yes\r\nInverter Technology : Yes\r\nCooling Technology : Static\r\nTotal Gross Volume (cu.ft) (Requested By Customer) : 16.45")
            {
                if (SECOND_PROD == "Vitamin Fresh Technology : Yes\r\nInverter Technology : Yes\r\nCooling Technology : Static\r\nTotal Gross Volume (cu.ft) (Requested By Customer) : 9,49")
                {
                    if (THIRD_PROD == "Vitamin Fresh Technology : Yes\r\nInverter Technology : Yes\r\nCooling Technology : Static\r\nTotal Gross Volume (cu.ft) (Requested By Customer) : 13,52")
                    {
                        Sleep();
                        Webdriver.Quit();
                    }
                    else
                    {
                        Webdriver.Quit();
                        Assert.Fail();
                    }
                }
                else
                {
                    Webdriver.Quit();
                    Assert.Fail();
                }
            }
            else
            {
                Webdriver.Quit();
                Assert.Fail();
            }
        }


        //*******************************  PLP End



        //*******************************  PDP Start

        [TestMethod]
        public void PDP_01_StoreNearby()
        {
            _Start();
            WebDriverWait wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(60));
            Actions ac = new Actions(Webdriver);
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            element = Webdriver.FindElement(By.CssSelector("a[href='/refrigerators-and-freezers']"));
            ac.MoveToElement(element).Perform();
            ac.Reset();
            Webdriver.FindElement(By.XPath("//a[@href='/refrigerators']")).Click();
            _wait2();
            element = Webdriver.FindElements(By.XPath("//a[@href='/double-door-refrigerator/91999-avante-ruby-red-refrigerators']"))[0];
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            element.Click();
            _wait2();
            element = Webdriver.FindElement(By.XPath("//li[@class='pdp-store-locator']"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            element.Click();
            element = Webdriver.FindElement(By.XPath("//button[@title='Find Store']"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            Webdriver.FindElement(By.Id("cityCode")).FindElement(By.XPath("//option[@value='922']")).Click();
            Sleep();
            Webdriver.FindElement(By.Id("townCode")).FindElement(By.XPath("//option[@value='922.2']")).Click();
            Sleep();
            element.Click();
            Sleep();
            string RESULT = Webdriver.FindElement(By.XPath("//div[@class='stf-result']")).Text.Trim();
            if (RESULT == "No store found in the selected area")
            {
                Sleep();
                Webdriver.Quit();
            }
            else
            {
                Webdriver.Quit();
                Assert.Fail();
            }
        }



        [TestMethod]
        public void PDP_02_Highlight()
        {
            _Start();
            WebDriverWait wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(60));
            Actions ac = new Actions(Webdriver);
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            element = Webdriver.FindElement(By.CssSelector("a[href='/refrigerators-and-freezers']"));
            ac.MoveToElement(element).Perform();
            ac.Reset();
            Webdriver.FindElement(By.XPath("//a[@href='/refrigerators']")).Click();
            _wait2();
            element = Webdriver.FindElements(By.XPath("//a[@href='/double-door-refrigerator/91999-avante-ruby-red-refrigerators']"))[0];
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            element.Click();
            _wait2();
            element = Webdriver.FindElement(By.XPath("//div[@class='pdp-features']"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            string HIGHLIGHT = element.Text.Trim();
            if (HIGHLIGHT == "Vitamin Fresh Technology\r\nYes\r\nCooling Technology\r\nStatic\r\nInverter Technology\r\nYes")
            {
                Sleep();
                Webdriver.Quit();
            }
            else
            {
                Webdriver.Quit();
                Assert.Fail();
            }
        }



        [TestMethod]
        public void PDP_03_Image()
        {
            _Start();
            WebDriverWait wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(60));
            Actions ac = new Actions(Webdriver);
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            element = Webdriver.FindElement(By.CssSelector("a[href='/refrigerators-and-freezers']"));
            ac.MoveToElement(element).Perform();
            ac.Reset();
            Webdriver.FindElement(By.XPath("//a[@href='/refrigerators']")).Click();
            _wait2();
            element = Webdriver.FindElements(By.XPath("//a[@href='/double-door-refrigerator/91999-avante-ruby-red-refrigerators']"))[0];
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            element.Click();
            _wait2();
            string IMAGE_1 = Webdriver.FindElement(By.CssSelector("#pdp-general > div.pdp-gallery > div.swiper-container.swiper-activeted.item-1.swiper-container-initialized.swiper-container-horizontal > div > div > div > div > img")).GetAttribute("srcset");
            IMAGE_1 = IMAGE_1.Substring(0, IMAGE_1.IndexOf(","));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript("window.open();");
            Webdriver.SwitchTo().Window(Webdriver.WindowHandles.Last());
            Webdriver.Navigate().GoToUrl("https://dawlance-astra.c1m0wu3z2z-arcelikas1-s1-public.model-t.cc.commerce.ondemand.com/" + IMAGE_1);
            Sleep();
            string title = Webdriver.Title;
            if (title.Contains("image.png"))
            {

                Sleep();
                Webdriver.Quit();
            }
            else
            {
                Webdriver.Quit();
                Assert.Fail();
            }
        }



        [TestMethod]
        public void PDP_04_ProdSpec()
        {
            _Start();
            WebDriverWait wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(60));
            Actions ac = new Actions(Webdriver);
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            element = Webdriver.FindElement(By.CssSelector("a[href='/refrigerators-and-freezers']"));
            ac.MoveToElement(element).Perform();
            ac.Reset();
            Webdriver.FindElement(By.XPath("//a[@href='/refrigerators']")).Click();
            _wait2();
            element = Webdriver.FindElements(By.XPath("//a[@href='/double-door-refrigerator/91999-avante-ruby-red-refrigerators']"))[1];
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            element.Click();
            _wait2();
            element = Webdriver.FindElement(By.Id("pdp-technical"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            element.Click();
            element = Webdriver.FindElement(By.XPath("//div[@class='pdp-technical-features-list']"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            string SPECIFICATION = element.Text.Trim();
            if (SPECIFICATION == "Product Highlights\r\nCooling Technology\r\nStatic\r\nInverter Technology\r\nYes\r\nNature Lock Technology\r\nYes\r\nVitamin Fresh Technology\r\nYes\r\nGeneral Details\r\nVoltage (V)\r\n230\r\nClimate Class\r\nT\r\nEnergy Class\r\nA++\r\nFreezer Position\r\nTop\r\nRefrigerant\r\nR600a\r\nInterior\r\nType of Shelf\r\nGlass-All standard\r\nPower Cool Fan\r\nYes\r\nIce Tray\r\nIcebox (12 pcs)\r\nRefrigerator Light\r\nLED on Side Walls\r\nNumber of Crisper\r\n2\r\nEgg Tray\r\n2\r\nFreezer Door Pockets\r\n2\r\nTotal Shelves\r\n5\r\nRefrigerator Door Pockets\r\n5\r\nExterior\r\nProduct Color\r\nSapphire Purple\r\nTouch Display\r\nYes\r\nType of Handle\r\nIntegrated Handle on side\r\nHandle Material\r\nPlastic\r\nProduct Dimensions\r\nWidth (cm)\r\n77.6\r\nDepth (cm)\r\n65.2\r\nHeight (cm)\r\n177")
            {
                Sleep();
                Webdriver.Quit();
            }
            else
            {
                Webdriver.Quit();
                Assert.Fail();
            }
        }


        //*******************************  PDP End



        //*******************************  HomePage Start

        [TestMethod]
        public void HomePage_01_Search()
        {
            _Start();
            Webdriver.FindElement(By.XPath("//div[@class='placeholder']")).Click();
            Sleep();
            Webdriver.FindElement(By.Id("searchText")).SendKeys("Refrigerators");
            Webdriver.FindElements(By.CssSelector("svg[class='icon icon-search']"))[3].Click();
            _wait2();
            int COUNT_PROD = Webdriver.FindElement(By.XPath("//div[@class='products productgridcomponent-page']")).FindElements(By.CssSelector("div[class='prd product-item']")).Count();
            if (COUNT_PROD > 0)
            {
                Sleep();
                Webdriver.Quit();
            }
            else
            {
                Webdriver.Quit();
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
                Webdriver.Quit();
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
                element = Webdriver.FindElement(By.XPath("//a[@title='Shop Now']")); 
                ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
                Sleep();
                element.Click();
                _wait2();
                Webdriver.Quit();
            }
            else
            {
                Webdriver.Quit();
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HomePage_04_Store()
        {
            _Start();
            Webdriver.FindElement(By.CssSelector("a[href='/store-finder']")).Click();
            _wait2();
            Webdriver.FindElement(By.Id("cityCode")).FindElement(By.CssSelector("option[value='cityAll']")).Click();
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
                Webdriver.Quit();
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
                else
                {
                    Webdriver.Quit();
                    Assert.Fail();
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
                    Webdriver.Quit();
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
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
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
                    Webdriver.Quit();
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Webdriver.Quit();
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
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
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
                    Webdriver.Quit();
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Webdriver.Quit();
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
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
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
                    Webdriver.Quit();
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Webdriver.Quit();
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
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
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
                    Webdriver.Quit();
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Webdriver.Quit();
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
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
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
                    Webdriver.Quit();
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Webdriver.Quit();
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
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
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
                    Webdriver.Quit();
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Webdriver.Quit();
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
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
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
                    Webdriver.Quit();
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Webdriver.Quit();
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
            int COUNT = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("li")).Count();
            ac.KeyDown(Keys.LeftControl).Click(element).KeyUp(Keys.LeftControl).Perform();
            ac.Reset();
            List<string> _titles = new List<string>();
            for (int i = 0; i < COUNT - 4; i++)
            {
                string title = Webdriver.FindElement(By.CssSelector("li[class='lv1-li selected']")).FindElement(By.CssSelector("div[class='sub']")).FindElement(By.CssSelector("ul[class='ul-clear lv-2']")).FindElements(By.TagName("a"))[i].GetAttribute("title");
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
                    Webdriver.Quit();
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Webdriver.Quit();
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
            string title = Webdriver.FindElements(By.ClassName("bnr-title"))[10].Text.Trim();
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
                Webdriver.Quit();
                Assert.Fail();
            }
        }


        [TestMethod]
        public void HomePage_08_SliderCategories()
        {
            _Start();
            Actions ac = new Actions(Webdriver);
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
                        Webdriver.Quit();
                        Assert.Fail();
                    }
                    try
                    {
                        error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                        if (error.Contains("404"))
                        {
                            Webdriver.Quit();
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
                Webdriver.Quit();
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
                        Webdriver.Quit();
                        Assert.Fail();
                    }
                    try
                    {
                        error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                        if (error.Contains("404"))
                        {
                            Webdriver.Quit();
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
                Webdriver.Quit();
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
                    Webdriver.Quit();
                    Assert.Fail();
                }
                try
                {
                    error = Webdriver.FindElement(By.CssSelector("div[class='pnf-title']")).Text.Trim();
                    if (error.Contains("404"))
                    {
                        Webdriver.Quit();
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

        [TestMethod]
        public void Payment_01_CreditCard()
        {
            User_01_Login();
            Sleep();
            WebDriverWait wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(60));
            IJavaScriptExecutor jse = (IJavaScriptExecutor)Webdriver;
            Basket_Control();
            _wait2();
            Actions ac = new Actions(Webdriver);
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            element = Webdriver.FindElement(By.CssSelector("a[href='/refrigerators-and-freezers']"));
            ac.MoveToElement(element).Perform();
            ac.Reset();
            Webdriver.FindElement(By.XPath("//a[@href='/refrigerators']")).Click();
            _wait2();
            element = Webdriver.FindElements(By.XPath("//a[@href='/double-door-refrigerator/91999-avante-ruby-red-refrigerators']"))[0];
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            element.Click();
            _wait2();
            Webdriver.FindElement(By.XPath("//button[@title='Add to Cart']")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[title='Go to Cart']"))).Click();
            _wait2();
            element = Webdriver.FindElement(By.XPath("//a[@title='Continue Checkout']"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            element.Click();
            element = Webdriver.FindElement(By.Id("pay-payment-checkout"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            element.Click();
            element = Webdriver.FindElement(By.Id("cardNumber"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            element.SendKeys("4242424242424242");
            Webdriver.FindElement(By.Id("chk_cart_sum_confirm_2")).Click();
            Sleep();
            Webdriver.FindElement(By.Id("expiryDate")).SendKeys("1230");
            Sleep();
            element = Webdriver.FindElement(By.Id("cvv"));
            element.SendKeys("0");
            element.SendKeys("1");
            element.SendKeys("0");
            element.SendKeys("0");
            Webdriver.FindElement(By.Id("postPayment")).Click();
            Sleep();
            _wait2();
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("cko-3ds2-iframe"));
            element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
            element.SendKeys("Checkout1!");
            element = Webdriver.FindElement(By.Id("txtButton"));
            element.Click();
            Webdriver.SwitchTo().DefaultContent();
            _wait2();
            string RESULT = Webdriver.FindElement(By.XPath("//div[@class='result-grid-header pk-result-grid-header']")).Text.Trim();
            if (RESULT.Contains("Thank you!"))
            {
                Sleep();
                Webdriver.Quit();
            }
            else
            {
                Webdriver.Quit();
                Assert.Fail();
            }
        }





        [TestMethod]
        public void Payment_02_CancelOrder()
        {
            User_01_Login();
            Sleep();
            _wait2();
            Webdriver.FindElement(By.XPath("//a[@data-selector='user-name']")).Click();
            _wait2();
            Webdriver.FindElement(By.CssSelector("a[href='/my-account/orders ']")).Click();
            _wait2();
            element = Webdriver.FindElements(By.XPath("//a[@class='link-inline btn-ord-back  ']"))[0];
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            element.Click();
            _wait2();
            element = Webdriver.FindElement(By.Id("ocp_reason"));
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            element.FindElement(By.CssSelector("option[value='cReason5_PK_OrderedByMistake']")).Click();
            Webdriver.FindElement(By.XPath("//button[@title='Proceed']")).Click();
            _wait2();
            string RESULT = Webdriver.FindElement(By.XPath("//div[@class='form-result-title']")).Text.Trim();
            if (RESULT == "Cancel/Return Request Has been Received Successfully.")
            {
                Sleep();
                Webdriver.FindElement(By.XPath("//a[@class='btn btn-primary']")).Click();
                _wait2();
                Webdriver.Quit();
            }
            else
            {
                Webdriver.Quit();
                Assert.Fail();
            }
        }

        //*******************************  Payment End



        //*******************************  BasketPage Start

        [TestMethod]
        public void BasketPage_01_Count()
        {
            _Start();
            WebDriverWait wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(60));
            Actions ac = new Actions(Webdriver);
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            element = Webdriver.FindElement(By.CssSelector("a[href='/refrigerators-and-freezers']"));
            ac.MoveToElement(element).Perform();
            ac.Reset();
            Webdriver.FindElement(By.XPath("//a[@href='/refrigerators']")).Click();
            _wait2();
            element = Webdriver.FindElements(By.XPath("//a[@href='/double-door-refrigerator/91999-avante-ruby-red-refrigerators']"))[1];
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            element.Click();
            _wait2();
            Webdriver.FindElement(By.XPath("//button[@title='Add to Cart']")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[title='Go to Cart']"))).Click();
            _wait2();
            Webdriver.FindElement(By.CssSelector("button[class='btn-plus ']")).Click();
            _wait2();
            string CHECK_QUANTITY = Webdriver.FindElement(By.XPath("//input[@data-mask='otp']")).GetAttribute("value");
            if (CHECK_QUANTITY == "2")
            {
                Sleep();
                Webdriver.FindElement(By.CssSelector("button[class='btn-minus']")).Click();
                _wait2();
                string LAST_QUANTITY = Webdriver.FindElement(By.XPath("//input[@data-mask='otp']")).GetAttribute("value");
                if (LAST_QUANTITY == "1")
                {
                    Sleep();
                    Webdriver.Quit();
                }
                else
                {
                    Webdriver.Quit();
                    Assert.Fail();
                }
            }
            else
            {
                Webdriver.Quit();
                Assert.Fail();
            }
        }




        [TestMethod]
        public void BasketPage_02_ClearBasket()
        {
            _Start();
            WebDriverWait wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(60));
            Actions ac = new Actions(Webdriver);
            Webdriver.FindElement(By.CssSelector("button[aria-label='menu']")).Click();
            Sleep();
            element = Webdriver.FindElement(By.CssSelector("a[href='/refrigerators-and-freezers']"));
            ac.MoveToElement(element).Perform();
            ac.Reset();
            Webdriver.FindElement(By.XPath("//a[@href='/refrigerators']")).Click();
            _wait2();
            element = Webdriver.FindElements(By.XPath("//a[@href='/double-door-refrigerator/91999-avante-ruby-red-refrigerators']"))[1];
            ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
            Sleep();
            element.Click();
            _wait2();
            Webdriver.FindElement(By.XPath("//button[@title='Add to Cart']")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[title='Go to Cart']"))).Click();
            Basket_Control();
            _wait2();
            Webdriver.Quit();
        }

        //*******************************  BasketPage End



        //*******************************  Discount Start

        //*******************************  Discount End




        //*******************************  Methods


        public void Basket_Control()
        {
            _wait2(); 
            WebDriverWait wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(60)); 
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@class='btn-cart']"))).Click();
            //Webdriver.FindElement(By.XPath("//a[@class='btn-cart']")).Click();
            try
            {
                //Sepet Doluysa

                _wait2();
                element = Webdriver.FindElement(By.CssSelector("button[class='link-inline js-remove-cart-entries']"));
                ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
                Sleep();
                element.Click();

                _wait2();
                element = Webdriver.FindElement(By.CssSelector("a[class='btn btn-primary']"));
                ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
                Sleep();
                element.Click();

            }
            catch (Exception)
            {
                //Sepet Boşsa
                _wait2();
                element = Webdriver.FindElement(By.CssSelector("a[class='btn btn-primary']"));
                ((IJavaScriptExecutor)Webdriver).ExecuteScript(scrollElementIntoMiddle, element);
                Sleep();
                element.Click();
            }
        }


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
