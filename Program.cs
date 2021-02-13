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

            List<KeyList> OutputValues = createAndCompareValues(PresetData, Settings.getValues(), Settings.DefaultValues, Settings.DefaultValue) ; ;


            Console.WriteLine("Names and Values: ");
            foreach (KeyList outputValue in OutputValues)
            {
                Console.Write("Name: " + outputValue.keyName + " Values: ");
                outputValue.keyValues.ForEach(value => Console.Write("{0}\t", value));
                Console.WriteLine();
            }

            await generateXmlFileAsync(OutputValues, Settings.OutputFile);


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
        /// <param name="presetData">Ridden Names and Values</param>
        /// <param name="xmlAttributes">Only names to generate values</param>
        /// <param name="defaultValues">Default values for some of the <paramref name="xmlAttributes"/></param>
        /// <param name="fallbackDefaultValue">Default value if there is no value set in <paramref name="presetData"/> and <paramref name="defaultValues"/></param>
        /// <returns>Complete list with names and values</returns>
        private static List<KeyList> createAndCompareValues(List<KeyList> presetData, List<string> xmlAttributes, List<KeyValuePair<string, string>> defaultValues, string fallbackDefaultValue)
        {
            #region initialize
            //set the maximum lenght of each list to the shortes row of the original Table to prevent null-pointer
            // count -1 becaus we want the biggest i#ndex
            int maxLength = presetData[0].keyValues.Count()-1;

            foreach (KeyList preset in presetData)
            {
                if(preset.keyValues.Count()-1 < maxLength)
                {
                    maxLength = preset.keyValues.Count()-1;
                }
            }

            // Create empty output list
            List<KeyList> outputData = new List<KeyList>();
            #endregion
            #region compareism
            // Filling output list
            foreach (string xmlAttribute in xmlAttributes)
            {
                // Setting Name of XML-Attribute
                KeyList outputItem = new KeyList(xmlAttribute);

                //check if there is somthing in the Presets matching
                if(presetData.FindIndex(x => x.keyName.Equals(xmlAttribute)) != -1 && !xmlAttribute.Equals("Favorit") && !xmlAttribute.Equals("M"))
                {
                    #region presets to set
                    // Make a list of things to set...
                    List<string> valuesToSet = presetData.Find(x => x.keyName.Equals(xmlAttribute)).keyValues;
                    // ...and set it for each item
                    for(int i = 0; i <= maxLength; i++)
                    {
                        outputItem.keyValues.Add(valuesToSet[i]);
                    }
                    #endregion
                }
                else if (xmlAttribute.Equals("Favorit"))
                {
                    List<string> valuesToSet = presetData.Find(x => x.keyName.Equals("M")).keyValues;
                    // Checking if there is a specific default value and if so, sets it

                    //setting favorite-alues
                    for (int i = 0; i <= maxLength; i++)
                    {
                        outputItem.keyValues.Add(Settings.FavoritString + " M" + valuesToSet[i]);
                    }
                }
                else if (xmlAttribute.Equals("M"))
                {
                    //do nothing
                }
                else
                {
                    #region defaults to set
                    // Initializing default value
                    string valueToSet = fallbackDefaultValue;
                    // Checking if there is a specific default value and if so, sets it
                    if (defaultValues.FindIndex(x => x.Key.Equals(xmlAttribute)) != -1)
                        valueToSet = defaultValues.Find(x => x.Key.Equals(xmlAttribute)).Value;

                    //setting default-FValues
                    for (int i = 0; i <= maxLength; i++)
                    {
                        outputItem.keyValues.Add(valueToSet);
                    }
                    #endregion
                }
                // Finally adding it for each item to the output-object
                outputData.Add(outputItem);
                
            }

            //check, if there are attributes in the presetData, which arent in the xmlAttributes, but have to be set
            foreach(KeyList presetItem in presetData)
            {
                //finding none existing attribute
                if(xmlAttributes.FindIndex(x => x.Equals(presetItem.keyName)) == -1 && !presetItem.keyName.Equals("Favorit") && !presetItem.keyName.Equals("M"))
                {
                    // Setting Name of XML-Attribute
                    KeyList outputItem = new KeyList(presetItem.keyName);
                    // Set values for key
                    for (int i = 0; i <= maxLength; i++)
                    {
                        outputItem.keyValues.Add(presetItem.keyValues[i]);
                    }
                    outputData.Add(outputItem);
                }
            }
            #endregion

            return outputData;
        }

        /// <summary>
        /// Generates xml-file out of presets
        /// </summary>
        /// <param name="tabelDatas">names and values to generate/replace</param>
        /// <param name="lists">all names</param>
        /// <param name="outputFile">file to store XML</param>
        private static async System.Threading.Tasks.Task generateXmlFileAsync(List<KeyList> outputValues, string outputFile)
        {
            // Set some settings
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Async = true,
            };

            //create filestream
            using (FileStream fileStream = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                //create header
               /*string header = "<?xml version=\"1.0\" standalone=\"yes\"?>";
                fileStream.Write(Encoding.UTF8.GetBytes(header), 0, Encoding.UTF8.GetByteCount(header));*/
                using (XmlWriter writer = XmlWriter.Create(fileStream, settings))
                {   
                    // Write root-element
                    await writer.WriteStartElementAsync(null, "DocumentElement", null);
                    // for each column
                    for (int i = 0; i < outputValues[0].keyValues.Count; i++)
                    {
                        await writer.WriteStartElementAsync(null, "Favoriten", null);

                        //for each row
                        for (int j = 0; j < outputValues.Count; j++)
                        {
                            await writer.WriteStartElementAsync( null, outputValues[j].keyName, null);
                            await writer.WriteStringAsync(outputValues[j].keyValues[i]);
                            await writer.WriteEndElementAsync();
                        }
                        await writer.WriteEndElementAsync();
                    }
                    //TODO for each element in data
                    await writer.WriteEndElementAsync();
                    writer.Close();
                }
                fileStream.Close();
            }
        }
        #endregion
    }
}
