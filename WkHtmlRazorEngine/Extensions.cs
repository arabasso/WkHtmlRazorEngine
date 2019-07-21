using RazorEngine.Templating;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace WkHtmlRazorEngine
{
    public static class Extensions
    {
        public static void RunCompile(
            this IRazorEngineService service,
            string templateKey,
            Stream stream,
            Type type,
            object model)
        {
            var wkhtmltopdf = Path.Combine(@"C:\Program Files\wkhtmltopdf", "bin", "wkhtmltopdf.exe");

            var startInfo = new ProcessStartInfo(wkhtmltopdf, "-q - -")
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(startInfo);

            using (var s = new StreamWriter(process.StandardInput.BaseStream, Encoding.UTF8))
            {
                service.RunCompile(templateKey, s, type, model);
            }

            process.StandardOutput.BaseStream.CopyTo(stream);
        }
    }
}
