using RazorEngine.Configuration;
using RazorEngine.Templating;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using WkHtmlRazorEngine.Models;

namespace WkHtmlRazorEngine
{
    class Program
    {
        static List<UserModel>
            List = new List<UserModel>
            {
                new UserModel
                {
                    Id = 1,
                    Name = "Raphael Basso",
                    Login = "arabasso",
                    Password = "123",
                    Email = "arabasso@yahoo.com.br",
                    Active = true
                },
                new UserModel
                {
                    Id = 2,
                    Name = "José da Silva",
                    Login = "ze",
                    Password = "456",
                    Email = "ze@gmail.com",
                    Active = true
                },
                new UserModel
                {
                    Id = 3,
                    Name = "Maria dos Santos",
                    Login = "maria",
                    Password = "789",
                    Email = "maria@hotmail.com",
                    Active = true
                },
                new UserModel
                {
                    Id = 4,
                    Name = "João da Costa",
                    Login = "jao",
                    Password = "150",
                    Email = "jao@outlook.com",
                    Active = false
                }
            };

        static void Main(string[] args)
        {
            var config = new TemplateServiceConfiguration
            {
                Debug = false,
                DisableTempFileLocking = true,
                CachingProvider = new DefaultCachingProvider(_ => { }),
                TemplateManager = new EmbeddedResourceTemplateManager(typeof(Program))
            };

            using (var service = RazorEngineService.Create(config))
            {
                string pdf = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetRandomFileName(), ".pdf"));

                using (var stream = new FileStream(pdf, FileMode.Create))
                {
                    service.RunCompile("Views.User", stream, List.GetType(), List);
                }

                Process.Start(pdf);
            }
        }
    }
}
