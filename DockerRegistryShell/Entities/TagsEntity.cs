using System;
using System.Collections.Generic;

namespace DockerRegistryShell
{
	public class TagsEntity
	{
		public string name;
		public List<string> tags;

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

		public List<string>  Tags
		{
			get
			{
				return tags;
			}

			set
			{
				tags = value;
			}
		}

		public override string ToString()
		{
			return string.Format("[TagsEntity: name={0}, tags={1}]", name, string.Join(",", tags.ToArray()));
		}
	}
}
