using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace BehaviorEditor.MVVM.Model.Starfield.Properties
{
	[Serializable]
    [XmlRoot(ElementName = "property_sheet")]
    public class PropertySheet
    {
        [XmlElement(ElementName = "num_columns", Order =1)]
        public int NumColumns { get; set; } = 0;


        [XmlElement(ElementName ="column", Order =2)]
        public List<Column> Columns = new List<Column>();


        [XmlElement(ElementName ="row", Order =3)]
        public List<Row> Rows = new List<Row>();


        internal PropertySheet() { }

        public bool FindLocalPropertyByValue(string value, out Property? outProperty)
        {
            foreach(Row row in Rows)
            {
                if (row.FindPropertyByValue(value, out outProperty)) return true;
            }
            outProperty = null;
            return false;
        }
        
    }
}
