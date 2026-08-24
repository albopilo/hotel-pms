using System.Drawing;

namespace LockSoftware;

public class ClassFont
{
	private static ClassFont instance;

	public bool enabled;

	private FontStruct[] FontStyles;

	public static ClassFont Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new ClassFont();
			}
			return instance;
		}
	}

	private ClassFont()
	{
		FontStyles = new FontStruct[5];
		FontStyles[0].familyName = "Times New Roman";
		FontStyles[0].emSize = 13.5f;
		FontStyles[0].style = FontStyle.Regular;
		FontStyles[1].familyName = "Times New Roman";
		FontStyles[1].emSize = 13.5f;
		FontStyles[1].style = FontStyle.Regular;
		FontStyles[2].familyName = "Times New Roman";
		FontStyles[2].emSize = 13.5f;
		FontStyles[2].style = FontStyle.Regular;
		FontStyles[3].familyName = "Times New Roman";
		FontStyles[3].emSize = 15f;
		FontStyles[3].style = FontStyle.Regular;
		FontStyles[4].familyName = "Times New Roman";
		FontStyles[4].emSize = 13.5f;
		FontStyles[4].style = FontStyle.Regular;
	}

	public Font GetFont(uint index)
	{
		if (index >= FontStyles.Length)
		{
			index = 0u;
		}
		return new Font(FontStyles[index].familyName, FontStyles[index].emSize, FontStyles[index].style);
	}
}
