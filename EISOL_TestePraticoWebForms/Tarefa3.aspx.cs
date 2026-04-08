using System;

namespace EISOL_TestePraticoWebForms
{
    public partial class Tarefa3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregarControles();
            }
        }

        /// <summary>
        /// Carregar dados e povoar os controles
        /// </summary>
        private void CarregarControles()
        {
            // Povoando as Unidades da Federação.
            ddlUf.Items.Clear();
            ddlUf.DataSource = new BLL.UF().CarregarTodos();
            ddlUf.DataTextField = "NOME";
            ddlUf.DataValueField = "COD_UF";
            ddlUf.DataBind();
            ddlUf.Items.Insert(0, new System.Web.UI.WebControls.ListItem("[Selecione]", string.Empty));

            // Povoando as Cidades
            LimparCidades();
        }

        protected void ddlUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ddlUf.SelectedValue))
            {
                LimparCidades();
                return;
            }

            decimal codigoUf;
            if (!decimal.TryParse(ddlUf.SelectedValue, out codigoUf))
            {
                LimparCidades();
                return;
            }

            ddlCidades.Items.Clear();
            ddlCidades.DataSource = new BLL.CIDADES().CarregarPorUF(codigoUf);
            ddlCidades.DataTextField = "NOME";
            ddlCidades.DataValueField = "COD_CIDADE";
            ddlCidades.DataBind();
            ddlCidades.Items.Insert(0, new System.Web.UI.WebControls.ListItem("[Selecione]", string.Empty));
        }

        private void LimparCidades()
        {
            ddlCidades.Items.Clear();
            ddlCidades.Items.Insert(0, new System.Web.UI.WebControls.ListItem("[Selecione]", string.Empty));
        }
    }
}