
namespace System.IO.Abstraction {

  public class AfsAttributeDescriptor {

    public String AttributeName { get; set; }

    public AfsAttributeType AttributeType { get; set; } = AfsAttributeType.String;

    public bool RequiredOnCreation { get; set; } = false;
    public bool Updatable { get; set; } = false;

    /// <summary>
    /// Indicates, that the values for this attribute are a 'managed' list of allows values!
    /// These can be evaluated using the 'ListValueRange' method.
    /// This is also possible for attributes with type 'AreaPath' (for the very common use case of
    /// emulating 'Directories' almost like working on a local filesystem)
    /// </summary>
    public bool IsManagedValueRange { get; set; } = false;

  }

}
