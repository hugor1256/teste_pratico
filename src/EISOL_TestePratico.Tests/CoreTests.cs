using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.UI;
using System.Web.UI.WebControls;
using EISOL_TestePraticoWebForms;
using System.Linq;
using BLL;

namespace EISOL_TestePratico.Tests
{
    [TestClass]
    public class CpfValidator
    {
        [TestMethod]
        public void CpfValido_DeveAceitarCpfValidoComMascara()
        {
            Assert.IsTrue(EISOL_TestePraticoWebForms.Utils.CpfValidator.CpfValido("529.982.247-25"));
            Assert.IsTrue(EISOL_TestePraticoWebForms.Utils.CpfValidator.CpfValido("111.444.777-35"));
        }

        [TestMethod]
        public void CpfValido_DeveAceitarCpfValidoSomenteDigitos()
        {
            Assert.IsTrue(EISOL_TestePraticoWebForms.Utils.CpfValidator.CpfValido("52998224725"));
        }

        [TestMethod]
        public void CpfValido_DeveRejeitarCpfInvalido()
        {
            Assert.IsFalse(EISOL_TestePraticoWebForms.Utils.CpfValidator.CpfValido("111.111.111-11"));
            Assert.IsFalse(EISOL_TestePraticoWebForms.Utils.CpfValidator.CpfValido("123.456.789-00"));
            Assert.IsFalse(EISOL_TestePraticoWebForms.Utils.CpfValidator.CpfValido("123"));
            Assert.IsFalse(EISOL_TestePraticoWebForms.Utils.CpfValidator.CpfValido(null));
        }

        [TestMethod]
        public void CpfValido_DeveRejeitarTamanhoIncorreto()
        {
            Assert.IsFalse(EISOL_TestePraticoWebForms.Utils.CpfValidator.CpfValido("1234567890")); // 10
            Assert.IsFalse(EISOL_TestePraticoWebForms.Utils.CpfValidator.CpfValido("123456789012")); // 12
        }
    }

    [TestClass]
    public class BasePageTests
    {
        [TestMethod]
        public void NormalizarTexto_DeveTrimarETruncar()
        {
            Assert.AreEqual("abc", BasePageAccessor.Normalizar("  abc  ", 10));
            Assert.AreEqual("abc", BasePageAccessor.Normalizar("abc", 3));
            Assert.AreEqual("ab", BasePageAccessor.Normalizar("abcd", 2));
            Assert.IsNull(BasePageAccessor.Normalizar("   ", 10));
        }

        [TestMethod]
        public void NormalizarTexto_DeveRetornarNullParaNull()
        {
            Assert.IsNull(BasePageAccessor.Normalizar(null, 10));
        }

        [TestMethod]
        public void SomenteDigitos_DeveRemoverNaoNumericos()
        {
            Assert.AreEqual("123456", BasePageAccessor.Digitos("123.456"));
            Assert.AreEqual("", BasePageAccessor.Digitos("-"));
            Assert.IsNull(BasePageAccessor.Digitos(null));
        }

        [TestMethod]
        public void SomenteDigitos_DeveRetornarNullParaEspacos()
        {
            Assert.IsNull(BasePageAccessor.Digitos("   "));
        }

        [TestMethod]
        public void EmailValido_DeveValidarFormatoBasico()
        {
            Assert.IsTrue(BasePageAccessor.EmailOk("teste@dominio.com"));
            Assert.IsTrue(BasePageAccessor.EmailOk("teste+tag@dominio.com"));
            Assert.IsFalse(BasePageAccessor.EmailOk("teste@dominio"));
            Assert.IsFalse(BasePageAccessor.EmailOk("teste"));
        }

        [TestMethod]
        public void EmailValido_DeveRejeitarEspacosENull()
        {
            Assert.IsFalse(BasePageAccessor.EmailOk("teste @dominio.com"));
            Assert.IsFalse(BasePageAccessor.EmailOk(null));
        }

        [TestMethod]
        public void LimparControles_DeveLimparCamposRecursivamente()
        {
            var root = new Panel();
            var nested = new Panel();

            var txt = new TextBox { Text = "abc" };
            var ddl = new DropDownList();
            ddl.Items.Add(new ListItem("Selecione", ""));
            ddl.Items.Add(new ListItem("Item", "1"));
            ddl.SelectedIndex = 1;

            var chk = new CheckBox { Checked = true };
            var rb = new RadioButton { Checked = true };
            var lst = new ListBox();
            lst.Items.Add(new ListItem("A", "A"));
            lst.Items.Add(new ListItem("B", "B"));
            lst.SelectedIndex = 1;

            nested.Controls.Add(txt);
            nested.Controls.Add(ddl);
            nested.Controls.Add(chk);
            nested.Controls.Add(rb);
            nested.Controls.Add(lst);
            root.Controls.Add(nested);

            BasePageAccessor.Limpar(root);

            Assert.AreEqual(string.Empty, txt.Text);
            Assert.AreEqual(0, ddl.SelectedIndex);
            Assert.IsFalse(chk.Checked);
            Assert.IsFalse(rb.Checked);
            Assert.AreEqual(-1, lst.SelectedIndex);
        }

        [TestMethod]
        public void LimparControles_DeveSuportarRootSemFilhos()
        {
            var root = new Panel();
            BasePageAccessor.Limpar(root);
        }

        [TestMethod]
        public void ExibirOcultarErro_DeveAlternarDisplay()
        {
            var label = new Label();
            BasePageAccessor.Exibir(label);
            Assert.AreEqual("block", label.Style["display"]);

            BasePageAccessor.Ocultar(label);
            Assert.AreEqual("none", label.Style["display"]);
        }

        private class BasePageAccessor : BasePage
        {
            public static string Normalizar(string valor, int maxLength) => NormalizarTexto(valor, maxLength);
            public static string Digitos(string valor) => SomenteDigitos(valor);
            public static bool EmailOk(string email) => EmailValido(email);
            public static void Limpar(Control root) => LimparControles(root);
            public static void Exibir(Label label) => ExibirErro(label);
            public static void Ocultar(Label label) => OcultarErro(label);
        }
    }

    [TestClass]
    public class UfAndCityTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Uf_CarregarTodos_DeveConterUfsBasicas()
        {
            var ufs = new UF().CarregarTodos();
            Assert.IsNotNull(ufs);
            Assert.IsTrue(ufs.Count >= 4);
            Assert.IsTrue(ufs.Any(u => u.NOME == "MT"));
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Cidades_CarregarPorUf_DeveFiltrarCorretamente()
        {
            var cidades = new CIDADES().CarregarPorUF(3); // MT
            Assert.IsNotNull(cidades);
            Assert.IsTrue(cidades.Count >= 1);
            Assert.IsTrue(cidades.All(c => c.COD_UF == 3));
            Assert.IsTrue(cidades.Any(c => c.NOME == "CuiabÃ¡"));
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Cidades_CarregarPorUf_Inexistente_DeveRetornarVazioOuNull()
        {
            var cidades = new CIDADES().CarregarPorUF(999);
            if (cidades != null)
            {
                Assert.AreEqual(0, cidades.Count);
            }
        }
    }
}
