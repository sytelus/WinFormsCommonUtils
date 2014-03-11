using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Sytel;
using System.Collections.Specialized;
using System.Diagnostics;


namespace Sytel.Common.WinForms
{
	/// <summary>
	/// Summary description for AssemblyInfoViewerUserControl.
	/// </summary>
	public class AssemblyInfoViewerUserControl : System.Windows.Forms.UserControl
	{
		private System.Reflection.Assembly m_assembly;

		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGrid detailView;
		private System.Windows.Forms.TextBox notSupportedTextBox;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AssemblyInfoViewerUserControl()
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
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.notSupportedTextBox = new System.Windows.Forms.TextBox();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.detailView = new System.Windows.Forms.DataGrid();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.detailView)).BeginInit();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.BackColor = System.Drawing.SystemColors.Window;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																				  new System.Windows.Forms.TreeNode("All Product Info", new System.Windows.Forms.TreeNode[] {
																																												new System.Windows.Forms.TreeNode("Basic Info"),
																																												new System.Windows.Forms.TreeNode("Refrenced Assemblies"),
																																												new System.Windows.Forms.TreeNode("Loaded Modules"),
																																												new System.Windows.Forms.TreeNode("Resource Names"),
																																												new System.Windows.Forms.TreeNode("Modules"),
																																												new System.Windows.Forms.TreeNode("Defined Types"),
																																												new System.Windows.Forms.TreeNode("Environment"),
																																												new System.Windows.Forms.TreeNode("Assembly Info"),
																																												new System.Windows.Forms.TreeNode("Executable File Info"),
																																												new System.Windows.Forms.TreeNode("Task View"),
																																												new System.Windows.Forms.TreeNode("Culture Info"),
																																												new System.Windows.Forms.TreeNode("Time Zone"),
																																												new System.Windows.Forms.TreeNode("Date Time Formats"),
																																												new System.Windows.Forms.TreeNode("Number Formats"),
																																												new System.Windows.Forms.TreeNode("Region Info")})});
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(144, 308);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(144, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 308);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.notSupportedTextBox);
			this.panel1.Controls.Add(this.propertyGrid1);
			this.panel1.Controls.Add(this.detailView);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(147, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(285, 308);
			this.panel1.TabIndex = 5;
			// 
			// notSupportedTextBox
			// 
			this.notSupportedTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.notSupportedTextBox.Location = new System.Drawing.Point(0, 0);
			this.notSupportedTextBox.Multiline = true;
			this.notSupportedTextBox.Name = "notSupportedTextBox";
			this.notSupportedTextBox.Size = new System.Drawing.Size(285, 308);
			this.notSupportedTextBox.TabIndex = 5;
			this.notSupportedTextBox.Text = "";
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid1.HelpVisible = false;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.propertyGrid1.Size = new System.Drawing.Size(285, 308);
			this.propertyGrid1.TabIndex = 6;
			this.propertyGrid1.Text = "propertyGrid1";
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// detailView
			// 
			this.detailView.CaptionVisible = false;
			this.detailView.DataMember = "";
			this.detailView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.detailView.FlatMode = true;
			this.detailView.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.detailView.Location = new System.Drawing.Point(0, 0);
			this.detailView.Name = "detailView";
			this.detailView.ReadOnly = true;
			this.detailView.Size = new System.Drawing.Size(285, 308);
			this.detailView.TabIndex = 3;
			// 
			// AssemblyInfoViewerUserControl
			// 
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.treeView1);
			this.Name = "AssemblyInfoViewerUserControl";
			this.Size = new System.Drawing.Size(432, 308);
			this.Load += new System.EventHandler(this.AssemblyInfoViewerUserControl_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.detailView)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void AssemblyInfoViewerUserControl_Load(object sender, System.EventArgs e)
		{
			treeView1.ExpandAll();
			treeView1.SelectedNode = treeView1.Nodes[0];			
		}

		public System.Reflection.Assembly AssmblyToUse
		{
			get
			{
				return m_assembly;
			}
			set
			{
				m_assembly=value;
			}
		}

		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			detailView.DataSource = null;
			detailView.Visible = false;
			propertyGrid1.SelectedObject = null;
			propertyGrid1.Visible = false;
			notSupportedTextBox.Visible=false;
			if (e.Node.Text == "All Product Info")
			{
				notSupportedTextBox.Text = "Select category from left";
				notSupportedTextBox.Visible=true;
			}
			else if (e.Node.Text == "Basic Info")
			{
				propertyGrid1.Visible = true;
				propertyGrid1.SelectedObject = new AssemblyAttributeInfo(m_assembly);
			}
			else if (e.Node.Text == "Assembly Info")
			{
				propertyGrid1.Visible = true;
				propertyGrid1.SelectedObject = m_assembly;
			}
			else if  (e.Node.Text == "Refrenced Assemblies")
			{
				detailView.DataSource = m_assembly.GetReferencedAssemblies();
				detailView.Visible = true;
			}
			else if  (e.Node.Text == "Exported Types")
			{
				detailView.DataSource = m_assembly.GetExportedTypes();
				detailView.Visible = true;
			}
			else if  (e.Node.Text == "Files")
			{
				detailView.DataSource = m_assembly.GetFiles();
				detailView.Visible = true;
			}
			else if  (e.Node.Text == "Loaded Modules")
			{
				detailView.DataSource = m_assembly.GetLoadedModules();
				detailView.Visible = true;
			}
			else if  (e.Node.Text == "Resource Names")
			{
				detailView.DataSource = m_assembly.GetManifestResourceNames();
				detailView.Visible = true;
			}
			else if  (e.Node.Text == "Modules")
			{
				detailView.DataSource = m_assembly.GetModules();
				detailView.Visible = true;
			}
			else if  (e.Node.Text == "Defined Types")
			{
				detailView.DataSource = m_assembly.GetTypes();
				detailView.Visible = true;
			}
			else if  (e.Node.Text == "Executable File Info")
			{
				propertyGrid1.SelectedObject = FileVersionInfo.GetVersionInfo(m_assembly.Location);
				propertyGrid1.Visible = true;
			}
			else if  (e.Node.Text == "Environment")
			{
				propertyGrid1.SelectedObject = CommonFunctions.GetPropertyBagForStaticMembers(typeof(System.Environment)) ;
				propertyGrid1.Visible = true;
			}
			else if  (e.Node.Text == "Task View")
			{
				detailView.DataSource = Process.GetProcesses();
				detailView.Visible = true;
			}
			else if  (e.Node.Text == "Culture Info")
			{
				propertyGrid1.SelectedObject = System.Threading.Thread.CurrentThread.CurrentCulture;
				propertyGrid1.Visible = true;
			}
			else if  (e.Node.Text == "Time Zone")
			{
				propertyGrid1.SelectedObject = System.TimeZone.CurrentTimeZone;
				propertyGrid1.Visible = true;
			}
			else if  (e.Node.Text == "Date Time Formats")
			{
				propertyGrid1.SelectedObject = System.Globalization.DateTimeFormatInfo.CurrentInfo;
				propertyGrid1.Visible = true;
			}
			else if  (e.Node.Text == "Number Formats")
			{
				propertyGrid1.SelectedObject = System.Globalization.NumberFormatInfo.CurrentInfo;
				propertyGrid1.Visible = true;
			}
			else if  (e.Node.Text == "Region Info")
			{
				propertyGrid1.SelectedObject = System.Globalization.RegionInfo.CurrentRegion;
				propertyGrid1.Visible = true;
			}
			else
			{
				notSupportedTextBox.Text = e.Node.Text + " is not implemented yet";
				notSupportedTextBox.Visible=true;
			}
		}
	}
}
