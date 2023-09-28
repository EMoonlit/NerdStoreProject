using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc.Razor;

namespace NSE.WebApp.MVC.Extensions;

public static class RazorHelpers
{
    public static string HashEmailForGravatar(this RazorPage page, string email)
    {
        var md5Hasher = MD5.Create();
        var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
        var sBuilder = new StringBuilder();
        foreach (var t in data)
        {
            sBuilder.Append(t.ToString("x2"));
        }

        return sBuilder.ToString();
    }
    public static string StockMessage(this RazorPage page, int quantity)
    {
        return quantity > 0 ? $"Apenas {quantity} em estoque!" : "Produto esgotado";
    }

    public static string MoneyFormat(this RazorPage page, decimal value)
    {
        return value > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", value) : "Gratuido";
    }

    public static string UnitsForProducts(this RazorPage page, int units)
    {
        return units > 1 ? $"{units} unidades" : $"{units} unidade";
    }

    public static string SelectOptionsForQuantity(this RazorPage page, int quantity, int selectedValue = 0)
    {
        var sb = new StringBuilder();
        for (var index = 1; index <= quantity; index += 1)
        {
            var selected = "";
            if (index == selectedValue) selected = "selected";
            sb.Append($"<option {selected} value='{index}'>{index}</option>");
        }

        return sb.ToString();
    }
}