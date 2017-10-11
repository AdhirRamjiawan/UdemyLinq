using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyLinq
{
    class Program
    {
        // Classification of Linq Operators:
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/classification-of-standard-query-operators-by-manner-of-execution

        static void Main(string[] args)
        {
            ConvertingDataTypes();
            ConvertDataTypes2();
            ArrayToEnumberable();
            SelectProject();
            Filtering();

            Console.ReadLine();
        }

        #region Converting
        static void ConvertingDataTypes()
        {
            var list = new ArrayList();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            Console.WriteLine(list.Cast<int>().Average()); // designed for weakly typed collections
        }

        static void ConvertDataTypes2()
        {
            var numbers = Enumerable.Range(1, 10);
            var arr = numbers.ToArray();

            var dict = numbers.ToDictionary(i => (double)i / 10, i => i % 2 == 0);
            FormatPrint<double, bool>(dict);
        }

        static void ArrayToEnumberable()
        {
            FormatPrint(new[] { 1, 2, 3 }.AsEnumerable());
        }
        #endregion

        #region Projection
        static void SelectProject()
        {
            var numbers = Enumerable.Range(1, 4);
            var squares = numbers.Select(x => x * x);
            FormatPrint(squares);

            string sentence = "This is a nice sentence";
            var wordLengths = sentence.Split().Select(w => w.Length);
            FormatPrint(wordLengths);

            var wordsWithLength = sentence.Split().Select(w => new { w, w.Length });
            FormatPrint(wordsWithLength);

            Random rand = new Random();
            var randomNumbers = Enumerable.Range(1, 10).Select(_ => rand.Next(10));
            FormatPrint(randomNumbers);

            var sequences = new[] { "red,green,blue", "orange", "white,pink"};
            var allwords = sequences.SelectMany(s => s.Split(','));
            FormatPrint(allwords);

            // cartesian product
            string[] objects = { "house", "car", "bicycle"};
            string[] colors = { "red", "green", "gray" };

            var pairs = colors.SelectMany(_ => objects, (c, o) => $"{c} {o}");
            FormatPrint(pairs);
        }

        #endregion

        #region Filtering
        static void Filtering()
        {
            var numbers = Enumerable.Range(1, 10);
            var evenNumbers = numbers.Where(n => n%2 == 0);

            FormatPrint(evenNumbers);

            var oddSquares = numbers.Select(x => x * x).Where(y => y % 2 == 1);
            FormatPrint(oddSquares);

            object[] values = { 1, 2.5, 3, 4.56 };
            FormatPrint(values.OfType<int>());
            FormatPrint(values.OfType<double>());
            FormatPrint(values.OfType<float>());
        }
        #endregion

        static void FormatPrint(IEnumerable collection)
        {
            foreach (var item in collection)
                Console.WriteLine(item.ToString());

            Console.WriteLine();
        }

        static void FormatPrint<T,K>(Dictionary<T,K> dictionary)
        {
            foreach (var item in dictionary)
            {
                Console.Write(item.Key.ToString());
                Console.Write(" - ");
                Console.WriteLine(item.Value.ToString());
            }
            Console.WriteLine();
        }
    }
}
