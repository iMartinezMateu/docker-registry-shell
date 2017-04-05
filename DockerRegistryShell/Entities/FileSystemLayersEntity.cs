namespace DockerRegistryShell
{
	public class FileSystemLayersEntity
	{
		public string blobSum;

		public string BlobSum
		{
			get
			{
				return blobSum;
			}

			set
			{
				blobSum = value;
			}
		}

		public override string ToString()
		{
			return string.Format("[FileSystemLayersEntity: blobSum={0}]", blobSum);
		}
	}
}