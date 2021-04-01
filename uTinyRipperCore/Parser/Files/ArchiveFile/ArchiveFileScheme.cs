using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using uTinyRipper.ArchiveFiles;
using uTinyRipper.SerializedFiles;

namespace uTinyRipper
{
	public sealed class ArchiveFileScheme : FileScheme
	{
		private ArchiveFileScheme(string filePath, string fileName) :
			base(filePath, fileName)
		{
		}

		internal static ArchiveFileScheme ReadScheme(byte[] buffer, string filePath, string fileName)
		{
			ArchiveFileScheme scheme = new ArchiveFileScheme(filePath, fileName);
			using (MemoryStream stream = new MemoryStream(buffer, 0, buffer.Length, false))
			{
				scheme.ReadScheme(stream);
			}
			return scheme;
		}

		internal static ArchiveFileScheme ReadScheme(Stream stream, string filePath, string fileName)
		{
			ArchiveFileScheme scheme = new ArchiveFileScheme(filePath, fileName);
			scheme.ReadScheme(stream);
			return scheme;
		}

		internal ArchiveFile ReadFile(GameProcessorContext context)
		{
			ArchiveFile archive = new ArchiveFile(this);
			archive.AddFile(context, WebScheme);
			return archive;
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		private void ReadScheme(Stream stream)
		{
			byte[] buffer;
			using (EndianReader reader = new EndianReader(stream, EndianType.BigEndian))
			{
				Header.Read(reader);
				switch (Header.Type)
				{
					case ArchiveType.GZip:
						buffer = ReadGZip(reader);
						break;

					default:
						throw new NotSupportedException(Header.Type.ToString());
				}
			}

			WebScheme = WebFile.ReadScheme(buffer, FilePath);
		}

		private byte[] ReadGZip(EndianReader reader)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(reader.BaseStream, CompressionMode.Decompress))
				{
					gzipStream.CopyTo(stream);
				}
				return stream.ToArray();
			}
		}

		public override FileEntryType SchemeType => FileEntryType.Archive;
		public override IEnumerable<FileIdentifier> Dependencies => WebScheme.Dependencies;

		public ArchiveHeader Header { get; } = new ArchiveHeader();
		public WebFileScheme WebScheme { get; private set; }
	}
}
