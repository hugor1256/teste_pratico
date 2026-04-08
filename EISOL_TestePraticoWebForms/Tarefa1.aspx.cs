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

			this.divAlerta.Visible = false;

			DateTime dataNascimento;
			if (!this.ValidarFormulario(out dataNascimento))
			{
				return;
			}

			pessoa.NOME = this.NormalizarTexto(this.txtNome.Text, 200);
			pessoa.CPF = this.NormalizarTexto(this.SomenteDigitos(this.txtCpf.Text), 11);
			pessoa.RG = this.NormalizarTexto(this.txtRg.Text, 15);
			pessoa.TELEFONE = this.NormalizarTexto(this.txtTelefone.Text, 20);
			pessoa.EMAIL = this.NormalizarTexto(this.txtEmail.Text, 200);
			pessoa.SEXO = this.ddlSexo.SelectedValue;
			pessoa.DATA_NASCIMENTO = dataNascimento;

			this.Gravar(pessoa);
			this.Limpar();
		}

		/// <summary>
		/// Persistir os dados no Banco.
		/// </summary>
		/// <param name="pessoa">DAO.PESSOAS</param>
		private void Gravar(DAO.PESSOAS pessoa)
		{
			// Se a pessoa for uma pessoa de verdade e feliz, com certeza ela será lembrada pelo banco de dados.
			new BLL.PESSOAS().Adicionar(pessoa);
			this.Alertar();
		} 

		/// <summary>
		/// Apresentar o alerta de sucesso na operação.
		/// </summary>
		private void Alertar()
		{
			this.divAlerta.Visible = true;
		}

		/// <summary>
		/// Limpar os campos após a presistência dos dados.
		/// </summary>
		private void Limpar()
		{
			// Isso é apenas um bônus!
			// Tente fazê-lo e colocar em um lugar apropriado no código.
			this.txtNome.Text = string.Empty;
			this.txtCpf.Text = string.Empty;
			this.txtRg.Text = string.Empty;
			this.txtTelefone.Text = string.Empty;
			this.txtEmail.Text = string.Empty;
			this.txtDataNascimento.Text = string.Empty;
			this.ddlSexo.SelectedIndex = 0;

			this.LimparValidacoes();
		}

		private bool ValidarFormulario(out DateTime dataNascimento)
		{
			this.LimparValidacoes();
			dataNascimento = DateTime.MinValue;

			bool valido = true;

			if (string.IsNullOrWhiteSpace(this.txtNome.Text))
			{
				this.valNome.Visible = true;
				valido = false;
			}

			if (string.IsNullOrWhiteSpace(this.txtCpf.Text))
			{
				this.valCpf.Visible = true;
				valido = false;
			}

			if (string.IsNullOrWhiteSpace(this.txtRg.Text))
			{
				this.valRg.Visible = true;
				valido = false;
			}

			if (string.IsNullOrWhiteSpace(this.ddlSexo.SelectedValue))
			{
				this.valSexo.Visible = true;
				valido = false;
			}

			string emailTexto = this.txtEmail.Text?.Trim();
			if (!string.IsNullOrWhiteSpace(emailTexto) && !this.EmailValido(emailTexto))
			{
				this.valEmail.Visible = true;
				valido = false;
			}

			string dataTexto = this.txtDataNascimento.Text?.Trim();
			if (string.IsNullOrWhiteSpace(dataTexto))
			{
				this.valDataNascimento.Visible = true;
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
					this.valDataNascimento.Visible = true;
					valido = false;
				}
			}

			return valido;
		}

		private void LimparValidacoes()
		{
			this.valNome.Visible = false;
			this.valCpf.Visible = false;
			this.valRg.Visible = false;
			this.valEmail.Visible = false;
			this.valSexo.Visible = false;
			this.valDataNascimento.Visible = false;
		}

		private string NormalizarTexto(string valor, int maxLength)
		{
			if (string.IsNullOrWhiteSpace(valor))
			{
				return null;
			}

			string normalizado = valor.Trim();
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
