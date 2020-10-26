using System;
using System.Diagnostics;
using NUnit.Framework;
using FlaUI.Core;
using FlaUI.Core.Tools;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA2;

namespace Legacy.Tests.Helpers
{
    internal class ApplicationRunner
    {
        private Application app;

        internal void StartLegacyUi()
        {
            var startInfo = new ProcessStartInfo("legacy-ui.exe")
            {
                WorkingDirectory = @"C:\Users\carlos\src\legacy\legacy-ui\bin\Debug"
            };
            app = Application.Launch(startInfo);
            
            using (var automation = new UIA2Automation())
            {
                var window = app.GetMainWindow(automation);
                Assert.AreEqual("Legacy Weather", window.Title);
                Assert.AreEqual("[resultado]",
                    window.FindFirstDescendant(cf => cf.ByAutomationId("labelResultado")).AsLabel().Text);
            }
        }

        internal void RequestWeatherFor(string location)
        {
            using (var automation = new UIA2Automation())
            {
                var window = app.GetMainWindow(automation);

                var textLocation = window.FindFirstDescendant(cf => cf.ByAutomationId("textCiudad")).AsTextBox();
                textLocation.Text = location;

                var buttonSearch = window.FindFirstDescendant(cf => cf.ByAutomationId("buttonBuscar")).AsButton();
                buttonSearch.Click();
            }
        }

        internal string GetResultText()
        {
            using (var automation = new UIA2Automation())
            {
                var window = app.GetMainWindow(automation);
                
                var retryResult = Retry.While<string>(
                    () => window.FindFirstDescendant(cf => cf.ByAutomationId("labelResultado")).AsLabel().Text,
                    (value) => value == "[resultado]",
                    TimeSpan.FromSeconds(5));

                return retryResult.Result;
            }
        }

        internal void Stop()
        {
            app.Close();
            app.Dispose();
        }
    }
}