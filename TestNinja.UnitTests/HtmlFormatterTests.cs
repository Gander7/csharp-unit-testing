using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class HtmlFormatterTests
    {
        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseStringWithStrongElement()
        {
            var formatter = new HtmlFormatter();

            var actual = formatter.FormatAsBold("abc");

            // Specific, good but not flexible enough for changes that don't affect this test case
            // ie, adding a period, excalamation mark etc.
            // This is a good choice for simple tests tho
            Assert.That(actual, Is.EqualTo("<strong>abc</strong>"));

            // just right
            Assert.That(actual, Does.StartWith("<strong>").IgnoreCase); // assertion ignores case sensitivity
            Assert.That(actual, Does.EndWith("</strong>"));
            Assert.That(actual, Does.Contain("abc"));
        }
    }
}
