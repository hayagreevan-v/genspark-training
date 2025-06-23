using System;
using System.ComponentModel.DataAnnotations;

namespace FirstAPI.Misc;

public class NameValidation : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        string s = value as string ?? "";
        if (string.IsNullOrEmpty(s)) return false;

        foreach (char c in s.ToCharArray())
        {
            if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
            {
                return false;
            }
        }
        return true;
    }
}
