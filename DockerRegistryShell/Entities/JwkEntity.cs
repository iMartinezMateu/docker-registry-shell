namespace DockerRegistryShell
{
	public class JwkEntity
	{
		public string crv;
		public string kid;
		public string kty;
		public string x;
		public string y;

		public string Crv
		{
			get
			{
				return crv;
			}

			set
			{
				crv = value;
			}
		}

		public string Kid
		{
			get
			{
				return kid;
			}

			set
			{
				kid = value;
			}
		}

		public string Kty
		{
			get
			{
				return kty;
			}

			set
			{
				kty = value;
			}
		}

		public string X
		{
			get
			{
				return x;
			}

			set
			{
				x = value;
			}
		}

		public string Y
		{
			get
			{
				return y;
			}

			set
			{
				y = value;
			}
		}

		public override string ToString()
		{
			return string.Format("[JwkEntity: crv={0}, kid={1}, kty={2}, x={3}, y={4}]", crv, kid, kty, x, y);
		}
	}
}