using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Songs
{
    class JSTableFile
    {
        List<JSTable> _Tables = new List<JSTable>();

        public void Add(JSTable table)
        {
            _Tables.Add(table); 
        }

        public void WriteFile()
        {
            try
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.InitialDirectory = @"D:\Dietrich\SoftwareDevelopment\GitHubRepository\NetlifyWebsite"; // TODO should configure this
                sd.FileName = "Tables";
                sd.DefaultExt = "js";

                if (sd.ShowDialog() == DialogResult.OK)
                {
                    Stream stream;
                    if ((stream = sd.OpenFile()) != null)
                    {
                        StreamWriter writer = new StreamWriter(stream);

                        foreach (JSTable jsTable in _Tables)
                            jsTable.WriteToFile(writer);

                        writer.Close();

                        stream.Close();
                    }
                }
                MessageBox.Show(sd.FileName + " module has been written.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("WriteJSTableFile failed:" + ex.Message);
            }
        }
    }
}
