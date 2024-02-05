using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace System.IO.Abstraction.DbStore {

  public partial class AfsDbContext : DbContext {

    public AfsDbContext() {
    }

    public AfsDbContext(string connectionString) {
      _ConnectionString = connectionString;
    }

  }

}
