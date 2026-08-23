using System.ComponentModel;
using ComponentDll;

namespace LockSoftware.Controls;

public class keyBtn : KeyboardButton
{
	private IContainer components;

	public keyBtn()
	{
		InitializeComponent();
	}

	public keyBtn(IContainer container)
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
		this.components = new System.ComponentModel.Container();
	}
}
