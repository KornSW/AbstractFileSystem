using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.IO.Abstraction {

  [TestClass]
  public class AfsTests2 {

    [TestMethod]
    public void AfsTest2() {


      IAfsRepository repo = new AfsLocalRepository("C:\\Temp");



      var x = repo.ListAllFiles(AfsWellknownAttributeNames.FileSizeBytes, 100, 0);



      Assert.IsFalse(false);

    }

  }

}
