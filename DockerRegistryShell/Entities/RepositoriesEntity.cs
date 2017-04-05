using System;
using System.Collections.Generic;

namespace DockerRegistryShell
{
	public class RepositoriesEntity
	{
		public List<string> repositories;

		public List<string>  Repositories
		{
			get
			{
				return repositories;
			}

			set
			{
				repositories = value;
			}
		}

		public override string ToString()
		{
			return string.Format("[RepositoriesEntity: repositories={0}]", string.Join(",", repositories.ToArray()));
		}
	}
}
