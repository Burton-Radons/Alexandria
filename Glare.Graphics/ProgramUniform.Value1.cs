
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace Glare.Graphics {
	public partial class ProgramUniform {

							unsafe static void FromBoolean(int first, int count, IList<bool> data) { for(int index = 0; index < count; index++)data[first + index] = (bool)(( Marshal.ReadInt32(list, (index + 0) * 4 * 1 * 4) != 0 ? 1 : 0 ) != 0); }					unsafe static void FromDouble(int first, int count, IList<bool> data) { for(int index = 0; index < count; index++)data[first + index] = (bool)(( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 1) ) != 0); }unsafe static void FromSingle(int first, int count, IList<bool> data) { for(int index = 0; index < count; index++)data[first + index] = (bool)(( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 1) ) != 0); }unsafe static void FromInt32(int first, int count, IList<bool> data) { for(int index = 0; index < count; index++)data[first + index] = (bool)(( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 1) ) != 0); }unsafe static void FromUInt32(int first, int count, IList<bool> data) { for(int index = 0; index < count; index++)data[first + index] = (bool)(( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 1) ) != 0); }
					unsafe static void ToBoolean(int first, int count, IList<bool> data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (data[first + index] ? 1 : 0) != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<bool> data)
					{
						GetList(count * 2 * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 1 * 8, BitConverter.DoubleToInt64Bits((double)(data[first + index] ? 1 : 0)));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<bool> data)
					{
						SetupFloatList(count * 1);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 1 / 4] = (float)(data[first + index] ? 1 : 0);
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<bool> data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)(data[first + index] ? 1 : 0));
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<bool> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)(uint)(data[first + index] ? 1 : 0));
							}
					}
									unsafe static void FromBoolean(int count, bool* data) { for(int index = 0; index < count; index++)data[index] = (bool)(( Marshal.ReadInt32(list, (index + 0) * 4 * 1 * 4) != 0 ? 1 : 0 ) != 0); }					unsafe static void FromDouble(int count, bool* data) { for(int index = 0; index < count; index++)data[index] = (bool)(( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 1) ) != 0); }unsafe static void FromSingle(int count, bool* data) { for(int index = 0; index < count; index++)data[index] = (bool)(( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 1) ) != 0); }unsafe static void FromInt32(int count, bool* data) { for(int index = 0; index < count; index++)data[index] = (bool)(( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 1) ) != 0); }unsafe static void FromUInt32(int count, bool* data) { for(int index = 0; index < count; index++)data[index] = (bool)(( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 1) ) != 0); }
					unsafe static void ToBoolean(int count, bool* data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (data[index] ? 1 : 0) != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int count, bool* data)
					{
						GetList(count * 2 * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 1 * 8, BitConverter.DoubleToInt64Bits((double)(data[index] ? 1 : 0)));
							}
					}

					unsafe static void ToSingle(int count, bool* data)
					{
						SetupFloatList(count * 1);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 1 / 4] = (float)(data[index] ? 1 : 0);
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int count, bool* data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)(data[index] ? 1 : 0));
							}
					}

					unsafe static void ToUInt32(int count, bool* data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)(uint)(data[index] ? 1 : 0));
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<double> data) { for(int index = 0; index < count; index++)data[first + index] = (double)( Marshal.ReadInt32(list, (index + 0) * 4 * 1 * 4) != 0 ? 1 : 0 ); }					unsafe static void FromDouble(int first, int count, IList<double> data) { for(int index = 0; index < count; index++)data[first + index] = (double)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromSingle(int first, int count, IList<double> data) { for(int index = 0; index < count; index++)data[first + index] = (double)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromInt32(int first, int count, IList<double> data) { for(int index = 0; index < count; index++)data[first + index] = (double)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromUInt32(int first, int count, IList<double> data) { for(int index = 0; index < count; index++)data[first + index] = (double)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 1) ); }
					unsafe static void ToBoolean(int first, int count, IList<double> data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, data[first + index] != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<double> data)
					{
						GetList(count * 2 * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 1 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index]));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<double> data)
					{
						SetupFloatList(count * 1);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 1 / 4] = (float)data[first + index];
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<double> data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)data[first + index]);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<double> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)(uint)data[first + index]);
							}
					}
									unsafe static void FromBoolean(int count, double* data) { for(int index = 0; index < count; index++)data[index] = (double)( Marshal.ReadInt32(list, (index + 0) * 4 * 1 * 4) != 0 ? 1 : 0 ); }					unsafe static void FromDouble(int count, double* data) { for(int index = 0; index < count; index++)data[index] = (double)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromSingle(int count, double* data) { for(int index = 0; index < count; index++)data[index] = (double)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromInt32(int count, double* data) { for(int index = 0; index < count; index++)data[index] = (double)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromUInt32(int count, double* data) { for(int index = 0; index < count; index++)data[index] = (double)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 1) ); }
					unsafe static void ToBoolean(int count, double* data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, data[index] != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int count, double* data)
					{
						GetList(count * 2 * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 1 * 8, BitConverter.DoubleToInt64Bits((double)data[index]));
							}
					}

					unsafe static void ToSingle(int count, double* data)
					{
						SetupFloatList(count * 1);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 1 / 4] = (float)data[index];
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int count, double* data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)data[index]);
							}
					}

					unsafe static void ToUInt32(int count, double* data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)(uint)data[index]);
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<Vector2d> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2d((double)( Marshal.ReadInt32(list, (index + 0) * 4 * 2 * 4) != 0 ? 1 : 0 ), (double)( Marshal.ReadInt32(list, (index + 1) * 4 * 2 * 4) != 0 ? 1 : 0 )); }					unsafe static void FromDouble(int first, int count, IList<Vector2d> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2d((double)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 2) ), (double)( *((System.Double*)list.ToPointer() + (index + 1) * 4 * 2) )); }unsafe static void FromSingle(int first, int count, IList<Vector2d> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2d((double)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 2) ), (double)( *((System.Single*)list.ToPointer() + (index + 1) * 4 * 2) )); }unsafe static void FromInt32(int first, int count, IList<Vector2d> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2d((double)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 2) ), (double)( *((System.Int32*)list.ToPointer() + (index + 1) * 4 * 2) )); }unsafe static void FromUInt32(int first, int count, IList<Vector2d> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2d((double)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 2) ), (double)( *((System.UInt32*)list.ToPointer() + (index + 1) * 4 * 2) )); }
					unsafe static void ToBoolean(int first, int count, IList<Vector2d> data)
					{
						GetList(count * 2);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 2 * 4, data[first + index].X != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 1) * 4 * 2 * 4, data[first + index].Y != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<Vector2d> data)
					{
						GetList(count * 2 * 2);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 2 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].X));
															Marshal.WriteInt64(list, (index + 1) * 4 * 2 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Y));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<Vector2d> data)
					{
						SetupFloatList(count * 2);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 2 / 4] = (float)data[first + index].X;
															floatList[(index + 1) * 4 * 2 / 4] = (float)data[first + index].Y;
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<Vector2d> data)
					{
						GetList(count * 2);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 2 * 4, (int)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 2 * 4, (int)data[first + index].Y);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<Vector2d> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 2 * 4, (int)(uint)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 2 * 4, (int)(uint)data[first + index].Y);
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<Vector3d> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3d((double)( Marshal.ReadInt32(list, (index + 0) * 4 * 3 * 4) != 0 ? 1 : 0 ), (double)( Marshal.ReadInt32(list, (index + 1) * 4 * 3 * 4) != 0 ? 1 : 0 ), (double)( Marshal.ReadInt32(list, (index + 2) * 4 * 3 * 4) != 0 ? 1 : 0 )); }					unsafe static void FromDouble(int first, int count, IList<Vector3d> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3d((double)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 3) ), (double)( *((System.Double*)list.ToPointer() + (index + 1) * 4 * 3) ), (double)( *((System.Double*)list.ToPointer() + (index + 2) * 4 * 3) )); }unsafe static void FromSingle(int first, int count, IList<Vector3d> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3d((double)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 3) ), (double)( *((System.Single*)list.ToPointer() + (index + 1) * 4 * 3) ), (double)( *((System.Single*)list.ToPointer() + (index + 2) * 4 * 3) )); }unsafe static void FromInt32(int first, int count, IList<Vector3d> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3d((double)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 3) ), (double)( *((System.Int32*)list.ToPointer() + (index + 1) * 4 * 3) ), (double)( *((System.Int32*)list.ToPointer() + (index + 2) * 4 * 3) )); }unsafe static void FromUInt32(int first, int count, IList<Vector3d> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3d((double)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 3) ), (double)( *((System.UInt32*)list.ToPointer() + (index + 1) * 4 * 3) ), (double)( *((System.UInt32*)list.ToPointer() + (index + 2) * 4 * 3) )); }
					unsafe static void ToBoolean(int first, int count, IList<Vector3d> data)
					{
						GetList(count * 3);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 3 * 4, data[first + index].X != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 1) * 4 * 3 * 4, data[first + index].Y != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 2) * 4 * 3 * 4, data[first + index].Z != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<Vector3d> data)
					{
						GetList(count * 2 * 3);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 3 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].X));
															Marshal.WriteInt64(list, (index + 1) * 4 * 3 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Y));
															Marshal.WriteInt64(list, (index + 2) * 4 * 3 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Z));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<Vector3d> data)
					{
						SetupFloatList(count * 3);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 3 / 4] = (float)data[first + index].X;
															floatList[(index + 1) * 4 * 3 / 4] = (float)data[first + index].Y;
															floatList[(index + 2) * 4 * 3 / 4] = (float)data[first + index].Z;
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<Vector3d> data)
					{
						GetList(count * 3);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 3 * 4, (int)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 3 * 4, (int)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 3 * 4, (int)data[first + index].Z);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<Vector3d> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 3 * 4, (int)(uint)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 3 * 4, (int)(uint)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 3 * 4, (int)(uint)data[first + index].Z);
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<Vector4d> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4d((double)( Marshal.ReadInt32(list, (index + 0) * 4 * 4 * 4) != 0 ? 1 : 0 ), (double)( Marshal.ReadInt32(list, (index + 1) * 4 * 4 * 4) != 0 ? 1 : 0 ), (double)( Marshal.ReadInt32(list, (index + 2) * 4 * 4 * 4) != 0 ? 1 : 0 ), (double)( Marshal.ReadInt32(list, (index + 3) * 4 * 4 * 4) != 0 ? 1 : 0 )); }					unsafe static void FromDouble(int first, int count, IList<Vector4d> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4d((double)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 4) ), (double)( *((System.Double*)list.ToPointer() + (index + 1) * 4 * 4) ), (double)( *((System.Double*)list.ToPointer() + (index + 2) * 4 * 4) ), (double)( *((System.Double*)list.ToPointer() + (index + 3) * 4 * 4) )); }unsafe static void FromSingle(int first, int count, IList<Vector4d> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4d((double)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 4) ), (double)( *((System.Single*)list.ToPointer() + (index + 1) * 4 * 4) ), (double)( *((System.Single*)list.ToPointer() + (index + 2) * 4 * 4) ), (double)( *((System.Single*)list.ToPointer() + (index + 3) * 4 * 4) )); }unsafe static void FromInt32(int first, int count, IList<Vector4d> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4d((double)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 4) ), (double)( *((System.Int32*)list.ToPointer() + (index + 1) * 4 * 4) ), (double)( *((System.Int32*)list.ToPointer() + (index + 2) * 4 * 4) ), (double)( *((System.Int32*)list.ToPointer() + (index + 3) * 4 * 4) )); }unsafe static void FromUInt32(int first, int count, IList<Vector4d> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4d((double)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 4) ), (double)( *((System.UInt32*)list.ToPointer() + (index + 1) * 4 * 4) ), (double)( *((System.UInt32*)list.ToPointer() + (index + 2) * 4 * 4) ), (double)( *((System.UInt32*)list.ToPointer() + (index + 3) * 4 * 4) )); }
					unsafe static void ToBoolean(int first, int count, IList<Vector4d> data)
					{
						GetList(count * 4);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 4 * 4, data[first + index].X != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 1) * 4 * 4 * 4, data[first + index].Y != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 2) * 4 * 4 * 4, data[first + index].Z != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 3) * 4 * 4 * 4, data[first + index].W != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<Vector4d> data)
					{
						GetList(count * 2 * 4);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].X));
															Marshal.WriteInt64(list, (index + 1) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Y));
															Marshal.WriteInt64(list, (index + 2) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Z));
															Marshal.WriteInt64(list, (index + 3) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].W));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<Vector4d> data)
					{
						SetupFloatList(count * 4);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 4 / 4] = (float)data[first + index].X;
															floatList[(index + 1) * 4 * 4 / 4] = (float)data[first + index].Y;
															floatList[(index + 2) * 4 * 4 / 4] = (float)data[first + index].Z;
															floatList[(index + 3) * 4 * 4 / 4] = (float)data[first + index].W;
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<Vector4d> data)
					{
						GetList(count * 4);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 4 * 4, (int)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 4 * 4, (int)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 4 * 4, (int)data[first + index].Z);
															Marshal.WriteInt32(list, (index + 3) * 4 * 4 * 4, (int)data[first + index].W);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<Vector4d> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 4 * 4, (int)(uint)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 4 * 4, (int)(uint)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 4 * 4, (int)(uint)data[first + index].Z);
															Marshal.WriteInt32(list, (index + 3) * 4 * 4 * 4, (int)(uint)data[first + index].W);
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<float> data) { for(int index = 0; index < count; index++)data[first + index] = (float)( Marshal.ReadInt32(list, (index + 0) * 4 * 1 * 4) != 0 ? 1 : 0 ); }					unsafe static void FromDouble(int first, int count, IList<float> data) { for(int index = 0; index < count; index++)data[first + index] = (float)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromSingle(int first, int count, IList<float> data) { for(int index = 0; index < count; index++)data[first + index] = (float)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromInt32(int first, int count, IList<float> data) { for(int index = 0; index < count; index++)data[first + index] = (float)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromUInt32(int first, int count, IList<float> data) { for(int index = 0; index < count; index++)data[first + index] = (float)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 1) ); }
					unsafe static void ToBoolean(int first, int count, IList<float> data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, data[first + index] != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<float> data)
					{
						GetList(count * 2 * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 1 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index]));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<float> data)
					{
						SetupFloatList(count * 1);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 1 / 4] = (float)data[first + index];
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<float> data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)data[first + index]);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<float> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)(uint)data[first + index]);
							}
					}
									unsafe static void FromBoolean(int count, float* data) { for(int index = 0; index < count; index++)data[index] = (float)( Marshal.ReadInt32(list, (index + 0) * 4 * 1 * 4) != 0 ? 1 : 0 ); }					unsafe static void FromDouble(int count, float* data) { for(int index = 0; index < count; index++)data[index] = (float)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromSingle(int count, float* data) { for(int index = 0; index < count; index++)data[index] = (float)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromInt32(int count, float* data) { for(int index = 0; index < count; index++)data[index] = (float)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromUInt32(int count, float* data) { for(int index = 0; index < count; index++)data[index] = (float)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 1) ); }
					unsafe static void ToBoolean(int count, float* data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, data[index] != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int count, float* data)
					{
						GetList(count * 2 * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 1 * 8, BitConverter.DoubleToInt64Bits((double)data[index]));
							}
					}

					unsafe static void ToSingle(int count, float* data)
					{
						SetupFloatList(count * 1);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 1 / 4] = (float)data[index];
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int count, float* data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)data[index]);
							}
					}

					unsafe static void ToUInt32(int count, float* data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)(uint)data[index]);
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<Vector2f> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2f((float)( Marshal.ReadInt32(list, (index + 0) * 4 * 2 * 4) != 0 ? 1 : 0 ), (float)( Marshal.ReadInt32(list, (index + 1) * 4 * 2 * 4) != 0 ? 1 : 0 )); }					unsafe static void FromDouble(int first, int count, IList<Vector2f> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2f((float)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 2) ), (float)( *((System.Double*)list.ToPointer() + (index + 1) * 4 * 2) )); }unsafe static void FromSingle(int first, int count, IList<Vector2f> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2f((float)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 2) ), (float)( *((System.Single*)list.ToPointer() + (index + 1) * 4 * 2) )); }unsafe static void FromInt32(int first, int count, IList<Vector2f> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2f((float)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 2) ), (float)( *((System.Int32*)list.ToPointer() + (index + 1) * 4 * 2) )); }unsafe static void FromUInt32(int first, int count, IList<Vector2f> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2f((float)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 2) ), (float)( *((System.UInt32*)list.ToPointer() + (index + 1) * 4 * 2) )); }
					unsafe static void ToBoolean(int first, int count, IList<Vector2f> data)
					{
						GetList(count * 2);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 2 * 4, data[first + index].X != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 1) * 4 * 2 * 4, data[first + index].Y != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<Vector2f> data)
					{
						GetList(count * 2 * 2);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 2 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].X));
															Marshal.WriteInt64(list, (index + 1) * 4 * 2 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Y));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<Vector2f> data)
					{
						SetupFloatList(count * 2);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 2 / 4] = (float)data[first + index].X;
															floatList[(index + 1) * 4 * 2 / 4] = (float)data[first + index].Y;
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<Vector2f> data)
					{
						GetList(count * 2);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 2 * 4, (int)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 2 * 4, (int)data[first + index].Y);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<Vector2f> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 2 * 4, (int)(uint)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 2 * 4, (int)(uint)data[first + index].Y);
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<Vector3f> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3f((float)( Marshal.ReadInt32(list, (index + 0) * 4 * 3 * 4) != 0 ? 1 : 0 ), (float)( Marshal.ReadInt32(list, (index + 1) * 4 * 3 * 4) != 0 ? 1 : 0 ), (float)( Marshal.ReadInt32(list, (index + 2) * 4 * 3 * 4) != 0 ? 1 : 0 )); }					unsafe static void FromDouble(int first, int count, IList<Vector3f> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3f((float)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 3) ), (float)( *((System.Double*)list.ToPointer() + (index + 1) * 4 * 3) ), (float)( *((System.Double*)list.ToPointer() + (index + 2) * 4 * 3) )); }unsafe static void FromSingle(int first, int count, IList<Vector3f> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3f((float)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 3) ), (float)( *((System.Single*)list.ToPointer() + (index + 1) * 4 * 3) ), (float)( *((System.Single*)list.ToPointer() + (index + 2) * 4 * 3) )); }unsafe static void FromInt32(int first, int count, IList<Vector3f> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3f((float)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 3) ), (float)( *((System.Int32*)list.ToPointer() + (index + 1) * 4 * 3) ), (float)( *((System.Int32*)list.ToPointer() + (index + 2) * 4 * 3) )); }unsafe static void FromUInt32(int first, int count, IList<Vector3f> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3f((float)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 3) ), (float)( *((System.UInt32*)list.ToPointer() + (index + 1) * 4 * 3) ), (float)( *((System.UInt32*)list.ToPointer() + (index + 2) * 4 * 3) )); }
					unsafe static void ToBoolean(int first, int count, IList<Vector3f> data)
					{
						GetList(count * 3);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 3 * 4, data[first + index].X != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 1) * 4 * 3 * 4, data[first + index].Y != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 2) * 4 * 3 * 4, data[first + index].Z != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<Vector3f> data)
					{
						GetList(count * 2 * 3);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 3 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].X));
															Marshal.WriteInt64(list, (index + 1) * 4 * 3 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Y));
															Marshal.WriteInt64(list, (index + 2) * 4 * 3 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Z));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<Vector3f> data)
					{
						SetupFloatList(count * 3);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 3 / 4] = (float)data[first + index].X;
															floatList[(index + 1) * 4 * 3 / 4] = (float)data[first + index].Y;
															floatList[(index + 2) * 4 * 3 / 4] = (float)data[first + index].Z;
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<Vector3f> data)
					{
						GetList(count * 3);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 3 * 4, (int)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 3 * 4, (int)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 3 * 4, (int)data[first + index].Z);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<Vector3f> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 3 * 4, (int)(uint)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 3 * 4, (int)(uint)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 3 * 4, (int)(uint)data[first + index].Z);
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<Vector4f> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4f((float)( Marshal.ReadInt32(list, (index + 0) * 4 * 4 * 4) != 0 ? 1 : 0 ), (float)( Marshal.ReadInt32(list, (index + 1) * 4 * 4 * 4) != 0 ? 1 : 0 ), (float)( Marshal.ReadInt32(list, (index + 2) * 4 * 4 * 4) != 0 ? 1 : 0 ), (float)( Marshal.ReadInt32(list, (index + 3) * 4 * 4 * 4) != 0 ? 1 : 0 )); }					unsafe static void FromDouble(int first, int count, IList<Vector4f> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4f((float)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 4) ), (float)( *((System.Double*)list.ToPointer() + (index + 1) * 4 * 4) ), (float)( *((System.Double*)list.ToPointer() + (index + 2) * 4 * 4) ), (float)( *((System.Double*)list.ToPointer() + (index + 3) * 4 * 4) )); }unsafe static void FromSingle(int first, int count, IList<Vector4f> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4f((float)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 4) ), (float)( *((System.Single*)list.ToPointer() + (index + 1) * 4 * 4) ), (float)( *((System.Single*)list.ToPointer() + (index + 2) * 4 * 4) ), (float)( *((System.Single*)list.ToPointer() + (index + 3) * 4 * 4) )); }unsafe static void FromInt32(int first, int count, IList<Vector4f> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4f((float)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 4) ), (float)( *((System.Int32*)list.ToPointer() + (index + 1) * 4 * 4) ), (float)( *((System.Int32*)list.ToPointer() + (index + 2) * 4 * 4) ), (float)( *((System.Int32*)list.ToPointer() + (index + 3) * 4 * 4) )); }unsafe static void FromUInt32(int first, int count, IList<Vector4f> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4f((float)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 4) ), (float)( *((System.UInt32*)list.ToPointer() + (index + 1) * 4 * 4) ), (float)( *((System.UInt32*)list.ToPointer() + (index + 2) * 4 * 4) ), (float)( *((System.UInt32*)list.ToPointer() + (index + 3) * 4 * 4) )); }
					unsafe static void ToBoolean(int first, int count, IList<Vector4f> data)
					{
						GetList(count * 4);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 4 * 4, data[first + index].X != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 1) * 4 * 4 * 4, data[first + index].Y != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 2) * 4 * 4 * 4, data[first + index].Z != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 3) * 4 * 4 * 4, data[first + index].W != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<Vector4f> data)
					{
						GetList(count * 2 * 4);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].X));
															Marshal.WriteInt64(list, (index + 1) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Y));
															Marshal.WriteInt64(list, (index + 2) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Z));
															Marshal.WriteInt64(list, (index + 3) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].W));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<Vector4f> data)
					{
						SetupFloatList(count * 4);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 4 / 4] = (float)data[first + index].X;
															floatList[(index + 1) * 4 * 4 / 4] = (float)data[first + index].Y;
															floatList[(index + 2) * 4 * 4 / 4] = (float)data[first + index].Z;
															floatList[(index + 3) * 4 * 4 / 4] = (float)data[first + index].W;
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<Vector4f> data)
					{
						GetList(count * 4);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 4 * 4, (int)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 4 * 4, (int)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 4 * 4, (int)data[first + index].Z);
															Marshal.WriteInt32(list, (index + 3) * 4 * 4 * 4, (int)data[first + index].W);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<Vector4f> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 4 * 4, (int)(uint)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 4 * 4, (int)(uint)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 4 * 4, (int)(uint)data[first + index].Z);
															Marshal.WriteInt32(list, (index + 3) * 4 * 4 * 4, (int)(uint)data[first + index].W);
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<int> data) { for(int index = 0; index < count; index++)data[first + index] = (int)( Marshal.ReadInt32(list, (index + 0) * 4 * 1 * 4) != 0 ? 1 : 0 ); }					unsafe static void FromDouble(int first, int count, IList<int> data) { for(int index = 0; index < count; index++)data[first + index] = (int)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromSingle(int first, int count, IList<int> data) { for(int index = 0; index < count; index++)data[first + index] = (int)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromInt32(int first, int count, IList<int> data) { for(int index = 0; index < count; index++)data[first + index] = (int)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromUInt32(int first, int count, IList<int> data) { for(int index = 0; index < count; index++)data[first + index] = (int)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 1) ); }
					unsafe static void ToBoolean(int first, int count, IList<int> data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, data[first + index] != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<int> data)
					{
						GetList(count * 2 * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 1 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index]));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<int> data)
					{
						SetupFloatList(count * 1);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 1 / 4] = (float)data[first + index];
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<int> data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)data[first + index]);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<int> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)(uint)data[first + index]);
							}
					}
									unsafe static void FromBoolean(int count, int* data) { for(int index = 0; index < count; index++)data[index] = (int)( Marshal.ReadInt32(list, (index + 0) * 4 * 1 * 4) != 0 ? 1 : 0 ); }					unsafe static void FromDouble(int count, int* data) { for(int index = 0; index < count; index++)data[index] = (int)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromSingle(int count, int* data) { for(int index = 0; index < count; index++)data[index] = (int)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromInt32(int count, int* data) { for(int index = 0; index < count; index++)data[index] = (int)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromUInt32(int count, int* data) { for(int index = 0; index < count; index++)data[index] = (int)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 1) ); }
					unsafe static void ToBoolean(int count, int* data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, data[index] != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int count, int* data)
					{
						GetList(count * 2 * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 1 * 8, BitConverter.DoubleToInt64Bits((double)data[index]));
							}
					}

					unsafe static void ToSingle(int count, int* data)
					{
						SetupFloatList(count * 1);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 1 / 4] = (float)data[index];
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int count, int* data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)data[index]);
							}
					}

					unsafe static void ToUInt32(int count, int* data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)(uint)data[index]);
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<Vector2i> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2i((int)( Marshal.ReadInt32(list, (index + 0) * 4 * 2 * 4) != 0 ? 1 : 0 ), (int)( Marshal.ReadInt32(list, (index + 1) * 4 * 2 * 4) != 0 ? 1 : 0 )); }					unsafe static void FromDouble(int first, int count, IList<Vector2i> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2i((int)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 2) ), (int)( *((System.Double*)list.ToPointer() + (index + 1) * 4 * 2) )); }unsafe static void FromSingle(int first, int count, IList<Vector2i> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2i((int)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 2) ), (int)( *((System.Single*)list.ToPointer() + (index + 1) * 4 * 2) )); }unsafe static void FromInt32(int first, int count, IList<Vector2i> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2i((int)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 2) ), (int)( *((System.Int32*)list.ToPointer() + (index + 1) * 4 * 2) )); }unsafe static void FromUInt32(int first, int count, IList<Vector2i> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2i((int)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 2) ), (int)( *((System.UInt32*)list.ToPointer() + (index + 1) * 4 * 2) )); }
					unsafe static void ToBoolean(int first, int count, IList<Vector2i> data)
					{
						GetList(count * 2);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 2 * 4, data[first + index].X != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 1) * 4 * 2 * 4, data[first + index].Y != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<Vector2i> data)
					{
						GetList(count * 2 * 2);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 2 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].X));
															Marshal.WriteInt64(list, (index + 1) * 4 * 2 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Y));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<Vector2i> data)
					{
						SetupFloatList(count * 2);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 2 / 4] = (float)data[first + index].X;
															floatList[(index + 1) * 4 * 2 / 4] = (float)data[first + index].Y;
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<Vector2i> data)
					{
						GetList(count * 2);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 2 * 4, (int)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 2 * 4, (int)data[first + index].Y);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<Vector2i> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 2 * 4, (int)(uint)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 2 * 4, (int)(uint)data[first + index].Y);
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<Vector3i> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3i((int)( Marshal.ReadInt32(list, (index + 0) * 4 * 3 * 4) != 0 ? 1 : 0 ), (int)( Marshal.ReadInt32(list, (index + 1) * 4 * 3 * 4) != 0 ? 1 : 0 ), (int)( Marshal.ReadInt32(list, (index + 2) * 4 * 3 * 4) != 0 ? 1 : 0 )); }					unsafe static void FromDouble(int first, int count, IList<Vector3i> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3i((int)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 3) ), (int)( *((System.Double*)list.ToPointer() + (index + 1) * 4 * 3) ), (int)( *((System.Double*)list.ToPointer() + (index + 2) * 4 * 3) )); }unsafe static void FromSingle(int first, int count, IList<Vector3i> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3i((int)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 3) ), (int)( *((System.Single*)list.ToPointer() + (index + 1) * 4 * 3) ), (int)( *((System.Single*)list.ToPointer() + (index + 2) * 4 * 3) )); }unsafe static void FromInt32(int first, int count, IList<Vector3i> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3i((int)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 3) ), (int)( *((System.Int32*)list.ToPointer() + (index + 1) * 4 * 3) ), (int)( *((System.Int32*)list.ToPointer() + (index + 2) * 4 * 3) )); }unsafe static void FromUInt32(int first, int count, IList<Vector3i> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3i((int)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 3) ), (int)( *((System.UInt32*)list.ToPointer() + (index + 1) * 4 * 3) ), (int)( *((System.UInt32*)list.ToPointer() + (index + 2) * 4 * 3) )); }
					unsafe static void ToBoolean(int first, int count, IList<Vector3i> data)
					{
						GetList(count * 3);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 3 * 4, data[first + index].X != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 1) * 4 * 3 * 4, data[first + index].Y != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 2) * 4 * 3 * 4, data[first + index].Z != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<Vector3i> data)
					{
						GetList(count * 2 * 3);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 3 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].X));
															Marshal.WriteInt64(list, (index + 1) * 4 * 3 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Y));
															Marshal.WriteInt64(list, (index + 2) * 4 * 3 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Z));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<Vector3i> data)
					{
						SetupFloatList(count * 3);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 3 / 4] = (float)data[first + index].X;
															floatList[(index + 1) * 4 * 3 / 4] = (float)data[first + index].Y;
															floatList[(index + 2) * 4 * 3 / 4] = (float)data[first + index].Z;
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<Vector3i> data)
					{
						GetList(count * 3);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 3 * 4, (int)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 3 * 4, (int)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 3 * 4, (int)data[first + index].Z);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<Vector3i> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 3 * 4, (int)(uint)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 3 * 4, (int)(uint)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 3 * 4, (int)(uint)data[first + index].Z);
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<Vector4i> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4i((int)( Marshal.ReadInt32(list, (index + 0) * 4 * 4 * 4) != 0 ? 1 : 0 ), (int)( Marshal.ReadInt32(list, (index + 1) * 4 * 4 * 4) != 0 ? 1 : 0 ), (int)( Marshal.ReadInt32(list, (index + 2) * 4 * 4 * 4) != 0 ? 1 : 0 ), (int)( Marshal.ReadInt32(list, (index + 3) * 4 * 4 * 4) != 0 ? 1 : 0 )); }					unsafe static void FromDouble(int first, int count, IList<Vector4i> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4i((int)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 4) ), (int)( *((System.Double*)list.ToPointer() + (index + 1) * 4 * 4) ), (int)( *((System.Double*)list.ToPointer() + (index + 2) * 4 * 4) ), (int)( *((System.Double*)list.ToPointer() + (index + 3) * 4 * 4) )); }unsafe static void FromSingle(int first, int count, IList<Vector4i> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4i((int)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 4) ), (int)( *((System.Single*)list.ToPointer() + (index + 1) * 4 * 4) ), (int)( *((System.Single*)list.ToPointer() + (index + 2) * 4 * 4) ), (int)( *((System.Single*)list.ToPointer() + (index + 3) * 4 * 4) )); }unsafe static void FromInt32(int first, int count, IList<Vector4i> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4i((int)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 4) ), (int)( *((System.Int32*)list.ToPointer() + (index + 1) * 4 * 4) ), (int)( *((System.Int32*)list.ToPointer() + (index + 2) * 4 * 4) ), (int)( *((System.Int32*)list.ToPointer() + (index + 3) * 4 * 4) )); }unsafe static void FromUInt32(int first, int count, IList<Vector4i> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4i((int)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 4) ), (int)( *((System.UInt32*)list.ToPointer() + (index + 1) * 4 * 4) ), (int)( *((System.UInt32*)list.ToPointer() + (index + 2) * 4 * 4) ), (int)( *((System.UInt32*)list.ToPointer() + (index + 3) * 4 * 4) )); }
					unsafe static void ToBoolean(int first, int count, IList<Vector4i> data)
					{
						GetList(count * 4);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 4 * 4, data[first + index].X != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 1) * 4 * 4 * 4, data[first + index].Y != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 2) * 4 * 4 * 4, data[first + index].Z != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 3) * 4 * 4 * 4, data[first + index].W != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<Vector4i> data)
					{
						GetList(count * 2 * 4);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].X));
															Marshal.WriteInt64(list, (index + 1) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Y));
															Marshal.WriteInt64(list, (index + 2) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Z));
															Marshal.WriteInt64(list, (index + 3) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].W));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<Vector4i> data)
					{
						SetupFloatList(count * 4);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 4 / 4] = (float)data[first + index].X;
															floatList[(index + 1) * 4 * 4 / 4] = (float)data[first + index].Y;
															floatList[(index + 2) * 4 * 4 / 4] = (float)data[first + index].Z;
															floatList[(index + 3) * 4 * 4 / 4] = (float)data[first + index].W;
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<Vector4i> data)
					{
						GetList(count * 4);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 4 * 4, (int)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 4 * 4, (int)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 4 * 4, (int)data[first + index].Z);
															Marshal.WriteInt32(list, (index + 3) * 4 * 4 * 4, (int)data[first + index].W);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<Vector4i> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 4 * 4, (int)(uint)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 4 * 4, (int)(uint)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 4 * 4, (int)(uint)data[first + index].Z);
															Marshal.WriteInt32(list, (index + 3) * 4 * 4 * 4, (int)(uint)data[first + index].W);
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<uint> data) { for(int index = 0; index < count; index++)data[first + index] = (uint)( Marshal.ReadInt32(list, (index + 0) * 4 * 1 * 4) != 0 ? 1 : 0 ); }					unsafe static void FromDouble(int first, int count, IList<uint> data) { for(int index = 0; index < count; index++)data[first + index] = (uint)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromSingle(int first, int count, IList<uint> data) { for(int index = 0; index < count; index++)data[first + index] = (uint)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromInt32(int first, int count, IList<uint> data) { for(int index = 0; index < count; index++)data[first + index] = (uint)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromUInt32(int first, int count, IList<uint> data) { for(int index = 0; index < count; index++)data[first + index] = (uint)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 1) ); }
					unsafe static void ToBoolean(int first, int count, IList<uint> data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, data[first + index] != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<uint> data)
					{
						GetList(count * 2 * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 1 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index]));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<uint> data)
					{
						SetupFloatList(count * 1);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 1 / 4] = (float)data[first + index];
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<uint> data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)data[first + index]);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<uint> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)(uint)data[first + index]);
							}
					}
									unsafe static void FromBoolean(int count, uint* data) { for(int index = 0; index < count; index++)data[index] = (uint)( Marshal.ReadInt32(list, (index + 0) * 4 * 1 * 4) != 0 ? 1 : 0 ); }					unsafe static void FromDouble(int count, uint* data) { for(int index = 0; index < count; index++)data[index] = (uint)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromSingle(int count, uint* data) { for(int index = 0; index < count; index++)data[index] = (uint)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromInt32(int count, uint* data) { for(int index = 0; index < count; index++)data[index] = (uint)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 1) ); }unsafe static void FromUInt32(int count, uint* data) { for(int index = 0; index < count; index++)data[index] = (uint)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 1) ); }
					unsafe static void ToBoolean(int count, uint* data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, data[index] != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int count, uint* data)
					{
						GetList(count * 2 * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 1 * 8, BitConverter.DoubleToInt64Bits((double)data[index]));
							}
					}

					unsafe static void ToSingle(int count, uint* data)
					{
						SetupFloatList(count * 1);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 1 / 4] = (float)data[index];
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int count, uint* data)
					{
						GetList(count * 1);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)data[index]);
							}
					}

					unsafe static void ToUInt32(int count, uint* data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 1 * 4, (int)(uint)data[index]);
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<Vector2ui> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2ui((uint)( Marshal.ReadInt32(list, (index + 0) * 4 * 2 * 4) != 0 ? 1 : 0 ), (uint)( Marshal.ReadInt32(list, (index + 1) * 4 * 2 * 4) != 0 ? 1 : 0 )); }					unsafe static void FromDouble(int first, int count, IList<Vector2ui> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2ui((uint)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 2) ), (uint)( *((System.Double*)list.ToPointer() + (index + 1) * 4 * 2) )); }unsafe static void FromSingle(int first, int count, IList<Vector2ui> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2ui((uint)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 2) ), (uint)( *((System.Single*)list.ToPointer() + (index + 1) * 4 * 2) )); }unsafe static void FromInt32(int first, int count, IList<Vector2ui> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2ui((uint)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 2) ), (uint)( *((System.Int32*)list.ToPointer() + (index + 1) * 4 * 2) )); }unsafe static void FromUInt32(int first, int count, IList<Vector2ui> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector2ui((uint)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 2) ), (uint)( *((System.UInt32*)list.ToPointer() + (index + 1) * 4 * 2) )); }
					unsafe static void ToBoolean(int first, int count, IList<Vector2ui> data)
					{
						GetList(count * 2);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 2 * 4, data[first + index].X != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 1) * 4 * 2 * 4, data[first + index].Y != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<Vector2ui> data)
					{
						GetList(count * 2 * 2);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 2 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].X));
															Marshal.WriteInt64(list, (index + 1) * 4 * 2 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Y));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<Vector2ui> data)
					{
						SetupFloatList(count * 2);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 2 / 4] = (float)data[first + index].X;
															floatList[(index + 1) * 4 * 2 / 4] = (float)data[first + index].Y;
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<Vector2ui> data)
					{
						GetList(count * 2);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 2 * 4, (int)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 2 * 4, (int)data[first + index].Y);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<Vector2ui> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 2 * 4, (int)(uint)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 2 * 4, (int)(uint)data[first + index].Y);
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<Vector3ui> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3ui((uint)( Marshal.ReadInt32(list, (index + 0) * 4 * 3 * 4) != 0 ? 1 : 0 ), (uint)( Marshal.ReadInt32(list, (index + 1) * 4 * 3 * 4) != 0 ? 1 : 0 ), (uint)( Marshal.ReadInt32(list, (index + 2) * 4 * 3 * 4) != 0 ? 1 : 0 )); }					unsafe static void FromDouble(int first, int count, IList<Vector3ui> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3ui((uint)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 3) ), (uint)( *((System.Double*)list.ToPointer() + (index + 1) * 4 * 3) ), (uint)( *((System.Double*)list.ToPointer() + (index + 2) * 4 * 3) )); }unsafe static void FromSingle(int first, int count, IList<Vector3ui> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3ui((uint)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 3) ), (uint)( *((System.Single*)list.ToPointer() + (index + 1) * 4 * 3) ), (uint)( *((System.Single*)list.ToPointer() + (index + 2) * 4 * 3) )); }unsafe static void FromInt32(int first, int count, IList<Vector3ui> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3ui((uint)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 3) ), (uint)( *((System.Int32*)list.ToPointer() + (index + 1) * 4 * 3) ), (uint)( *((System.Int32*)list.ToPointer() + (index + 2) * 4 * 3) )); }unsafe static void FromUInt32(int first, int count, IList<Vector3ui> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector3ui((uint)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 3) ), (uint)( *((System.UInt32*)list.ToPointer() + (index + 1) * 4 * 3) ), (uint)( *((System.UInt32*)list.ToPointer() + (index + 2) * 4 * 3) )); }
					unsafe static void ToBoolean(int first, int count, IList<Vector3ui> data)
					{
						GetList(count * 3);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 3 * 4, data[first + index].X != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 1) * 4 * 3 * 4, data[first + index].Y != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 2) * 4 * 3 * 4, data[first + index].Z != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<Vector3ui> data)
					{
						GetList(count * 2 * 3);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 3 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].X));
															Marshal.WriteInt64(list, (index + 1) * 4 * 3 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Y));
															Marshal.WriteInt64(list, (index + 2) * 4 * 3 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Z));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<Vector3ui> data)
					{
						SetupFloatList(count * 3);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 3 / 4] = (float)data[first + index].X;
															floatList[(index + 1) * 4 * 3 / 4] = (float)data[first + index].Y;
															floatList[(index + 2) * 4 * 3 / 4] = (float)data[first + index].Z;
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<Vector3ui> data)
					{
						GetList(count * 3);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 3 * 4, (int)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 3 * 4, (int)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 3 * 4, (int)data[first + index].Z);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<Vector3ui> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 3 * 4, (int)(uint)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 3 * 4, (int)(uint)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 3 * 4, (int)(uint)data[first + index].Z);
							}
					}
									unsafe static void FromBoolean(int first, int count, IList<Vector4ui> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4ui((uint)( Marshal.ReadInt32(list, (index + 0) * 4 * 4 * 4) != 0 ? 1 : 0 ), (uint)( Marshal.ReadInt32(list, (index + 1) * 4 * 4 * 4) != 0 ? 1 : 0 ), (uint)( Marshal.ReadInt32(list, (index + 2) * 4 * 4 * 4) != 0 ? 1 : 0 ), (uint)( Marshal.ReadInt32(list, (index + 3) * 4 * 4 * 4) != 0 ? 1 : 0 )); }					unsafe static void FromDouble(int first, int count, IList<Vector4ui> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4ui((uint)( *((System.Double*)list.ToPointer() + (index + 0) * 4 * 4) ), (uint)( *((System.Double*)list.ToPointer() + (index + 1) * 4 * 4) ), (uint)( *((System.Double*)list.ToPointer() + (index + 2) * 4 * 4) ), (uint)( *((System.Double*)list.ToPointer() + (index + 3) * 4 * 4) )); }unsafe static void FromSingle(int first, int count, IList<Vector4ui> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4ui((uint)( *((System.Single*)list.ToPointer() + (index + 0) * 4 * 4) ), (uint)( *((System.Single*)list.ToPointer() + (index + 1) * 4 * 4) ), (uint)( *((System.Single*)list.ToPointer() + (index + 2) * 4 * 4) ), (uint)( *((System.Single*)list.ToPointer() + (index + 3) * 4 * 4) )); }unsafe static void FromInt32(int first, int count, IList<Vector4ui> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4ui((uint)( *((System.Int32*)list.ToPointer() + (index + 0) * 4 * 4) ), (uint)( *((System.Int32*)list.ToPointer() + (index + 1) * 4 * 4) ), (uint)( *((System.Int32*)list.ToPointer() + (index + 2) * 4 * 4) ), (uint)( *((System.Int32*)list.ToPointer() + (index + 3) * 4 * 4) )); }unsafe static void FromUInt32(int first, int count, IList<Vector4ui> data) { for(int index = 0; index < count; index++)data[first + index] = new Vector4ui((uint)( *((System.UInt32*)list.ToPointer() + (index + 0) * 4 * 4) ), (uint)( *((System.UInt32*)list.ToPointer() + (index + 1) * 4 * 4) ), (uint)( *((System.UInt32*)list.ToPointer() + (index + 2) * 4 * 4) ), (uint)( *((System.UInt32*)list.ToPointer() + (index + 3) * 4 * 4) )); }
					unsafe static void ToBoolean(int first, int count, IList<Vector4ui> data)
					{
						GetList(count * 4);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 4 * 4, data[first + index].X != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 1) * 4 * 4 * 4, data[first + index].Y != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 2) * 4 * 4 * 4, data[first + index].Z != 0 ? 1 : 0);
															Marshal.WriteInt32(list, (index + 3) * 4 * 4 * 4, data[first + index].W != 0 ? 1 : 0);
							}
					}

					unsafe static void ToDouble(int first, int count, IList<Vector4ui> data)
					{
						GetList(count * 2 * 4);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt64(list, (index + 0) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].X));
															Marshal.WriteInt64(list, (index + 1) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Y));
															Marshal.WriteInt64(list, (index + 2) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].Z));
															Marshal.WriteInt64(list, (index + 3) * 4 * 4 * 8, BitConverter.DoubleToInt64Bits((double)data[first + index].W));
							}
					}

					unsafe static void ToSingle(int first, int count, IList<Vector4ui> data)
					{
						SetupFloatList(count * 4);
						for (int index = 0; index < count; index++) {								floatList[(index + 0) * 4 * 4 / 4] = (float)data[first + index].X;
															floatList[(index + 1) * 4 * 4 / 4] = (float)data[first + index].Y;
															floatList[(index + 2) * 4 * 4 / 4] = (float)data[first + index].Z;
															floatList[(index + 3) * 4 * 4 / 4] = (float)data[first + index].W;
							}
						Marshal.Copy(floatList, 0, list, count);
					}

					unsafe static void ToInt32(int first, int count, IList<Vector4ui> data)
					{
						GetList(count * 4);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 4 * 4, (int)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 4 * 4, (int)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 4 * 4, (int)data[first + index].Z);
															Marshal.WriteInt32(list, (index + 3) * 4 * 4 * 4, (int)data[first + index].W);
							}
					}

					unsafe static void ToUInt32(int first, int count, IList<Vector4ui> data)
					{
						GetList(count);
						for (int index = 0; index < count; index++) {								Marshal.WriteInt32(list, (index + 0) * 4 * 4 * 4, (int)(uint)data[first + index].X);
															Marshal.WriteInt32(list, (index + 1) * 4 * 4 * 4, (int)(uint)data[first + index].Y);
															Marshal.WriteInt32(list, (index + 2) * 4 * 4 * 4, (int)(uint)data[first + index].Z);
															Marshal.WriteInt32(list, (index + 3) * 4 * 4 * 4, (int)(uint)data[first + index].W);
							}
					}
				
					public unsafe void Get(System.Boolean* data, Vector2i dimensions, int count)
			{
				if(data == null)
					throw new ArgumentNullException("data");
				if(count != Count)
					throw new ArgumentException("count");
				if(dimensions != ComponentDimensions)
					throw new ArgumentException("dimensions");
				using (Context.Lock())
					lock (locker)
						switch (ComponentType.Name)
						{
															case "Boolean":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Boolean), 4) / 4);
										GL.GetUniform(Program.Id, location, (int*)list.ToPointer());
										FromBoolean(Count * ComponentCount, data);
																		break;
															case "Double":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Boolean), 4) / 4);
										GL.GetUniform(Program.Id, location, (double*)list.ToPointer());
										FromDouble(Count * ComponentCount, data);
																		break;
															case "Single":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Boolean), 4) / 4);
										GL.GetUniform(Program.Id, location, (float*)list.ToPointer());
										FromSingle(Count * ComponentCount, data);
																		break;
															case "Int32":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Boolean), 4) / 4);
										GL.GetUniform(Program.Id, location, (int*)list.ToPointer());
										FromInt32(Count * ComponentCount, data);
																		break;
															case "UInt32":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Boolean), 4) / 4);
										GL.GetUniform((uint)Program.Id, location, (uint*)list.ToPointer());
										FromUInt32(Count * ComponentCount, data);
																		break;
														default: throw new InvalidOperationException();
						}
			}
					public unsafe void Get(System.Double* data, Vector2i dimensions, int count)
			{
				if(data == null)
					throw new ArgumentNullException("data");
				if(count != Count)
					throw new ArgumentException("count");
				if(dimensions != ComponentDimensions)
					throw new ArgumentException("dimensions");
				using (Context.Lock())
					lock (locker)
						switch (ComponentType.Name)
						{
															case "Boolean":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Double), 4) / 4);
										GL.GetUniform(Program.Id, location, (int*)list.ToPointer());
										FromBoolean(Count * ComponentCount, data);
																		break;
															case "Double":
																			GL.GetUniform(Program.Id, location, (double*)data);
																		break;
															case "Single":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Double), 4) / 4);
										GL.GetUniform(Program.Id, location, (float*)list.ToPointer());
										FromSingle(Count * ComponentCount, data);
																		break;
															case "Int32":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Double), 4) / 4);
										GL.GetUniform(Program.Id, location, (int*)list.ToPointer());
										FromInt32(Count * ComponentCount, data);
																		break;
															case "UInt32":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Double), 4) / 4);
										GL.GetUniform((uint)Program.Id, location, (uint*)list.ToPointer());
										FromUInt32(Count * ComponentCount, data);
																		break;
														default: throw new InvalidOperationException();
						}
			}
					public unsafe void Get(System.Single* data, Vector2i dimensions, int count)
			{
				if(data == null)
					throw new ArgumentNullException("data");
				if(count != Count)
					throw new ArgumentException("count");
				if(dimensions != ComponentDimensions)
					throw new ArgumentException("dimensions");
				using (Context.Lock())
					lock (locker)
						switch (ComponentType.Name)
						{
															case "Boolean":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Single), 4) / 4);
										GL.GetUniform(Program.Id, location, (int*)list.ToPointer());
										FromBoolean(Count * ComponentCount, data);
																		break;
															case "Double":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Single), 4) / 4);
										GL.GetUniform(Program.Id, location, (double*)list.ToPointer());
										FromDouble(Count * ComponentCount, data);
																		break;
															case "Single":
																			GL.GetUniform(Program.Id, location, (float*)data);
																		break;
															case "Int32":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Single), 4) / 4);
										GL.GetUniform(Program.Id, location, (int*)list.ToPointer());
										FromInt32(Count * ComponentCount, data);
																		break;
															case "UInt32":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Single), 4) / 4);
										GL.GetUniform((uint)Program.Id, location, (uint*)list.ToPointer());
										FromUInt32(Count * ComponentCount, data);
																		break;
														default: throw new InvalidOperationException();
						}
			}
					public unsafe void Get(System.Int32* data, Vector2i dimensions, int count)
			{
				if(data == null)
					throw new ArgumentNullException("data");
				if(count != Count)
					throw new ArgumentException("count");
				if(dimensions != ComponentDimensions)
					throw new ArgumentException("dimensions");
				using (Context.Lock())
					lock (locker)
						switch (ComponentType.Name)
						{
															case "Boolean":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Int32), 4) / 4);
										GL.GetUniform(Program.Id, location, (int*)list.ToPointer());
										FromBoolean(Count * ComponentCount, data);
																		break;
															case "Double":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Int32), 4) / 4);
										GL.GetUniform(Program.Id, location, (double*)list.ToPointer());
										FromDouble(Count * ComponentCount, data);
																		break;
															case "Single":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Int32), 4) / 4);
										GL.GetUniform(Program.Id, location, (float*)list.ToPointer());
										FromSingle(Count * ComponentCount, data);
																		break;
															case "Int32":
																			GL.GetUniform(Program.Id, location, (int*)data);
																		break;
															case "UInt32":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.Int32), 4) / 4);
										GL.GetUniform((uint)Program.Id, location, (uint*)list.ToPointer());
										FromUInt32(Count * ComponentCount, data);
																		break;
														default: throw new InvalidOperationException();
						}
			}
					public unsafe void Get(System.UInt32* data, Vector2i dimensions, int count)
			{
				if(data == null)
					throw new ArgumentNullException("data");
				if(count != Count)
					throw new ArgumentException("count");
				if(dimensions != ComponentDimensions)
					throw new ArgumentException("dimensions");
				using (Context.Lock())
					lock (locker)
						switch (ComponentType.Name)
						{
															case "Boolean":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.UInt32), 4) / 4);
										GL.GetUniform(Program.Id, location, (int*)list.ToPointer());
										FromBoolean(Count * ComponentCount, data);
																		break;
															case "Double":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.UInt32), 4) / 4);
										GL.GetUniform(Program.Id, location, (double*)list.ToPointer());
										FromDouble(Count * ComponentCount, data);
																		break;
															case "Single":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.UInt32), 4) / 4);
										GL.GetUniform(Program.Id, location, (float*)list.ToPointer());
										FromSingle(Count * ComponentCount, data);
																		break;
															case "Int32":
																			GetList(dimensions.X * dimensions.Y * count * Math.Min(sizeof(System.UInt32), 4) / 4);
										GL.GetUniform(Program.Id, location, (int*)list.ToPointer());
										FromInt32(Count * ComponentCount, data);
																		break;
															case "UInt32":
																			GL.GetUniform((uint)Program.Id, location, (uint*)data);
																		break;
														default: throw new InvalidOperationException();
						}
			}
		
						
				public unsafe void Set2(int count, bool* data)
				{
					using (Context.Lock())
						lock (locker)
							switch (type)
							{
																	case ActiveUniformType.BoolVec2:
																					ToBoolean(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.DoubleVec2:
																					ToDouble(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (double*)list.ToPointer());
																				break;
																	case ActiveUniformType.FloatVec2:
																					ToSingle(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (float*)list.ToPointer());
																				break;
																	case ActiveUniformType.IntVec2:
																					ToInt32(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.UnsignedIntVec2:
																					ToUInt32(count * 2, data);
											GL.ProgramUniform2((uint)Program.Id, location, count, (uint*)list.ToPointer());
																				break;
																default: throw new InvalidOperationException();
							}
				}

													public Matrix2d GetMatrix2d()
					{
						Matrix2d result = new Matrix2d();
						unsafe { Get(&result.XX, new Vector2i(2, 2), 1); }
						return result;
					}
				
				public unsafe void Set2(int count, double* data)
				{
					using (Context.Lock())
						lock (locker)
							switch (type)
							{
																	case ActiveUniformType.BoolVec2:
																					ToBoolean(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.DoubleVec2:
																					GL.ProgramUniform2(Program.Id, location, count, (double*)data);
																				break;
																	case ActiveUniformType.FloatVec2:
																					ToSingle(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (float*)list.ToPointer());
																				break;
																	case ActiveUniformType.IntVec2:
																					ToInt32(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.UnsignedIntVec2:
																					ToUInt32(count * 2, data);
											GL.ProgramUniform2((uint)Program.Id, location, count, (uint*)list.ToPointer());
																				break;
																default: throw new InvalidOperationException();
							}
				}

									public void Set(Vector2d value) { unsafe { Set2(1, (double*)&value); } }

					public void Set(ref Vector2d value) { unsafe { fixed(Vector2d* pointer = &value) Set2(1, (double*)pointer); } }

					public void Set(params Vector2d[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						unsafe 
						{
							fixed(Vector2d* pointer = values)
								Set2(values.Length, (double*)pointer);
						}
					}

					public void Set(int first, int count, params Vector2d[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						if(first < 0 || first > values.Length)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > values.Length)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							fixed(Vector2d* pointer = values)
								Set2(count, (double*)(pointer + first));
						}
					}

					public void Set(IList<Vector2d> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						Set(0, list.Count, list);
					}

					public void Set(int first, int count, IList<Vector2d> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						if(first < 0 || first > list.Count)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > list.Count)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							using (Context.Lock())
								lock (locker)
									switch (type)
									{
																					case ActiveUniformType.BoolVec2:
												ToBoolean(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.DoubleVec2:
												ToDouble(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (double*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.FloatVec2:
												ToSingle(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (float*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.IntVec2:
												ToInt32(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.UnsignedIntVec2:
												ToUInt32(first, count, list);
												GL.ProgramUniform2((uint)Program.Id, location, count, (uint*)ProgramUniform.list.ToPointer());
												break;
																				default: throw new InvalidOperationException();
									}							
						}
					}
													public Matrix2f GetMatrix2f()
					{
						Matrix2f result = new Matrix2f();
						unsafe { Get(&result.XX, new Vector2i(2, 2), 1); }
						return result;
					}
				
				public unsafe void Set2(int count, float* data)
				{
					using (Context.Lock())
						lock (locker)
							switch (type)
							{
																	case ActiveUniformType.BoolVec2:
																					ToBoolean(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.DoubleVec2:
																					ToDouble(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (double*)list.ToPointer());
																				break;
																	case ActiveUniformType.FloatVec2:
																					GL.ProgramUniform2(Program.Id, location, count, (float*)data);
																				break;
																	case ActiveUniformType.IntVec2:
																					ToInt32(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.UnsignedIntVec2:
																					ToUInt32(count * 2, data);
											GL.ProgramUniform2((uint)Program.Id, location, count, (uint*)list.ToPointer());
																				break;
																default: throw new InvalidOperationException();
							}
				}

									public void Set(Vector2f value) { unsafe { Set2(1, (float*)&value); } }

					public void Set(ref Vector2f value) { unsafe { fixed(Vector2f* pointer = &value) Set2(1, (float*)pointer); } }

					public void Set(params Vector2f[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						unsafe 
						{
							fixed(Vector2f* pointer = values)
								Set2(values.Length, (float*)pointer);
						}
					}

					public void Set(int first, int count, params Vector2f[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						if(first < 0 || first > values.Length)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > values.Length)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							fixed(Vector2f* pointer = values)
								Set2(count, (float*)(pointer + first));
						}
					}

					public void Set(IList<Vector2f> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						Set(0, list.Count, list);
					}

					public void Set(int first, int count, IList<Vector2f> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						if(first < 0 || first > list.Count)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > list.Count)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							using (Context.Lock())
								lock (locker)
									switch (type)
									{
																					case ActiveUniformType.BoolVec2:
												ToBoolean(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.DoubleVec2:
												ToDouble(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (double*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.FloatVec2:
												ToSingle(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (float*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.IntVec2:
												ToInt32(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.UnsignedIntVec2:
												ToUInt32(first, count, list);
												GL.ProgramUniform2((uint)Program.Id, location, count, (uint*)ProgramUniform.list.ToPointer());
												break;
																				default: throw new InvalidOperationException();
									}							
						}
					}
								
				public unsafe void Set2(int count, int* data)
				{
					using (Context.Lock())
						lock (locker)
							switch (type)
							{
																	case ActiveUniformType.BoolVec2:
																					ToBoolean(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.DoubleVec2:
																					ToDouble(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (double*)list.ToPointer());
																				break;
																	case ActiveUniformType.FloatVec2:
																					ToSingle(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (float*)list.ToPointer());
																				break;
																	case ActiveUniformType.IntVec2:
																					GL.ProgramUniform2(Program.Id, location, count, (int*)data);
																				break;
																	case ActiveUniformType.UnsignedIntVec2:
																					ToUInt32(count * 2, data);
											GL.ProgramUniform2((uint)Program.Id, location, count, (uint*)list.ToPointer());
																				break;
																default: throw new InvalidOperationException();
							}
				}

									public void Set(Vector2i value) { unsafe { Set2(1, (int*)&value); } }

					public void Set(ref Vector2i value) { unsafe { fixed(Vector2i* pointer = &value) Set2(1, (int*)pointer); } }

					public void Set(params Vector2i[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						unsafe 
						{
							fixed(Vector2i* pointer = values)
								Set2(values.Length, (int*)pointer);
						}
					}

					public void Set(int first, int count, params Vector2i[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						if(first < 0 || first > values.Length)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > values.Length)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							fixed(Vector2i* pointer = values)
								Set2(count, (int*)(pointer + first));
						}
					}

					public void Set(IList<Vector2i> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						Set(0, list.Count, list);
					}

					public void Set(int first, int count, IList<Vector2i> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						if(first < 0 || first > list.Count)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > list.Count)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							using (Context.Lock())
								lock (locker)
									switch (type)
									{
																					case ActiveUniformType.BoolVec2:
												ToBoolean(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.DoubleVec2:
												ToDouble(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (double*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.FloatVec2:
												ToSingle(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (float*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.IntVec2:
												ToInt32(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.UnsignedIntVec2:
												ToUInt32(first, count, list);
												GL.ProgramUniform2((uint)Program.Id, location, count, (uint*)ProgramUniform.list.ToPointer());
												break;
																				default: throw new InvalidOperationException();
									}							
						}
					}
								
				public unsafe void Set2(int count, uint* data)
				{
					using (Context.Lock())
						lock (locker)
							switch (type)
							{
																	case ActiveUniformType.BoolVec2:
																					ToBoolean(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.DoubleVec2:
																					ToDouble(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (double*)list.ToPointer());
																				break;
																	case ActiveUniformType.FloatVec2:
																					ToSingle(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (float*)list.ToPointer());
																				break;
																	case ActiveUniformType.IntVec2:
																					ToInt32(count * 2, data);
											GL.ProgramUniform2(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.UnsignedIntVec2:
																					GL.ProgramUniform2((uint)Program.Id, location, count, (uint*)data);
																				break;
																default: throw new InvalidOperationException();
							}
				}

									public void Set(Vector2ui value) { unsafe { Set2(1, (uint*)&value); } }

					public void Set(ref Vector2ui value) { unsafe { fixed(Vector2ui* pointer = &value) Set2(1, (uint*)pointer); } }

					public void Set(params Vector2ui[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						unsafe 
						{
							fixed(Vector2ui* pointer = values)
								Set2(values.Length, (uint*)pointer);
						}
					}

					public void Set(int first, int count, params Vector2ui[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						if(first < 0 || first > values.Length)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > values.Length)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							fixed(Vector2ui* pointer = values)
								Set2(count, (uint*)(pointer + first));
						}
					}

					public void Set(IList<Vector2ui> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						Set(0, list.Count, list);
					}

					public void Set(int first, int count, IList<Vector2ui> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						if(first < 0 || first > list.Count)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > list.Count)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							using (Context.Lock())
								lock (locker)
									switch (type)
									{
																					case ActiveUniformType.BoolVec2:
												ToBoolean(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.DoubleVec2:
												ToDouble(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (double*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.FloatVec2:
												ToSingle(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (float*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.IntVec2:
												ToInt32(first, count, list);
												GL.ProgramUniform2(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.UnsignedIntVec2:
												ToUInt32(first, count, list);
												GL.ProgramUniform2((uint)Program.Id, location, count, (uint*)ProgramUniform.list.ToPointer());
												break;
																				default: throw new InvalidOperationException();
									}							
						}
					}
								
				public unsafe void Set3(int count, bool* data)
				{
					using (Context.Lock())
						lock (locker)
							switch (type)
							{
																	case ActiveUniformType.BoolVec3:
																					ToBoolean(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.DoubleVec3:
																					ToDouble(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (double*)list.ToPointer());
																				break;
																	case ActiveUniformType.FloatVec3:
																					ToSingle(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (float*)list.ToPointer());
																				break;
																	case ActiveUniformType.IntVec3:
																					ToInt32(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.UnsignedIntVec3:
																					ToUInt32(count * 3, data);
											GL.ProgramUniform3((uint)Program.Id, location, count, (uint*)list.ToPointer());
																				break;
																default: throw new InvalidOperationException();
							}
				}

													public Matrix3d GetMatrix3d()
					{
						Matrix3d result = new Matrix3d();
						unsafe { Get(&result.XX, new Vector2i(3, 3), 1); }
						return result;
					}
				
				public unsafe void Set3(int count, double* data)
				{
					using (Context.Lock())
						lock (locker)
							switch (type)
							{
																	case ActiveUniformType.BoolVec3:
																					ToBoolean(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.DoubleVec3:
																					GL.ProgramUniform3(Program.Id, location, count, (double*)data);
																				break;
																	case ActiveUniformType.FloatVec3:
																					ToSingle(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (float*)list.ToPointer());
																				break;
																	case ActiveUniformType.IntVec3:
																					ToInt32(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.UnsignedIntVec3:
																					ToUInt32(count * 3, data);
											GL.ProgramUniform3((uint)Program.Id, location, count, (uint*)list.ToPointer());
																				break;
																default: throw new InvalidOperationException();
							}
				}

									public void Set(Vector3d value) { unsafe { Set3(1, (double*)&value); } }

					public void Set(ref Vector3d value) { unsafe { fixed(Vector3d* pointer = &value) Set3(1, (double*)pointer); } }

					public void Set(params Vector3d[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						unsafe 
						{
							fixed(Vector3d* pointer = values)
								Set3(values.Length, (double*)pointer);
						}
					}

					public void Set(int first, int count, params Vector3d[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						if(first < 0 || first > values.Length)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > values.Length)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							fixed(Vector3d* pointer = values)
								Set3(count, (double*)(pointer + first));
						}
					}

					public void Set(IList<Vector3d> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						Set(0, list.Count, list);
					}

					public void Set(int first, int count, IList<Vector3d> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						if(first < 0 || first > list.Count)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > list.Count)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							using (Context.Lock())
								lock (locker)
									switch (type)
									{
																					case ActiveUniformType.BoolVec3:
												ToBoolean(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.DoubleVec3:
												ToDouble(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (double*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.FloatVec3:
												ToSingle(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (float*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.IntVec3:
												ToInt32(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.UnsignedIntVec3:
												ToUInt32(first, count, list);
												GL.ProgramUniform3((uint)Program.Id, location, count, (uint*)ProgramUniform.list.ToPointer());
												break;
																				default: throw new InvalidOperationException();
									}							
						}
					}
													public Matrix3f GetMatrix3f()
					{
						Matrix3f result = new Matrix3f();
						unsafe { Get(&result.XX, new Vector2i(3, 3), 1); }
						return result;
					}
				
				public unsafe void Set3(int count, float* data)
				{
					using (Context.Lock())
						lock (locker)
							switch (type)
							{
																	case ActiveUniformType.BoolVec3:
																					ToBoolean(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.DoubleVec3:
																					ToDouble(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (double*)list.ToPointer());
																				break;
																	case ActiveUniformType.FloatVec3:
																					GL.ProgramUniform3(Program.Id, location, count, (float*)data);
																				break;
																	case ActiveUniformType.IntVec3:
																					ToInt32(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.UnsignedIntVec3:
																					ToUInt32(count * 3, data);
											GL.ProgramUniform3((uint)Program.Id, location, count, (uint*)list.ToPointer());
																				break;
																default: throw new InvalidOperationException();
							}
				}

									public void Set(Vector3f value) { unsafe { Set3(1, (float*)&value); } }

					public void Set(ref Vector3f value) { unsafe { fixed(Vector3f* pointer = &value) Set3(1, (float*)pointer); } }

					public void Set(params Vector3f[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						unsafe 
						{
							fixed(Vector3f* pointer = values)
								Set3(values.Length, (float*)pointer);
						}
					}

					public void Set(int first, int count, params Vector3f[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						if(first < 0 || first > values.Length)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > values.Length)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							fixed(Vector3f* pointer = values)
								Set3(count, (float*)(pointer + first));
						}
					}

					public void Set(IList<Vector3f> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						Set(0, list.Count, list);
					}

					public void Set(int first, int count, IList<Vector3f> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						if(first < 0 || first > list.Count)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > list.Count)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							using (Context.Lock())
								lock (locker)
									switch (type)
									{
																					case ActiveUniformType.BoolVec3:
												ToBoolean(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.DoubleVec3:
												ToDouble(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (double*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.FloatVec3:
												ToSingle(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (float*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.IntVec3:
												ToInt32(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.UnsignedIntVec3:
												ToUInt32(first, count, list);
												GL.ProgramUniform3((uint)Program.Id, location, count, (uint*)ProgramUniform.list.ToPointer());
												break;
																				default: throw new InvalidOperationException();
									}							
						}
					}
								
				public unsafe void Set3(int count, int* data)
				{
					using (Context.Lock())
						lock (locker)
							switch (type)
							{
																	case ActiveUniformType.BoolVec3:
																					ToBoolean(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.DoubleVec3:
																					ToDouble(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (double*)list.ToPointer());
																				break;
																	case ActiveUniformType.FloatVec3:
																					ToSingle(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (float*)list.ToPointer());
																				break;
																	case ActiveUniformType.IntVec3:
																					GL.ProgramUniform3(Program.Id, location, count, (int*)data);
																				break;
																	case ActiveUniformType.UnsignedIntVec3:
																					ToUInt32(count * 3, data);
											GL.ProgramUniform3((uint)Program.Id, location, count, (uint*)list.ToPointer());
																				break;
																default: throw new InvalidOperationException();
							}
				}

									public void Set(Vector3i value) { unsafe { Set3(1, (int*)&value); } }

					public void Set(ref Vector3i value) { unsafe { fixed(Vector3i* pointer = &value) Set3(1, (int*)pointer); } }

					public void Set(params Vector3i[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						unsafe 
						{
							fixed(Vector3i* pointer = values)
								Set3(values.Length, (int*)pointer);
						}
					}

					public void Set(int first, int count, params Vector3i[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						if(first < 0 || first > values.Length)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > values.Length)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							fixed(Vector3i* pointer = values)
								Set3(count, (int*)(pointer + first));
						}
					}

					public void Set(IList<Vector3i> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						Set(0, list.Count, list);
					}

					public void Set(int first, int count, IList<Vector3i> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						if(first < 0 || first > list.Count)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > list.Count)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							using (Context.Lock())
								lock (locker)
									switch (type)
									{
																					case ActiveUniformType.BoolVec3:
												ToBoolean(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.DoubleVec3:
												ToDouble(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (double*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.FloatVec3:
												ToSingle(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (float*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.IntVec3:
												ToInt32(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.UnsignedIntVec3:
												ToUInt32(first, count, list);
												GL.ProgramUniform3((uint)Program.Id, location, count, (uint*)ProgramUniform.list.ToPointer());
												break;
																				default: throw new InvalidOperationException();
									}							
						}
					}
								
				public unsafe void Set3(int count, uint* data)
				{
					using (Context.Lock())
						lock (locker)
							switch (type)
							{
																	case ActiveUniformType.BoolVec3:
																					ToBoolean(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.DoubleVec3:
																					ToDouble(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (double*)list.ToPointer());
																				break;
																	case ActiveUniformType.FloatVec3:
																					ToSingle(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (float*)list.ToPointer());
																				break;
																	case ActiveUniformType.IntVec3:
																					ToInt32(count * 3, data);
											GL.ProgramUniform3(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.UnsignedIntVec3:
																					GL.ProgramUniform3((uint)Program.Id, location, count, (uint*)data);
																				break;
																default: throw new InvalidOperationException();
							}
				}

									public void Set(Vector3ui value) { unsafe { Set3(1, (uint*)&value); } }

					public void Set(ref Vector3ui value) { unsafe { fixed(Vector3ui* pointer = &value) Set3(1, (uint*)pointer); } }

					public void Set(params Vector3ui[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						unsafe 
						{
							fixed(Vector3ui* pointer = values)
								Set3(values.Length, (uint*)pointer);
						}
					}

					public void Set(int first, int count, params Vector3ui[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						if(first < 0 || first > values.Length)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > values.Length)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							fixed(Vector3ui* pointer = values)
								Set3(count, (uint*)(pointer + first));
						}
					}

					public void Set(IList<Vector3ui> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						Set(0, list.Count, list);
					}

					public void Set(int first, int count, IList<Vector3ui> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						if(first < 0 || first > list.Count)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > list.Count)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							using (Context.Lock())
								lock (locker)
									switch (type)
									{
																					case ActiveUniformType.BoolVec3:
												ToBoolean(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.DoubleVec3:
												ToDouble(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (double*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.FloatVec3:
												ToSingle(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (float*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.IntVec3:
												ToInt32(first, count, list);
												GL.ProgramUniform3(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.UnsignedIntVec3:
												ToUInt32(first, count, list);
												GL.ProgramUniform3((uint)Program.Id, location, count, (uint*)ProgramUniform.list.ToPointer());
												break;
																				default: throw new InvalidOperationException();
									}							
						}
					}
								
				public unsafe void Set4(int count, bool* data)
				{
					using (Context.Lock())
						lock (locker)
							switch (type)
							{
																	case ActiveUniformType.BoolVec4:
																					ToBoolean(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.DoubleVec4:
																					ToDouble(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (double*)list.ToPointer());
																				break;
																	case ActiveUniformType.FloatVec4:
																					ToSingle(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (float*)list.ToPointer());
																				break;
																	case ActiveUniformType.IntVec4:
																					ToInt32(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.UnsignedIntVec4:
																					ToUInt32(count * 4, data);
											GL.ProgramUniform4((uint)Program.Id, location, count, (uint*)list.ToPointer());
																				break;
																default: throw new InvalidOperationException();
							}
				}

													public Matrix4d GetMatrix4d()
					{
						Matrix4d result = new Matrix4d();
						unsafe { Get(&result.XX, new Vector2i(4, 4), 1); }
						return result;
					}
				
				public unsafe void Set4(int count, double* data)
				{
					using (Context.Lock())
						lock (locker)
							switch (type)
							{
																	case ActiveUniformType.BoolVec4:
																					ToBoolean(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.DoubleVec4:
																					GL.ProgramUniform4(Program.Id, location, count, (double*)data);
																				break;
																	case ActiveUniformType.FloatVec4:
																					ToSingle(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (float*)list.ToPointer());
																				break;
																	case ActiveUniformType.IntVec4:
																					ToInt32(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.UnsignedIntVec4:
																					ToUInt32(count * 4, data);
											GL.ProgramUniform4((uint)Program.Id, location, count, (uint*)list.ToPointer());
																				break;
																default: throw new InvalidOperationException();
							}
				}

									public void Set(Vector4d value) { unsafe { Set4(1, (double*)&value); } }

					public void Set(ref Vector4d value) { unsafe { fixed(Vector4d* pointer = &value) Set4(1, (double*)pointer); } }

					public void Set(params Vector4d[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						unsafe 
						{
							fixed(Vector4d* pointer = values)
								Set4(values.Length, (double*)pointer);
						}
					}

					public void Set(int first, int count, params Vector4d[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						if(first < 0 || first > values.Length)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > values.Length)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							fixed(Vector4d* pointer = values)
								Set4(count, (double*)(pointer + first));
						}
					}

					public void Set(IList<Vector4d> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						Set(0, list.Count, list);
					}

					public void Set(int first, int count, IList<Vector4d> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						if(first < 0 || first > list.Count)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > list.Count)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							using (Context.Lock())
								lock (locker)
									switch (type)
									{
																					case ActiveUniformType.BoolVec4:
												ToBoolean(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.DoubleVec4:
												ToDouble(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (double*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.FloatVec4:
												ToSingle(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (float*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.IntVec4:
												ToInt32(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.UnsignedIntVec4:
												ToUInt32(first, count, list);
												GL.ProgramUniform4((uint)Program.Id, location, count, (uint*)ProgramUniform.list.ToPointer());
												break;
																				default: throw new InvalidOperationException();
									}							
						}
					}
													public Matrix4f GetMatrix4f()
					{
						Matrix4f result = new Matrix4f();
						unsafe { Get(&result.XX, new Vector2i(4, 4), 1); }
						return result;
					}
				
				public unsafe void Set4(int count, float* data)
				{
					using (Context.Lock())
						lock (locker)
							switch (type)
							{
																	case ActiveUniformType.BoolVec4:
																					ToBoolean(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.DoubleVec4:
																					ToDouble(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (double*)list.ToPointer());
																				break;
																	case ActiveUniformType.FloatVec4:
																					GL.ProgramUniform4(Program.Id, location, count, (float*)data);
																				break;
																	case ActiveUniformType.IntVec4:
																					ToInt32(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.UnsignedIntVec4:
																					ToUInt32(count * 4, data);
											GL.ProgramUniform4((uint)Program.Id, location, count, (uint*)list.ToPointer());
																				break;
																default: throw new InvalidOperationException();
							}
				}

									public void Set(Vector4f value) { unsafe { Set4(1, (float*)&value); } }

					public void Set(ref Vector4f value) { unsafe { fixed(Vector4f* pointer = &value) Set4(1, (float*)pointer); } }

					public void Set(params Vector4f[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						unsafe 
						{
							fixed(Vector4f* pointer = values)
								Set4(values.Length, (float*)pointer);
						}
					}

					public void Set(int first, int count, params Vector4f[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						if(first < 0 || first > values.Length)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > values.Length)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							fixed(Vector4f* pointer = values)
								Set4(count, (float*)(pointer + first));
						}
					}

					public void Set(IList<Vector4f> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						Set(0, list.Count, list);
					}

					public void Set(int first, int count, IList<Vector4f> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						if(first < 0 || first > list.Count)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > list.Count)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							using (Context.Lock())
								lock (locker)
									switch (type)
									{
																					case ActiveUniformType.BoolVec4:
												ToBoolean(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.DoubleVec4:
												ToDouble(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (double*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.FloatVec4:
												ToSingle(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (float*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.IntVec4:
												ToInt32(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.UnsignedIntVec4:
												ToUInt32(first, count, list);
												GL.ProgramUniform4((uint)Program.Id, location, count, (uint*)ProgramUniform.list.ToPointer());
												break;
																				default: throw new InvalidOperationException();
									}							
						}
					}
								
				public unsafe void Set4(int count, int* data)
				{
					using (Context.Lock())
						lock (locker)
							switch (type)
							{
																	case ActiveUniformType.BoolVec4:
																					ToBoolean(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.DoubleVec4:
																					ToDouble(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (double*)list.ToPointer());
																				break;
																	case ActiveUniformType.FloatVec4:
																					ToSingle(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (float*)list.ToPointer());
																				break;
																	case ActiveUniformType.IntVec4:
																					GL.ProgramUniform4(Program.Id, location, count, (int*)data);
																				break;
																	case ActiveUniformType.UnsignedIntVec4:
																					ToUInt32(count * 4, data);
											GL.ProgramUniform4((uint)Program.Id, location, count, (uint*)list.ToPointer());
																				break;
																default: throw new InvalidOperationException();
							}
				}

									public void Set(Vector4i value) { unsafe { Set4(1, (int*)&value); } }

					public void Set(ref Vector4i value) { unsafe { fixed(Vector4i* pointer = &value) Set4(1, (int*)pointer); } }

					public void Set(params Vector4i[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						unsafe 
						{
							fixed(Vector4i* pointer = values)
								Set4(values.Length, (int*)pointer);
						}
					}

					public void Set(int first, int count, params Vector4i[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						if(first < 0 || first > values.Length)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > values.Length)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							fixed(Vector4i* pointer = values)
								Set4(count, (int*)(pointer + first));
						}
					}

					public void Set(IList<Vector4i> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						Set(0, list.Count, list);
					}

					public void Set(int first, int count, IList<Vector4i> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						if(first < 0 || first > list.Count)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > list.Count)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							using (Context.Lock())
								lock (locker)
									switch (type)
									{
																					case ActiveUniformType.BoolVec4:
												ToBoolean(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.DoubleVec4:
												ToDouble(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (double*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.FloatVec4:
												ToSingle(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (float*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.IntVec4:
												ToInt32(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.UnsignedIntVec4:
												ToUInt32(first, count, list);
												GL.ProgramUniform4((uint)Program.Id, location, count, (uint*)ProgramUniform.list.ToPointer());
												break;
																				default: throw new InvalidOperationException();
									}							
						}
					}
								
				public unsafe void Set4(int count, uint* data)
				{
					using (Context.Lock())
						lock (locker)
							switch (type)
							{
																	case ActiveUniformType.BoolVec4:
																					ToBoolean(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.DoubleVec4:
																					ToDouble(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (double*)list.ToPointer());
																				break;
																	case ActiveUniformType.FloatVec4:
																					ToSingle(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (float*)list.ToPointer());
																				break;
																	case ActiveUniformType.IntVec4:
																					ToInt32(count * 4, data);
											GL.ProgramUniform4(Program.Id, location, count, (int*)list.ToPointer());
																				break;
																	case ActiveUniformType.UnsignedIntVec4:
																					GL.ProgramUniform4((uint)Program.Id, location, count, (uint*)data);
																				break;
																default: throw new InvalidOperationException();
							}
				}

									public void Set(Vector4ui value) { unsafe { Set4(1, (uint*)&value); } }

					public void Set(ref Vector4ui value) { unsafe { fixed(Vector4ui* pointer = &value) Set4(1, (uint*)pointer); } }

					public void Set(params Vector4ui[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						unsafe 
						{
							fixed(Vector4ui* pointer = values)
								Set4(values.Length, (uint*)pointer);
						}
					}

					public void Set(int first, int count, params Vector4ui[] values)
					{
						if(values == null)
							throw new ArgumentNullException("values");
						if(first < 0 || first > values.Length)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > values.Length)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							fixed(Vector4ui* pointer = values)
								Set4(count, (uint*)(pointer + first));
						}
					}

					public void Set(IList<Vector4ui> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						Set(0, list.Count, list);
					}

					public void Set(int first, int count, IList<Vector4ui> list)
					{
						if(list == null)
							throw new ArgumentNullException("list");
						if(first < 0 || first > list.Count)
							throw new ArgumentOutOfRangeException("first");
						if(count <= 0 || first + count > list.Count)
							throw new ArgumentOutOfRangeException("count");
						unsafe
						{
							using (Context.Lock())
								lock (locker)
									switch (type)
									{
																					case ActiveUniformType.BoolVec4:
												ToBoolean(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.DoubleVec4:
												ToDouble(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (double*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.FloatVec4:
												ToSingle(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (float*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.IntVec4:
												ToInt32(first, count, list);
												GL.ProgramUniform4(Program.Id, location, count, (int*)ProgramUniform.list.ToPointer());
												break;
																					case ActiveUniformType.UnsignedIntVec4:
												ToUInt32(first, count, list);
												GL.ProgramUniform4((uint)Program.Id, location, count, (uint*)ProgramUniform.list.ToPointer());
												break;
																				default: throw new InvalidOperationException();
									}							
						}
					}
					}
}

