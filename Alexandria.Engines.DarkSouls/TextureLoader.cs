using Alexandria.Resources;
using Glare.Graphics;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.DarkSouls {
	public class TextureLoader : Loader {
		const int Magic = 0x020200FF;

		public TextureLoader(Engine engine)
			: base(engine) {
		}

		public override LoaderMatchLevel Match(System.IO.BinaryReader reader, string name, LoaderFileOpener opener, Resource context) {
			if (reader.TryReadInt32() != Magic)
				return LoaderMatchLevel.None;
			return LoaderMatchLevel.Strong;
		}

		public override Resource Load(System.IO.BinaryReader reader, string name, LoaderFileOpener opener, Resource context) {
			reader.Require(Magic);
			var contentSize = reader.ReadInt32(); // total size - header size
			reader.Require(1);
			reader.Require(0);
			reader.Require(0x80); // headerSize
			if (contentSize + 0x80 != reader.BaseStream.Length)
				throw new Exception();
			reader.Require(contentSize); // contentSize2
			reader.Require(0x85040201);
			reader.Require(0xAAE4);
			int width = reader.ReadUInt16();
			int height = reader.ReadUInt16();
			reader.Require((ushort)1);
			reader.RequireZeroes(90);

			if (width != height)
				throw new InvalidDataException("Width and height must be equal for a cube map.");

			TextureCube cube = new TextureCube();
			//cube.Storage(
				//cube., width, TextureFormats.Vector4nb);
			throw new NotImplementedException();
			/*
			byte[] data = new byte[width * height * 4];

			foreach (CubeFace face in TextureCube.Faces) {
				int lod = 0;

				for (int size = width; size >= 8; size /= 2, lod++) {
					reader.Read(data, 0, size * size * 4);
					cube.SetData(face, lod, Box2i.CreateSize(size, size), data);
				}

				cube.MaxLod = lod - 1;
			}

			return new TextureResource(Manager, cube, name);*/
		}
	}
}
