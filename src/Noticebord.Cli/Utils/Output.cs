using System.IO.Enumeration;
using System.Net;
using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using static Noticebord.Cli.Utils.Output.OutputFormats;

namespace Noticebord.Cli.Utils
{
    public class Output
    {
        static JsonSerializerOptions jsonOptions = new()
        {
            WriteIndented = true
        };

        public enum OutputFormats
        {
            Text,
            Json
        }

        public static void Display<T>(T item, OutputFormats format)
        {
            var output = Format<T>(item, format);
            Console.WriteLine(output);
        }

        public static void DisplayMany<T>(IEnumerable<T> items, OutputFormats format)
        {
            var output = FormatMany<T>(items, format);
            Console.WriteLine(output);
        }

        public static async Task ExportAsync<T>(T item, OutputFormats format)
        {
            var output = Format<T>(item, format);
            var extension = format switch 
            {
                Json => ".json",
                Text or _ => ".txt"
            };

            await File.WriteAllTextAsync(Guid.NewGuid() + extension, output);
        }

        public static async Task ExportManyAsync<T>(IEnumerable<T> items, OutputFormats format)
        {
            var output = FormatMany<T>(items, format);
            var extension = format switch 
            {
                Json => ".json",
                Text or _ => ".txt"
            };

            await File.WriteAllTextAsync(Guid.NewGuid() + extension, output);
        }

        public static string Format<T>(T item, OutputFormats format) =>
            format is Json ? JsonSerializer.Serialize(item, jsonOptions) : item.ToString();

        public static string FormatMany<T>(IEnumerable<T> items, OutputFormats format)
        {
            var output = string.Empty;
            if (format is Json) output = JsonSerializer.Serialize(items, jsonOptions);
            else foreach (var item in items) output += $"{item}\n";

            return output;
        }
    }
}