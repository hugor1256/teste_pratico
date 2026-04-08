using System.Linq;

namespace EISOL_TestePraticoWebForms.Utils
{
	public static class DocumentoValidator
	{
		public static bool CpfValido(string cpf)
		{
			if (string.IsNullOrWhiteSpace(cpf))
			{
				return false;
			}

			var digits = new string(cpf.Where(char.IsDigit).ToArray());
			if (digits.Length != 11)
			{
				return false;
			}

			if (digits.Distinct().Count() == 1)
			{
				return false;
			}

			var numbers = digits.Select(c => c - '0').ToArray();

			var sum1 = 0;
			for (var i = 0; i < 9; i++)
			{
				sum1 += numbers[i] * (10 - i);
			}

			var mod1 = (sum1 * 10) % 11;
			if (mod1 == 10)
			{
				mod1 = 0;
			}

			if (numbers[9] != mod1)
			{
				return false;
			}

			var sum2 = 0;
			for (int i = 0; i < 10; i++)
			{
				sum2 += numbers[i] * (11 - i);
			}

			var mod2 = (sum2 * 10) % 11;
			if (mod2 == 10)
			{
				mod2 = 0;
			}

			return numbers[10] == mod2;
		}
	}
}
