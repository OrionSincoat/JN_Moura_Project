<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <h2>Agenda Telefônica</h2>
                    <p>
                        Nome:
                        <asp:TextBox runat="server" ID="txtNome"></asp:TextBox>
                        <asp:RequiredFieldValidator id="rfvTxtNome" ControlToValidate="txtNome"
                            Display="Static" ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="NovoRegistro"/>
                    </p>
                    <p>
                        Telefone:
                        <asp:TextBox runat="server" ID="txtTelefone"></asp:TextBox>
                        <asp:RequiredFieldValidator id="rfvTxtTelefone" ControlToValidate="txtTelefone"
                            Display="Static" ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="NovoRegistro"/>
                    </p>
                    <p>
                        Endereço:
                        <asp:TextBox runat="server" ID="txtEndereco"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator1" ControlToValidate="txtTelefone"
                            Display="Static" ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="NovoRegistro"/>
                    </p>
                    <p>
                        <asp:Button runat="server" ID="btnCriar" CssClass="btn btn-success" Text="Novo Registro" OnClick="btnCriar_Click" 
                            ValidationGroup="NovoRegistro" CausesValidation="true"/>
                        <asp:Label runat="server" ID="lblErro" ForeColor="Red"></asp:Label>
                    </p>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView runat="server" ID="gvAgenda" AutoGenerateColumns="false" CssClass="table table-condensed table-hover" 
                        OnRowCommand="gvAgenda_RowCommand" >
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-CssClass="col-md-1" />
                            <asp:TemplateField HeaderText ="Nome">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtNome" Text='<%# Eval("nome") %>' 
                                        Enabled="false" CssClass="col-md-12"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText ="Telefone">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtTelefone" Text='<%# Eval("telefone") %>' 
                                        Enabled="false" CssClass="col-md-12" placeholder="Não encontrado ou Incorreto"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText ="Endereço">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtEndereco" Text='<%# Eval("endereco") %>' 
                                        Enabled="false" CssClass="col-md-12"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btnEditar" Text="Editar" CssClass="btn btn-success"  CommandName="Editar" />
                                    <asp:LinkButton runat="server" ID="btnExcluir" Text="Excluir" CssClass="btn btn-danger" CommandName="Deletar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
