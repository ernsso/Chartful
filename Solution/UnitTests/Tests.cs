using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chartful;
using Chartful.Model;

namespace UnitTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Test_ParseToHTML()
        {
            bool valid = false;
            Document doc = new Document("True.xml");
            doc.ParseFromXML();
            string test = doc.ParseToHTML();
            string excpect_content = "<html><header></header><body><div style=”Width:596px; Height : 896px; Margin:0; Padding:0; Background:#ffffff; border:solid #999999 1px;font-family:arial; font-size:12px; color:#333333””><IMG style=”position:absolute; Top:10px; left:100px;” src=”Image” /><span style=”position:absolute; Top:100px; left:50px;”>HelloWorld</span></div></body></html>";
            if (excpect_content == test)
                valid = true;

            Assert.IsTrue(valid);
        }
    }
}
