using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace EbayScrapper
{
    class USStates
    {
        public string state { get; set; }

        public string confirmed { get; set; }

        public string death { get; set; }

        public string fatalityRate { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            GetHtmlAsync();
            Console.ReadLine();
        }
        static async void GetHtmlAsync()
        {
            var url = "https://coronavirus.1point3acres.com/en";
            var httpclient = new HttpClient();
            var html = await httpclient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var ProductsHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                    .Equals("jsx-1168542486 stat row")).ToList();
          
      
            List<USStates> statesList = new List<USStates>();
            for (int i = 0; i < 57; i++)
            {
                USStates states = new USStates();

                int j = 0;
                foreach (var ProductListItem in ProductsHtml[i].Descendants("span")
                    .Where(node => node.GetAttributeValue("class", "")
                        .Contains("jsx-1168542486")).ToList())
                {
                   
                    if(j ==0)
                    {
                        states.state = ProductListItem.InnerText;
                    }


                    if (j == 1)
                    {
                        states.confirmed = ProductListItem.InnerText;
                    }

                    if (j == 2)
                    {
                        states.death = ProductListItem.InnerText;
                    }

                    if (j == 3)
                    {
                        states.fatalityRate = ProductListItem.InnerText;
                    }
                    j = j + 1;
                }
                j = 0;
                statesList.Add(states);
            }
            string str = "";
        }    }}
           
       
