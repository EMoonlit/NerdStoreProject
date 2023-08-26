using System.Text.RegularExpressions;

namespace NSE.Core.DomainObjects;

public class Email
{
    public const int EmailAdressMaxLength = 254;
    public const int EmailAdressMinLength = 5;
    public string EmailAdress { get; private set; }

    //Construtor for EntityFramework
    protected Email() { }

    public Email(string emailAdress)
    {
        if (!IsValid(emailAdress)) throw new DomainException("Invalid E-mail");
        EmailAdress = emailAdress;
    }

    public static bool IsValid(string email)
    {
        var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        return regexEmail.IsMatch(email);
    }
}