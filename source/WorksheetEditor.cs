using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;


namespace Sytel.Common.WinForms.ContentEditors
{
	/// <summary>
	/// Summary description for WorksheetEditor.
	/// </summary>
	public class WorksheetEditor : System.Windows.Forms.UserControl, IContentEditor
	{
		private System.Data.DataTable Worksheet1;
		private System.Data.DataColumn dataColumnA;
		private System.Data.DataColumn dataColumnB;
		private System.Data.DataColumn dataColumnC;
		private System.Data.DataColumn dataColumnD;
		private System.Data.DataColumn dataColumnE;
		private System.Data.DataColumn dataColumnF;
		private System.Windows.Forms.DataGrid workSheetDataGrid;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Data.DataSet workSheetDataSetTemplate;

		private string m_currentFormat = ContentFormats.FORMAT_HTML;
		private string m_currentNoteCaption = "";
		private string m_currentNoteID = "";
		private System.Data.DataSet workSheetDataSet;
		

		public WorksheetEditor()
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
			this.workSheetDataGrid = new System.Windows.Forms.DataGrid();
			this.workSheetDataSetTemplate = new System.Data.DataSet();
			this.Worksheet1 = new System.Data.DataTable();
			this.dataColumnA = new System.Data.DataColumn();
			this.dataColumnB = new System.Data.DataColumn();
			this.dataColumnC = new System.Data.DataColumn();
			this.dataColumnD = new System.Data.DataColumn();
			this.dataColumnE = new System.Data.DataColumn();
			this.dataColumnF = new System.Data.DataColumn();
			this.workSheetDataSet = new System.Data.DataSet();
			((System.ComponentModel.ISupportInitialize)(this.workSheetDataGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.workSheetDataSetTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Worksheet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.workSheetDataSet)).BeginInit();
			this.SuspendLayout();
			// 
			// workSheetDataGrid
			// 
			this.workSheetDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.workSheetDataGrid.DataMember = "";
			this.workSheetDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.workSheetDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.workSheetDataGrid.Name = "workSheetDataGrid";
			this.workSheetDataGrid.Size = new System.Drawing.Size(150, 150);
			this.workSheetDataGrid.TabIndex = 0;
			this.workSheetDataGrid.CurrentCellChanged += new System.EventHandler(this.workSheetDataGrid_CurrentCellChanged);
			// 
			// workSheetDataSetTemplate
			// 
			this.workSheetDataSetTemplate.DataSetName = "NewDataSet";
			this.workSheetDataSetTemplate.Locale = new System.Globalization.CultureInfo("en-US");
			this.workSheetDataSetTemplate.Tables.AddRange(new System.Data.DataTable[] {
																						  this.Worksheet1});
			// 
			// Worksheet1
			// 
			this.Worksheet1.Columns.AddRange(new System.Data.DataColumn[] {
																			  this.dataColumnA,
																			  this.dataColumnB,
																			  this.dataColumnC,
																			  this.dataColumnD,
																			  this.dataColumnE,
																			  this.dataColumnF});
			this.Worksheet1.TableName = "Worksheet1";
			// 
			// dataColumnA
			// 
			this.dataColumnA.Caption = "A";
			this.dataColumnA.ColumnName = "A";
			this.dataColumnA.DefaultValue = "";
			// 
			// dataColumnB
			// 
			this.dataColumnB.Caption = "B";
			this.dataColumnB.ColumnName = "B";
			this.dataColumnB.DefaultValue = "";
			// 
			// dataColumnC
			// 
			this.dataColumnC.Caption = "C";
			this.dataColumnC.ColumnName = "C";
			this.dataColumnC.DefaultValue = "";
			// 
			// dataColumnD
			// 
			this.dataColumnD.Caption = "D";
			this.dataColumnD.ColumnName = "D";
			this.dataColumnD.DefaultValue = "";
			// 
			// dataColumnE
			// 
			this.dataColumnE.ColumnName = "E";
			this.dataColumnE.DefaultValue = "";
			// 
			// dataColumnF
			// 
			this.dataColumnF.ColumnName = "F";
			this.dataColumnF.DefaultValue = "";
			// 
			// workSheetDataSet
			// 
			this.workSheetDataSet.DataSetName = "NewDataSet";
			this.workSheetDataSet.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// WorksheetEditor
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.workSheetDataGrid});
			this.Name = "WorksheetEditor";
			((System.ComponentModel.ISupportInitialize)(this.workSheetDataGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.workSheetDataSetTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Worksheet1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.workSheetDataSet)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region IContentEditor interface implementation
		string IContentEditor.EditorName
		{
			get
			{
				return this.GetType().Name;
			}
		}
		string IContentEditor.ContentID
		{
			get
			{
				return m_currentNoteID;
			}
			set
			{
				m_currentNoteID = value;
			}
		}
		string IContentEditor.ContentCaption
		{
			get
			{
				return m_currentNoteCaption;
			}
			set
			{
				m_currentNoteCaption = value;
				workSheetDataGrid.CaptionText = m_currentNoteCaption;
			}
		}
		bool IContentEditor.IsModified
		{
			get
			{
				return workSheetDataSet.HasChanges();
			}
		}
		
		bool IContentEditor.IsReadOnly
		{
			get
			{
				return workSheetDataGrid.ReadOnly;
			}
			set
			{
				workSheetDataGrid.ReadOnly = value;
			}
		}
		
		bool IContentEditor.IsVisible
		{
			get
			{
				return Visible;
			}
			
			set
			{
				Visible = value;
			}
		}
		
		
		string IContentEditor.EditorUserFriendlyName
		{
			get
			{
				return this.Name;
			}
		}
		
		string IContentEditor.GetContent()
		{
			//Check if grid is still in edit mode, then flush the data
			DataGridCell savedCurrentCellValue = workSheetDataGrid.CurrentCell;	//This is value type
			workSheetDataGrid.CurrentCell = new DataGridCell(-1,-1);	//unselect current cell
			workSheetDataGrid.CurrentCell = savedCurrentCellValue;
		
			StringBuilder serializedDataSetStringBuilder = new StringBuilder();
			workSheetDataSet.WriteXml(new StringWriter(serializedDataSetStringBuilder), System.Data.XmlWriteMode.DiffGram);
			return serializedDataSetStringBuilder.ToString();
		}
		
		void IContentEditor.SetContentInfo(string noteID, string noteCaption, string noteContent)
		{
			((IContentEditor) this).ContentID = noteID;
			((IContentEditor) this).ContentCaption = noteCaption;
			
			((IContentEditor)this).ClearContent();
			if (noteContent != string.Empty)
				workSheetDataSet.ReadXml(new System.IO.StringReader(noteContent), XmlReadMode.DiffGram);
			else {};	//contents is already cleared
		}
		void IContentEditor.ClearContent()
		{
			workSheetDataSet.Tables[0].Rows.Clear();
		}
		void IContentEditor.ResetAll()
		{
			workSheetDataGrid.DataMember = null; 
			workSheetDataGrid.DataSource = null;
			workSheetDataSet.Dispose();
			workSheetDataSet = workSheetDataSetTemplate.Clone();
			
			try
			{
				workSheetDataGrid.SetDataBinding(workSheetDataSet, "Worksheet1");
			}
			catch
			{}	//DataGrid has error while setting databinding in event: "'0' is not a valid value for 'value'. 'value' should be between 'minimum' and 'maximum'"
			m_currentNoteCaption = "";
			m_currentNoteID = "";
		}
		#endregion

		public string ContentFormat
		{
			get
			{
				return m_currentFormat;
			}
			set
			{
				if (value == ContentFormats.FORMAT_WORKSHEET)
				{
					m_currentFormat = value;
				}
				else
					throw new ArgumentException("Specified format '"+ value +"' is not recognized by '"+ this.Name + "'. Only WORKSHEET format is acceptable", "CurrentFormat");
			}
		}
		
		private void workSheetDataGrid_CurrentCellChanged(object sender, System.EventArgs e)
		{
			if (workSheetDataGrid.CurrentCell.ColumnNumber == workSheetDataSet.Tables["Worksheet1"].Columns.Count - 1)
			{
				int asciiForA = Convert.ToByte('A');
			
				DataGridCell savedCurrentCell = workSheetDataGrid.CurrentCell;
				DataColumn newColumn = workSheetDataSet.Tables["Worksheet1"].Columns.Add();

				int nextAsciiValue  = workSheetDataSet.Tables["Worksheet1"].Columns.Count + Convert.ToInt32('A');
				string nextColumnHeading;
				if (nextAsciiValue > Convert.ToByte('Z')) 
				{
					//TODO : correct these formulas
					nextColumnHeading = Convert.ToChar(asciiForA + ((nextAsciiValue-asciiForA) % asciiForA)).ToString();
					nextColumnHeading += Convert.ToInt32(((nextAsciiValue-asciiForA) / asciiForA)+1).ToString();
				}
				else nextColumnHeading = Convert.ToChar(nextAsciiValue).ToString();
				newColumn.Caption = nextColumnHeading;
				newColumn.ColumnName = newColumn.Caption;
				newColumn.DefaultValue = "";
				
				foreach (DataRow rowToSetBlank in workSheetDataSet.Tables["Worksheet1"].Rows)
				{
					rowToSetBlank[newColumn] = "";
				}
				workSheetDataGrid.CurrentCell = savedCurrentCell; 
			}		
		}
					
	}
}
