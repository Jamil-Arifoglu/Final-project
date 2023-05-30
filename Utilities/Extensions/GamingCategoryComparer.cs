using Gaming.Entities;
using Gaming.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Gaming.Utilities.Extensions
{
    public class GamingCategoryComparer : IEqualityComparer<GamingCategory>
    {
        public bool Equals(GamingCategory? x, GamingCategory? y)
        {
            if (Equals(x?.Category.Id, y?.Category.Id)) return true;
            return false;
        }

        public int GetHashCode([DisallowNull] GamingCategory obj)
        {
            throw new NotImplementedException();
        }

    }
}
