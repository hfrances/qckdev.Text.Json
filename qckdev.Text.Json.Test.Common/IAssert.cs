using System;
using System.Collections.Generic;
using System.Text;

namespace qckdev.Text.Json.Test.Common
{
    public interface IAssert
    {

        void AreEqual(object expected, object actual);
        void Inconclusive();

    }
}
