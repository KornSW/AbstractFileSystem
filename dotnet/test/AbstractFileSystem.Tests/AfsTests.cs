using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.IO.Abstraction {

  [TestClass]
  public class AfsTests {

    [TestMethod]
    public void AfsTest() {


      IAfsRepository repo = new AfsLocalRepository("C:\\Temp");



      var x = repo.ListAllFiles(AfsWellknownAttributeNames.FileSizeBytes, 100, 0);





      Assert.IsFalse(false); 










    }














  }

}
