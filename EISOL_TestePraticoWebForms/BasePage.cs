using System.Linq;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EISOL_TestePraticoWebForms
{
    public abstract class BasePage : Page
    {
        protected static string NormalizarTexto(string valor, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return null;
            }

            var normalizado = valor.Trim();
            return normalizado.Length <= maxLength ? normalizado : normalizado.Substring(0, maxLength);
        }

        protected static string SomenteDigitos(string valor)
            => string.IsNullOrWhiteSpace(valor) ? null : new string(valor.Where(char.IsDigit).ToArray());


        protected static bool EmailValido(string email)
        {
            try
            {
                var address = new MailAddress(email);
                return string.Equals(address.Address, email, System.StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        protected static void LimparControles(Control root)
        {
            foreach (Control control in root.Controls)
            {
                var textBox = control as TextBox;
                if (textBox != null)
                {
                    textBox.Text = string.Empty;
                }
                else
                {
                    var dropDownList = control as DropDownList;
                    if (dropDownList != null)
                    {
                        dropDownList.SelectedIndex = 0;
                    }
                    else
                    {
                        var radioButton = control as RadioButton;
                        if (radioButton != null)
                        {
                            radioButton.Checked = false;
                        }
                        else
                        {
                            var checkBox = control as CheckBox;
                            if (checkBox != null)
                            {
                                checkBox.Checked = false;
                            }
                            else
                            {
                                var listBox = control as ListBox;
                                if (listBox != null)
                                {
                                    listBox.ClearSelection();
                                }
                            }
                        }
                    }
                }

                if (control.HasControls())
                {
                    LimparControles(control);
                }
            }
        }

        protected static void ExibirErro(Label label)
        {
            label.Style["display"] = "block";
        }

        protected static void OcultarErro(Label label)
        {
            label.Style["display"] = "none";
        }
    }
}