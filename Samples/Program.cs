using System;
using System.Collections.Generic;
using System.Linq;

namespace Packt.Csharp.Linq.Samples
{
    public sealed class Program
    {
        private static void Main()
        {
            /* Consider to remove comments to execute examples. */

            //RunExample1();
            //RunExample1_New();
            //RunExample2();
            //RunExample3();
            //RunExample4();
        }

        private static void RunExample1()
        {
            var vowels = new[] { 'a', 'e', 'i', 'o', 'u' };
            var upperVowels = vowels.Select(char.ToUpper);
            Console.WriteLine(string.Join(", ", upperVowels));

            /* OUTPUT:
            * A, E, I, O, U
            */
        }

        private static void RunExample1_New()
        {
            var vowels = new[] { 'a', 'e', 'i', 'o', 'u' };
            var upperVowels = from vowel in vowels select char.ToUpper(vowel);
            Console.WriteLine(string.Join(", ", upperVowels));

            /* OUTPUT:
            * A, E, I, O, U
            */
        }

        private static void RunExample2()
        {
            var elements = new (int id, string text)[]
            {
                (1, "lorem"),
                (2, "ipsum"),
                (3, "dolor")
            };
            var filteredElements = elements
                .Where(_ => !_.text.Contains("m"));
            var formattedElements = filteredElements
                .Select(_ => $"[id: {_.id}, text: {_.text}]");
            Console.WriteLine(string.Join(", ", formattedElements));

            /* OUTPUT:
            * [id: 3, text: dolor]
            */
        }

        private static void RunExample3()
        {
            var random = new Random();
            var elements = new Dictionary<int, (string text, bool randomBool)>()
            {
                [1] = ("lorem", random.NextDouble() >= 0.5),
                [2] = ("ipsum", random.NextDouble() >= 0.5),
                [3] = ("dolor", random.NextDouble() >= 0.5)
            };
            var filteredElements = new List<KeyValuePair<int, (string, bool)>>();
            foreach (var element in elements)
            {
                var (_, randomBool) = element.Value;
                if (randomBool)
                    filteredElements.Add(element);
            }
            var formattedElements = new List<string>();
            foreach (var filteredElement in filteredElements)
                formattedElements.Add(
                    $"[key: {filteredElement.Key}, value -> " +
                    $"(text: {filteredElement.Value.Item1}, " +
                    $"randomBool: {filteredElement.Value.Item2})]"
                );
            Console.WriteLine(
                formattedElements.Count == 0
                ? "empty!"
                : string.Join(",\n", formattedElements)
            );

            /* OUTPUT (it can vary, because 'randomBool' is set randomly):
            * [key: 3, value -> (text: dolor, randomBool: True)]
            */
        }

        private static void RunExample4()
        {
            var random = new Random();
            var elements = new Dictionary<int, (string text, bool randomBool)>()
            {
                [1] = ("lorem", random.NextDouble() >= 0.5),
                [2] = ("ipsum", random.NextDouble() >= 0.5),
                [3] = ("dolor", random.NextDouble() >= 0.5)
            };
            var filteredElements =
                from element in elements
                where element.Value.randomBool
                select element;
            var formattedElements =
                from filteredElement in filteredElements
                select
                    $"[key: {filteredElement.Key}, value -> " +
                    $"(text: {filteredElement.Value.text}, " +
                    $"randomBool: {filteredElement.Value.randomBool})]";
            Console.WriteLine(
                formattedElements.Count() == 0
                ? "empty!"
                : string.Join(",\n", formattedElements)
            );

            /* OUTPUT (it can vary, because 'randomBool' is set randomly):
            * [key: 2, value -> (text: ipsum, randomBool: True)],
            * [key: 3, value -> (text: dolor, randomBool: True)]
            */
        }
    }
}
