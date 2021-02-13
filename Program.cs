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

            List<KeyList> PresetData = readContent(filepath);

            List<KeyList> OutputValues = createAndCompareValues(PresetData, Settings.getValues()); ;


            Console.WriteLine("Names and Values: ");
            foreach (KeyList pPresetData in PresetData)
            {
                Console.Write("Name: " + pPresetData.keyName + " Values: ");
                pPresetData.keyValues.ForEach(value => Console.Write("{0}\t", value));
                Console.WriteLine();
            }

            await generateXmlFileAsync(PresetData, Settings.getValues(), Settings.OutputFile);


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
        /// return content for csv-file as <see cref="KeyList"/> object
        /// </summary>
        /// <param name="filepath">path to file</param>
        /// <returns><see cref="KeyList"/> object, containing values of file</returns>
        private static List<KeyList> readContent(string filepath)
        {
            // Create empty ist in which all the Data of the File is stored
            List<KeyList> tabelDatas = new List<KeyList>();

            // Create empty property for line
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(filepath);
            while ((line = file.ReadLine()) != null)
            {
                string[] lineContent = line.Split(Settings.Delimiter);
                // Do nothing if there is nothing in this line
                if (lineContent[0] != "" || lineContent[0] != " " || lineContent.Length <= 0)
                {
                    // Setting firs sign of line as name of line
                    KeyList row = new KeyList(lineContent[0]);

                    // Remove linename, to not be the first value
                    lineContent = lineContent.Skip(1).ToArray();
                    // Add content
                    foreach (string value in lineContent)
                    {
                        row.keyValues.Add(value);
                    }
                    tabelDatas.Add(row);
                }
            }

            file.Close();
            return tabelDatas;
        }

        /// <summary>
        /// Make a List of Names and Values to print in xml vie <see cref="createAndCompareValues(List{KeyList}, List{string})"/>
        /// </summary>
        /// <param name="tabelDatas">Read Names and Values</param>
        /// <param name="lists">Only names to generate values</param>
        /// <returns>Complete list with names and values</returns>
        private static List<KeyList> createAndCompareValues(List<KeyList> PresetData, List<string> lists)
        {
            throw new NotImplementedException();
            //todo Set default Values, if nothing else exists.
            //Override Settings.defaultValues if there are Values in PresetData,
            //add new Values if there are some in PresetData
        }

        /// <summary>
        /// Generates xml-file out of presets
        /// </summary>
        /// <param name="tabelDatas">names and values to generate/replace</param>
        /// <param name="lists">all names</param>
        /// <param name="outputFile">file to store XML</param>
        private static async System.Threading.Tasks.Task generateXmlFileAsync(List<KeyList> tabelDatas, List<string> lists, string outputFile)
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
                fileStream.Close();
            }
        }
        #endregion
    }
}
