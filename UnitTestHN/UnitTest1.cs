using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HNTest;


namespace UnitTestHN
{
    [TestClass]
    public class UnitTest1
    {
 
        [TestMethod]
        public async Task HNLTest()
        {
            HN hn = new HN();

            var x = await hn.Newest();
            Assert.IsTrue(x.Count > 0);

        }
    }
}
