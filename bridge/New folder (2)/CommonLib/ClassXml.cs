using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace CommonLib;

public class ClassXml
{
	private string pathXml;

	private byte[] headerBytes;

	private static ClassXml instance;

	public string PathXml => pathXml;

	public static ClassXml Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new ClassXml(Environment.CurrentDirectory + "\\test.xml", "root");
			}
			return instance;
		}
	}

	public ClassXml(string path, string root)
	{
		pathXml = path;
		headerBytes = Encoding.Default.GetBytes("<?xml version=\"1.0\" encoding=\"UTF-8\" ?><" + root + "></" + root + ">");
	}

	public Dictionary<string, structxml> GetElements(string pathNodes, int level)
	{
		Dictionary<string, structxml> dictionary = new Dictionary<string, structxml>();
		XmlDocument xmlDocument = new XmlDocument();
		if (File.Exists(pathXml))
		{
			XmlNode xmlNode;
			try
			{
				xmlDocument.Load(pathXml);
				xmlNode = xmlDocument.SelectSingleNode(pathNodes);
			}
			catch
			{
				return dictionary;
			}
			{
				foreach (XmlNode childNode in xmlNode.ChildNodes)
				{
					dictionary.Add(childNode.Name, new structxml(level, childNode.Value, (childNode.Attributes != null && childNode.Attributes.Count > 0) ? childNode.Attributes[0].Value : "", childNode.InnerText, childNode.InnerXml, GetElements(childNode, level + 1)));
				}
				return dictionary;
			}
		}
		return dictionary;
	}

	public XmlNodeList GetElements(string pathNodes)
	{
		XmlNodeList result = null;
		XmlDocument xmlDocument = new XmlDocument();
		if (File.Exists(pathXml))
		{
			XmlNode xmlNode;
			try
			{
				xmlDocument.Load(pathXml);
				xmlNode = xmlDocument.SelectSingleNode(pathNodes);
			}
			catch
			{
				return result;
			}
			if (xmlNode != null)
			{
				result = xmlNode.ChildNodes;
			}
			return result;
		}
		return result;
	}

	public Dictionary<string, structxml> GetElements()
	{
		Dictionary<string, structxml> dictionary = new Dictionary<string, structxml>();
		XmlDocument xmlDocument = new XmlDocument();
		if (File.Exists(pathXml))
		{
			try
			{
				xmlDocument.Load(pathXml);
			}
			catch
			{
				return dictionary;
			}
			{
				foreach (XmlNode childNode in xmlDocument.ChildNodes)
				{
					dictionary.Add(childNode.Name, new structxml(1, childNode.Value, (childNode.Attributes != null && childNode.Attributes.Count > 0) ? childNode.Attributes[0].Value : "", childNode.InnerText, childNode.InnerXml, GetElements(childNode, 2)));
				}
				return dictionary;
			}
		}
		return dictionary;
	}

	private Dictionary<string, structxml> GetElements(XmlNode xn, int level)
	{
		Dictionary<string, structxml> dictionary = new Dictionary<string, structxml>();
		foreach (XmlNode childNode in xn.ChildNodes)
		{
			dictionary.Add(childNode.Name, new structxml(level, childNode.Value, (childNode.Attributes != null && childNode.Attributes.Count > 0) ? childNode.Attributes[0].Value : "", childNode.InnerText, childNode.InnerXml, GetElements(childNode, level + 1)));
		}
		return dictionary;
	}

	public bool SaveElement(string keypath, string innertext)
	{
		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			if (!File.Exists(pathXml))
			{
				FileStream fileStream = new FileStream(pathXml, FileMode.Create);
				fileStream.Write(headerBytes, 0, headerBytes.Length);
				fileStream.Close();
			}
			xmlDocument.Load(pathXml);
			XmlNode xmlNode = xmlDocument.SelectSingleNode(keypath);
			if (xmlNode == null)
			{
				CreatPath(keypath);
				xmlDocument.Load(pathXml);
				xmlNode = xmlDocument.SelectSingleNode(keypath);
				if (xmlNode == null)
				{
					return false;
				}
			}
			xmlNode.InnerText = innertext;
			xmlDocument.Save(pathXml);
			return true;
		}
		catch
		{
			return false;
		}
	}

	private void CreatPath(string keypath)
	{
		string[] array = keypath.Split('/');
		if (array.Length < 2)
		{
			return;
		}
		XmlDocument xmlDocument = new XmlDocument();
		if (!File.Exists(pathXml))
		{
			FileStream fileStream = new FileStream(pathXml, FileMode.Create);
			fileStream.Write(headerBytes, 0, headerBytes.Length);
			fileStream.Close();
		}
		xmlDocument.Load(pathXml);
		string text = "";
		for (int i = 1; i < array.Length; i++)
		{
			if (array[i].Trim().Length == 0)
			{
				return;
			}
			string text2 = text;
			text = text + "/" + array[i];
			XmlNode xmlNode = xmlDocument.SelectSingleNode(text);
			if (xmlNode == null)
			{
				XmlNode xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, array[i], "");
				xmlNode2.InnerText = "";
				if (text2.Trim().Length == 0)
				{
					xmlDocument.AppendChild(xmlNode2);
				}
				else
				{
					xmlDocument.SelectSingleNode(text2).AppendChild(xmlNode2);
				}
			}
		}
		xmlDocument.Save(pathXml);
	}

	public bool DealUser(int _ID, string _Name, string _Password, bool _save, ref XmlDocument xd)
	{
		try
		{
			XmlDocument xmlDocument = ((xd != null) ? xd : new XmlDocument());
			if (!File.Exists(pathXml))
			{
				FileStream fileStream = new FileStream(pathXml, FileMode.Create);
				fileStream.Write(headerBytes, 0, headerBytes.Length);
				fileStream.Close();
			}
			xmlDocument.Load(pathXml);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/SystemConfig/Users");
			if (xmlNode == null)
			{
				CreatPath("/SystemConfig/Users");
				xmlDocument.Load(pathXml);
				xmlNode = xmlDocument.SelectSingleNode("/SystemConfig/Users");
				if (xmlNode == null)
				{
					return false;
				}
			}
			XmlNode xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "User", "");
			XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("ID");
			xmlAttribute.Value = _ID.ToString();
			XmlAttribute xmlAttribute2 = xmlDocument.CreateAttribute("Name");
			xmlAttribute2.Value = _Name;
			XmlAttribute xmlAttribute3 = xmlDocument.CreateAttribute("Password");
			xmlAttribute3.Value = _Password;
			xmlNode2.Attributes.Append(xmlAttribute);
			xmlNode2.Attributes.Append(xmlAttribute2);
			xmlNode2.Attributes.Append(xmlAttribute3);
			foreach (XmlNode childNode in xmlNode.ChildNodes)
			{
				if (childNode.Attributes["Name"].Value == _Name)
				{
					if (_save)
					{
						xmlNode.ReplaceChild(xmlNode2, childNode);
					}
					else
					{
						xmlNode.RemoveChild(childNode);
					}
					break;
				}
				if (xmlNode.LastChild == childNode && _save)
				{
					xmlNode.AppendChild(xmlNode2);
				}
			}
			if (xmlNode.ChildNodes.Count == 0 && _save)
			{
				xmlNode.AppendChild(xmlNode2);
			}
			xmlDocument.Save(pathXml);
			return true;
		}
		catch
		{
			return false;
		}
	}
}
