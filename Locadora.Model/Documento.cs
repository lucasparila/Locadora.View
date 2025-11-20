using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    

    public class Documento
    {

        public static readonly String INSERTDOCUMENTO = "INSERT INTO tblDocumentos" +
                                                        "(ClienteID, TipoDocumento, Numero, DataEmissao, DataValidade)" +
                                                          "VALUES (@ClienteId, @TipoDocumento, @Numero, @DataEmissao, @DataValidade);";

        public static readonly String UPDATEDOCUMENTO = "UPDATE tblDocumentos SET TipoDocumento = @TipoDocumento, " +
                                                       "Numero = @Numero, " +
                                                       "DataEmissao = @DataEmissao, " +
                                                       "DataValidade = @DataValidade " +
                                                       "WHERE ClienteID = @ClienteId;";

        public int DocumentoID { get; private set; }
        public int ClienteID { get; private set; }
        public string TipoDocumento { get; private set; }
        public string Numero {  get; private set; }
        public DateOnly DataEmissao{ get; private set; }
        public DateOnly DataValidade { get; private set; }

        public Documento(string tipoDocumento, string numero, DateOnly dataEmissao, DateOnly dataValidade)
        {
            TipoDocumento = tipoDocumento;
            Numero = numero;
            DataEmissao = dataEmissao;
            DataValidade = dataValidade;
        }
        public void setClienteID(int clienteId)
        {
            this.ClienteID = clienteId;
        }

        public override string ToString()
        {
            return $"Tipo Documento: {this.TipoDocumento}\n" +
                $"Número: {this.Numero}\n" +
                $"Data de Emissão: {this.DataEmissao}\n" +
                $"DataValidade: {this.DataValidade}\n";
        }
    }
}
