using BehaviorEditor.Extern;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace BehaviorEditor.MVVM.Model.Starfield
{
	public class BehaviorDataProvider
	{
		public BehaviorDataProvider() { }

		XmlSerializer serializer =
new XmlSerializer(typeof(RootContainer));


		public RootContainer LoadFile()
		{
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.DefaultExt = ".agx";
			Nullable<bool> result = dlg.ShowDialog();
			RootContainer root = new RootContainer();
			if (result == true)
			{
				
				// Open document 
				string filename = dlg.FileName;
				using (XmlReader reader = XmlReader.Create(filename))
				{
					root = (RootContainer)serializer.Deserialize(reader)!;
				}
			}
			return root;
		}

		public void SaveFile(RootContainer root)
		{
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
			saveFileDialog1.Filter = "Behavior File|*.agx";
			saveFileDialog1.Title = "Save The Behavior File";
			saveFileDialog1.ShowDialog();
			if (string.IsNullOrWhiteSpace(saveFileDialog1.FileName)) return;
			XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, IndentChars = "\t", 
				NewLineChars = "\r\n", NewLineHandling = NewLineHandling.Replace, Encoding = Encoding.UTF8, OmitXmlDeclaration=true };

			using (XmlWriter writer = XmlWriter.Create(saveFileDialog1.FileName, settings))
			{
				XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
				ns.Add("", "");
				serializer.Serialize(new VerboseXmlWriter(writer), root!, ns);
			}
		}
	}
}
