using System.ComponentModel;
using ComponentDll;

namespace LockSoftware.Controls;

public class ToolsBtn : ClsLabel
{
	private IContainer components;

	public ToolsBtn()
	{
		base.GuidInfo = "&56~01'][Manson]v%#@";
		AutoSize = false;
		InitializeComponent();
	}

	public ToolsBtn(IContainer container)
	{
		base.GuidInfo = "&56~01'][Manson]v%#@";
		AutoSize = false;
		container.Add(this);
		InitializeComponent();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		base.ResumeLayout(false);
	}
}
