using System;

namespace Domain.Contexts.UserBoundedContext.Constants
{
    public class UserConstants
    {
        public class NameProperty
        {
            public const int MaxLength = 1024;
        }

        public class SubNameProperty
        {
            public const int MaxLength = 256;
        }

        public class AdminEntity
        {
            public static readonly Guid Id = Guid.Parse("6EC427E5-63C4-4687-BFF0-153E21C06D30");
            public const string Email = "h.f.alvarez.rubio@gmail.com";
            public const string UserName = "Enryu19_";
            public const string Name = "Hernán";
            public const string SubName = "Álvarez Rubio";
            public const string PhoneNumber = "+569 4979 8355";
        }
    }
}
