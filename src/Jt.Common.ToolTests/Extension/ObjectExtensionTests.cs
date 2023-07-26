using NUnit.Framework;
using Jt.Common.Tool.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jt.Common.ToolTests;
using Jt.Common.Tool.Helper;
using System.Diagnostics;

namespace Jt.Common.Tool.Extension.Tests
{
    [TestFixture()]
    public class ObjectExtensionTests
    {
        [Test()]
        public void ToJsonTest()
        {
            User user = new User() { Age = 1, Name = "apple" };
            string json = user.ToJson();
            Assert.IsTrue(ValidateHelper.IsJson(json));
        }

        [Test()]
        public void DeepCopyByReflectTest()
        {
            User user = new User() { Age = 1, Name = "apple" };
            var copy = user.DeepCopyByReflect();
            Assert.IsTrue(!user.Equals(copy) && user.ValueEquals(copy) );
        }

        [Test()]
        public void DeepCopyBySerializeTest()
        {
            User user = new User() { Age = 1, Name = "apple" };
            var copy = user.DeepCopyByReflect();
            Assert.IsTrue(!user.Equals(copy) && user.ValueEquals(copy));
        }

        [Test()]
        public void CopyValueTest()
        {
            User user = new User() { Age = 1, Name = "apple" };
            var copy = user.CopyValue<User, User>();
            Assert.IsTrue(!user.Equals(copy) && user.ValueEquals(copy));
        }

        [Test()]
        public void CopyValueTest1()
        {
            User user = new User() { Age = 1, Name = "apple" };
            List<User> users = new List<User>() { user };
            var copy = users.CopyValue<User, User>();
            var flag = true;
            for (int i = 0; i < users.Count; i++)
            {
                flag = users[i].ValueEquals(copy[i]);
            }

            Assert.IsTrue(flag);
        }

        [Test()]
        public void FillEmptyStringTest()
        {
            User user = new User() { Age = 1 };
            user.FillEmptyString();
            Assert.IsTrue(user.Name == "");
        }

        [Test()]
        public void BytesToHexStringTest()
        {
            byte[] bytes = new byte[] { 11, 02 };
            string hexStr = bytes.BytesToHexString();
            Debug.WriteLine(hexStr);
            Assert.IsTrue(hexStr.IsNotNullOrWhiteSpace());
        }

        [Test()]
        public void BytesToHexStringTest1()
        {
            byte[] bytes = new byte[] { 11, 02 };
            string hexStr = bytes.BytesToHexString('/');
            Debug.WriteLine(hexStr);
            Assert.IsTrue(hexStr.IsNotNullOrWhiteSpace());
        }

        [Test()]
        public void ByteToHexStringTest()
        {
            byte bytes = 11;
            string hexStr = bytes.ByteToHexString();
            Debug.WriteLine(hexStr);
            Assert.IsTrue(hexStr.IsNotNullOrWhiteSpace());
        }
    }
}