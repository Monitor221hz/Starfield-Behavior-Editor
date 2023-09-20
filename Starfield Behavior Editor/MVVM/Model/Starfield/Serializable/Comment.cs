using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BehaviorEditor.MVVM.Model.Starfield
{
	[Serializable]
	[XmlRoot(ElementName = "comment")]
	public class Comment
	{
		[XmlElement(ElementName = "text")]
		public string Text { get; set; } = string.Empty;

		[XmlElement(ElementName = "X")]
		public float X {  get; set; }

		[XmlElement(ElementName = "Y")]
		public float Y { get; set; }


		[XmlElement(ElementName = "TargetX")]
		public float TargetX { get; set; } = 0.0f;

		public bool TargetXSpecified => TargetX != 0.0f;

		[XmlElement(ElementName = "TargetY")]
		public float TargetY { get; set; } = 0.0f;

		public bool TargetYSpecified => TargetX != 0.0f;

		internal Comment() { }

		public Comment(Comment comment)
		{
			Text = comment.Text;
			X = comment.X;
			Y = comment.Y;
			TargetX = comment.TargetX;
			TargetY = comment.TargetY;
		}

	}
}
