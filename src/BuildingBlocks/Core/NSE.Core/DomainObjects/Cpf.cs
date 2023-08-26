namespace NSE.Core.DomainObjects;

public class Cpf
{
    public const int CpfMaxLength = 11;
    public string CpfDigits { get; private set; }

    protected Cpf() { }

    public Cpf(string cpfDigits)
    {
        if (!IsValid(cpfDigits)) throw new DomainException("Invalid CPF Document");
        CpfDigits = cpfDigits;
    }

    public static bool IsValid(string cpfDigits) {

        int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        cpfDigits = cpfDigits.Trim();
        cpfDigits = cpfDigits.Replace(".", "").Replace("-", "");
 
        if (cpfDigits.Length != 11)
            return false;

        var tempCpf = cpfDigits.Substring(0, 9);
        var sum = 0;

        for (int i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

        var rest = sum % 11;

        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        var digit = rest.ToString();

        tempCpf += digit;

        sum = 0;

        for (int i = 0; i < 10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

        rest = sum % 11;
  
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit += rest.ToString();

        return cpfDigits.EndsWith(digit);
    }
}