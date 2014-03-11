using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;


namespace Sytel.Common.WinForms
{
	/// <summary>
	/// Summary description for ApplicationPropertyEditor.
	/// </summary>
	public class ApplicationPropertyEditor : System.Windows.Forms.UserControl
	{
		private ApplicationSettings m_NotepadXApplicationSettings = null;
	
		private System.Windows.Forms.Panel propertyPanel;
		private System.Windows.Forms.PropertyGrid innerPropertyGrid;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ApplicationPropertyEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.propertyPanel = new System.Windows.Forms.Panel();
			this.innerPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.propertyPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// propertyPanel
			// 
			this.propertyPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
																						this.innerPropertyGrid});
			this.propertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyPanel.Name = "propertyPanel";
			this.propertyPanel.Size = new System.Drawing.Size(150, 150);
			this.propertyPanel.TabIndex = 0;
			// 
			// innerPropertyGrid
			// 
			this.innerPropertyGrid.CommandsVisibleIfAvailable = true;
			this.innerPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.innerPropertyGrid.LargeButtons = false;
			this.innerPropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.innerPropertyGrid.Name = "innerPropertyGrid";
			this.innerPropertyGrid.Size = new System.Drawing.Size(150, 150);
			this.innerPropertyGrid.TabIndex = 1;
			this.innerPropertyGrid.Text = "propertyGrid1";
			this.innerPropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.innerPropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// ApplicationPropertyEditor
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.propertyPanel});
			this.Name = "ApplicationPropertyEditor";
			this.propertyPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			
			//Stuff to run after init
			AfterInitializeComponent();
		}
		#endregion
		
		private void AfterInitializeComponent()
		{
			PropertyBag bag = new PropertyBag();
			bag.GetValue += new PropertySpecEventHandler(this.bag_GetValue);
			bag.SetValue += new PropertySpecEventHandler(this.bag_SetValue);
			
			innerPropertyGrid.SelectedObject = bag;
		}
		public ApplicationSettings NotepadXApplicationSettingsToUse
		{
			get
			{
				return m_NotepadXApplicationSettings;
			}
			set
			{
				m_NotepadXApplicationSettings = value;
			}
		}		
		public PropertyBag ApplicationPropertiesInfo
		{
			get
			{
				return (PropertyBag)innerPropertyGrid.SelectedObject;
			}
		}
		public void RefreshPropertyDisplay()
		{
			PropertyBag currentBag = this.ApplicationPropertiesInfo;
			
			//refresh
			innerPropertyGrid.SelectedObject = null;
			innerPropertyGrid.SelectedObject = currentBag;
		}
		
		private void bag_GetValue(object sender, PropertySpecEventArgs e)
		{
			//Get this setting from app settings
			e.Value =Convert.ChangeType(m_NotepadXApplicationSettings.GetSetting(e.Property.Category, e.Property.Name, e.Property.DefaultValue.ToString()), Type.GetType(e.Property.TypeName));
		}
		private void bag_SetValue(object sender, PropertySpecEventArgs e)
		{
			m_NotepadXApplicationSettings.SetSetting(e.Property.Category, e.Property.Name, e.Value.ToString(), false);
		}
	}
}
