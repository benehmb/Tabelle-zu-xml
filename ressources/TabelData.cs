using System;
using System.Collections.Generic;
using System.Text;

namespace Marvin_Tabelle_zu_xml
{
    class TabelData
    {
        #region public properties
        /// <summary>
        /// Private attribute for name of line
        /// </summary>
        public string lineName;

        /// <summary>
        /// Private attribute for values of line
        /// </summary>
        public List<String> lineValues;
        #endregion

        #region constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public TabelData() {
            lineValues = new List<string>();
        }

        /// <summary>
        /// Constructor with direct Setter for line name
        /// </summary>
        /// <param name="lineName">name of the line</param>
        public TabelData(string lineName)
        {
            this.lineName = lineName;
            lineValues = new List<string>();
        }

        #endregion

    }
}
