// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: DuplicateFieldNamesWithDifferentTypes.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from DuplicateFieldNamesWithDifferentTypes.proto</summary>
public static partial class DuplicateFieldNamesWithDifferentTypesReflection {

  #region Descriptor
  /// <summary>File descriptor for DuplicateFieldNamesWithDifferentTypes.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static DuplicateFieldNamesWithDifferentTypesReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "CitEdXBsaWNhdGVGaWVsZE5hbWVzV2l0aERpZmZlcmVudFR5cGVzLnByb3Rv",
          "IjUKA0R1cBIOCgZGaWVsZDEYASABKAUSDgoGRmllbGQyGAIgASgFEg4KBkZp",
          "ZWxkMxgDIAEoBSIqCgVEdXBlchIOCgZGaWVsZDEYASABKAkSEQoDUGV0GAIg",
          "ASgLMgQuRHVwYgZwcm90bzM="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::Dup), global::Dup.Parser, new[]{ "Field1", "Field2", "Field3" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::Duper), global::Duper.Parser, new[]{ "Field1", "Pet" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class Dup : pb::IMessage<Dup> {
  private static readonly pb::MessageParser<Dup> _parser = new pb::MessageParser<Dup>(() => new Dup());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<Dup> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::DuplicateFieldNamesWithDifferentTypesReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Dup() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Dup(Dup other) : this() {
    field1_ = other.field1_;
    field2_ = other.field2_;
    field3_ = other.field3_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Dup Clone() {
    return new Dup(this);
  }

  /// <summary>Field number for the "Field1" field.</summary>
  public const int Field1FieldNumber = 1;
  private int field1_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Field1 {
    get { return field1_; }
    set {
      field1_ = value;
    }
  }

  /// <summary>Field number for the "Field2" field.</summary>
  public const int Field2FieldNumber = 2;
  private int field2_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Field2 {
    get { return field2_; }
    set {
      field2_ = value;
    }
  }

  /// <summary>Field number for the "Field3" field.</summary>
  public const int Field3FieldNumber = 3;
  private int field3_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Field3 {
    get { return field3_; }
    set {
      field3_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as Dup);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(Dup other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Field1 != other.Field1) return false;
    if (Field2 != other.Field2) return false;
    if (Field3 != other.Field3) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Field1 != 0) hash ^= Field1.GetHashCode();
    if (Field2 != 0) hash ^= Field2.GetHashCode();
    if (Field3 != 0) hash ^= Field3.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (Field1 != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(Field1);
    }
    if (Field2 != 0) {
      output.WriteRawTag(16);
      output.WriteInt32(Field2);
    }
    if (Field3 != 0) {
      output.WriteRawTag(24);
      output.WriteInt32(Field3);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Field1 != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Field1);
    }
    if (Field2 != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Field2);
    }
    if (Field3 != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Field3);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(Dup other) {
    if (other == null) {
      return;
    }
    if (other.Field1 != 0) {
      Field1 = other.Field1;
    }
    if (other.Field2 != 0) {
      Field2 = other.Field2;
    }
    if (other.Field3 != 0) {
      Field3 = other.Field3;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          Field1 = input.ReadInt32();
          break;
        }
        case 16: {
          Field2 = input.ReadInt32();
          break;
        }
        case 24: {
          Field3 = input.ReadInt32();
          break;
        }
      }
    }
  }

}

public sealed partial class Duper : pb::IMessage<Duper> {
  private static readonly pb::MessageParser<Duper> _parser = new pb::MessageParser<Duper>(() => new Duper());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<Duper> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::DuplicateFieldNamesWithDifferentTypesReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Duper() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Duper(Duper other) : this() {
    field1_ = other.field1_;
    pet_ = other.pet_ != null ? other.pet_.Clone() : null;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Duper Clone() {
    return new Duper(this);
  }

  /// <summary>Field number for the "Field1" field.</summary>
  public const int Field1FieldNumber = 1;
  private string field1_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Field1 {
    get { return field1_; }
    set {
      field1_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "Pet" field.</summary>
  public const int PetFieldNumber = 2;
  private global::Dup pet_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public global::Dup Pet {
    get { return pet_; }
    set {
      pet_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as Duper);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(Duper other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Field1 != other.Field1) return false;
    if (!object.Equals(Pet, other.Pet)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Field1.Length != 0) hash ^= Field1.GetHashCode();
    if (pet_ != null) hash ^= Pet.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (Field1.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(Field1);
    }
    if (pet_ != null) {
      output.WriteRawTag(18);
      output.WriteMessage(Pet);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Field1.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Field1);
    }
    if (pet_ != null) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(Pet);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(Duper other) {
    if (other == null) {
      return;
    }
    if (other.Field1.Length != 0) {
      Field1 = other.Field1;
    }
    if (other.pet_ != null) {
      if (pet_ == null) {
        Pet = new global::Dup();
      }
      Pet.MergeFrom(other.Pet);
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          Field1 = input.ReadString();
          break;
        }
        case 18: {
          if (pet_ == null) {
            Pet = new global::Dup();
          }
          input.ReadMessage(Pet);
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code
