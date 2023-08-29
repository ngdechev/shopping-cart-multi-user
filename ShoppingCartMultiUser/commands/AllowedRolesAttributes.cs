using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser.commands
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class AllowedRolesAttribute : Attribute
    {
        public UserRole[] Roles { get; }

        public AllowedRolesAttribute(params UserRole[] roles)
        {
            Roles = roles;
        }
    }
}
