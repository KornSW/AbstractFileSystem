
namespace System.IO.Abstraction {

  public enum AfsAttributeType {

    /// <summary> just a simple string (single-line, WITHOUT linebreaks) </summary>
    String = 0,

    /// <summary> an Path which is always starting with (and can be separated by) FORWARD-slash-characters ('/')  </summary>
    AreaPath = 1,

    /// <summary> TagA;TagB;TagC </summary>
    SemicolonSeparadedTags = 2,

    /// <summary>
    /// a string that can contain linebreaks
    /// (specifying a syntax like 'MarkDown' can be declarad using the 'SchemaDefinition' property)
    /// </summary>
    StringMultiline = 3,

    /// <summary> 
    /// a Number, which is an INTEGER in default.
    /// If decimal digits are allowed (see AfsAttributeDescriptor.SchemaDefinition),
    /// then they are always devided by a "." character. The ',' character is forbidden!
    /// </summary>
    Number = 10,

    /// <summary> Date/Time as Unix Timestamp (seconds since 1970.01.01) </summary>
    UnixTimestamp = 20,

    /// <summary> Date/Time in ISO 8601 Format </summary>
    ISODateTime = 21,

    /// <summary> boolean flag "0"|"1" (GENERIC) </summary>
    Flag = 30,

    /// <summary> boolean flag "0"|"1" that indicates the 'ARCHIVE' semantic as known from windows filesystems </summary>
    AchiveFlag = 31,

    /// <summary> boolean flag "0"|"1" that indicates the 'READONLY' semantic as known from windows filesystems </summary>
    WriteProtectionFlag = 32,

    /// <summary> boolean flag "0"|"1" that indicates the 'HIDDEN' semantic as known from windows filesystems </summary>
    HiddenFlag = 33,

    /// <summary> identities (used for the 'Owner' attribute) </summary>
    UserIdenity = 40,

    /// <summary> as JSON string (see also AfsAttributeDescriptor.HasSchema)</summary>
    ObjectGraph = 50,

  }

}
