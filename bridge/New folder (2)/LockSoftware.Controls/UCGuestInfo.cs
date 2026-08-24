using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LockSoftware.Controls;

public class UCGuestInfo : UserControl
{
	private IContainer components;

	private Label label1;

	private Label label2;

	private Label label3;

	private TableLayoutPanel tableLayoutPanel1;

	private TextBox textBox1;

	private ComboBox comboBox1;

	private TextBox textBox2;

	public UCGuestInfo()
	{
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
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.comboBox1 = new System.Windows.Forms.ComboBox();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.tableLayoutPanel1.SuspendLayout();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(3, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(71, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "Guest Name:";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(3, 34);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(77, 12);
		this.label2.TabIndex = 1;
		this.label2.Text = "Certificate:";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(3, 68);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(47, 12);
		this.label3.TabIndex = 2;
		this.label3.Text = "Number:";
		this.tableLayoutPanel1.ColumnCount = 3;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.01992f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.91235f));
		this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
		this.tableLayoutPanel1.Controls.Add(this.comboBox1, 1, 1);
		this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.textBox2, 1, 2);
		this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 3;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(254, 103);
		this.tableLayoutPanel1.TabIndex = 3;
		this.textBox1.Location = new System.Drawing.Point(87, 3);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(106, 21);
		this.textBox1.TabIndex = 0;
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Location = new System.Drawing.Point(87, 37);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(108, 20);
		this.comboBox1.TabIndex = 1;
		this.textBox2.Location = new System.Drawing.Point(87, 71);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(106, 21);
		this.textBox2.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.tableLayoutPanel1);
		base.Name = "UCGuestInfo";
		base.Size = new System.Drawing.Size(254, 103);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
