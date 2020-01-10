// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: NestedInside.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from NestedInside.proto</summary>
public static partial class NestedInsideReflection {

  #region Descriptor
  /// <summary>File descriptor for NestedInside.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static NestedInsideReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChJOZXN0ZWRJbnNpZGUucHJvdG8ilAEKCUZvb0luc2lkZRIKCgJJZBgBIAEo",
          "BRIRCglGaXJzdE5hbWUYAiABKAkSDwoHU3VybmFtZRgDIAEoCRIrCg1OZXN0",
          "ZWRNZXNzYWdlGAQgASgLMhQuRm9vSW5zaWRlLkJhckluc2lkZRoqCglCYXJJ",
          "bnNpZGUSDAoEU3RhchgBIAEoCRIPCgdGaWdodGVyGAIgASgJYgZwcm90bzM="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::FooInside), global::FooInside.Parser, new[]{ "Id", "FirstName", "Surname", "NestedMessage" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::FooInside.Types.BarInside), global::FooInside.Types.BarInside.Parser, new[]{ "Star", "Fighter" }, null, null, null, null)})
        }));
  }
  #endregion

}
#region Messages
public sealed partial class FooInside : pb::IMessage<FooInside> {
  private static readonly pb::MessageParser<FooInside> _parser = new pb::MessageParser<FooInside>(() => new FooInside());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<FooInside> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::NestedInsideReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public FooInside() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public FooInside(FooInside other) : this() {
    id_ = other.id_;
    firstName_ = other.firstName_;
    surname_ = other.surname_;
    nestedMessage_ = other.nestedMessage_ != null ? other.nestedMessage_.Clone() : null;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public FooInside Clone() {
    return new FooInside(this);
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
  private global::FooInside.Types.BarInside nestedMessage_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public global::FooInside.Types.BarInside NestedMessage {
    get { return nestedMessage_; }
    set {
      nestedMessage_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as FooInside);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(FooInside other) {
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
  public void MergeFrom(FooInside other) {
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
        NestedMessage = new global::FooInside.Types.BarInside();
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
            NestedMessage = new global::FooInside.Types.BarInside();
          }
          input.ReadMessage(NestedMessage);
          break;
        }
      }
    }
  }

  #region Nested types
  /// <summary>Container for nested types declared in the FooInside message type.</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static partial class Types {
    public sealed partial class BarInside : pb::IMessage<BarInside> {
      private static readonly pb::MessageParser<BarInside> _parser = new pb::MessageParser<BarInside>(() => new BarInside());
      private pb::UnknownFieldSet _unknownFields;
      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public static pb::MessageParser<BarInside> Parser { get { return _parser; } }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public static pbr::MessageDescriptor Descriptor {
        get { return global::FooInside.Descriptor.NestedTypes[0]; }
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      pbr::MessageDescriptor pb::IMessage.Descriptor {
        get { return Descriptor; }
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public BarInside() {
        OnConstruction();
      }

      partial void OnConstruction();

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public BarInside(BarInside other) : this() {
        star_ = other.star_;
        fighter_ = other.fighter_;
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public BarInside Clone() {
        return new BarInside(this);
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
        return Equals(other as BarInside);
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public bool Equals(BarInside other) {
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
      public void MergeFrom(BarInside other) {
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

  }
  #endregion

}

#endregion


#endregion Designer generated code