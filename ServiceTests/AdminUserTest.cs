using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZSZ.Service;

namespace ServiceTests
{
    [TestClass]
    public class AdminUserTest
    {
        [TestMethod]
        public void AddAdminUserTest()
        {
            AdminUserService adminService = new AdminUserService();

            long id = new AdminUserService().AddAdminUser("cyq", "189", "mima", "123", null);
            var user = adminService.GetById(id);
            Assert.AreEqual("cyq", user.Name);
            Assert.AreEqual("189", user.PhoneNum);
            Assert.AreEqual("123", user.Email);
            Assert.IsTrue(adminService.CheckLogin("189", "mima"));
            Assert.IsFalse(adminService.CheckLogin("189", "mima1"));
            adminService.GetAll();
            Assert.IsNotNull(adminService.GetByPhoneNum("189"));
            adminService.MarkDeleted(id);
        }
        [TestMethod]
        public void PermissionTest()
        {
            PermissionService permService = new PermissionService();
            AdminUserService userService = new AdminUserService();
            RoleService roleService = new RoleService();
            string roleName1 = Guid.NewGuid().ToString();
            string permName1 = Guid.NewGuid().ToString();
            long roleId1 = roleService.AddNew(roleName1);
            long permId = permService.AddPermission(permName1, permName1);
            string userPhone = "139138";
            long userId = userService.AddAdminUser("Aaa", userPhone, "mima", "email", null);
            roleService.AddRoleIds(userId, new long[] { roleId1 });
            permService.AddPermIds(roleId1, new long[] { permId });
            Assert.IsTrue(userService.HasPermission(userId, permName1));
            Assert.IsFalse(userService.HasPermission(userId, "1"));
            userService.MarkDeleted(userId);

        }
    }
}
