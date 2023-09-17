using BehaviorEditor.MVVM.Model.Starfield;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Editor_Test
{
	public class IOTests
	{
		private XmlSerializer serializer =
	new XmlSerializer(typeof(RootContainer));
		[Fact]
		public void SerializeTest()
		{

			DirectoryInfo GraphFolder = new DirectoryInfo("C:\\Games\\Starfield Modding\\Unpacked\\Starfield_-_Animations\\meshes\\animtextdata\\tables\\testinggraphs");

			var graphFiles = GraphFolder.GetFiles("*.agx");
			
			foreach(var graphFile in graphFiles)
			{
				RootContainer root;
				using (var stream = graphFile.OpenRead())
				{
					
					using (XmlReader reader = XmlReader.Create(stream))
					{
						root = (RootContainer)serializer.Deserialize(reader)!;
					}
					



				}
				FileInfo outGraphFile = new FileInfo(graphFile.FullName+".txt");
				outGraphFile.Delete();
				using (var stream = outGraphFile.OpenWrite())
				{
					XmlWriterSettings settings = new XmlWriterSettings()
					{
						Indent = true,
						IndentChars = "\t",
						NewLineChars = "\r\n",
						NewLineHandling = NewLineHandling.Replace,
						Encoding = Encoding.UTF8
					};

					using (XmlWriter writer =  XmlWriter.Create(stream, settings)) 
					{
						XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
						ns.Add("", "");

						serializer.Serialize(writer, root,ns);
					}
				}

				XDocument oldDoc = XDocument.Load(graphFile.FullName);
				XDocument newDoc = XDocument.Load(outGraphFile.FullName);

				Assert.True(oldDoc.DescendantNodes().Count() == newDoc.DescendantNodes().Count(), $"{outGraphFile.Name} failed deep equals on export");
			}
		}
	}
}