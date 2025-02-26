
namespace System.IO.Abstraction {

  public static class AfsWellknownAttributes {

    public static AfsAttributeDescriptor Id_Guid() {
      return new AfsAttributeDescriptor() {
        AttributeName = AfsWellknownAttributeNames.Id,
        AttributeType = AfsAttributeType.String,
        RequiredOnCreation = false,
        Updatable = false,
        IsManagedValueRange = true,
        SchemaDefinition = null
      };
    }

    public static AfsAttributeDescriptor Id_Numeric() {
      return new AfsAttributeDescriptor() {
        AttributeName = AfsWellknownAttributeNames.Id,
        AttributeType = AfsAttributeType.Number,
        RequiredOnCreation = false,
        Updatable = false,
        IsManagedValueRange = true,
        SchemaDefinition = null
      };
    }

    public static AfsAttributeDescriptor MimeType(bool requiredOnCreation, bool updatable) {
      return new AfsAttributeDescriptor() {
        AttributeName = AfsWellknownAttributeNames.MimeType,
        AttributeType = AfsAttributeType.String,
        RequiredOnCreation = requiredOnCreation,
        Updatable = updatable, 
        IsManagedValueRange = false, //TODO: überlegen, ob das sinn macht...
        SchemaDefinition = null         
      };
    }

    public static AfsAttributeDescriptor FileFullName() {
      return new AfsAttributeDescriptor() {
        AttributeName = AfsWellknownAttributeNames.FileFullName,
        AttributeType = AfsAttributeType.String,
        RequiredOnCreation = false,
        Updatable = false,
        IsManagedValueRange = false,
        SchemaDefinition = null
      };
    }

    public static AfsAttributeDescriptor Directory(bool updatable) {
      return new AfsAttributeDescriptor() {
        AttributeName = AfsWellknownAttributeNames.Directory,
        AttributeType = AfsAttributeType.AreaPath,
        RequiredOnCreation = true,
        Updatable = updatable,
        IsManagedValueRange = true,
        SchemaDefinition = null //TODO: regex aus den erlaubeten chars (dynamisch je nach os)
      };
    }

    public static AfsAttributeDescriptor FileName(bool updatable) {
      return new AfsAttributeDescriptor() {
        AttributeName = AfsWellknownAttributeNames.FileName,
        AttributeType = AfsAttributeType.String,
        RequiredOnCreation = true,
        Updatable = updatable,
        IsManagedValueRange = false,
        SchemaDefinition = null //TODO: regex aus den erlaubeten chars (dynamisch je nach os)
      };
    }

    public static AfsAttributeDescriptor FileSizeBytes() {
      return new AfsAttributeDescriptor() {
        AttributeName = AfsWellknownAttributeNames.FileSizeBytes,
        AttributeType = AfsAttributeType.Number,
        RequiredOnCreation = false,
        Updatable = false,
        IsManagedValueRange = false,
        SchemaDefinition = "0;" + long.MaxValue + ";0" 
      };
    }

    public static AfsAttributeDescriptor TimeOfLastWrite() {
      return new AfsAttributeDescriptor() {
        AttributeName = AfsWellknownAttributeNames.TimeOfLastWrite,
        AttributeType = AfsAttributeType.ISODateTime,
        RequiredOnCreation = false,
        Updatable = false,
        IsManagedValueRange = false,
        SchemaDefinition = null
      };
    }

    public static AfsAttributeDescriptor TimeOfCreation() {
      return new AfsAttributeDescriptor() {
        AttributeName = AfsWellknownAttributeNames.TimeOfCreation,
        AttributeType = AfsAttributeType.ISODateTime,
        RequiredOnCreation = false,
        Updatable = false,
        IsManagedValueRange = false,
        SchemaDefinition = null
      };
    }

    public static AfsAttributeDescriptor AchiveFlag(bool updatable) {
      return new AfsAttributeDescriptor() {
        AttributeName = AfsWellknownAttributeNames.AchiveFlag,
        AttributeType = AfsAttributeType.AchiveFlag,
        RequiredOnCreation = false,
        Updatable = updatable,
        IsManagedValueRange = false,
        SchemaDefinition = null
      };
    }

