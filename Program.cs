using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            Settings settings = new Settings();
            settings.readSettingsFromFile();
            #region Get input-parameter
            string OutputFilePath="";
            string InputFilePath = "";
            if (args.Length != 0)
            {
                for (int i = 0; i < args.Length; i+=2)
                {
                    if (args[i].Equals("-o"))
                    {
                        OutputFilePath = args[i + 1];
                    }
                    else if (args[i].Equals("-f"))
                    {
                        InputFilePath = args[i + 1];
                    }
                    else
                    {
                        throw new ArgumentException("Unknown Argument: " + args[i]);
                    }
                }
            }
            if (String.IsNullOrEmpty(InputFilePath))
                InputFilePath = getFilePath("Pfad zur CSV-datei: ");

            if(String.IsNullOrEmpty(OutputFilePath))
                OutputFilePath = getFilePath("Pfad zur Ausgabe-Date: ");
            #endregion

            #region Check input-parameter
            if (!InputFilePath.ToLower().EndsWith(".csv") && (File.GetAttributes(OutputFilePath) & FileAttributes.Directory) != FileAttributes.Directory)
                throw new ArgumentException("Der angegebene Pfad ist nicht gültig!");

            if (OutputFilePath.EndsWith("\\"))
                OutputFilePath += Settings.OutputFile;
            else if((File.GetAttributes(OutputFilePath) & FileAttributes.Directory) == FileAttributes.Directory)
                OutputFilePath += "\\" + Settings.OutputFile;
            else if (!OutputFilePath.ToLower().EndsWith(".xml"))
                throw new ArgumentException("Der angegebene Pfad ist nicht gültig!");

            // Set output-file-path
            Settings.OutputFile = OutputFilePath;
            #endregion

            // Get given data
            List<KeyList> PresetData = readContent(InputFilePath);

            // Prepare given data and Settings for output
            List<KeyList> OutputValues = createAndCompareValues(PresetData, Settings.getValues(), Settings.DefaultValues, Settings.DefaultValue); ;

            Console.WriteLine("Names and Values: ");
            foreach (KeyList outputValue in OutputValues)
            {
                Console.Write("Name: " + outputValue.keyName + " Values: ");
                outputValue.keyValues.ForEach(value => Console.Write("{0}\t", value));
                Console.WriteLine();
            }

            // Write anything in XML-file
            await generateXmlFileAsync(OutputValues, Settings.OutputFile);


            // Suspend the screen.  
            System.Console.ReadLine();
        }

        #endregion

        #region helperfunctions
        /// <summary>
        /// get the path tho the File, without '"'
        /// </summary>
        /// <param name="output">String to be printed</param>
        /// <returns>path to csv-file</returns>
        private static string getFilePath(string output)
        {
            // Get path
            Console.Write(output);
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
            int maxLength = presetData[0].keyValues.Count() - 1;

            foreach (KeyList preset in presetData)
            {
                if (preset.keyValues.Count() - 1 < maxLength)
                {
                    maxLength = preset.keyValues.Count() - 1;
                }
            }

            // Create empty output list
            List<KeyList> outputData = new List<KeyList>();
            #endregion
            #region compareism
            //Handling special cases ("Favorit" and "M")
            specialCaseHandling(outputData, presetData, xmlAttributes, maxLength);

            // Filling output list
            foreach (string xmlAttribute in xmlAttributes)
            {
                // Setting Name of XML-Attribute
                KeyList outputItem = new KeyList(xmlAttribute);

                //check if there is somthing in the Presets matching
                if (presetData.FindIndex(x => x.keyName.Equals(xmlAttribute)) != -1)
                {
                    #region presets to set
                    // Make a list of things to set...
                    List<string> valuesToSet = presetData.Find(x => x.keyName.Equals(xmlAttribute)).keyValues;
                    // ...and set it for each item
                    for (int i = 0; i <= maxLength; i++)
                    {
                        outputItem.keyValues.Add(valuesToSet[i]);
                    }
                    #endregion
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
            foreach (KeyList presetItem in presetData)
            {
                //finding none existing attribute
                if (xmlAttributes.FindIndex(x => x.Equals(presetItem.keyName)) == -1)
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
        /// Handle special cases ("Favoriten" and "M") and remove them from Lists
        /// </summary>
        /// <param name="outputData">Object, in which anything that shoult be written in the XML-file is stored</param>
        /// <param name="presetData">Ridden Names and Values</param>
        /// <param name="xmlAttributes">Only names to generate values</param>
        /// <param name="maxLength">Maximum length of any values-lits in <paramref name="outputData"/></param>
        private static void specialCaseHandling(List<KeyList> outputData, List<KeyList> presetData, List<string> xmlAttributes, int maxLength)
        {

            // Check Parameter and remove special cases
            if (xmlAttributes.FindIndex(x => x.Equals("Favorit")) != -1)
                xmlAttributes.RemoveAt(xmlAttributes.FindIndex(x => x.Equals("Favorit")));

            if (presetData.FindIndex(x => x.keyName.Equals("Favorit")) != -1)
                presetData.RemoveAt(presetData.FindIndex(x => x.keyName.Equals("Favorit")));

            if (xmlAttributes.FindIndex(x => x.Equals("M")) != -1)
                xmlAttributes.RemoveAt(xmlAttributes.FindIndex(x => x.Equals("M")));

            if (presetData.FindIndex(x => x.keyName.Equals("M")) == -1)
                throw new ArgumentException("Die CSV-Date enthält keine 'M'-Spalte");

            // Exclude special case for "Favorit" and "M"
            KeyList outputItem = new KeyList("Favorit");
            List<string> valuesToSet = presetData.Find(x => x.keyName.Equals("M")).keyValues;
            // Checking if there is a specific default value and if so, sets it

            // Setting favorite-values
            for (int i = 0; i <= maxLength; i++)
            {
                outputItem.keyValues.Add(Settings.FavoritString + " M" + valuesToSet[i]);
            }
            outputData.Add(outputItem);
            
            // Remove "M"-Column to avoit being in output
            presetData.RemoveAt(presetData.FindIndex(x => x.keyName.Equals("M"))); //TODO Wieso verursacht das einen Fehler?
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

                //clean content before writing
                fileStream.SetLength(0);
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
                            await writer.WriteStartElementAsync(null, outputValues[j].keyName, null);
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
