using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2OrmLib.Dto
{
    public enum Visibility
    {
        Public,
        Private,
        Unlisted
    }

    public static class VisibilityExtensions
    {
        public static string ToReprString(this Visibility visibility)
        {
            return visibility switch
            {
                Visibility.Public => "public",
                Visibility.Private => "private",
                Visibility.Unlisted => "unlisted",
                _ => throw new ArgumentOutOfRangeException(nameof(visibility), visibility, null)
            };
        }

        public static Visibility ToVisibilityEnum(this string visibility)
        {
            return visibility switch
            {
                "public" => Visibility.Public,
                "private" => Visibility.Private,
                "unlisted" => Visibility.Unlisted,
                _ => throw new ArgumentOutOfRangeException(nameof(visibility), visibility, null)
            };
        }
    }
}
