using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class _Default : Page
    {
        public static clsAgenda objAgenda
        {
            get {
                var agenda = HttpContext.Current.Session["Agenda"] as clsAgenda;
                if (agenda == null)
                {
                    agenda = new clsAgenda();
                    HttpContext.Current.Session["Agenda"] = agenda;
                }
                return agenda;
            }
        }

        public bool isEditando
        {
            get { return (bool)(ViewState["isEditando"] ?? false); }
            set { ViewState["isEditando"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //populando um pouco a tabela
                objAgenda.newItem("teste 1", 1633223322, "av teste1, n145");
                objAgenda.newItem("teste 2", 17988855444, "av teste2, n546");
                objAgenda.newItem("teste 3", 1254445555, "av teste3, n1789");
                objAgenda.newItem("teste 4", 168882884444, "av teste4, n321"); //telefone erro (Simulação dado corrompido/incorreto no banco de dados)
                objAgenda.newItem("teste 5", 1266666666, "av teste5, n453");

                carregarDados();
            }
        }

        protected void carregarDados()
        {
            lblErro.Text = string.Empty;

            gvAgenda.DataSource = objAgenda.Itens.AsEnumerable();
            gvAgenda.DataBind(); //não usar databind no page.load, interrompe o funcionamento dos botões
        }

        protected void btnCriar_Click(object sender, EventArgs e)
        {
            lblErro.Text = string.Empty;

            // já foi verificado se o campo está nulo no required field validation
            string nome = txtNome.Text;
            long telefone = -1;
            if (!long.TryParse(txtTelefone.Text, out telefone))
                logErro("telefone deve conter apenas números, Ex: 1633353844");
            string endereco = txtEndereco.Text;

            if (string.IsNullOrEmpty(lblErro.Text)) {
                objAgenda.newItem(nome, telefone, endereco);

                carregarDados();
                //OBS:  Eu poderia usar a validação do clsAgenda para testar se o número de telefone é correto, entretanto
                //      quis induzir um erro no telefone para o service segurar o erro para protejer o Banco de dados
            }
        }

        protected void gvAgenda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int rowIndex = row.RowIndex;

            switch (e.CommandName.ToString())
            {
                case "Editar":
                    
                    if (isEditando)
                    {
                        string telsrt = ((TextBox)row.FindControl("txtTelefone")).Text;
                        long tel = -1;

                        long.TryParse(telsrt, out tel);

                        objAgenda.updateItem(
                            objAgenda.Itens[row.RowIndex].Id,
                            ((TextBox)row.FindControl("txtNome")).Text,
                            tel,
                            ((TextBox)row.FindControl("txtEndereco")).Text
                        );

                        isEditando = false;
                        carregarDados();
                    }
                    else
                    {
                        isEditando = true;
                    }

                    break;
                case "Deletar":
                    if (isEditando) //Cancelar
                    {
                        isEditando = false;
                    }
                    else
                    {
                        objAgenda.deleteItem(objAgenda.Itens[row.RowIndex].Id);
                        carregarDados();
                    }
                    break;
                default:
                    break;
            }

            isEditarConfig(row, isEditando);
        }
        private void isEditarConfig(GridViewRow row, bool isEditar)
        {
            if (isEditando)
            {
                ((LinkButton)row.FindControl("btnEditar")).Text = "Salvar";
                ((LinkButton)row.FindControl("btnExcluir")).Text = "Cancelar";

                ((TextBox)row.FindControl("txtNome")).Enabled = true;
                ((TextBox)row.FindControl("txtTelefone")).Enabled = true;
                ((TextBox)row.FindControl("txtEndereco")).Enabled = true;
            }
            else
            {
                ((LinkButton)row.FindControl("btnEditar")).Text = "Editar";
                ((LinkButton)row.FindControl("btnExcluir")).Text = "Excluir";

                ((TextBox)row.FindControl("txtNome")).Enabled = false;
                ((TextBox)row.FindControl("txtTelefone")).Enabled = false;
                ((TextBox)row.FindControl("txtEndereco")).Enabled = false;
            }

        }

        private void logErro(string msg)
        {
            lblErro.Text = string.IsNullOrEmpty(msg) ? "Ocorreu um erro Inesperado" : msg;
        }
    }
}