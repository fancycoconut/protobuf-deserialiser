// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: PhilsEdgeCase1.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from PhilsEdgeCase1.proto</summary>
public static partial class PhilsEdgeCase1Reflection {

  #region Descriptor
  /// <summary>File descriptor for PhilsEdgeCase1.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static PhilsEdgeCase1Reflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChRQaGlsc0VkZ2VDYXNlMS5wcm90byIbCglQaGlsRWRnZTESDgoGRmllbGQx",
          "GAEgASgFYgZwcm90bzM="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::PhilEdge1), global::PhilEdge1.Parser, new[]{ "Field1" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class PhilEdge1 : pb::IMessage<PhilEdge1> {
  private static readonly pb::MessageParser<PhilEdge1> _parser = new pb::MessageParser<PhilEdge1>(() => new PhilEdge1());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<PhilEdge1> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::PhilsEdgeCase1Reflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PhilEdge1() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PhilEdge1(PhilEdge1 other) : this() {
    field1_ = other.field1_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PhilEdge1 Clone() {
    return new PhilEdge1(this);
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

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as PhilEdge1);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(PhilEdge1 other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Field1 != other.Field1) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Field1 != 0) hash ^= Field1.GetHashCode();
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
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(PhilEdge1 other) {
    if (other == null) {
      return;
    }
    if (other.Field1 != 0) {
      Field1 = other.Field1;
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
      }
    }
  }

}

#endregion


#endregion Designer generated code