    public static AfsAttributeDescriptor WriteProtectionFlag(bool updatable) {
      return new AfsAttributeDescriptor() {
        AttributeName = AfsWellknownAttributeNames.WriteProtectionFlag,
        AttributeType = AfsAttributeType.WriteProtectionFlag,
        RequiredOnCreation = false,
        Updatable = updatable,
        IsManagedValueRange = false,
        SchemaDefinition = null
      };
    }

    public static AfsAttributeDescriptor HiddenFlag(bool updatable) {
      return new AfsAttributeDescriptor() {
        AttributeName = AfsWellknownAttributeNames.HiddenFlag,
        AttributeType = AfsAttributeType.HiddenFlag,
        RequiredOnCreation = false,
        Updatable = updatable,
        IsManagedValueRange = false,
        SchemaDefinition = null
      };
    }

    public static AfsAttributeDescriptor Owner(bool requiredOnCreation, bool updatable, bool isManagedValueRange) {
      return new AfsAttributeDescriptor() {
        AttributeName = AfsWellknownAttributeNames.Owner,
        AttributeType = AfsAttributeType.UserIdenity,
        RequiredOnCreation = requiredOnCreation,
        Updatable = updatable,
        IsManagedValueRange = isManagedValueRange,
        SchemaDefinition = null
      };
    }

    /// <summary> A Fallback to be displayed instead of Thumbnails </summary>
    /// <param name="requiredOnCreation"></param>
    /// <param name="updatable"></param>
    /// <returns></returns>
    public static AfsAttributeDescriptor PreviewText(bool requiredOnCreation, bool updatable) {
      return new AfsAttributeDescriptor() {
        AttributeName = AfsWellknownAttributeNames.PreviewText,
        AttributeType = AfsAttributeType.String,
        RequiredOnCreation = requiredOnCreation,
        Updatable = updatable,
        IsManagedValueRange = false,
        SchemaDefinition = null
      };
    }

    /// <summary>
    /// Abstraction: when Files are used to represent Cards on a Board
    /// </summary>
    public static class BoardRelated {

      public static AfsAttributeDescriptor AreaPath(bool updatable) {
        return new AfsAttributeDescriptor() {
          AttributeName = AfsWellknownAttributeNames.BoardRelated.AreaPath,
          AttributeType = AfsAttributeType.AreaPath,
          RequiredOnCreation = true,
          Updatable = updatable,
          IsManagedValueRange = true,
          SchemaDefinition = null
        };
      }

      public static AfsAttributeDescriptor IterationPath(bool updatable) {
        return new AfsAttributeDescriptor() {
          AttributeName = AfsWellknownAttributeNames.BoardRelated.IterationPath,
          AttributeType = AfsAttributeType.AreaPath,
          RequiredOnCreation = true,
          Updatable = updatable,
          IsManagedValueRange = true,
          SchemaDefinition = null
        };
      }

      public static AfsAttributeDescriptor BoardColumn(bool updatable) {
        return new AfsAttributeDescriptor() {
          AttributeName = AfsWellknownAttributeNames.BoardRelated.BoardColumn,
          AttributeType = AfsAttributeType.String,
          RequiredOnCreation = true,
          Updatable = updatable,
          IsManagedValueRange = true,
          SchemaDefinition = null
        };
      }

      public static AfsAttributeDescriptor BoardLane(bool requiredOnCreation, bool updatable) {
        return new AfsAttributeDescriptor() {
          AttributeName = AfsWellknownAttributeNames.BoardRelated.BoardLane,
          AttributeType = AfsAttributeType.String,
          RequiredOnCreation = requiredOnCreation,
          Updatable = updatable,
          IsManagedValueRange = true,
          SchemaDefinition = null
        };
      }

      public static AfsAttributeDescriptor AssignedTo(bool updatable, bool isManagedValueRange) {
        return new AfsAttributeDescriptor() {
          AttributeName = AfsWellknownAttributeNames.BoardRelated.AssignedTo,
          AttributeType = AfsAttributeType.UserIdenity,
          RequiredOnCreation = false,
          Updatable = updatable,
          IsManagedValueRange = isManagedValueRange,
          SchemaDefinition = null
        };
      }

      public static AfsAttributeDescriptor DueDate(bool updatable) {
        return new AfsAttributeDescriptor() {
          AttributeName = AfsWellknownAttributeNames.BoardRelated.DueDate,
          AttributeType = AfsAttributeType.ISODateTime,
          RequiredOnCreation = false,
          Updatable = updatable,
          IsManagedValueRange = false,
          SchemaDefinition = null
        };
      }

