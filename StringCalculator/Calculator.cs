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

                var collectionOfDelimeters = GetDelimeters(splitOnFirstNewLine[0]);
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

        private static IEnumerable<string> GetDelimeters(string leftSplitOnFirstNewLine)
        {
            var delimetersPattern = leftSplitOnFirstNewLine.Replace("//", string.Empty);

            var delimetersResult = new List<string>();

            int stringCounter = 1;
            for (int i = 0; i < delimetersPattern.Length; i++)
            {
                char curr = delimetersPattern[i];
                char next = i != delimetersPattern.Length - 1 
                    ? delimetersPattern[i + 1]
                    : Convert.ToChar(Convert.ToInt32(delimetersPattern.Last()) + 27);

                if (curr != next)
                {
                    delimetersResult.Add(new string(curr, stringCounter));
                    
                    stringCounter = 1;
                }
                else
                {
                    stringCounter++;
                }
            }

            return delimetersResult;
        }
    }
}