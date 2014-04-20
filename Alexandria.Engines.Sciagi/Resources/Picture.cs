using Alexandria.Engines.Sciagi.Controls;
using Glare;
using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi.Resources {
	public class Picture : ResourceData {
		public ReadOnlyList<PictureInstruction> Instructions { get; private set; }

		public Picture(BinaryReader reader, Resource resource)
			: base(resource) {
			byte[] data = reader.ReadBytes(checked((int)reader.BaseStream.Length));
			Stream stream = new MemoryStream(data, false);
			RichList<PictureInstruction> instructions = new RichList<PictureInstruction>();
			PicturePatternFlags patternFlags = PicturePatternFlags.None;
			byte patternNumber = 0;

			while (true) {
				PictureInstruction instruction = PictureInstruction.ReadInstruction(stream, ref patternFlags, ref patternNumber);
				instructions.Add(instruction);
				if (instruction.IsQuit)
					break;
			}

			Instructions = instructions;
		}

		public override System.Windows.Forms.Control Browse() {
			return new PictureBrowser(this);
		}
	}/*
        public struct Renderer {
            MemoryStream Reader;

        }

    }*/
}
