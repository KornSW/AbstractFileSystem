using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace System.IO.Abstraction.DbStore {

public class AllowedValueEntity {

  /// <summary> *this field has a max length of 150 </summary>
  [MaxLength(150), Required]
  public String AttributeName { get; set; }

  [Required]
  public String Value { get; set; }

  [Lookup]
  public virtual AttributeDescriptorEntity AttributeDescriptor { get; set; }

}

public class AttributeDescriptorEntity {

  /// <summary> *this field has a max length of 150 </summary>
  [MaxLength(150), Required]
  public String AttributeName { get; set; }

  [Required]
  public String AttributeType { get; set; }

  [Required]
  public Boolean RequiredOnCreation { get; set; }

  [Required]
  public Boolean Updatable { get; set; }

  [Required]
  public Boolean IsManagedValueRange { get; set; }

  [Referrer]
  public virtual ObservableCollection<AllowedValueEntity> AllowedValues { get; set; } = new ObservableCollection<AllowedValueEntity>();

}

public class FileAttributeEntity {

  [Required]
  public Int64 FileId { get; set; }

  /// <summary> *this field has a max length of 150 </summary>
  [MaxLength(150), Required]
  public String AttributeName { get; set; }

  [Required]
  public String AttributeValue { get; set; }

  [Lookup]
  public virtual FileContentBlobEntity File { get; set; }

  [Lookup]
  public virtual AttributeDescriptorEntity AttributeDescriptor { get; set; }

}

public class FileContentBlobEntity {

  [Required]
  public Int64 FileId { get; set; }

  /// <summary> *this field is optional (use null as value) </summary>
  public Byte[] RawContent { get; set; }

  [Required]
  public String MimeType { get; set; }

  [Referrer]
  public virtual ObservableCollection<FileAttributeEntity> Attributes { get; set; } = new ObservableCollection<FileAttributeEntity>();

}

}
