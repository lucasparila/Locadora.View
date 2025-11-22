namespace Locadora.Models
{
    public class Cliente
    {
        public readonly static string INSERTCLIENTE = "INSERT INTO tblClientes VALUES (@Nome, @Email, @Telefone);" +
                                                      "SELECT SCOPE_IDENTITY();";


        public readonly static string SELECTALLCLIENTES = "SELECT c.Nome, c.Email, c.Telefone, " +
                                                          "d.TipoDocumento, d.Numero, d.DataEmissao, d.DataValidade " +
                                                          "FROM tblClientes c " +
                                                          "join tblDocumentos d " +
                                                          "on c.ClienteID = d.ClienteID;";

        public readonly static string UPDATEFONECLIENTE = "UPDATE tblClientes SET Telefone = @Telefone WHERE ClienteID = @IdCliente";

        public readonly static string SELECTCLIENTEPOREMAIL = "SELECT c.ClienteID, c.Nome, c.Email, c.Telefone, " +
                                                          "d.TipoDocumento, d.Numero, d.DataEmissao, d.DataValidade " +
                                                          "FROM tblClientes c " +
                                                          "join tblDocumentos d " +
                                                          "on c.ClienteID = d.ClienteID " +
                                                           "WHERE c.Email = @Email";


        public readonly static string SELECTCLIENTEPORID = "SELECT c.ClienteID, c.Nome, c.Email, c.Telefone, " +
                                                          "d.TipoDocumento, d.Numero, d.DataEmissao, d.DataValidade " +
                                                          "FROM tblClientes c " +
                                                          "join tblDocumentos d " +
                                                          "on c.ClienteID = d.ClienteID " +
                                                           "WHERE c.ClienteID = @ClienteID";

        public readonly static string DELETECLIENTE = "DELETE FROM tblClientes WHERE ClienteID = @IdCliente";

        public int ClienteID { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string? Telefone { get; private set; } = String.Empty;
        public Documento Documento { get; private set; }

        public Cliente(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public Cliente(string nome, string email, string? telefone) : this(nome, email)
        {
            Telefone = telefone;
        }

        public void setTelefone(string tel)
        {
            this.Telefone = tel;
        }
        public void setClienteID(int id)
        {
            this.ClienteID = id;
        }
        public void setDocumento(Documento documento)
        {
            this.Documento = documento;
        }
        public override string ToString()
        {
            return $"Nome: {this.Nome}\n" +
                $"Email: {this.Email}\n" +
                $"Telefone: {(Telefone == string.Empty ? "Sem telefone" : Telefone)}\n" +
                $"{this.Documento.ToString()}";
        }
    }
}


