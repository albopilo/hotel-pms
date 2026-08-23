using System.ComponentModel;
using ComponentDll;

namespace LockSoftware.Controls;

public class GlassBtn : ComponentDll.GlassBtn
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
		base.ResumeLayout(false);
	}

	public GlassBtn()
	{
		InitializeComponent();
		base.GuidInfo = "&56~01'][Manson]v%#@";
	}

	public GlassBtn(IContainer container)
	{
		container.Add(this);
		base.GuidInfo = "&56~01'][Manson]v%#@";
		InitializeComponent();
	}
}
