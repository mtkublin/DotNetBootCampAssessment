using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void AddFilesBtn_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            string fileContent = string.Empty;

            if (this.addFilesDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = addFilesDialog.FileName;

                string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                this.addedFilesListView.Items.Add(fileName);

                if (filePath.EndsWith(".xml"))
                {
                    DeserializeXmlObject(filePath);
                }
            }
        }

        public class request
        {
            public string clientId { get; set; }
            public int requestId { get; set; }
            public string name { get; set; }
            public int quantity { get; set; }
            public double price { get; set; }
        }

        private void DeserializeXmlObject(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<request>), new XmlRootAttribute("requests"));

            StreamReader reader = new StreamReader(path);

            List<request> productList = (List<request>)serializer.Deserialize(reader);
        }

        //[Serializable()]
        //public class RequestObject
        //{
        //    [XmlElement("clientId")]
        //    public string ClientID { get; set; }

        //    [XmlElement("requestId")]
        //    public int RequestID { get; set; }

        //    [XmlElement("name")]
        //    public string Name { get; set; }

        //    [XmlElement("quantity")]
        //    public int Quantity { get; set; }

        //    [XmlElement("price")]
        //    public double Price { get; set; }
        //}

        //[Serializable()]
        //[XmlRoot("RequestsCollection")]
        //public class RequestsCollection
        //{
        //    [XmlArray("requests")]
        //    [XmlArrayItem("request", typeof(RequestObject))]
        //    public RequestObject[] Request { get; set; }
        //}

        //private void DeserializeXmlObject(string path)
        //{
        //    RequestsCollection reqs = null;

        //    XmlSerializer serializer = new XmlSerializer(typeof(RequestsCollection));

        //    StreamReader reader = new StreamReader(path);
        //    reqs = (RequestsCollection)serializer.Deserialize(reader);
        //    reader.Close();
        //}
    }
}
