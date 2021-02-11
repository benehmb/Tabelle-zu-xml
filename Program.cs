using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Marvin_Tabelle_zu_xml
{
    class Program
    { 
        #region main
        /// <summary>
        /// main methode
        /// </summary>
        /// <param name="args"></param>
       public static async System.Threading.Tasks.Task Main(string[] args)
        {
            string filepath = getFilePath();

            List<TabelData> tabelDatas = readContent(filepath);

            await generateXmlFileAsync(tabelDatas, XmlValues.getValues(), XmlValues.OutputFile);


            Console.WriteLine("Names and Values: ");
            foreach (TabelData tabeldata in tabelDatas)
            {
                Console.Write("Name: " + tabeldata.lineName + " Values: ");
                tabeldata.lineValues.ForEach(value => Console.Write("{0}\t", value));
                Console.WriteLine();
            }
            // Suspend the screen.  
            System.Console.ReadLine();
        }
        #endregion

        #region helperfunctions
        /// <summary>
        /// get the path tho the File, without '"'
        /// </summary>
        /// <returns>path to csv-file</returns>
        private static string getFilePath()
        {
            // Get path
            Console.Write("Dateipfad: ");
            string filepath = Console.ReadLine();

            // Remove '"', which is added if you directly pull file into script
            return filepath.Replace("\"", string.Empty);
        }

        /// <summary>
        /// return content for csv-file as <see cref="TabelData"/> object
        /// </summary>
        /// <param name="filepath">path to file</param>
        /// <returns><see cref="TabelData"/> object, containing values of file</returns>
        private static List<TabelData> readContent(string filepath)
        {
            // Create empty ist in which all the Data of the File is stored
            List<TabelData> tabelDatas = new List<TabelData>();

            // Create empty property for line
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(filepath);
            while ((line = file.ReadLine()) != null)
            {
                string[] lineContent = line.Split(XmlValues.Delimiter);
                // Do nothing if there is nothing in this line
                if (lineContent[0] != "" || lineContent[0] != " " || lineContent.Length <= 0)
                {
                    // Setting firs sign of line as name of line
                    TabelData row = new TabelData(lineContent[0]);

                    // Remove linename, to not be the first value
                    lineContent = lineContent.Skip(1).ToArray();
                    // Add content
                    foreach (string value in lineContent)
                    {
                        row.lineValues.Add(value);
                    }
                    tabelDatas.Add(row);
                }
            }

            file.Close();
            return tabelDatas;
        }

        /// <summary>
        /// Generates xml-file out of presets
        /// </summary>
        /// <param name="tabelDatas">names and values to generate/replace</param>
        /// <param name="lists">all names</param>
        /// <param name="outputFile">file to store XML</param>
        private static async System.Threading.Tasks.Task generateXmlFileAsync(List<TabelData> tabelDatas, List<string> lists, string outputFile)
        {
            //throw new NotImplementedException();

            // Set some settings
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Async = true
            };

            //create filestream
            using (FileStream fileStream = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                string header = "<?xml version=\"1.0\" standalone=\"yes\"?>";
                fileStream.Write(Encoding.UTF8.GetBytes(header), 0, Encoding.UTF8.GetByteCount(header));
                using (XmlWriter writer = XmlWriter.Create(fileStream, settings))
                {   
                    await writer.WriteStartElementAsync(null, "DocumentElement", null);  
                    //TODO for each row
                    await writer.WriteStartElementAsync(null, "Favoriten", null);
                    //TODO for each element in data
                    writer.WriteAttributeString("xmlns", "x", null, "abc");
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
            }
        }
        #endregion
    }
}
