using System;
using System.Globalization;

namespace EISOL_TestePraticoWebForms
{
	public partial class Tarefa2 : BasePage
	{
		/*
         * 
         * 
         * 
         *  Sério mesmo que você acho que eu responderia a tarefa 1 aqui!?!?
         *  Nananinanão.
         *  Se ele funcionou lá, com certeza funcionará aqui! ;D
         * 
         * 
         * 
         * 
         * */

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
			LimparControles(this);
			LimparValidacoes();
		}

		private bool ValidarFormulario(out DateTime dataNascimento)
		{
			LimparValidacoes();
			dataNascimento = DateTime.MinValue;

			var valido = true;

			if (string.IsNullOrWhiteSpace(txtNome.Text))
			{
				ExibirErro(valNome);
				valido = false;
			}

			if (string.IsNullOrWhiteSpace(txtCpf.Text))
			{
				ExibirErro(valCpf);
				valido = false;
			}
			else
			{
				var cpfDigits = SomenteDigitos(txtCpf.Text);
				if (!Utils.CpfValidator.CpfValido(cpfDigits))
				{
					ExibirErro(valCpfInvalido);
					valido = false;
				}
			}

			if (string.IsNullOrWhiteSpace(txtRg.Text))
			{
				ExibirErro(valRg);
				valido = false;
			}

			if (string.IsNullOrWhiteSpace(ddlSexo.SelectedValue))
			{
				ExibirErro(valSexo);
				valido = false;
			}

			var emailTexto = txtEmail.Text?.Trim();
			if (!string.IsNullOrWhiteSpace(emailTexto) && !EmailValido(emailTexto))
			{
				ExibirErro(valEmail);
				valido = false;
			}

			var dataTexto = txtDataNascimento.Text?.Trim();
			if (string.IsNullOrWhiteSpace(dataTexto))
			{
				ExibirErro(valDataNascimento);
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
					ExibirErro(valDataNascimento);
					valido = false;
				}
				else if (dataNascimento.Date > DateTime.Today)
				{
					ExibirErro(valDataNascimentoInvalida);
					valido = false;
				}
			}

			return valido;
		}

		private void LimparValidacoes()
		{
			OcultarErro(valNome);
			OcultarErro(valCpf);
			OcultarErro(valCpfInvalido);
			OcultarErro(valRg);
			OcultarErro(valEmail);
			OcultarErro(valSexo);
			OcultarErro(valDataNascimento);
			OcultarErro(valDataNascimentoInvalida);
		}

	}
}
