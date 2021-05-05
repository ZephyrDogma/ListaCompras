using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace WebFormCompras
{
    public partial class Import : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void btnImport_Click(object sender, EventArgs e)
        {
            string pasta = Server.MapPath("~/Upload/");
            //
            string ficheiro = pasta + Path.GetFileName(FileUpload1.FileName);
            FileUpload1.SaveAs(ficheiro);
            //
            if (XSDValidateXML(ficheiro) || DTDValidateXML(ficheiro))
            {
                DataSet ds = XmltoDS(ficheiro);

                string ficheiro2 = Path.GetDirectoryName(ficheiro) + "\\"
                    + Path.GetFileNameWithoutExtension(ficheiro) + "(1)"
                    + ".csv";

                //DTtoXML(dt, ficheiro2);

                DStoCSV(ds, ficheiro2);

                DStoDB(ds);
                resultado.Text = Path.GetFileName(FileUpload1.FileName) + " Importado!";
            }

        }

        private bool DTDValidateXML(string ficheiro)
        {
            Boolean retorno = false;
            // Ler o XML
            XmlReader xmlReader = null;
            try
            {
                StringBuilder mensagem = new StringBuilder();
                XmlReaderSettings settings = new XmlReaderSettings();
                // o ficheiro dtd                
                settings.ValidationType = ValidationType.DTD;
                settings.DtdProcessing = DtdProcessing.Parse;
                settings.ValidationEventHandler += (sender, args) => mensagem.AppendLine(args.Message);
                // Valida com os settings
                xmlReader = XmlReader.Create(ficheiro, settings);

                // iterar o XML
                while (xmlReader.Read()) { }
                retorno = true;
            }
            catch (Exception ex)
            {
                resultado.Text = "Validation Failed. Error: " + ex.Message;
                retorno = false;
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
            }
            //if (retorno)
            //{
            //    resultado.Text = "Validação com sucesso.";
            //}
            return retorno;
        }

        private bool XSDValidateXML(string ficheiro)
        {
            Boolean retorno = false;
            // Ler o XML
            XmlReader xmlReader = null;
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                // o ficheiro xsd
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                settings.ValidationEventHandler += new ValidationEventHandler(this.ValidationEventHandle);
                // Valida com os settings
                xmlReader = XmlReader.Create(ficheiro, settings);

                // iterar o XML
                while (xmlReader.Read()) { }
                retorno = true;
            } catch (Exception ex)
            {
                resultado.Text = "Validation Failed. Error: " + ex.Message;
                retorno = false;
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }                
            }
            return retorno;
        }

        private void ValidationEventHandle(object sender, ValidationEventArgs e)
        {
            // só vem cá se algo de errado estiver com o XML
            Console.WriteLine("\r\n\tValidation Error: " + e.Message);
            resultado.Text = "Validação Falhou. Erro: " + e.Message;
            // a tal exceção
            throw new Exception("Validação Falhou. Erro: " + e.Message);
        }

        private void DStoDB(DataSet ds)
        {
            try
            {
                foreach (DataTable dt in ds.Tables)
                {

                    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    string sql = "INSERT INTO Roupa(Marca,Tipo,Tamanho,Preco) VALUES(@marca,@tipo,@tamanho,@preco);";
                    using (SqlConnection connection = new SqlConnection(conn))
                    {
                        connection.Open();
                        foreach (DataRow dr in dt.Rows)
                        {
                            var cmd = new SqlCommand(sql, connection);
                            cmd.Parameters.Add("@marca", SqlDbType.NChar).Value = dr[0];
                            cmd.Parameters.Add("@tipo", SqlDbType.NChar).Value = dr[1];
                            cmd.Parameters.Add("@tamanho", SqlDbType.NChar).Value = dr[2];
                            cmd.Parameters.Add("@preco", SqlDbType.NChar).Value = dr[3];

                            cmd.ExecuteNonQuery();
                        }
                    }
                    GridView1.DataBind();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void DTtoXML(DataTable dt, string fich3)
        {
            //throw new NotImplementedException();

            //Metodo 2
            fich3 = Path.GetDirectoryName(fich3) + "\\"
                + Path.GetFileNameWithoutExtension(fich3) + "_m2"
                + ".xml";
            using (TextWriter writer = File.CreateText(fich3))
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, true);

        }

        private DataSet XmltoDS(string ficheiro)
        {
            DataSet ds = new DataSet("Dados");
            ds.ReadXml(ficheiro);

           
            return ds;
        }

        private void DStoCSV(DataSet ds, string ficheiro2)
        {
            try
            {
                foreach (DataTable DDT in ds.Tables)
                {

                    using (var outputFile = File.CreateText(ficheiro2))
                    {
                        String CsvText = string.Empty;

                        foreach (DataColumn DC in DDT.Columns)
                        {
                            if (CsvText != "")
                                CsvText = CsvText + ";" + DC.ColumnName.ToString();
                            else
                                CsvText = DC.ColumnName.ToString();
                        }
                        outputFile.WriteLine(CsvText.ToString().TrimEnd(';'));
                        CsvText = string.Empty;

                        foreach (DataRow DDR in DDT.Rows)
                        {
                            foreach (DataColumn DCC in DDT.Columns)
                            {
                                if (CsvText != "")
                                    CsvText = CsvText + ";" + DDR[DCC.ColumnName.ToString()].ToString();
                                else
                                    CsvText = DDR[DCC.ColumnName.ToString()].ToString();
                            }
                            outputFile.WriteLine(CsvText.ToString().TrimEnd(';'));
                            CsvText = string.Empty;
                        }
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }

}