// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: NestedOutside.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from NestedOutside.proto</summary>
public static partial class NestedOutsideReflection {

  #region Descriptor
  /// <summary>File descriptor for NestedOutside.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static NestedOutsideReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChNOZXN0ZWRPdXRzaWRlLnByb3RvImAKCkZvb091dHNpZGUSCgoCSWQYASAB",
          "KAUSEQoJRmlyc3ROYW1lGAIgASgJEg8KB1N1cm5hbWUYAyABKAkSIgoNTmVz",
          "dGVkTWVzc2FnZRgEIAEoCzILLkJhck91dHNpZGUiKwoKQmFyT3V0c2lkZRIM",
          "CgRTdGFyGAEgASgJEg8KB0ZpZ2h0ZXIYAiABKAliBnByb3RvMw=="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::FooOutside), global::FooOutside.Parser, new[]{ "Id", "FirstName", "Surname", "NestedMessage" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::BarOutside), global::BarOutside.Parser, new[]{ "Star", "Fighter" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class FooOutside : pb::IMessage<FooOutside> {
  private static readonly pb::MessageParser<FooOutside> _parser = new pb::MessageParser<FooOutside>(() => new FooOutside());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<FooOutside> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::NestedOutsideReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public FooOutside() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public FooOutside(FooOutside other) : this() {
    id_ = other.id_;
    firstName_ = other.firstName_;
    surname_ = other.surname_;
    nestedMessage_ = other.nestedMessage_ != null ? other.nestedMessage_.Clone() : null;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public FooOutside Clone() {
    return new FooOutside(this);
  }

  /// <summary>Field number for the "Id" field.</summary>
  public const int IdFieldNumber = 1;
  private int id_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Id {
    get { return id_; }
    set {
      id_ = value;
    }
  }

  /// <summary>Field number for the "FirstName" field.</summary>
  public const int FirstNameFieldNumber = 2;
  private string firstName_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string FirstName {
    get { return firstName_; }
    set {
      firstName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "Surname" field.</summary>
  public const int SurnameFieldNumber = 3;
  private string surname_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Surname {
    get { return surname_; }
    set {
      surname_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "NestedMessage" field.</summary>
  public const int NestedMessageFieldNumber = 4;
  private global::BarOutside nestedMessage_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public global::BarOutside NestedMessage {
    get { return nestedMessage_; }
    set {
      nestedMessage_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as FooOutside);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(FooOutside other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Id != other.Id) return false;
    if (FirstName != other.FirstName) return false;
    if (Surname != other.Surname) return false;
    if (!object.Equals(NestedMessage, other.NestedMessage)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Id != 0) hash ^= Id.GetHashCode();
    if (FirstName.Length != 0) hash ^= FirstName.GetHashCode();
    if (Surname.Length != 0) hash ^= Surname.GetHashCode();
    if (nestedMessage_ != null) hash ^= NestedMessage.GetHashCode();
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
    if (Id != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(Id);
    }
    if (FirstName.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(FirstName);
    }
    if (Surname.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(Surname);
    }
    if (nestedMessage_ != null) {
      output.WriteRawTag(34);
      output.WriteMessage(NestedMessage);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Id != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
    }
    if (FirstName.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(FirstName);
    }
    if (Surname.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Surname);
    }
    if (nestedMessage_ != null) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(NestedMessage);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(FooOutside other) {
    if (other == null) {
      return;
    }
    if (other.Id != 0) {
      Id = other.Id;
    }
    if (other.FirstName.Length != 0) {
      FirstName = other.FirstName;
    }
    if (other.Surname.Length != 0) {
      Surname = other.Surname;
    }
    if (other.nestedMessage_ != null) {
      if (nestedMessage_ == null) {
        NestedMessage = new global::BarOutside();
      }
      NestedMessage.MergeFrom(other.NestedMessage);
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
          Id = input.ReadInt32();
          break;
        }
        case 18: {
          FirstName = input.ReadString();
          break;
        }
        case 26: {
          Surname = input.ReadString();
          break;
        }
        case 34: {
          if (nestedMessage_ == null) {
            NestedMessage = new global::BarOutside();
          }
          input.ReadMessage(NestedMessage);
          break;
        }
      }
    }
  }

}

public sealed partial class BarOutside : pb::IMessage<BarOutside> {
  private static readonly pb::MessageParser<BarOutside> _parser = new pb::MessageParser<BarOutside>(() => new BarOutside());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<BarOutside> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::NestedOutsideReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public BarOutside() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public BarOutside(BarOutside other) : this() {
    star_ = other.star_;
    fighter_ = other.fighter_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public BarOutside Clone() {
    return new BarOutside(this);
  }

  /// <summary>Field number for the "Star" field.</summary>
  public const int StarFieldNumber = 1;
  private string star_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Star {
    get { return star_; }
    set {
      star_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "Fighter" field.</summary>
  public const int FighterFieldNumber = 2;
  private string fighter_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Fighter {
    get { return fighter_; }
    set {
      fighter_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as BarOutside);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(BarOutside other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Star != other.Star) return false;
    if (Fighter != other.Fighter) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Star.Length != 0) hash ^= Star.GetHashCode();
    if (Fighter.Length != 0) hash ^= Fighter.GetHashCode();
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
    if (Star.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(Star);
    }
    if (Fighter.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Fighter);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Star.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Star);
    }
    if (Fighter.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Fighter);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(BarOutside other) {
    if (other == null) {
      return;
    }
    if (other.Star.Length != 0) {
      Star = other.Star;
    }
    if (other.Fighter.Length != 0) {
      Fighter = other.Fighter;
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
          Star = input.ReadString();
          break;
        }
        case 18: {
          Fighter = input.ReadString();
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code