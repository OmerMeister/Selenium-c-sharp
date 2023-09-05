using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Selenium_Basic
{
    class Selenium_Basic
    {
        //gets a string (in hebrew) and returns the reversed string
        public static string Rev(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public static void Selenium()
        {
            //fix hebrew text on console
            Console.OutputEncoding = Encoding.GetEncoding("Windows-1255");

            //loads edge driver
            EdgeOptions edge_options = new EdgeOptions();
            //diables annoying log messages to the console
            edge_options.AddArgument("--log-level=3");
            IWebDriver driver_e = new EdgeDriver(edge_options);

            //goes to imdb site
            driver_e.Url = "https://www.imdb.com/";


            try
            {
                //get the search box element
                IWebElement element = driver_e.FindElement(By.XPath("//*[@id=\"suggestion-search\"]"));
                //just to make sure this is the right element
                string input_desc = ((WebElement)element).ComputedAccessibleLabel;
                Console.WriteLine(input_desc);

                //writing the query into the search box and begin search
                element.Click();
                element.SendKeys("hannibal");
                element.SendKeys(Keys.Enter);

                //select the ul containing the five results
                IWebElement five_results_list = driver_e.FindElement(By.XPath("//*[@id=\"__next\"]/main/div[2]/div[3]/section/div/div[1]/section[2]/div[2]/ul"));
                //creating a list of 
                ICollection<IWebElement> result_li_elements = five_results_list.FindElements(By.XPath("//*[@id=\"__next\"]/main/div[2]/div[3]/section/div/div[1]/section[2]/div[2]/ul/li"));
                //set the number of 'go_to_result_num' to click the desired result
                int go_to_result_num = 2;
                string go_to_result_url = "";
                int i = 1;
                // Iterate over each 'li' get the 'a' out of it, and print the href attribute
                foreach (IWebElement li_element in result_li_elements)
                {
                    IWebElement link_element = li_element.FindElement(By.TagName("a"));
                    Console.WriteLine(link_element.GetAttribute("href"));
                    //if the 'go_to_result_num' equals hte number of current li in the list, the link will be copied to 'go_to_result_url'
                    if (go_to_result_num != 0 && go_to_result_num == i) { go_to_result_url = link_element.GetAttribute("href"); }
                    i++;
                }
                if (go_to_result_url != "") { driver_e.Url = go_to_result_url; }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n an error occurred: \n" + ex);
            }

            //driver_e.Close();

        }

        static void Main()
        {
            Selenium();


        }
    }
}
