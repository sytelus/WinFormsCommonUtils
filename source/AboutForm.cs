using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;


namespace Sytel.Common.WinForms
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		internal System.Windows.Forms.PictureBox PictureBox1;
		internal System.Windows.Forms.Label lblDescription;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.LinkLabel linkCompany;
		internal System.Windows.Forms.Label lblPartner;
		internal System.Windows.Forms.Button butInfo;
		internal System.Windows.Forms.Button butOK;
		internal System.Windows.Forms.LinkLabel linkProduct;
		internal System.Windows.Forms.Label lblVersion;
		internal System.Windows.Forms.Label label2;
		internal System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtCopyrightText;
		private AssemblyInfoViewerUserControl assemblyInfoViewerUserControl1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
			this.topPanel = new System.Windows.Forms.Panel();
			this.lblVersion = new System.Windows.Forms.Label();
			this.PictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.assemblyInfoViewerUserControl1 = new Sytel.Common.WinForms.AssemblyInfoViewerUserControl();
			this.Label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.txtCopyrightText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblPartner = new System.Windows.Forms.Label();
			this.butInfo = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.linkProduct = new System.Windows.Forms.LinkLabel();
			this.linkCompany = new System.Windows.Forms.LinkLabel();
			this.topPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
// 
// topPanel
// 
			this.topPanel.Controls.Add(this.PictureBox1);
			this.topPanel.Controls.Add(this.lblDescription);
			this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.topPanel.Location = new System.Drawing.Point(0, 0);
			this.topPanel.Name = "topPanel";
			this.topPanel.Size = new System.Drawing.Size(552, 48);
			this.topPanel.TabIndex = 0;
			this.topPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.topPanel_Paint);
// 
// lblVersion
// 
			this.lblVersion.AccessibleDescription = "1.0.0.0";
			this.lblVersion.AccessibleName = "Version";
			this.lblVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblVersion.Location = new System.Drawing.Point(264, 0);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(288, 16);
			this.lblVersion.TabIndex = 9;
			this.lblVersion.Text = "< version >";
			this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
// 
// PictureBox1
// 
			this.PictureBox1.AccessibleDescription = "TaskVision Logo";
			this.PictureBox1.AccessibleName = "Application Logo";
			this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
			this.PictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.PictureBox1.Location = new System.Drawing.Point(10, 6);
			this.PictureBox1.Name = "PictureBox1";
			this.PictureBox1.Size = new System.Drawing.Size(48, 48);
			this.PictureBox1.TabIndex = 8;
			this.PictureBox1.TabStop = false;
// 
// lblDescription
// 
			this.lblDescription.AccessibleDescription = "Change Management Software";
			this.lblDescription.AccessibleName = "TaskDescription";
			this.lblDescription.AutoSize = true;
			this.lblDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblDescription.Location = new System.Drawing.Point(72, 8);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(78, 14);
			this.lblDescription.TabIndex = 7;
			this.lblDescription.Text = "< description >";
// 
// panel1
// 
			this.panel1.Controls.Add(this.lblVersion);
			this.panel1.Controls.Add(this.assemblyInfoViewerUserControl1);
			this.panel1.Controls.Add(this.Label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 48);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(552, 262);
			this.panel1.TabIndex = 1;
// 
// assemblyInfoViewerUserControl1
// 
			this.assemblyInfoViewerUserControl1.AssmblyToUse = null;
			this.assemblyInfoViewerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.assemblyInfoViewerUserControl1.Location = new System.Drawing.Point(0, 16);
			this.assemblyInfoViewerUserControl1.Name = "assemblyInfoViewerUserControl1";
			this.assemblyInfoViewerUserControl1.Size = new System.Drawing.Size(552, 246);
			this.assemblyInfoViewerUserControl1.TabIndex = 9;
// 
// Label1
// 
			this.Label1.AccessibleDescription = "Product components";
			this.Label1.AccessibleName = "Product Components";
			this.Label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.Label1.Location = new System.Drawing.Point(0, 0);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(552, 16);
			this.Label1.TabIndex = 6;
			this.Label1.Text = "Product Information:";
// 
// panel2
// 
			this.panel2.Controls.Add(this.txtCopyrightText);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.lblPartner);
			this.panel2.Controls.Add(this.butInfo);
			this.panel2.Controls.Add(this.butOK);
			this.panel2.Controls.Add(this.linkProduct);
			this.panel2.Controls.Add(this.linkCompany);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 310);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(552, 88);
			this.panel2.TabIndex = 2;
// 
// txtCopyrightText
// 
			this.txtCopyrightText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtCopyrightText.Location = new System.Drawing.Point(128, 40);
			this.txtCopyrightText.Multiline = true;
			this.txtCopyrightText.Name = "txtCopyrightText";
			this.txtCopyrightText.ReadOnly = true;
			this.txtCopyrightText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtCopyrightText.Size = new System.Drawing.Size(304, 40);
			this.txtCopyrightText.TabIndex = 20;
