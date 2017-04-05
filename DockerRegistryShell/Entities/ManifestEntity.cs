using System;
using System.Collections.Generic;
using System.Linq;

namespace DockerRegistryShell
{
	public class ManifestEntity
	{
		public int schemaVersion;
		public string name;
		public string tag;
		public string architecture;
		public List<FileSystemLayersEntity> fsLayers;
		public List<HistoryEntity> history;
		public List<SignaturesEntity> signatures;

		public int SchemaVersion
		{
			get
			{
				return schemaVersion;
			}

			set
			{
				schemaVersion = value;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}

			set
			{
				name = value;
			}
		}

		public string Tag
		{
			get
			{
				return tag;
			}

			set
			{
				tag = value;
			}
		}

		public string Architecture
		{
			get
			{
				return architecture;
			}

			set
			{
				architecture = value;
			}
		}

		public override string ToString()
		{
			return string.Format("[ManifestEntity: schemaVersion={0}, name={1}, tag={2}, architecture={3}, fsLayers={4}, history={5}, signatures={6}]", schemaVersion, name, tag, architecture, string.Join(",", fsLayers), string.Join(",", history), string.Join(",", signatures));
		}
	}
}
