using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using System.Diagnostics;
namespace HNTest
{
    public class HN
    {
        private const string baseUrl = "https://news.ycombinator.com/";
        private const string newestUrl = "newest";
 
        protected HttpClient httpClient;

        private string nextUrl;

        public HN()
        {
            httpClient = new HttpClient() { BaseAddress = new Uri(baseUrl) };
        }

        /// <summary>
        /// Return the latest news
        /// </summary>
        public async Task<List<HackerItem>> Newest(bool nextPage = false)
        {
            HtmlDocument doc;
            if (nextPage && nextUrl != null)
            {
                doc = await Load(nextUrl);
            }
            else
            {
                doc = await Load(newestUrl);
            }

            var mainTable = doc.DocumentNode.Descendants("table").ToList()[2];
            var rows = mainTable.Descendants("tr").ToList();

            var items = ParseList(rows);
            return items;
        }

        protected async Task<HtmlDocument> Load(string url)
        {
            var stream = await httpClient.GetStreamAsync(url);
            HtmlDocument doc = new HtmlDocument();
            doc.Load(stream);
            return doc;
        }

        protected List<HackerItem> ParseList(List<HtmlNode> rows)
        {
            List<HackerItem> items = new List<HackerItem>();
            HackerItem newsItem = null;

            newsItem = new HackerItem();
            for (int i = 0; i < rows.Count; i++)
            {
                try
                {
                    var row = rows[i];
                    var tableData = row.Descendants("td");

                    if (tableData != null && tableData.Count() > 0)
                    {

                        if (tableData.Count(x => x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("title")) > 0)
                        {
                            if (i == rows.Count - 1)
                            {
                                nextUrl = row.Descendants("a").ToList()[0].Attributes["href"].Value;
                                break;
                            }

                            ParsingHelpers.ParseUrl(newsItem, row.Descendants("a").ToList());
                            ParsingHelpers.ParseTitle(newsItem, row.Descendants("a").ToList());
                        }
                        else if (tableData.Count(x => x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("subtext")) > 0)
                        {
                            var links = row.Descendants("a").ToList();
                            ParsingHelpers.ParseUser(newsItem, links);
                            if (newsItem.Title != "")
                                items.Add(newsItem);
                            
                            newsItem = new HackerItem();
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new HackerException("Could not parse the news feed. See inner exception for details", e);
                }
            }

            return items;
        }
    }
}