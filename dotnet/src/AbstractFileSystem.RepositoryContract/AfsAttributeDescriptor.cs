
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

    /// <summary>
    /// If provided, then values for this attribute are required
    /// to be compliant to this schema.
    /// For AfsAttributeType.ObjectGraph, this would be an 'FUSE-fx.ModelDescription'.
    /// For all other string-based attribute types (AfsAttributeType.String|.AreaPath|...)
    /// this would be an RegEx-Expression.
    /// For numeric attributes this can be a an expression like '-100;100;2' to
    /// declare that the number should be within -100 to 100 and has a decimal precition of 2
    /// </summary>
    public string SchemaDefinition { get; set; } = null;

  }

}