      public static AfsAttributeDescriptor SmanticTags(bool updatable, bool isManagedValueRange) {
        return new AfsAttributeDescriptor() {
          AttributeName = AfsWellknownAttributeNames.BoardRelated.SmanticTags,
          AttributeType = AfsAttributeType.SemicolonSeparadedTags,
          RequiredOnCreation = false,
          Updatable = updatable,
          IsManagedValueRange = isManagedValueRange,
          SchemaDefinition = null
        };
      }

      public static AfsAttributeDescriptor LinkedChildKeys(bool updatable, bool isManagedValueRange) {
        return new AfsAttributeDescriptor() {
          AttributeName = AfsWellknownAttributeNames.BoardRelated.LinkedChildKeys,
          AttributeType = AfsAttributeType.SemicolonSeparadedTags,
          RequiredOnCreation = false,
          Updatable = updatable,
          IsManagedValueRange = isManagedValueRange,
          SchemaDefinition = null
        };
      }

      public static AfsAttributeDescriptor LinkedParentKey(bool requiredOnCreation, bool updatable, bool isManagedValueRange) {
        return new AfsAttributeDescriptor() {
          AttributeName = AfsWellknownAttributeNames.BoardRelated.LinkedParentKey,
          AttributeType = AfsAttributeType.String,
          RequiredOnCreation = requiredOnCreation,
          Updatable = updatable,
          IsManagedValueRange = isManagedValueRange,
          SchemaDefinition = null
        };
      }

      public static AfsAttributeDescriptor LinkedRelatedKeys(bool updatable, bool isManagedValueRange) {
        return new AfsAttributeDescriptor() {
          AttributeName = AfsWellknownAttributeNames.BoardRelated.LinkedRelatedKeys,
          AttributeType = AfsAttributeType.SemicolonSeparadedTags,
          RequiredOnCreation = false,
          Updatable = updatable,
          IsManagedValueRange = isManagedValueRange,
          SchemaDefinition = null
        };
      }

      public static AfsAttributeDescriptor LinkedLinkedDuplicateKeys(bool updatable, bool isManagedValueRange) {
        return new AfsAttributeDescriptor() {
          AttributeName = AfsWellknownAttributeNames.BoardRelated.LinkedLinkedDuplicateKeys,
          AttributeType = AfsAttributeType.SemicolonSeparadedTags,
          RequiredOnCreation = false,
          Updatable = updatable,
          IsManagedValueRange = isManagedValueRange,
          SchemaDefinition = null
        };
      }

    }

  }

  public static class AfsWellknownAttributeNames {

    public const string Id = nameof(Id);

    public const string MimeType = nameof(MimeType);

    public const string FileFullName = nameof(FileFullName);

    public const string Directory = nameof(Directory);

    public const string FileName = nameof(FileName);

    public const string FileSizeBytes = nameof(FileSizeBytes);

    public const string TimeOfLastWrite = nameof(TimeOfLastWrite);

    public const string TimeOfCreation = nameof(TimeOfCreation);

    public const string AchiveFlag = nameof(AchiveFlag);

    public const string WriteProtectionFlag = nameof(WriteProtectionFlag);

    public const string HiddenFlag = nameof(HiddenFlag);

    public const string Owner = nameof(Owner);

    /// <summary> A Fallback to be displayed instead of Thumbnails </summary>

    public const string PreviewText = nameof(PreviewText);

    /// <summary>
    /// Abstraction: when Files are used to represent Cards on a Board
    /// </summary>
    public static class BoardRelated {

      public const string AreaPath = nameof(AreaPath);

      public const string IterationPath = nameof(IterationPath);

      public const string BoardColumn = nameof(BoardColumn);

      public const string BoardLane = nameof(BoardLane);

      public const string AssignedTo = nameof(AssignedTo);

      public const string DueDate = nameof(DueDate);

      public const string SmanticTags = nameof(SmanticTags);

      public const string LinkedChildKeys = nameof(LinkedChildKeys);

      public const string LinkedParentKey = nameof(LinkedParentKey);

      public const string LinkedRelatedKeys = nameof(LinkedRelatedKeys);

      public const string LinkedLinkedDuplicateKeys = nameof(LinkedLinkedDuplicateKeys);

    }

  }

}
