using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            var delimiters = new List<string> { ",", "\n" };

            if (numbers.StartsWith("//"))
            {
                var splitOnFirstNewLine = numbers.Split(new[] { '\n' }, 2);
                var customDelimiters = splitOnFirstNewLine[0].Replace("//", string.Empty);

                var delimetersMap = new Dictionary<char, int>();
                for (int i = 0; i < customDelimiters.Length; i++)
                {
                    char c = customDelimiters[i];
                    if (delimetersMap.ContainsKey(c))
                    {
                        delimetersMap[c]++;
                    }
                    else
                    {
                        delimetersMap.Add(c, 1);
                    }
                }

                var collectionOfDelimeters = delimetersMap.Select(x => new string(x.Key, x.Value));
                delimiters.AddRange(collectionOfDelimeters);
                numbers = splitOnFirstNewLine[1];
            }

            var splitNumbers = numbers
                .Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Where(num => num <= 1000)
                .ToList();

            var negativeNumbers = splitNumbers.Where(x => x < 0).ToList();

            if (negativeNumbers.Any())
            {
                throw new Exception("Negatives not allowed: " + string.Join(",", negativeNumbers));
            }

            return splitNumbers.Sum();
        }
    }
}