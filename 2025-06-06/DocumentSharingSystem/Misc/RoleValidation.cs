using System.ComponentModel.DataAnnotations;

namespace DocumentSharingSystem.Misc
{
    public class RoleValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string role = value as string ?? "";
            if (string.IsNullOrEmpty(role)) return false;

            if (role == "Admin" || role == "User") return true;
            return false;
        }
    }
}