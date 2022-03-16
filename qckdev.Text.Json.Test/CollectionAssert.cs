using UnitTesting = Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qckdev.Text.Json.Test
{
    sealed class CollectionAssert : Common.ICollectionAssert
    {
        public void AreEqual(ICollection expected, ICollection actual)
        {
            UnitTesting.CollectionAssert.AreEqual(expected, actual);
        }
    }
}
