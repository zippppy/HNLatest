using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HNTest
{
    static class ParsingHelpers
    {
        public static void ParseUrl(HackerItem item, List<HtmlNode> links)
        {
            HtmlNode link;

            if (links.Count == 3)
            {
                link = links[1];
            }
            else
            {
                link = links[0];
            }

            item.URL = link.Attributes["href"].Value;
            
        }

        public static void ParseUser(HackerItem item, List<HtmlNode> links)
        {
            var userLink = links.Where(x => x.Attributes["href"].Value.Contains("user")).ToList();

            if (userLink.Count >= 1)
            {
                item.User = userLink[0].InnerText;
            }
        }

        public static void ParseTitle(HackerItem item, List<HtmlNode> links)
        {
            HtmlNode link;

            if (links.Count == 3)
                link = links[1];
            else
                link = links[0];

            item.Title = link.InnerText;
            
        }
    }
}