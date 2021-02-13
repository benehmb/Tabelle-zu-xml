using System;
using System.Collections.Generic;
using System.Text;

namespace Marvin_Tabelle_zu_xml
{
    /// <summary>
    /// Class to be used for Key lists with multiple values, or objects
    /// </summary>
    class KeyList
    {
        #region public properties
        /// <summary>
        /// Private attribute for name of line
        /// </summary>
        public string keyName;

        /// <summary>
        /// Private attribute for values of line
        /// </summary>
        public List<String> keyValues;
        #endregion

        #region constructor
        /// <summary>
        /// Constructor with direct Setter for line name
        /// </summary>
        /// <param name="keyName">name of the line</param>
        public KeyList(string keyName)
        {
            this.keyName = keyName;
            keyValues = new List<string>();
        }

        #endregion

    }
}
