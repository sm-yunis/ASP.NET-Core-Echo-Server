using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Hosting;

namespace EchoServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(new string[] { "http://0.0.0.0:5000" });
                    webBuilder.Configure(app =>
                    {

                        app.Run(async context =>
                        {

                            await context.Response.WriteAsync("************************\n");
                            await context.Response.WriteAsync($"Method :  {context.Request.Method.ToUpper()} \n");
                            await context.Response.WriteAsync($"Path :  {context.Request.GetEncodedPathAndQuery() } \n");


                            await context.Response.WriteAsync("\n******* Headers ********\n");
                            foreach (var header in context.Request.Headers)
                            {
                                await context.Response.WriteAsync($"{header.Key} : {header.Value} \n");
                            }

                            await context.Response.WriteAsync("\n********* Body *********\n");

                            string requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
                            await context.Response.WriteAsync(requestBody);


                            await context.Response.WriteAsync("\n********* Time *********\n");
                            await context.Response.WriteAsync(DateTime.Now.ToString());
                            await context.Response.WriteAsync("\n\n************************\n");


                        });

                    });

                });



    }
}
