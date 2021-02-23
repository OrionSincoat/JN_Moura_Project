using System;

public class clsAgenda
{
    private List<clsAgeItem> item;

    public List<clsAgeItem> Item { get => item; set => item = value; }

    public clsAgenda() => Item = new List<clsAgeItem>;


}

public class clsAgeItem
{
    private int id;
    private string nome;
    private int telefone;
    private string endereco;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }
    public int Telefone
    {
        get { return telefone; }
        set { telefone = value; }
    }
    public string Endereco
    {
        get { return endereco; }
        set { endereco = value; }
    }

    public string getString ()
    {
        return String.Format("Nome: {0}, Telefone: {1}, Endereço: {2}",Nome, Telefone, Endereco);
    }

    public clsAgeItem()
    {

    }
}
