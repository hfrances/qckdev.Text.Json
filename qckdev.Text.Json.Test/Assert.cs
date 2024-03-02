using UnitTesting = Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace qckdev.Text.Json.Test
{
    sealed class Assert : Common.IAssert
    {
        public void AreEqual(object expected, object actual)
        {
            UnitTesting.Assert.AreEqual(expected, actual);
        }

        public void Inconclusive()
        {
            UnitTesting.Assert.Inconclusive();
        }
    }
}
