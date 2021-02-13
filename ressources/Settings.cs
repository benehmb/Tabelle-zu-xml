﻿using System.Collections.Generic;

namespace Marvin_Tabelle_zu_xml
{
    class Settings
    {
        //TODO Make this class to be fenerated by settings
        #region private properties

        /// <summary>
        /// Start of "Favorit"-String
        /// </summary>
        private static string favoritString = "Senkung Zylinderkopf ISO 4762";

        /// <summary>
        /// Divider, between data of file
        /// </summary>
        private static char delimiter = ';';

        /// <summary>
        /// File, in which output is stored
        /// </summary>
        private static string outputFile = @"Senkungenoutput.xml";

        /// <summary>
        /// fallback default value if no value is set in table if there is no specific
        /// </summary>
        private static string fallbackDefaultValue = "n.v.";

        /// <summary>
        /// Values to put in XML-file
        /// </summary>
        private static List<string> values = new List<string> { "Favorit", "Bohrungstyp", "Ø", "Ø_x0020_Tol.", "Flach", "Ausführungstyp", "Abstand", "Abst._x0020_Tol.", "Ø_2", "Ø_2_x0020_Tol.", "Tiefe", "Tiefe_x0020_Tol.", "Winkel", "Gewindebez", "Gewindetyp", "Norm", "Klasse", "volle_x0020_Tiefe", "Gewindetiefe" };

        /// <summary>
        /// Default Values for specific values in <see cref="values"/>
        /// </summary>
        // Declare the list
        private static List<KeyValuePair<string, string>> defaultValues =
            //initialize the list 
            new List<KeyValuePair<string, string>> {
                //setten Values not by names, but refered to the Objects of <see cref="values"/>
                new KeyValuePair<string, string>("Bohrungstyp", "Zapfen"),
                new KeyValuePair<string, string>("Flach", "ja"),
                new KeyValuePair<string, string>("Ø_x0020_Tol.", "H13(E)"),
                new KeyValuePair<string, string>("Flach", "ja"),
                new KeyValuePair<string, string>("Ausführungstyp", "Durch"),
                new KeyValuePair<string, string>("Abstand", "Durch"),
                new KeyValuePair<string, string>("Ø_2_x0020_Tol.", "H13(E)"),
                new KeyValuePair<string, string>("Tiefe_x0020_Tol.>", "0,01 mm/0 mm(B)")
            };
        #region newer but disfunctional Version of defaultValues
        /*   private static List<KeyValuePair<string, string>> defaultValues =
                //initialize the list 
                new List<KeyValuePair<string, string>> {
                    //setten Values not by names, but refered to the Objects of <see cref="values"/>
                    new KeyValuePair<string, string>(values.Find( x => x.Equals("Bohrrungstyp")), "Zapfen"),
                    new KeyValuePair<string, string>(values.Find( x => x.Equals("Flach")), "ja"),
                    new KeyValuePair<string, string>(values.Find( x => x.Equals("Ø_x0020_Tol.")), "H13(E)"),
                    new KeyValuePair<string, string>(values.Find( x => x.Equals("Flach")), "ja"),
                    new KeyValuePair<string, string>(values.Find( x => x.Equals("Ausführungstyp")), "Durch"),
                    new KeyValuePair<string, string>(values.Find( x => x.Equals("Abstand")), "Durch"),
                    new KeyValuePair<string, string>(values.Find( x => x.Equals("Ø_2_x0020_Tol.")), "H13(E)"),
                    new KeyValuePair<string, string>(values.Find( x => x.Equals("Tiefe_x0020_Tol.>")), "0,01 mm/0 mm(B)")
                };*/
        #endregion
        #endregion

        #region public properties

        /// <summary>
        /// Getter of <see cref="delimiter"/>
        /// </summary>
        public static char Delimiter { get => delimiter; }

        /// <summary>
        /// getter of <see cref="fallbackDefaultValue"/>
        /// </summary>
        public static string DefaultValue { get => fallbackDefaultValue; }

        /// <summary>
        /// getter and setter of <see cref="outputFile"/>
        /// </summary>
        public static string OutputFile { get => outputFile; set => outputFile = value; }

        /// <summary>
        /// getter for <see cref="defaultValues"/>-list
        /// </summary>
        public static List<KeyValuePair<string, string>> DefaultValues { get => defaultValues; }

        /// <summary>
        /// getter for <see cref="favoritString"/>
        /// </summary>
        public static string FavoritString { get => favoritString; }

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
