using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;



namespace Sytel.Common.WinForms
{
	/// <summary>
	/// Summary description for AppOptionsForm.
	/// </summary>
	public class AppOptionsForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.Panel bottomPanel;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private ApplicationPropertyEditor applicationPropertyEditor1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AppOptionsForm()
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
			this.mainPanel = new System.Windows.Forms.Panel();
			this.applicationPropertyEditor1 = new ApplicationPropertyEditor();
			this.bottomPanel = new System.Windows.Forms.Panel();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.mainPanel.SuspendLayout();
			this.bottomPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainPanel
			// 
			this.mainPanel.Controls.Add(this.applicationPropertyEditor1);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(416, 370);
			this.mainPanel.TabIndex = 0;
			// 
			// applicationPropertyEditor1
			// 
			this.applicationPropertyEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.applicationPropertyEditor1.Location = new System.Drawing.Point(0, 0);
			this.applicationPropertyEditor1.Name = "applicationPropertyEditor1";
			this.applicationPropertyEditor1.NotepadXApplicationSettingsToUse = null;
			this.applicationPropertyEditor1.Size = new System.Drawing.Size(416, 370);
			this.applicationPropertyEditor1.TabIndex = 0;
			// 
			// bottomPanel
			// 
			this.bottomPanel.Controls.Add(this.buttonCancel);
			this.bottomPanel.Controls.Add(this.buttonOk);
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.Location = new System.Drawing.Point(0, 370);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(416, 36);
			this.bottomPanel.TabIndex = 1;
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(336, 8);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			// 
			// buttonOk
			// 
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOk.Location = new System.Drawing.Point(256, 8);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "OK";
			// 
			// AppOptionsForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(416, 406);
			this.Controls.Add(this.mainPanel);
			this.Controls.Add(this.bottomPanel);
			this.Name = "AppOptionsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Application Options";
			this.mainPanel.ResumeLayout(false);
			this.bottomPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public PropertyBag ApplicationPropertiesInfo
		{
			get
			{
				return applicationPropertyEditor1.ApplicationPropertiesInfo;
			}
		}
		public ApplicationSettings ApplicationSettingsToUse
		{
			get
			{
				return applicationPropertyEditor1.NotepadXApplicationSettingsToUse;
			}
			set
			{
				applicationPropertyEditor1.NotepadXApplicationSettingsToUse = value;
			}
		}
		public void RefreshPropertyDisplay()
		{
			applicationPropertyEditor1.RefreshPropertyDisplay();
		}
	}
	
}
