using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace qckdev.Text.Json.Test.Common
{
    public interface ICollectionAssert
    {

        void AreEqual(ICollection expected, ICollection actual);

    }
}
