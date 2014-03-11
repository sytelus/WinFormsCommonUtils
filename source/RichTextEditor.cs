using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace Sytel.Common.WinForms.ContentEditors
{
	/// <summary>
	/// Summary description for RichTextEditor.
	/// </summary>
	public class RichTextEditor : System.Windows.Forms.UserControl, IContentEditor
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ImageList mainImageList;
		private System.Windows.Forms.ToolBar editorToolbar;
		private System.Windows.Forms.ToolBarButton printToolButton;
		private System.Windows.Forms.ToolBarButton printPreviewToolButton;
		private System.Windows.Forms.ToolBarButton saveAsToolButton;
		private System.Windows.Forms.ToolBarButton emailToolButton;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton boldToolButton;
		private System.Windows.Forms.ToolBarButton underlineToolButton;
		private System.Windows.Forms.ToolBarButton italicToolButton;
		private System.Windows.Forms.ToolBarButton fontToolButton;
		private System.Windows.Forms.ToolBarButton leftAlignToolButton;
		private System.Windows.Forms.ToolBarButton rightAlignToolButton;
		private System.Windows.Forms.ToolBarButton unnumberedBulletToolButton;
		private System.Windows.Forms.ToolBarButton numberedBulletToolButton;
		private System.Windows.Forms.ToolBarButton findToolButton;
		private System.Windows.Forms.ToolBarButton findNextToolButton;
		private System.Windows.Forms.ToolBarButton undoToolButton;
		private System.Windows.Forms.ToolBarButton redoToolButton;
		private System.Windows.Forms.ToolBarButton cutToolButton;
		private System.Windows.Forms.ToolBarButton copyToolButton;
		private System.Windows.Forms.ToolBarButton pasteToolButton;
		private System.Windows.Forms.ToolBarButton hyperLinkToolButton;
		private System.Windows.Forms.ToolBarButton spellCheckToolButton;
		private System.Windows.Forms.ToolBarButton clearToolButton;
		private System.Windows.Forms.ToolBarButton insertDateToolButton;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel toolbarPanel;
		private System.Windows.Forms.FontDialog fontDialog1;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.ToolBarButton centerAlignToolButton;
		private System.Windows.Forms.ToolBarButton wordExportToolButton;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.ToolBarButton formatingSeperator1;
		private System.Windows.Forms.ToolBarButton bulletsSeperator;
		private System.Windows.Forms.ToolBarButton findSeperator;
		private System.Windows.Forms.ToolBarButton editingSeperator1;
		private System.Windows.Forms.ToolBarButton editingSeperator2;


		private string m_currentFormat = ContentFormats.FORMAT_HTML;
		private string m_currentNoteCaption = "";
		private System.Windows.Forms.ToolBarButton wordwrapToolButton;
		private System.Windows.Forms.RichTextBox innerRichTextBox;
		private string m_currentNoteID = "";

		
		public RichTextEditor()
		{
			InitializeComponent();
		}

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
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RichTextEditor));
			this.mainImageList = new System.Windows.Forms.ImageList(this.components);
			this.toolbarPanel = new System.Windows.Forms.Panel();
			this.editorToolbar = new System.Windows.Forms.ToolBar();
			this.wordExportToolButton = new System.Windows.Forms.ToolBarButton();
			this.printToolButton = new System.Windows.Forms.ToolBarButton();
			this.printPreviewToolButton = new System.Windows.Forms.ToolBarButton();
			this.saveAsToolButton = new System.Windows.Forms.ToolBarButton();
			this.emailToolButton = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
			this.boldToolButton = new System.Windows.Forms.ToolBarButton();
			this.underlineToolButton = new System.Windows.Forms.ToolBarButton();
			this.italicToolButton = new System.Windows.Forms.ToolBarButton();
			this.fontToolButton = new System.Windows.Forms.ToolBarButton();
			this.formatingSeperator1 = new System.Windows.Forms.ToolBarButton();
			this.leftAlignToolButton = new System.Windows.Forms.ToolBarButton();
			this.centerAlignToolButton = new System.Windows.Forms.ToolBarButton();
			this.rightAlignToolButton = new System.Windows.Forms.ToolBarButton();
			this.unnumberedBulletToolButton = new System.Windows.Forms.ToolBarButton();
			this.numberedBulletToolButton = new System.Windows.Forms.ToolBarButton();
			this.bulletsSeperator = new System.Windows.Forms.ToolBarButton();
			this.findToolButton = new System.Windows.Forms.ToolBarButton();
			this.findNextToolButton = new System.Windows.Forms.ToolBarButton();
			this.findSeperator = new System.Windows.Forms.ToolBarButton();
			this.undoToolButton = new System.Windows.Forms.ToolBarButton();
			this.redoToolButton = new System.Windows.Forms.ToolBarButton();
			this.editingSeperator1 = new System.Windows.Forms.ToolBarButton();
			this.cutToolButton = new System.Windows.Forms.ToolBarButton();
			this.copyToolButton = new System.Windows.Forms.ToolBarButton();
			this.pasteToolButton = new System.Windows.Forms.ToolBarButton();
			this.editingSeperator2 = new System.Windows.Forms.ToolBarButton();
			this.hyperLinkToolButton = new System.Windows.Forms.ToolBarButton();
			this.spellCheckToolButton = new System.Windows.Forms.ToolBarButton();
			this.clearToolButton = new System.Windows.Forms.ToolBarButton();
			this.insertDateToolButton = new System.Windows.Forms.ToolBarButton();
			this.wordwrapToolButton = new System.Windows.Forms.ToolBarButton();
			this.panel2 = new System.Windows.Forms.Panel();
			this.innerRichTextBox = new System.Windows.Forms.RichTextBox();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.toolbarPanel.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainImageList
			// 
			this.mainImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.mainImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("mainImageList.ImageStream")));
			this.mainImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// toolbarPanel
			// 
			this.toolbarPanel.Controls.Add(this.editorToolbar);
			this.toolbarPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.toolbarPanel.Location = new System.Drawing.Point(0, 0);
			this.toolbarPanel.Name = "toolbarPanel";
			this.toolbarPanel.Size = new System.Drawing.Size(432, 56);
			this.toolbarPanel.TabIndex = 2;
			// 
			// editorToolbar
			// 
			this.editorToolbar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.editorToolbar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																							 this.wordExportToolButton,
																							 this.printToolButton,
																							 this.printPreviewToolButton,
																							 this.saveAsToolButton,
																							 this.emailToolButton,
																							 this.toolBarButton5,
																							 this.boldToolButton,
																							 this.underlineToolButton,
																							 this.italicToolButton,
																							 this.fontToolButton,
																							 this.formatingSeperator1,
																							 this.leftAlignToolButton,
																							 this.centerAlignToolButton,
																							 this.rightAlignToolButton,
																							 this.unnumberedBulletToolButton,
																							 this.numberedBulletToolButton,
																							 this.bulletsSeperator,
																							 this.findToolButton,
																							 this.findNextToolButton,
																							 this.findSeperator,
																							 this.undoToolButton,
																							 this.redoToolButton,
																							 this.editingSeperator1,
																							 this.cutToolButton,
																							 this.copyToolButton,
																							 this.pasteToolButton,
																							 this.editingSeperator2,
																							 this.hyperLinkToolButton,
																							 this.spellCheckToolButton,
																							 this.clearToolButton,
																							 this.insertDateToolButton,
																							 this.wordwrapToolButton});
			this.editorToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorToolbar.DropDownArrows = true;
			this.editorToolbar.ImageList = this.mainImageList;
			this.editorToolbar.Location = new System.Drawing.Point(0, 0);
			this.editorToolbar.Name = "editorToolbar";
			this.editorToolbar.ShowToolTips = true;
			this.editorToolbar.Size = new System.Drawing.Size(432, 72);
			this.editorToolbar.TabIndex = 2;
			this.editorToolbar.SizeChanged += new System.EventHandler(this.editorToolbar_SizeChanged);
			this.editorToolbar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.editorToolbar_ButtonClick);
			// 
			// wordExportToolButton
			// 
			this.wordExportToolButton.ImageIndex = 31;
			this.wordExportToolButton.ToolTipText = "Edit this document to Microsoft Word";
			// 
			// printToolButton
			// 
			this.printToolButton.ImageIndex = 1;
			this.printToolButton.ToolTipText = "Print";
			// 
			// printPreviewToolButton
			// 
			this.printPreviewToolButton.ImageIndex = 0;
			this.printPreviewToolButton.ToolTipText = "Print Preview";
			// 
			// saveAsToolButton
			// 
			this.saveAsToolButton.ImageIndex = 32;
			this.saveAsToolButton.ToolTipText = "Save As";
			// 
			// emailToolButton
			// 
			this.emailToolButton.ImageIndex = 2;
			// 
			// toolBarButton5
			// 
			this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// boldToolButton
			// 
			this.boldToolButton.ImageIndex = 4;
			this.boldToolButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// underlineToolButton
			// 
			this.underlineToolButton.ImageIndex = 27;
			this.underlineToolButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// italicToolButton
			// 
			this.italicToolButton.ImageIndex = 14;
			this.italicToolButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// fontToolButton
			// 
			this.fontToolButton.ImageIndex = 12;
			// 
			// formatingSeperator1
			// 
			this.formatingSeperator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// leftAlignToolButton
			// 
			this.leftAlignToolButton.ImageIndex = 16;
			this.leftAlignToolButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// centerAlignToolButton
			// 
			this.centerAlignToolButton.ImageIndex = 30;
			this.centerAlignToolButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// rightAlignToolButton
			// 
			this.rightAlignToolButton.ImageIndex = 23;
			this.rightAlignToolButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// unnumberedBulletToolButton
			// 
			this.unnumberedBulletToolButton.ImageIndex = 19;
			this.unnumberedBulletToolButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// numberedBulletToolButton
			// 
			this.numberedBulletToolButton.ImageIndex = 20;
			// 
			// bulletsSeperator
			// 
			this.bulletsSeperator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// findToolButton
			// 
			this.findToolButton.ImageIndex = 3;
			// 
			// findNextToolButton
			// 
			this.findNextToolButton.ImageIndex = 11;
			// 
			// findSeperator
			// 
			this.findSeperator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// undoToolButton
			// 
			this.undoToolButton.ImageIndex = 28;
			this.undoToolButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.undoToolButton.ToolTipText = "Undo last operation";
			// 
			// redoToolButton
			// 
			this.redoToolButton.ImageIndex = 22;
			this.redoToolButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.redoToolButton.ToolTipText = "Redo last operation";
			// 
			// editingSeperator1
			// 
			this.editingSeperator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// cutToolButton
			// 
			this.cutToolButton.ImageIndex = 9;
			this.cutToolButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// copyToolButton
			// 
			this.copyToolButton.ImageIndex = 8;
			this.copyToolButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// pasteToolButton
			// 
			this.pasteToolButton.ImageIndex = 21;
			this.pasteToolButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// editingSeperator2
			// 
			this.editingSeperator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// hyperLinkToolButton
			// 
			this.hyperLinkToolButton.ImageIndex = 29;
			// 
			// spellCheckToolButton
			// 
			this.spellCheckToolButton.ImageIndex = 25;
			// 
			// clearToolButton
			// 
			this.clearToolButton.ImageIndex = 17;
			this.clearToolButton.ToolTipText = "Remove everything from this document";
			// 
			// insertDateToolButton
			// 
			this.insertDateToolButton.ImageIndex = 10;
			// 
			// wordwrapToolButton
			// 
			this.wordwrapToolButton.ImageIndex = 33;
			this.wordwrapToolButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.wordwrapToolButton.ToolTipText = "Turn on/off wordwrap";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.innerRichTextBox);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 57);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(432, 143);
			this.panel2.TabIndex = 3;
			// 
			// innerRichTextBox
			// 
			this.innerRichTextBox.AcceptsTab = true;
			this.innerRichTextBox.AutoWordSelection = true;
			this.innerRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.innerRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.innerRichTextBox.HideSelection = false;
			this.innerRichTextBox.Location = new System.Drawing.Point(0, 0);
			this.innerRichTextBox.Name = "innerRichTextBox";
			this.innerRichTextBox.ShowSelectionMargin = true;
			this.innerRichTextBox.Size = new System.Drawing.Size(432, 143);
			this.innerRichTextBox.TabIndex = 2;
			this.innerRichTextBox.Text = "richTextBox1";
			this.innerRichTextBox.WordWrap = false;
			this.innerRichTextBox.ReadOnlyChanged += new System.EventHandler(this.innerRichTextBox_ReadOnlyChanged);
			this.innerRichTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.innerRichTextBox_LinkClicked);
			this.innerRichTextBox.SelectionChanged += new System.EventHandler(this.innerRichTextBox_SelectionChanged);
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 56);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(432, 1);
			this.splitter1.TabIndex = 4;
			this.splitter1.TabStop = false;
			// 
			// fontDialog1
			// 
			this.fontDialog1.ShowApply = true;
			this.fontDialog1.ShowColor = true;
			this.fontDialog1.Apply += new System.EventHandler(this.fontDialog1_Apply);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "rtf";
			this.saveFileDialog1.Filter = "Rich Text File (*.rtf)|*.rtf|Rich Text File Without Ole Objects (*.rtf)|*.rtf|Tex" +
				"t File (*.txt)|*.txt|Text File Without Ole Objects (*.txt)|*.txt|ANSI Text File " +
				"(*.txt)|*.txt|All Files|*.*";
			this.saveFileDialog1.Title = "Save This Document As";
			// 
			// RichTextEditor
			// 
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.toolbarPanel);
			this.Name = "RichTextEditor";
			this.Size = new System.Drawing.Size(432, 200);
			this.toolbarPanel.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
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
			}
		}
		bool IContentEditor.IsModified
		{
			get
			{
				return innerRichTextBox.Modified;
			}
		}
		
		bool IContentEditor.IsReadOnly
		{
			get
			{
				return innerRichTextBox.ReadOnly;
			}
			set
			{
				innerRichTextBox.ReadOnly = value;
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
			if (m_currentFormat == ContentFormats.FORMAT_RTF)
				return innerRichTextBox.Rtf;
			else
				return innerRichTextBox.Text;
		}
		
		void IContentEditor.SetContentInfo(string noteID, string noteCaption, string noteContent)
		{
			((IContentEditor) this).ContentID = noteID;
			((IContentEditor) this).ContentCaption = noteCaption;

			if (m_currentFormat == ContentFormats.FORMAT_RTF)
			{
				SetRTFValueSafe(noteContent);
			}
			else
				innerRichTextBox.Text = noteContent;
			
			SetupUIForFormat(ContentFormat);
		}
		void IContentEditor.ClearContent()
		{
			innerRichTextBox.Text = "";
			
		}
		void IContentEditor.ResetAll()
		{
			((IContentEditor)this).ClearContent();
			m_currentNoteCaption = "";
			m_currentNoteID = "";
		}
		#endregion

		#region New members
		public string ContentFormat
		{
			get
			{
				return m_currentFormat;
			}
			set
			{
				if ((value == ContentFormats.FORMAT_RTF) || (value == ContentFormats.FORMAT_TEXT))
				{
					m_currentFormat = value;
				}
				else
					throw new ArgumentException("Specified format '"+ value +"' is not recognized by '"+ this.Name + "'. Only RTF and TEXT formats are acceptable", "CurrentFormat");
				
				//Update UI
				SetupUIForFormat(m_currentFormat);
			}
		}
		#endregion

		
		private void SetRTFValueSafe(string rtfValue)
		{
			try
			{
				innerRichTextBox.Rtf = rtfValue;
			}
			catch (ArgumentException)	//Invalid RTF format
			{
				innerRichTextBox.Text = rtfValue;
			}
		}

		public void ToggleBold()
		{
			if (innerRichTextBox.SelectionFont != null)
				innerRichTextBox.SelectionFont = new Font(innerRichTextBox.SelectionFont, innerRichTextBox.SelectionFont.Bold ? (~FontStyle.Bold & innerRichTextBox.SelectionFont.Style)  : (innerRichTextBox.SelectionFont.Style | FontStyle.Bold));
		}

		public void ToggleItalic()
		{
			if (innerRichTextBox.SelectionFont != null)
				innerRichTextBox.SelectionFont = new Font(innerRichTextBox.SelectionFont, innerRichTextBox.SelectionFont.Italic ? (~FontStyle.Italic & innerRichTextBox.SelectionFont.Style)  : (innerRichTextBox.SelectionFont.Style | FontStyle.Italic));
		}

		public void ToggleUnderline()
		{
			if (innerRichTextBox.SelectionFont != null)
				innerRichTextBox.SelectionFont = new Font(innerRichTextBox.SelectionFont, innerRichTextBox.SelectionFont.Underline ? (~FontStyle.Underline & innerRichTextBox.SelectionFont.Style)  : (innerRichTextBox.SelectionFont.Style | FontStyle.Underline));
		}
		
		public void SelectFont()
		{
			if (innerRichTextBox.SelectionFont != null)
			{
				Font oldFont = innerRichTextBox.SelectionFont;
				fontDialog1.Font = innerRichTextBox.SelectionFont;
				
				if (fontDialog1.ShowDialog()==DialogResult.OK)
				{
					innerRichTextBox.SelectionFont = fontDialog1.Font;
				}
				else
					innerRichTextBox.SelectionFont = oldFont;	//rollback changes done by Apply button in font dialog
			}
		}		
		
		public void ToggleUnnumberedBullet()
		{
			innerRichTextBox.SelectionBullet = !innerRichTextBox.SelectionBullet;
		}
		
		public void ToggleLeftAlign()
		{
			innerRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
		}
		public void ToggleRightAlign()
		{
			if (innerRichTextBox.SelectionAlignment != HorizontalAlignment.Right)
				innerRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
			else
				innerRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
		}
		public void ToggleCenterAlign()
		{
			if (innerRichTextBox.SelectionAlignment != HorizontalAlignment.Center)
				innerRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
			else
				innerRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
		}
		
		public void SelectFileSaveAs()
		{
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				RichTextBoxStreamType selectedStreamType;
				switch (saveFileDialog1.FilterIndex)
				{
					case 1:	//RTF with OLE obj
						selectedStreamType = RichTextBoxStreamType.RichText;
						break;
					case 2:	//RTF without OLE obj
						selectedStreamType = RichTextBoxStreamType.RichNoOleObjs;
						break;
					case 3:	//Unicode text with ole obj
						selectedStreamType = RichTextBoxStreamType.UnicodePlainText;
						break;
					case 4:	//ANSI text with ole object
						selectedStreamType = RichTextBoxStreamType.TextTextOleObjs;
						break;
					case 5:	//ANSI text without ole object
						selectedStreamType = RichTextBoxStreamType.PlainText;
						break;
					default:
						throw new ArgumentOutOfRangeException("saveFileDialog1.FilterIndex", saveFileDialog1.FilterIndex,"File type you have selected is not implemented yet. RTF format will be selected by default.");
				}
				innerRichTextBox.SaveFile(saveFileDialog1.FileName, selectedStreamType);
			}
		}
		
		private void OpenInWord()
		{
			string tempFileName = Path.GetTempFileName() + "_NotepadX.rtf";
			innerRichTextBox.SaveFile(tempFileName, RichTextBoxStreamType.RichText);
			
			if (innerRichTextBox.ReadOnly == false)
				MessageBox.Show ("Edit this document in Word. When finished, close Word to return back.");
			else
				MessageBox.Show ("You can now view this document in Word. Any changes made will not be saved because this is read-only document.");
			
			Process wordProcess = System.Diagnostics.Process.Start(tempFileName);

			if (innerRichTextBox.ReadOnly == false)
			{
				wordProcess.WaitForExit();
				LoadFileSafe(tempFileName);	
			}
		}
		
		public RichTextBox ContainedRichTextBox
		{
			get
			{
				return innerRichTextBox;
			}
		}
		
		private void LoadFileSafe(string fileName)
		{
			try
			{
				innerRichTextBox.LoadFile(fileName);		
			}
			catch (Exception ex)
			{
				MessageBox.Show("Can not load file in to editor. File: '"+ fileName +"', error: " + ex.ToString());
			}
		}

		private void SetupUIForFormat(string currentFormat)
		{
			bool isPlainTextFormat = (currentFormat == ContentFormats.FORMAT_TEXT);
			
			//Not supported functions are made invisible in all case
			printToolButton.Visible = false;
			printPreviewToolButton.Visible = false;	
			this.emailToolButton.Visible = false;
			this.findNextToolButton.Visible = false;
			this.findToolButton.Visible = false;
			this.hyperLinkToolButton.Visible = false;
			this.insertDateToolButton.Visible = false;
			this.numberedBulletToolButton.Visible = false;
			this.spellCheckToolButton.Visible = false;
			this.findSeperator.Visible = false;
			
			bool isEnableFormatingFunction = (!isPlainTextFormat && !innerRichTextBox.ReadOnly);
			this.centerAlignToolButton.Visible = isEnableFormatingFunction;
			this.fontToolButton.Visible = isEnableFormatingFunction;
			this.italicToolButton.Visible = isEnableFormatingFunction;
			this.leftAlignToolButton.Visible = isEnableFormatingFunction;
			this.rightAlignToolButton.Visible = isEnableFormatingFunction;
			this.underlineToolButton.Visible = isEnableFormatingFunction;
			this.unnumberedBulletToolButton.Visible = isEnableFormatingFunction;
			this.boldToolButton.Visible = isEnableFormatingFunction;
			this.formatingSeperator1.Visible = isEnableFormatingFunction;
			this.bulletsSeperator.Visible = isEnableFormatingFunction;

			bool isEnableEditingFunction = !innerRichTextBox.ReadOnly;
			copyToolButton.Visible = isEnableEditingFunction;
			pasteToolButton.Visible = isEnableEditingFunction;
			cutToolButton.Visible = isEnableEditingFunction;
			editingSeperator1.Visible = isEnableEditingFunction;
			editingSeperator2.Visible = isEnableEditingFunction;
			undoToolButton.Visible = isEnableEditingFunction;
			redoToolButton.Visible = isEnableEditingFunction;
			clearToolButton.Visible = isEnableEditingFunction;
			
			SetPushButtonState();
		}
		
		private void editorToolbar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if (e.Button == boldToolButton)
				ToggleBold();
			else if (e.Button == italicToolButton)
				ToggleItalic();
			else if (e.Button == underlineToolButton)
				ToggleUnderline();
			else if (e.Button == fontToolButton)
				SelectFont();
			else if (e.Button == unnumberedBulletToolButton)
				ToggleUnnumberedBullet();
			else if (e.Button == undoToolButton)
			{
				innerRichTextBox.Undo();				
				SetHazyButtonState();
			}			
			else if (e.Button == redoToolButton)
			{
				innerRichTextBox.Redo();				
				SetHazyButtonState();
			}			
			else if (e.Button == copyToolButton)
			{
				innerRichTextBox.Copy();
				SetHazyButtonState();
			}			
			else if (e.Button == pasteToolButton)
			{
				innerRichTextBox.Paste();
				SetHazyButtonState();
			}			
			else if (e.Button == cutToolButton)
			{
				innerRichTextBox.Cut();
				SetHazyButtonState();
			}			
			else if (e.Button == leftAlignToolButton)
			{
				ToggleLeftAlign();
				if (leftAlignToolButton.Pushed == true)
				{
					rightAlignToolButton.Pushed = false;
					centerAlignToolButton.Pushed = false;
				}
			}
			else if (e.Button == rightAlignToolButton)
			{
				ToggleRightAlign();
				if (rightAlignToolButton.Pushed == true)
				{
					leftAlignToolButton.Pushed = false;
					centerAlignToolButton.Pushed = false;
				}			
			}
			else if (e.Button == centerAlignToolButton)
			{
				ToggleCenterAlign();
				if (centerAlignToolButton.Pushed == true)
				{
					leftAlignToolButton.Pushed = false;
					rightAlignToolButton.Pushed = false;
				}					
			}
			else if (e.Button == saveAsToolButton)
				SelectFileSaveAs();
			else if (e.Button == wordExportToolButton)
				OpenInWord();
			else if (e.Button == clearToolButton)
				((IContentEditor) this).ClearContent();
			else if (e.Button == wordwrapToolButton)
				ToggleWordwrap();
			else 
			{
				MessageBox.Show ("This function ('"+ e.Button.ToString() +"') is not done yet");
			}
		}

		private void editorToolbar_SizeChanged(object sender, System.EventArgs e)
		{
			toolbarPanel.Size = editorToolbar.Size;		
		}

		private void fontDialog1_Apply(object sender, System.EventArgs e)
		{
			innerRichTextBox.SelectionFont = fontDialog1.Font;		
		}

		private void innerRichTextBox_ReadOnlyChanged(object sender, System.EventArgs e)
		{
			if (innerRichTextBox.ReadOnly == true)
			{
				innerRichTextBox.BackColor = Color.FromKnownColor(KnownColor.Silver);
			}
			else
			{
				innerRichTextBox.BackColor = Color.FromKnownColor(KnownColor.Window);
			}
			SetupUIForFormat(ContentFormat);
		}

		private void SetPushButtonState()
		{
			if (innerRichTextBox.SelectionFont != null)
			{
				boldToolButton.Pushed = innerRichTextBox.SelectionFont.Bold;
				italicToolButton.Pushed  = innerRichTextBox.SelectionFont.Italic;
				underlineToolButton.Pushed  = innerRichTextBox.SelectionFont.Underline;
				unnumberedBulletToolButton.Pushed = innerRichTextBox.SelectionBullet;
				rightAlignToolButton.Pushed = (innerRichTextBox.SelectionAlignment == HorizontalAlignment.Right);
				leftAlignToolButton.Pushed = (innerRichTextBox.SelectionAlignment == HorizontalAlignment.Left );
				centerAlignToolButton.Pushed = (innerRichTextBox.SelectionAlignment == HorizontalAlignment.Center );
			}
			else
			{
				boldToolButton.Pushed = false;
				italicToolButton.Pushed  = false;
				underlineToolButton.Pushed  = false;
				unnumberedBulletToolButton.Pushed = false;
				rightAlignToolButton.Pushed = false;
				leftAlignToolButton.Pushed = false;
				centerAlignToolButton.Pushed = false;
			}
			wordwrapToolButton.Pushed = innerRichTextBox.WordWrap;
			SetHazyButtonState();
		}

		private void ToggleWordwrap()
		{
			innerRichTextBox.WordWrap = !innerRichTextBox.WordWrap;
			SetPushButtonState();		
		}

		private void SetHazyButtonState()
		{
			//These buttons are not toggle buttons but for hazzy look we r doing this
			redoToolButton.Pushed  = false;
			undoToolButton.Pushed = false;
			copyToolButton.Pushed = false;
			cutToolButton.Pushed = false;
			
			redoToolButton.ToolTipText = (innerRichTextBox.RedoActionName=="")?("Nothing to redo"):("Redo " + innerRichTextBox.RedoActionName);
			undoToolButton.ToolTipText = (innerRichTextBox.UndoActionName=="")?("Nothing to undo"):("Undo " + innerRichTextBox.UndoActionName);
			
			redoToolButton.PartialPush = !innerRichTextBox.CanRedo;
			undoToolButton.PartialPush = !innerRichTextBox.CanUndo;
			copyToolButton.PartialPush = !(innerRichTextBox.SelectionLength > 0);
			cutToolButton.PartialPush = !(innerRichTextBox.SelectionLength > 0);
		}

		private void innerRichTextBox_SelectionChanged(object sender, System.EventArgs e)
		{
			SetPushButtonState();		
		}

		private void innerRichTextBox_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.LinkText);
		}

	}
}
