using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZSZ.Service;

namespace ServiceTests
{
    [TestClass]
    public class AdminLogTest
    {
        [TestMethod]
        public void AddAdminLogTest()
        {
            new AdminLogService().AddNew(1, "测试消息");
        }
    }
}
