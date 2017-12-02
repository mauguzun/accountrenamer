using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeRename
{
    class Program
    {
        static ChromeDriver dr;
        static Settings st;


        static void Main(string[] args)
        {
            dr = new ChromeDriver();
            dr.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 0, 35);


            st = new Settings();


            Console.WriteLine("set new settings?");
            if (Console.ReadLine() == "y")
            {

            }
            string[] acc = File.ReadAllLines(st.Set["file"]);
            var resut = acc;




            for (int i = 0; i < resut.Count(); i++)
            {
                string newResult = SetNewName(resut[i]);
                if (newResult != null)
                {
                    resut[i] = newResult;
                    File.WriteAllLines(st.Set["result"], resut);
                }
            }



        }


        static string SetNewName(string acc)
        {
            string[] param = acc.Split(':');
            if (param[2].Contains(st.Set["suffix"]))
                return null;


            WebDriverWait wait = new WebDriverWait(dr, new TimeSpan(0, 0, 35));


            try
            {

                dr.Url = "https://www.pinterest.com/login/";
                wait.Until((d) => d.Url == "https://www.pinterest.com/login/");
                dr.FindElementByName("id").SendKeys(param[0]);
                dr.FindElementByName("password").SendKeys(param[1]);
                dr.FindElementByCssSelector("form > button").Click();
                wait.Until((d) => d.Url == "https://www.pinterest.com/");

                dr.FindElementByCssSelector("body").Click();
            }
            catch
            {
                File.AppendAllText(st.Set["result_bad"], param[0] + Environment.NewLine);
                dr.Url = "https://www.pinterest.com/logout/";
                return null;
            }
            try
            {
                if (dr.FindElementsByCssSelector(".NagBase").Count > 0)
                {

                    dr.FindElementByCssSelector(".NagBase button").Click();

                    if (dr.FindElementByCssSelector(".NagBase button").Text == "Reset password")
                        return null;
                }
                //


                dr.Url = "https://www.pinterest.com/settings";

                string oldValue = dr.FindElementById("userUserName").GetAttribute("value");

                if (oldValue.Contains(st.Set["suffix"]))
                    return null;

                var newValue = $"{oldValue}{st.Set["suffix"]}";


                dr.FindElementById("userUserName").SendKeys(st.Set["suffix"]);
                dr.FindElementById("userAbout").SendKeys("l");

                if (dr.FindElementById("userUserName").GetAttribute("value") == newValue)
                {
                    var buttons = dr.FindElementsByCssSelector("form button[type = 'button']");
                    foreach (var button in buttons)
                    {
                        if (button.Text == "Save settings")
                        {
                            button.Click();
                            File.AppendAllText(st.Set["result_good"], newValue + Environment.NewLine);
                            dr.Url = "https://www.pinterest.com/logout/";

                            return $"{param[0]}:{param[1]}:{newValue}";
                        }



                    }

                }
                dr.Url = "https://www.pinterest.com/logout/";

                return null;
            }
            catch (Exception ex)
            {
                dr.Url = "https://www.pinterest.com/logout/";

                Console.WriteLine(ex.Message);
                return null;
            }


        }
    }
}
