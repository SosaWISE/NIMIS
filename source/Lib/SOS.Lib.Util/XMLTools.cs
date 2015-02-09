using System.Data;
using System.Xml;

namespace SOS.Lib.Util
{
	public sealed class XMLTools
	{
		#region Constructors

		public XMLTools()
		{
			System.Diagnostics.Debug.WriteLine(string.Format("Constructed {0}", GetType()));
		}

		#endregion Constructors

		#region Properties

		#region Private

		#endregion Private

		#region Public

		#endregion Public

		#endregion Properties

		#region Methods

		#region Private

		/// <summary>
		/// Given the element to get data from the XML file, the node value, the node text, and the path of the XML file it will
		/// extract the data and return a DataTable.
		/// </summary>
		/// <param name="szElementName">string</param>
		/// <param name="szNodeValue">string</param>
		/// <param name="szNodeText">string</param>
		/// <param name="szXmlPath">string</param>
		/// <returns>DataTable</returns>
		private static DataTable GetDataTableForCombos(string szElementName, string szNodeValue, string szNodeText,
		                                                string szXmlPath)
		{
			// Locals
			DataTable oDt = GetDtForCombosStructure();
			string oTarget = string.Empty, oTargetVal = string.Empty, oTargetTxt = string.Empty;
			bool bTarget = false;
			string szValue = string.Empty;
			string szText = string.Empty;

			// Load XML File and filter what we want.
			var oReader = new XmlTextReader(szXmlPath);
			if (oReader.NameTable != null)
			{
				oTarget = oReader.NameTable.Add(szElementName);
				oTargetVal = oReader.NameTable.Add(szNodeValue);
				oTargetTxt = oReader.NameTable.Add(szNodeText);
			}

			// Loop through library.
			while (oReader.Read())
			{
				// Check what type of element.
				if (oReader.NodeType == XmlNodeType.Element)
				{
					// Check attribute names.
					if (oReader.Name.Equals(oTarget))
					{
						bTarget = true;
						// Get attribute values in element
						if (oReader.HasAttributes)
						{
							if (oReader.MoveToAttribute(szNodeValue))
							{
								szValue = oReader.Value;
								oReader.MoveToElement();
							}
							if (oReader.MoveToAttribute(szNodeText))
							{
								szText = oReader.Value;
								oReader.MoveToElement();
							}
						}
						// Check the values and make sure that there is a value and text.
						if (szValue.Equals("") || szText.Equals("")) szValue = szText = string.Empty;
						if (!szValue.Equals(string.Empty) && (!szText.Equals(string.Empty)))
						{
							DataRow oRow = oDt.NewRow();
							oRow["Value"] = szValue;
							oRow["Text"] = szText;
							oDt.Rows.Add(oRow);
							szValue = szText = string.Empty;
						}
					}
					else if (bTarget)
					{
						// Check for child nodes that match
						if (oReader.Name.Equals(oTargetVal))
						{
							szValue = oReader.ReadString();
							if (szValue.Equals("")) szValue = string.Empty;
						}
						if (oReader.Name.Equals(oTargetTxt))
						{
							szText = oReader.ReadString();
							if (szText.Equals("")) szText = string.Empty;
						}
						if (!szValue.Equals(string.Empty) && !szText.Equals(string.Empty))
						{
							DataRow oRow = oDt.NewRow();
							oRow["Value"] = szValue;
							oRow["Text"] = szText;
							oDt.Rows.Add(oRow);
							szValue = szText = string.Empty;
						}
					}
				}
				if (oReader.NodeType == XmlNodeType.EndElement || oReader.IsEmptyElement)
				{
					if (oReader.Name.Equals(oTarget))
					{
						bTarget = false;
					}
				}
			}

			// Return result
			return oDt;
		}

		private static DataTable GetDtForCombosStructure()
		{
			// Locals 
			var oDt = new DataTable();
			var oCol = new DataColumn("Value", typeof (string));

			//Bind Columns
			oDt.Columns.Add(oCol);
			oCol = new DataColumn("Text", typeof (string));
			oDt.Columns.Add(oCol);

			// Return result
			return oDt;
		}

		#endregion Private

		#region Public

		public static DataTable GetDataTable(string szElementName, string szNodeValue, string szNodeText,
		                                     string szXmlPath)
		{
			return GetDataTableForCombos(szElementName, szNodeValue, szNodeText, szXmlPath);
		}

		#endregion Public

		#endregion Methods
	}
}