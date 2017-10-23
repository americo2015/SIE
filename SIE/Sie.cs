using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIE
{
    class Sie
    {
        static void Main(string[] args)
        {
            var streamReader = File.OpenText(@"SIE.txt");
            var rows = 0;
            var accounts = new Dictionary<string, decimal>();

            while (true)
            {
                var line = streamReader.ReadLine();
                if (line == null)
                    break;

                string pattern = @"(#TRANS) (\d{4}) {.*} (-?\d*.\d*)";
                var match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    rows++;

                    var accountId = match.Groups[2].Value;
                    var amount = decimal.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);

                    if (accounts.ContainsKey(accountId))
                        accounts[accountId] += amount;
                    else
                        accounts[accountId] = amount;
                }
                    
            }


            foreach (var entry in accounts.OrderBy(e => e.Key))
                Console.WriteLine($"{entry.Value.ToString("F2")}");

            Console.WriteLine("Total amount: " + accounts.Sum(entry => entry.Value));
            Console.WriteLine($"Total rows: {rows}");
            Console.ReadLine();
        }
    }
}
