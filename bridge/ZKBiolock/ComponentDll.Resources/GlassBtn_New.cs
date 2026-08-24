using System.ComponentModel;

namespace ComponentDll.Resources;

public class GlassBtn_New : Component
{
	private IContainer components;

	public GlassBtn_New()
	{
		InitializeComponent();
	}

	public GlassBtn_New(IContainer container)
	{
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
		components = new Container();
	}
}
