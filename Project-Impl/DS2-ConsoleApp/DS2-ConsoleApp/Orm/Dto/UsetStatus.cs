using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2ConsoleApp.Orm.Dto
{
    public enum UserStatus
    {
        Active, Banned, Suspended
    }

    public static class UserStatusExtensions
    {
        public static string ToReprString(this UserStatus status)
        {
            return status switch
            {
                UserStatus.Active => "active",
                UserStatus.Banned => "banned",
                UserStatus.Suspended => "suspended",
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
        public static UserStatus ToUserStatusEnum(this string status)
        {
            return status switch
            {
                "active" => UserStatus.Active,
                "banned" => UserStatus.Banned,
                "suspended" => UserStatus.Suspended,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}
