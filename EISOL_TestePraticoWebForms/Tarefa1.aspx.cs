using System;
using System.Globalization;
using System.Linq;
using System.Net.Mail;

namespace EISOL_TestePraticoWebForms
{
	public partial class Tarefa1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			// Para saber se o seu registro foi realmente adicionado à tabela, utilize um dos métodos de BLL.PESSOAS.
			// Você poderá realizar a depuração aqui no VS e conferir se tudo deu certo.
			// Sinta-se livre para fazer a sua arte, mas tente fazer o formulário funcionar ok!
		}

		protected void btnGravar_Click(object sender, EventArgs e)
		{
			/* Olá!
             * Trabalhamos com camadas de acesso a dados e negócios, isso também é conhecido por arquitetura em camadas ou N-Tier.
             * Observe que passamos um objeto tipado da camada de acesso (DAO - Data Access Object).
             * E devemos utilizar esse objeto DAO e chamar os métodos da camada de negócios (BLL - Business Logical Layer).
             * É o que por padrão o MVC te induz a fazer, mas aqui no WebForms devemos ter esse cuidado para não dificultar as coisas criando códigos macarrônicos (eita).
             * Você está livre para espiar os códigos e entender o seu funcionamento.
             * Só não vai me bagunçar os códigos pois deu muito trabalho fazer tudo isso aqui =/
             * */
			var pessoa = new DAO.PESSOAS();

			divAlerta.Visible = false;

			DateTime dataNascimento;
			if (!ValidarFormulario(out dataNascimento))
			{
				return;
			}

			pessoa.NOME = NormalizarTexto(txtNome.Text, 200);
			pessoa.CPF = NormalizarTexto(SomenteDigitos(txtCpf.Text), 11);
			pessoa.RG = NormalizarTexto(txtRg.Text, 15);
			pessoa.TELEFONE = NormalizarTexto(txtTelefone.Text, 20);
			pessoa.EMAIL = NormalizarTexto(txtEmail.Text, 200);
			pessoa.SEXO = ddlSexo.SelectedValue;
			pessoa.DATA_NASCIMENTO = dataNascimento;

			Gravar(pessoa);
			Limpar();
		}

		/// <summary>
		/// Persistir os dados no Banco.
		/// </summary>
		/// <param name="pessoa">DAO.PESSOAS</param>
		private void Gravar(DAO.PESSOAS pessoa)
		{
			// Se a pessoa for uma pessoa de verdade e feliz, com certeza ela será lembrada pelo banco de dados.
			new BLL.PESSOAS().Adicionar(pessoa);
			Alertar();
		} 

		/// <summary>
		/// Apresentar o alerta de sucesso na operação.
		/// </summary>
		private void Alertar()
		{
			divAlerta.Visible = true;
		}

		/// <summary>
		/// Limpar os campos após a presistência dos dados.
		/// </summary>
		private void Limpar()
		{
			// Isso é apenas um bônus!
			// Tente fazê-lo e colocar em um lugar apropriado no código.
			txtNome.Text = string.Empty;
			txtCpf.Text = string.Empty;
			txtRg.Text = string.Empty;
			txtTelefone.Text = string.Empty;
			txtEmail.Text = string.Empty;
			txtDataNascimento.Text = string.Empty;
			ddlSexo.SelectedIndex = 0;

			LimparValidacoes();
		}

		private bool ValidarFormulario(out DateTime dataNascimento)
		{
			LimparValidacoes();
			dataNascimento = DateTime.MinValue;

			var valido = true;

			if (string.IsNullOrWhiteSpace(txtNome.Text))
			{
				valNome.Visible = true;
				valido = false;
			}

			if (string.IsNullOrWhiteSpace(txtCpf.Text))
			{
				valCpf.Visible = true;
				valido = false;
			}
			else
			{
				var cpfDigits = SomenteDigitos(txtCpf.Text);
				if (!Utils.DocumentoValidator.CpfValido(cpfDigits))
				{
					valCpfInvalido.Visible = true;
					valido = false;
				}
			}

			if (string.IsNullOrWhiteSpace(txtRg.Text))
			{
				valRg.Visible = true;
				valido = false;
			}

			if (string.IsNullOrWhiteSpace(ddlSexo.SelectedValue))
			{
				valSexo.Visible = true;
				valido = false;
			}

			var emailTexto = txtEmail.Text?.Trim();
			if (!string.IsNullOrWhiteSpace(emailTexto) && !EmailValido(emailTexto))
			{
				valEmail.Visible = true;
				valido = false;
			}

			var dataTexto = txtDataNascimento.Text?.Trim();
			if (string.IsNullOrWhiteSpace(dataTexto))
			{
				valDataNascimento.Visible = true;
				valido = false;
			}
			else
			{
					if (!DateTime.TryParseExact(
							dataTexto,
							"dd/MM/yyyy",
							new CultureInfo("pt-BR"),
							DateTimeStyles.None,
							out dataNascimento))
					{
						valDataNascimento.Visible = true;
						valido = false;
					}
					else if (dataNascimento.Date > DateTime.Today)
					{
						valDataNascimentoInvalida.Visible = true;
						valido = false;
					}
				}

			return valido;
		}

		private void LimparValidacoes()
		{
			valNome.Visible = false;
			valCpf.Visible = false;
			valCpfInvalido.Visible = false;
			valRg.Visible = false;
			valEmail.Visible = false;
			valSexo.Visible = false;
			valDataNascimento.Visible = false;
			valDataNascimentoInvalida.Visible = false;
		}

		private string NormalizarTexto(string valor, int maxLength)
		{
			if (string.IsNullOrWhiteSpace(valor))
			{
				return null;
			}

			var normalizado = valor.Trim();
			return normalizado.Length <= maxLength ? normalizado : normalizado.Substring(0, maxLength);
		}

		private string SomenteDigitos(string valor)
		{
			if (string.IsNullOrWhiteSpace(valor))
			{
				return null;
			}

			var apenasDigitos = new string(valor.Where(char.IsDigit).ToArray());
			return apenasDigitos;
		}

		private bool EmailValido(string email)
		{
			try
			{
				var address = new MailAddress(email);
				return string.Equals(address.Address, email, StringComparison.OrdinalIgnoreCase);
			}
			catch
			{
				return false;
			}
		}
	}
}
