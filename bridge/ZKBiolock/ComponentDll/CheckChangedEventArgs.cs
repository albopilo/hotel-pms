using System;

namespace ComponentDll;

public class CheckChangedEventArgs : EventArgs
{
	private bool isChecked;

	public bool Checked
	{
		get
		{
			return isChecked;
		}
		set
		{
			isChecked = value;
		}
	}

	public CheckChangedEventArgs(bool isChecked)
	{
		this.isChecked = isChecked;
	}
}
