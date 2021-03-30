using System;
using System.Collections.Generic;
using System.Linq;
using uTinyRipper.Classes;

using Object = uTinyRipper.Classes.Object;

namespace uTinyRipper.Game.Assembly
{
	public struct SerializableField
	{
		public void Read(AssetReader reader, int depth, in SerializableType.Field etalon)
		{
			switch (etalon.Type.Type)
			{
				case PrimitiveType.Bool:
					if (etalon.IsArray)
					{
						CValue = reader.ReadBooleanArray();
					}
					else
					{
						PValue = reader.ReadBoolean() ? 1U : 0U;
					}
					reader.AlignStream();
					break;

				case PrimitiveType.Char:
					if (etalon.IsArray)
					{
						CValue = reader.ReadCharArray();
					}
					else
					{
						PValue = reader.ReadChar();
					}
					reader.AlignStream();
					break;

				case PrimitiveType.SByte:
					if (etalon.IsArray)
					{
						CValue = reader.ReadByteArray();
					}
					else
					{
						PValue = unchecked((byte)reader.ReadSByte());
					}
					reader.AlignStream();
					break;

				case PrimitiveType.Byte:
					if (etalon.IsArray)
					{
						CValue = reader.ReadByteArray();
					}
					else
					{
						PValue = reader.ReadByte();
					}
					reader.AlignStream();
					break;

				case PrimitiveType.Short:
					if (etalon.IsArray)
					{
						CValue = reader.ReadInt16Array();
					}
					else
					{
						PValue = unchecked((ushort)reader.ReadInt16());
					}
					reader.AlignStream();
					break;

				case PrimitiveType.UShort:
					if (etalon.IsArray)
					{
						CValue = reader.ReadUInt16Array();
					}
					else
					{
						PValue = reader.ReadUInt16();
					}
					reader.AlignStream();
					break;

				case PrimitiveType.Int:
					if (etalon.IsArray)
					{
						CValue = reader.ReadInt32Array();
					}
					else
					{
						PValue = unchecked((uint)reader.ReadInt32());
					}
					break;

				case PrimitiveType.UInt:
					if (etalon.IsArray)
					{
						CValue = reader.ReadUInt32Array();
					}
					else
					{
						PValue = reader.ReadUInt32();
					}
					break;

				case PrimitiveType.Long:
					if (etalon.IsArray)
					{
						CValue = reader.ReadInt64Array();
					}
					else
					{
						PValue = unchecked((ulong)reader.ReadInt64());
					}
					break;

				case PrimitiveType.ULong:
					if (etalon.IsArray)
					{
						CValue = reader.ReadUInt64Array();
					}
					else
					{
						PValue = reader.ReadUInt64();
					}
					break;

				case PrimitiveType.Single:
					if (etalon.IsArray)
					{
						CValue = reader.ReadSingleArray();
					}
					else
					{
						PValue = BitConverterExtensions.ToUInt32(reader.ReadSingle());
					}
					break;

				case PrimitiveType.Double:
					if (etalon.IsArray)
					{
						CValue = reader.ReadDoubleArray();
					}
					else
					{
						PValue = BitConverterExtensions.ToUInt64(reader.ReadDouble());
					}
					break;

				case PrimitiveType.String:
					if (etalon.IsArray)
					{
						CValue = reader.ReadStringArray();
					}
					else
					{
						CValue = reader.ReadString();
					}
					break;

				case PrimitiveType.Complex:
					if (etalon.IsArray)
					{
						int count = reader.ReadInt32();
						IAsset[] structures = new IAsset[count];
						for (int i = 0; i < count; i++)
						{
							IAsset structure = etalon.Type.CreateInstance(depth + 1);
							structure.Read(reader);
							structures[i] = structure;
						}
						CValue = structures;
					}
					else
					{
						IAsset structure = etalon.Type.CreateInstance(depth + 1);
						structure.Read(reader);
						CValue = structure;
					}
					break;

				default:
					throw new NotSupportedException(etalon.Type.Type.ToString());
			}
		}

