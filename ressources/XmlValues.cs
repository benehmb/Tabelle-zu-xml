using System;
using System.Collections.Generic;
using System.Text;

namespace Marvin_Tabelle_zu_xml
{
    class XmlValues
    {
        //TODO Make this class to be fenerated by settings
        #region private properties
        /// <summary>
        /// Divider, between data of file
        /// </summary>
        private static char delimiter = ';';

        /// <summary>
        /// default value if no value is set in table
        /// </summary>
        private static string defaultValue = "n.v.";

        /// <summary>
        /// File, in which output is stored
        /// </summary>
        private static string outputFile = "C:\\Users\\Benedikt\\Nextcloud (Bene)\\InventorTools\\Senkungenoutput.xml";

        /// <summary>
        /// Values to put in XML-file
        /// </summary>
        private static List<string> values = new List<string> { "Favorit", "Bohrungstyp", "Ø", "Ø_x0020_Tol.", "Flach", "Ausführungstyp", "Abstand", "Abst._x0020_Tol.", "Ø_2", "Ø_2_x0020_Tol.", "Tiefe", "Tiefe_x0020_Tol.", "Winkel", "Gewindebez", "Gewindetyp", "Norm", "Klasse", "volle_x0020_Tiefe", "Gewindetiefe" };
        #endregion

        #region public properties

        /// <summary>
        /// Getter of <see cref="delimiter"/>
        /// </summary>
        public static char Delimiter { get => delimiter; }

        /// <summary>
        /// getter of <see cref="defaultValue"/>
        /// </summary>
        public static string DefaultValue { get => defaultValue; }

        /// <summary>
        /// getter of <see cref="outputFile"/>
        /// </summary>
        public static string OutputFile { get => outputFile;  }

        #endregion

        #region public methodes
        #region getter and setter for values-list
        /// <summary>
        /// getter for <see cref="values"/>-list
        /// </summary>
        /// <returns><see cref="values"/>-list</returns>
        public static List<string> getValues()
        {
            return values;
        }

        /// <summary>
        /// Add value to <see cref="values"/>-list, if nessesary
        /// </summary>
        /// <param name="value"></param>
        public static void addValue(string value)
        {
            values.Add(value);
        }
        #endregion
        #endregion
    }
}
