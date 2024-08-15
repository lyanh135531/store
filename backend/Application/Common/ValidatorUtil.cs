using Domain.Core;

namespace Application.Common;

public static class ValidatorUtil
{
    public static bool ValidGender(string gender)
    {
        return Enum.TryParse(typeof(Gender), gender, true, out _);
    }
}