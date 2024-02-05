
namespace System.IO.Abstraction {

  public enum AfsAttributeType {

    /// <summary> just a simple string </summary>
    String = 0,

    /// <summary> an Path which is always starting with (and can be separated by) FORWARD-slash-characters ('/')  </summary>
    AreaPath = 1,

    /// <summary> an INTEGER </summary>
    Number = 10,

    /// <summary> Date/Time as Unix Timestamp (seconds since 1970.01.01) </summary>
    UnixTimestamp = 20,

    /// <summary> Date/Time in ISO 8601 Format </summary>
    ISODateTime = 21,

    /// <summary> boolean flag (GENERIC) </summary>
    Flag = 30,

    /// <summary> boolean flag that indicates the 'ARCHIVE' semantic as known from windows filesystems </summary>
    AchiveFlag = 31,

    /// <summary> boolean flag that indicates the 'READONLY' semantic as known from windows filesystems </summary>
    WriteProtectionFlag = 32,

    /// <summary> boolean flag that indicates the 'HIDDEN' semantic as known from windows filesystems </summary>
    HiddenFlag = 33,

    /// <summary> identities (used for the 'Owner' attribute) </summary>
    UserIdenity = 40,

  }

}
