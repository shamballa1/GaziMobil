//using HtmlAgilityPack;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web;

//namespace yemekhaneScraping
//{
//    public class YemekhaneMenuKaziyici
//    {
//        private static readonly HttpClient client = new HttpClient();

//        public async Task<string> GetYemekhaneMenu()
//        {
//            var url = "https://mediko.gazi.edu.tr/view/page/20412";
//            var response = await client.GetStringAsync(url);

//            var htmlDoc = new HtmlDocument();
//            htmlDoc.LoadHtml(response);

//            var menu = MenuyuCikar(htmlDoc);
//            return menu;
//        }

//        public async Task SaveMenuToFile(string filePath)
//        {
//            var menu = await GetYemekhaneMenu();
//            File.WriteAllText(filePath, menu);
//        }

//        private string MenuyuCikar(HtmlDocument htmlDoc)
//        {
//            var menuBuilder = new System.Text.StringBuilder();

//            var menuTable = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='subpage']/div/div[2]/div/div[2]/table[1]");

//            if (menuTable != null)
//            {
//                var rows = menuTable.SelectNodes(".//tr");
//                if (rows != null && rows.Count > 0)
//                {
//                    int columnCount = rows[0].SelectNodes(".//td").Count;
//                    var columns = new List<List<string>>();

//                    for (int i = 0; i < columnCount; i++)
//                    {
//                        columns.Add(new List<string>());
//                    }

//                    foreach (var row in rows)
//                    {
//                        var cells = row.SelectNodes(".//td");
//                        for (int i = 0; i < cells.Count; i++)
//                        {
//                            string cellText = HttpUtility.HtmlDecode(cells[i].InnerText.Trim());
//                            columns[i].Add(cellText);
//                        }
//                    }
//                    for (int i = 0; i < columnCount; i++)
//                    {
//                        menuBuilder.AppendLine($"Gün {i + 1}:");
//                        foreach (var item in columns[i])
//                        {
//                            menuBuilder.AppendLine(item);
//                        }
//                        menuBuilder.AppendLine(new string('-', 50));
//                    }
//                }
//            }

//            return menuBuilder.ToString();
//        }