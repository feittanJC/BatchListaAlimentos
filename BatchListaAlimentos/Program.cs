using Common.Settings;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Net;

using Common.Settings;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    static readonly HttpClient client = new HttpClient();


    public static IConfigurationLib Configuration { get; set; }

    static async Task Main()
    {
        var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

        Configuration = new ConfigurationLib(builder);

        string urlPrefix = Configuration.UrlBase + Configuration.PrefixBackOffice;

        var content = new StringContent("", Encoding.UTF8, "application/json");

        try
        {
            var result = await client.PostAsync(urlPrefix + Configuration.GenerateListController, content);
            var answer = await result.Content.ReadAsStringAsync();
            Console.WriteLine(answer);

            var result1 = await client.PostAsync(urlPrefix + Configuration.GenerateIndexController, content);
            var answer1 = await result1.Content.ReadAsStringAsync();
            Console.WriteLine(answer1);


                var result2 = await client.GetAsync(urlPrefix + Configuration.GeneratePDFController);
                var answer2 = await result2.Content.ReadAsStringAsync();
                Console.WriteLine(answer2);

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.WriteLine("Presiona Enter para salir...");
        Console.ReadLine(); // Pausa para evitar que el programa se cierre inmediatamente
    }
}
