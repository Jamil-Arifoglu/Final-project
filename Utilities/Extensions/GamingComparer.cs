using Gaming.Entities;
using Gaming.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Gaming.Utilities.Extensions
{
	public class GamingComparer : IEqualityComparer<GamingShop>
	{
		public bool Equals(GamingShop? x, GamingShop? y)
		{
			if (Equals(x?.Id, y?.Id)) return true;
			return false;
		}

		public int GetHashCode([DisallowNull] GamingShop obj)
		{
			throw new NotImplementedException();
		}
	}
}
