using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace WebFormCompras
{
    public partial class ImportCSV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            string pasta = Server.MapPath("~/Upload/");
            string ficheiro = pasta + Path.GetFileName(FileUpload1.FileName);
            FileUpload1.SaveAs(ficheiro);
            Label1.Text = "Ficheiro "
                + Path.GetFileName(FileUpload1.FileName) + " importado";
            // converter CSV em DataTable
            DataTable dt = ImportarCSVtoDT(ficheiro);
            // converter em XML
            string fich2 = Path.GetDirectoryName(ficheiro) + "\\"
                + Path.GetFileNameWithoutExtension(ficheiro) + "(2)"
                + ".xml";
            DTtoXML(dt, fich2);
        }

        private void DTtoXML(DataTable dt, string fich2)
        {
            //Metodo 1
            DataSet ds = new DataSet("Roupas");
            dt.TableName = "Roupa";
            ds.Tables.Add(dt);

            using (StreamWriter fs = new StreamWriter(fich2))
                ds.WriteXml(fs);

            //Metodo 2
            fich2 = Path.GetDirectoryName(fich2) + "\\"
                + Path.GetFileNameWithoutExtension(fich2) + "_m2"
                + ".xml";
            using (TextWriter writer = File.CreateText(fich2))
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, true);

            //Metodo 3 - Serialize
            fich2 = Path.GetDirectoryName(fich2) + "\\"
                + Path.GetFileNameWithoutExtension(fich2) + "_m3"
                + ".xml";

            XmlSerializer serializer = new XmlSerializer(typeof(Roupas));
            Roupas obj1 = new Roupas
            {
                id = 1,
                Marca = "Modalfa",
                Tipo = "Mala",
                Tamanho = "Normal",
                Preco = "100"
                
            };
            using (Stream stream = new FileStream(fich2, FileMode.Create))
                serializer.Serialize(stream, obj1);

            //Metodo 3b - Deserialize

            using (Stream stream = new FileStream(fich2, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Roupas obj2 = (Roupas)serializer.Deserialize(stream);
                resultado.Text = "À espera de Importação : meu objeto é: " + obj2.id + " " + obj2.Marca + " " + obj2.Tipo +
                    " " + obj2.Tamanho + " " + obj2.Preco;
            }
        }

        private DataTable ImportarCSVtoDT(string ficheiro)
        {
            DataTable dt = new DataTable();

            using (StreamReader sr = new StreamReader(ficheiro))
            {
                string[] colunas = sr.ReadLine().Split(';');
                foreach (string coluna in colunas)
                {
                    dt.Columns.Add(coluna);
                }
                while (!sr.EndOfStream)
                {
                    string[] celulas = sr.ReadLine().Split(';');
                    DataRow linha = dt.NewRow();
                    for (int i = 0; i < colunas.Length; i++)
                    {
                        linha[i] = celulas[i];
                    }
                    dt.Rows.Add(linha);
                }
            }

            return dt;
        }
    }

    public class Roupas
    {
        public int id;
        public string Marca;
        public string Tipo;
        public string Tamanho;
        public string Preco;
    }
}