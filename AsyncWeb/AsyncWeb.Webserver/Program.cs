using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.Configure(app =>
        {
            app.Run(async context =>
            {
                //Simulate BAD access of e:g DB
                //Thread.Sleep(1000);
                //Task.Delay(1000).Wait();
                //Task.Delay(TimeSpan.FromSeconds(10)).Wait();

                //Simulate Good Access of DB
                await Task.Delay(TimeSpan.FromSeconds(10));


                await context.Response.WriteAsync("Hello World");
            });
        });
    }).Build().Run();

