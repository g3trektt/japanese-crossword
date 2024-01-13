using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace japanese
{
    internal class NUTest
    {
        [Test]
        public void BitmapTest()
        {
            Bitmap bmp = new Bitmap(@"C:\Users\levik\OneDrive\Документы\ТУСУР\Курсач\japanese.bmp");
            Assert.DoesNotThrow(() => { var pic = BitmapTools.ToArray(bmp); }) ;
            Assert.Pass();

        }
        [Test]
        public void DBTest()
        {
            var db = new Database(@"C:\Users\levik\OneDrive\Документы\ТУСУР\Курсач\japanese\japaneseLoginBase.db");
            Assert.That(db.CheckUser("a", "a") != "a");

        }

    }
}
