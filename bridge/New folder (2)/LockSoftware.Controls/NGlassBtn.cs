using System.ComponentModel;
using ComponentDll;

namespace LockSoftware.Controls;

public class NGlassBtn : GlassBtn_New
{
	private IContainer components;

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
		base.GuidInfo = "&56~01'][Manson]v%#@";
		base.Name = "NGlassBtn";
		base.ResumeLayout(false);
	}

	public NGlassBtn()
	{
		InitializeComponent();
	}

	public NGlassBtn(IContainer container)
	{
		container.Add(this);
		InitializeComponent();
	}
}