// 
// label3
// 
			this.label3.AutoSize = true;
			this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label3.Location = new System.Drawing.Point(12, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71, 14);
			this.label3.TabIndex = 19;
			this.label3.Text = "Copyright (C) ";
// 
// label2
// 
			this.label2.AutoSize = true;
			this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label2.Location = new System.Drawing.Point(12, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105, 14);
			this.label2.TabIndex = 18;
			this.label2.Text = "Product Homepage:";
// 
// lblPartner
// 
			this.lblPartner.AutoSize = true;
			this.lblPartner.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPartner.Location = new System.Drawing.Point(12, 24);
			this.lblPartner.Name = "lblPartner";
			this.lblPartner.Size = new System.Drawing.Size(77, 14);
			this.lblPartner.TabIndex = 13;
			this.lblPartner.Text = "Developed by:";
// 
// butInfo
// 
			this.butInfo.AccessibleDescription = "System Info";
			this.butInfo.AccessibleName = "System Info";
			this.butInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.butInfo.Location = new System.Drawing.Point(448, 40);
			this.butInfo.Name = "butInfo";
			this.butInfo.Size = new System.Drawing.Size(88, 23);
			this.butInfo.TabIndex = 17;
			this.butInfo.Text = "System Info...";
			this.butInfo.Click += new System.EventHandler(this.butInfo_Click);
// 
// butOK
// 
			this.butOK.AccessibleDescription = "OK";
			this.butOK.AccessibleName = "OK";
			this.butOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.butOK.Location = new System.Drawing.Point(448, 8);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(88, 23);
			this.butOK.TabIndex = 16;
			this.butOK.Text = "OK";
// 
// linkProduct
// 
			this.linkProduct.AccessibleDescription = "Product Link";
			this.linkProduct.AccessibleName = "Product Link";
			this.linkProduct.AutoSize = true;
			this.linkProduct.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.linkProduct.Links.Add(new System.Windows.Forms.LinkLabel.Link(0, 12));
			this.linkProduct.Location = new System.Drawing.Point(128, 8);
			this.linkProduct.Name = "linkProduct";
			this.linkProduct.Size = new System.Drawing.Size(67, 14);
			this.linkProduct.TabIndex = 12;
			this.linkProduct.TabStop = true;
			this.linkProduct.Text = "Product Link";
			this.linkProduct.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkProduct_LinkClicked);
// 
// linkCompany
// 
			this.linkCompany.AccessibleDescription = "";
			this.linkCompany.AccessibleName = "";
			this.linkCompany.AutoSize = true;
			this.linkCompany.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.linkCompany.Links.Add(new System.Windows.Forms.LinkLabel.Link(0, 12));
			this.linkCompany.Location = new System.Drawing.Point(128, 23);
			this.linkCompany.Name = "linkCompany";
			this.linkCompany.Size = new System.Drawing.Size(77, 14);
			this.linkCompany.TabIndex = 14;
			this.linkCompany.TabStop = true;
			this.linkCompany.Text = "Company Link";
			this.linkCompany.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkProduct_LinkClicked);
// 
// AboutForm
// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 398);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.topPanel);
			this.Name = "AboutForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AboutForm";
			this.topPanel.ResumeLayout(false);
			this.topPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion


		public void SetupForm(System.Reflection.Assembly executingAssembly)
		{
			AssemblyAttributeInfo thisAssemblyAttributeInfo = new AssemblyAttributeInfo(executingAssembly);
			this.Text = "About " + thisAssemblyAttributeInfo.ProductName;

			// set labels
			lblDescription.Text = thisAssemblyAttributeInfo.ProductDescription;
			lblVersion.Text = "Version: " + thisAssemblyAttributeInfo.FriendlyVersion + "    (Build: " + thisAssemblyAttributeInfo.BuildVersion + ")";
			txtCopyrightText.Text = thisAssemblyAttributeInfo.CopyrightOwnerName + @".
" + thisAssemblyAttributeInfo.CopyrightText;

			// set links
			linkProduct.Text = thisAssemblyAttributeInfo.ProductHomepageUrl;
			linkProduct.Links.Clear();
			linkProduct.Links.Add(new LinkLabel.Link(0, linkProduct.Text.Length, thisAssemblyAttributeInfo.ProductHomepageUrl));
			linkCompany.Text = thisAssemblyAttributeInfo.CompanyUrl;
			linkCompany.Links.Clear();
			linkCompany.Links.Add(new LinkLabel.Link(0, linkCompany.Text.Length, thisAssemblyAttributeInfo.CompanyUrl));

			assemblyInfoViewerUserControl1.AssmblyToUse = executingAssembly;
		}

		private void butInfo_Click(object sender, System.EventArgs e)
		{
			Process.Start("msinfo32.exe");
		}

		private void linkProduct_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Process.Start((string)e.Link.LinkData);
		}

		private void topPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}
	}
}
