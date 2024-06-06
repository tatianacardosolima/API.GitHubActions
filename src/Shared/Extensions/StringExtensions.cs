using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fiap.Clientes.Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidTelefone(this string telefone)
        {
            if (telefone == null || telefone == "") return false;

            string pattern = @"^\(\d{2}\) \d{9}$";

            return Regex.IsMatch(telefone, pattern);
        }
        public static bool IsValidEmail(this string email)
        {
         
            if (email == null || email == "") return false;
            string pattern = @"^(?!\.)[a-zA-Z0-9._%+-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, pattern);
        }

        public static bool IsNumeric(this string input)
        {
            // Regular expression to match only numeric characters
            Regex regex = new Regex(@"^[0-9]+$");

            // Check if the input matches the regular expression
            return regex.IsMatch(input);
        }
        public static bool IsCPFValid(this string cpf)
        {

            if (cpf== null)
                return false;
            // Remover caracteres não numéricos do CPF
            cpf = cpf.Replace(".", "").Replace("-", "");

            // Verificar se o CPF tem 11 dígitos
            if (cpf.Length != 11)
                return false;

            if (cpf.Distinct().Count()==1)
                return false;

            if (!cpf.IsNumeric())
                return false;

            // Calcular o primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            // Verificar se o primeiro dígito verificador está correto
            if (int.Parse(cpf[9].ToString()) != digitoVerificador1)
                return false;

            // Calcular o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            // Verificar se o segundo dígito verificador está correto
            if (int.Parse(cpf[10].ToString()) != digitoVerificador2)
                return false;

            // CPF válido
            return true;
        }
    }
}