		public void Write(AssetWriter writer, in SerializableType.Field etalon)
		{
			switch (etalon.Type.Type)
			{
				case PrimitiveType.Bool:
					if (etalon.IsArray)
					{
						((bool[])CValue).Write(writer);
					}
					else
					{
						writer.Write(PValue != 0);
					}
					writer.AlignStream();
					break;

				case PrimitiveType.Char:
					if (etalon.IsArray)
					{
						((char[])CValue).Write(writer);
					}
					else
					{
						writer.Write((char)PValue);
					}
					writer.AlignStream();
					break;

				case PrimitiveType.SByte:
					if (etalon.IsArray)
					{
						((byte[])CValue).Write(writer);
					}
					else
					{
						writer.Write(unchecked((sbyte)PValue));
					}
					writer.AlignStream();
					break;

				case PrimitiveType.Byte:
					if (etalon.IsArray)
					{
						((byte[])CValue).Write(writer);
					}
					else
					{
						writer.Write((byte)PValue);
					}
					writer.AlignStream();
					break;

				case PrimitiveType.Short:
					if (etalon.IsArray)
					{
						((short[])CValue).Write(writer);
					}
					else
					{
						writer.Write(unchecked((short)PValue));
					}
					writer.AlignStream();
					break;

				case PrimitiveType.UShort:
					if (etalon.IsArray)
					{
						((ushort[])CValue).Write(writer);
					}
					else
					{
						writer.Write((ushort)PValue);
					}
					writer.AlignStream();
					break;

				case PrimitiveType.Int:
					if (etalon.IsArray)
					{
						((int[])CValue).Write(writer);
					}
					else
					{
						writer.Write(unchecked((int)PValue));
					}
					break;

				case PrimitiveType.UInt:
					if (etalon.IsArray)
					{
						((uint[])CValue).Write(writer);
					}
					else
					{
						writer.Write((uint)PValue);
					}
					break;

				case PrimitiveType.Long:
					if (etalon.IsArray)
					{
						((long[])CValue).Write(writer);
					}
					else
					{
						writer.Write(unchecked((long)PValue));
					}
					break;

				case PrimitiveType.ULong:
					if (etalon.IsArray)
					{
						((ulong[])CValue).Write(writer);
					}
					else
					{
						writer.Write(PValue);
					}
					break;

				case PrimitiveType.Single:
					if (etalon.IsArray)
					{
						((float[])CValue).Write(writer);
					}
					else
					{
						writer.Write(BitConverterExtensions.ToSingle((uint)PValue));
					}
					break;

				case PrimitiveType.Double:
					if (etalon.IsArray)
					{
						((double[])CValue).Write(writer);
					}
					else
					{
						writer.Write(BitConverterExtensions.ToDouble(PValue));
					}
					break;

				case PrimitiveType.String:
					if (etalon.IsArray)
					{
						((string[])CValue).Write(writer);
					}
					else
					{
						writer.Write((string)CValue);
					}
					break;

				case PrimitiveType.Complex:
					if (etalon.IsArray)
					{
						((IAsset[])CValue).Write(writer);
					}
					else
					{
						((IAsset)CValue).Write(writer);
					}
					break;

				default:
					throw new NotSupportedException(etalon.Type.Type.ToString());
			}
		}
		public IEnumerable<PPtr<Object>> FetchDependencies(DependencyContext context, SerializableType.Field etalon)
		{
			if (etalon.Type.Type == PrimitiveType.Complex)
			{
				if (etalon.IsArray)
				{
					IAsset[] structures = (IAsset[])CValue;
					if (structures.Length > 0 && structures[0] is IDependent)
					{
						foreach (PPtr<Object> asset in context.FetchDependencies(structures.Cast<IDependent>(), etalon.Name))
						{
							yield return asset;
						}
					}
				}
				else
				{
					IAsset structure = (IAsset)CValue;
					if (structure is IDependent dependent)
					{
						foreach (PPtr<Object> asset in context.FetchDependencies(dependent, etalon.Name))
						{
							yield return asset;
						}
					}
				}
			}
		}

		public ulong PValue { get; set; }
		public object CValue { get; set; }
	}
}
