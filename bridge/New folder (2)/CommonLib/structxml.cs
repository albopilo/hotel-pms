using System.Collections.Generic;

namespace CommonLib;

public class structxml
{
	private int level = 1;

	private string _value = "";

	private string attribute0 = "";

	private string innerText = "";

	private string innerXml = "";

	private Dictionary<string, structxml> children;

	public int Level
	{
		get
		{
			return level;
		}
		set
		{
			level = value;
		}
	}

	public string Value
	{
		get
		{
			return _value;
		}
		set
		{
			_value = value;
		}
	}

	public string Attribute0
	{
		get
		{
			return attribute0;
		}
		set
		{
			attribute0 = value;
		}
	}

	public string InnerText
	{
		get
		{
			return innerText;
		}
		set
		{
			innerText = value;
		}
	}

	public string InnerXml
	{
		get
		{
			return innerXml;
		}
		set
		{
			innerXml = value;
		}
	}

	public Dictionary<string, structxml> Children
	{
		get
		{
			return children;
		}
		set
		{
			children = value;
		}
	}

	public structxml(int _level, string _value0, string _attribute0, string _innerText, string _innerXml, Dictionary<string, structxml> _children)
	{
		level = _level;
		_value = _value0;
		attribute0 = _attribute0;
		innerText = _innerText;
		innerXml = _innerXml;
		children = _children;
	}

	public structxml(int _level, string _value0, string _attribute0, string _innerText, Dictionary<string, structxml> _children)
	{
		level = _level;
		_value = _value0;
		attribute0 = _attribute0;
		innerText = _innerText;
		children = _children;
	}
}
