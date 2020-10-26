using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.AutomationElements;
using NUnit.Framework;
using System.Diagnostics;

namespace Legacy.Tests.Helpers
{
    internal class ApplicationRunner
    {
        private const string appPath = @"C:\Users\carlos\src\legacy\legacy-ui\bin\Debug\legacy-ui.exe";
        
        private Application app;

        internal void StartLegacyUi()
        {
            var startInfo = new ProcessStartInfo(appPath);
            app = Application.Launch(startInfo);

            using (var automation = new UIA3Automation())
            {
                Assert.AreEqual("Legacy Weather", app.GetMainWindow(automation).Title);
            }
        }

        internal string GetResultText()
        {
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);

                var resultLabel = window.FindFirstDescendant(cf => cf.ByAutomationId("labelResultado")).AsLabel();
                Assert.NotNull(resultLabel, "No encontrada la etiqueta de resultado con nombre labelResultado");

                return resultLabel.Text;
            }
        }

        internal void Stop()
        {
            app.Close();
            app.Dispose();
        }
    }
}