using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Threading;

namespace Test
{
    static class TestUtility
    {
        internal static void TestExceptionMessage(Action action, string english, string japanese)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            TestExceptionMessage(action, english);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ja");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja");
            TestExceptionMessage(action, japanese);
        }

        private static void TestExceptionMessage(Action action, string message)
        {
            try
            {
                action();
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Contains(message));
            }
        }
    }
}
