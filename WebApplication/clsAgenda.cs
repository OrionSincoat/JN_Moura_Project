using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebApplication
{
    public class clsAgenda
    {
        private List<clsAgeItem> itens;

        public List<clsAgeItem> Itens { get => itens; }

        public clsAgenda()
        {
            itens = new List<clsAgeItem>();
        }

        public void newItem(string nome, long telefone, string endereco)
        {
            //sql: INSERT INTO AgendaTable VALUES (nome, telefone, endereco ) --caso id esteja auto incrementando
            itens.Add(new clsAgeItem(itens.Count +1,nome, telefone, endereco));
        }

        public void deleteItem(int id)
        {
            //sql: DELETE FROM AgendaTable WHERE ID = id
            itens.Remove(itens.Find(x => x.Id == id));
        }
        
        public void updateItem(int id, string nome, long? telefone, string endereco)
        {
            //sql: UPDATE AgendaTable SET Nome = nome, Telefone = telefone, Endereco = Endereco WHERE ID = id
            //não é eficiente, mas ainda não tinha usado o foreach
            foreach (var item in this.itens)
            {
                if (item.Id == id)
                {
                    item.Nome = nome;
                    item.Telefone = item.validarParamTelefone(telefone ?? 0);
                    item.Endereco = endereco;

                    continue;
                }
            }

        }
    }

    public class clsAgeItem
    {
        #region _Props_
        private int id;
        private string nome;
        private long? telefone;
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
        public long? Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }
        public string Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }
        #endregion

        public string getString()
        {
            return String.Format("Nome: {0}, Telefone: {1}, Endereço: {2}", Nome, Telefone, Endereco);
        }

        public clsAgeItem()
        {
            this.id = 0;
            this.nome = string.Empty;
            this.telefone = null;
            this.endereco = string.Empty;
        }

        public clsAgeItem(int id, string nome, long telefone, string endereco)
        {
            this.id = id;
            this.nome = nome;
            this.telefone = validarParamTelefone(telefone);
            this.endereco = endereco;
        }

        public long? validarParamTelefone(long telefone)
        {
            Regex rx = new Regex(@"^(?<ddd>\d{2})(?<tel>\d{8,9})$");
            MatchCollection match = rx.Matches(telefone.ToString());

            if (match.Count > 0)
            {
                GroupCollection group = match[0].Groups;
                string ddd = group["ddd"].Value;
                string tel = group["tel"].Value;

                bool val;
                //verifica ddds Brasil
                #region _Lista DDDs_
                /*
                 * lista de ddds do brasil
                 * 
                 * Se fosse focar em performance, seria um enum, fiz assim para só para usar o IndexOf
                 * EX: var ddd = (enumDDD)System.Enum.Parse(typeof(enumDDD), "16");
                 */
                const string dddBrasil = @"
                Centro-Oeste
                – Distrito Federal (61)
                – Goiás (62 e 64)
                – Mato Grosso (65 e 66)
                – Mato Grosso do Sul (67)

                Nordeste
                – Alagoas (82)
                – Bahia (71, 73, 74, 75 e 77)
                – Ceará (85 e 88)
                – Maranhão (98 e 99)
                – Paraíba (83)
                – Pernambuco (81 e 87)
                – Piauí (86 e 89)
                – Rio Grande do Norte (84)
                – Sergipe (79)

                Norte
                – Acre (68)
                – Amapá (96)
                – Amazonas (92 e 97)
                – Pará (91, 93 e 94)
                – Rondônia (69)
                – Roraima (95)
                – Tocantins (63)

                Sudeste
                – Espírito Santo (27 e 28)
                – Minas Gerais (31, 32, 33, 34, 35, 37 e 38)
                – Rio de Janeiro (21, 22 e 24)
                – São Paulo (11, 12, 13, 14, 15, 16, 17, 18 e 19)

                Sul
                – Paraná (41, 42, 43, 44, 45 e 46)
                – Rio Grande do Sul (51, 53, 54 e 55)
                – Santa Catarina (47, 48 e 49)
                ";
                #endregion

                val = dddBrasil.IndexOf(ddd) >= 0;
                //validação simples de telefone celular
                val &= tel.Length == 8 ? true : long.Parse(tel) >= 800000000;

                if (val)
                    return telefone;
            }
            return null;
        }
    }
}