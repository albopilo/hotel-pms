using System.ComponentModel;
using ComponentDll;

namespace LockSoftware.Controls;

public class clsBackPanel : ClsBackPanel
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
		this.components = new System.ComponentModel.Container();
	}

	public clsBackPanel()
	{
		InitializeComponent();
	}

	public clsBackPanel(IContainer container)
	{
		container.Add(this);
		InitializeComponent();
	}
}
