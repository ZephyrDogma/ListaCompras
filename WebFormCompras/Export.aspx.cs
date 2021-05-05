using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebFormCompras
{
    public partial class Ws : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }



        protected void BtnExportXML(object sender, EventArgs e)
        {
            string pasta = Server.MapPath("~/Upload/");
            string ficheiro = pasta + "ExportDados.xml";
            

            BDtoXML(ficheiro);

            

            
        }

        private void BDtoCSV(string ficheiro2)
        {
            //using (aDBComprasEntities2 dc = new aDBComprasEntities2())
            //{
            //    List<Roupa> exList = dc.Roupa.ToList();
            //    if (exList.Count > 0)
            //    {
            //var xEle = new XElement("Roupas",
            //    from exL in exList
            //    select new XElement("Roupa",
            //    new XElement("Id", exL.Id),
            //    new XElement("Marca", exL.Marca),
            //    new XElement("Tipo", exL.Tipo),
            //    new XElement("Tamanho", exL.Tamanho),
            //    new XElement("Preco", exL.Preco)
            //    ));

            //xEle.Save(ficheiro2);
            //tentativa 2
            //string csv = String.Join(";", exList.Select(x => x.ToString()).ToArray());

            //Console.WriteLine(csv);

            //File.WriteAllText(ficheiro2, csv);

            //tentativa 3
            //var engine = new FileHelperEngine<Roupa>();
            //engine.HeaderText = engine.GetFileHeader();
            //string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + ConfigurationManager.AppSettings["MyPath"];
            //if (!Directory.Exists(dirPath))
            //{
            //    Directory.CreateDirectory(dirPath);
            //}

            //File location, where the .csv goes and gets stored.
            //string filePath = Path.Combine(dirPath, "MyTestFile_" + ".csv");
            //engine.WriteFile(filePath, testobjs);

            //tentativa 4

            //}
            //resultado.Text = "CSV da Base de Dados exportado";
            //}
            string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            StringBuilder sb = new StringBuilder();
            SqlConnection con = new SqlConnection(cs);
            var cmd = new SqlCommand("SELECT [Marca], [Tipo], [Tamanho], [Preco] FROM [Roupa] ORDER BY [Id];", con);
            using (con)
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ds.Tables[0].TableName = "Roupas";

                foreach (DataRow drow in ds.Tables["Roupas"].Rows)
                {
                    sb.Append(drow["Marca"].ToString() + ";");
                    sb.Append(drow["Tipo"].ToString() + ";");
                    sb.Append(drow["Tamanho"].ToString() + ";");
                    sb.Append(drow["Preco"].ToString());
                    sb.Append("\r\n");
                }
            }

            StreamWriter file = new StreamWriter(ficheiro2);
            file.WriteLine(sb.ToString());
            file.Close();

            resultado.Text = "CSV da Base de Dados exportado";

        }

        private void BDtoXML(string ficheiro)
        {
            using (aDBComprasEntities2 dc = new aDBComprasEntities2())
            {
                List<Roupa> exList = dc.Roupa.ToList();
                if (exList.Count > 0)
                {
                    var xEle = new XElement("Roupas",
                        from exL in exList
                        select new XElement("Roupa",
                        new XElement("Id", exL.Id),
                        new XElement("Marca", exL.Marca),
                        new XElement("Tipo", exL.Tipo),
                        new XElement("Tamanho", exL.Tamanho),
                        new XElement("Preco", exL.Preco)
                        ));

                    xEle.Save(ficheiro);
                }
                resultado.Text = "XML da Base de Dados exportado";

            }
        }

        protected void BtnExportCSV(object sender, EventArgs e)
        {
            string pasta = Server.MapPath("~/Upload/");
            string ficheiro2 = pasta + "ExportDados.csv";
            BDtoCSV(ficheiro2);
        }
        //HttpContext contexto = HttpContext.Current;
        //Context.Response.Write(xEle);
        //Context.Response.ContentType = "application/xml";
        //Context.Response.AppendHeader("Content-Disposition", "attachment; filename=ConteudoTabela.xml");
        //Context.Response.End();
    }
}