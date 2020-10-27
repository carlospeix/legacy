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
        private const string DEFAULT_RESULT = "[resultado]";
        private Application app;

        internal void StartLegacyUi()
        {
            var startInfo = new ProcessStartInfo("legacy-ui.exe")
            {
                WorkingDirectory = $"{System.IO.Directory.GetCurrentDirectory()}\\legacy-ui\\bin\\Debug"
            };
            app = Application.Launch(startInfo);
            
            using (var automation = new UIA2Automation())
            {
                var window = app.GetMainWindow(automation);
                Assert.AreEqual("Legacy Weather", window.Title);
                Assert.AreEqual(DEFAULT_RESULT, GetElement(window, "labelResultado").AsLabel().Text);
            }
        }

        internal void RequestWeatherFor(string location)
        {
            using (var automation = new UIA2Automation())
            {
                var window = app.GetMainWindow(automation);

                var textLocation = GetElement(window, "textCiudad").AsTextBox();
                textLocation.Text = location;

                var buttonSearch = GetElement(window, "buttonBuscar").AsButton();
                buttonSearch.Click();
            }
        }

        internal string GetResultText()
        {
            using (var automation = new UIA2Automation())
            {
                var window = app.GetMainWindow(automation);

                var retryResult = Retry.While<string>(
                    () => GetElement(window, "labelResultado").AsLabel().Text,
                    (value) => value == DEFAULT_RESULT,
                    TimeSpan.FromSeconds(5));

                return retryResult.Result;
            }
        }

        private static AutomationElement GetElement(Window window, string elementName)
        {
            return window.FindFirstDescendant(cf => cf.ByAutomationId(elementName));
        }

        internal void Stop()
        {
            app.Close();
            app.Dispose();
        }
    }
}