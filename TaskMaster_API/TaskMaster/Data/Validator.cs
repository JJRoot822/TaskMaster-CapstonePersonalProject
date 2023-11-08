using System.Text.RegularExpressions;

namespace TaskMaster.Data;

public static class Validator
{
    public static bool IsNotEmpty(string value)
    {
        return value != "";
    }

    public static bool IsValidUsername(string username)
    {
        if (username.Length < 15)
        {
            return false;
        }

        Regex lowercase = new Regex("[a-z]");
        Regex uppercase = new Regex("[A-Z]");
        Regex digit = new Regex("[0-9]");
        Regex specialChar = new Regex("[._]");
        Regex startsWithDigitOrSpecialChar = new Regex("^[0-9._]");

        
        if (!lowercase.IsMatch(username) || !uppercase.IsMatch(username) || !digit.IsMatch(username) || !specialChar.IsMatch(username))
        {
            return false;
        }

        if (startsWithDigitOrSpecialChar.IsMatch(username))
        {
            return false;
        }

        return true;
    }

    public static bool IsValidEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        return Regex.IsMatch(email, emailPattern);
    }

    public static bool IsValidPassword(string password)
    {
        if (password.Length < 25)
        {
            return false;
        }

        Regex lowercase = new Regex("[a-z]");
        Regex uppercase = new Regex("[A-Z]");
        Regex digit = new Regex("[0-9]");
        Regex specialChar = new Regex("[.,@@_!#$%^&*()+=]");

        if (!lowercase.IsMatch(password) || !uppercase.IsMatch(password) || !digit.IsMatch(password) || !specialChar.IsMatch(password))
        {
            return false;
        }

        return true;
    }

}
