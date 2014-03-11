using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Sytel.Common.ContentManagement;
using Sytel.Common.WinForms.ContentEditors;




namespace Sytel.Common.WinForms
{
	/// <summary>
	/// Summary description for EditableTreeViewControl.
	/// </summary>
	
	public class ChooseNewContentFormatEventArgs : KeyEventArgs 
	{
		public string SelectedNewContentFormat = ContentFormats.FORMAT_TEXT;
		public ChooseNewContentFormatEventArgs(KeyEventArgs e) : base(e.KeyData)
		{
			
		}
	}
	
	public delegate void TreeReloadHandler(object sender, EventArgs e);
	public delegate void ChooseNewContentFormatHandler(object sender, ChooseNewContentFormatEventArgs e);
	public class EditableTreeViewControl : System.Windows.Forms.TreeView 
	{
		public event TreeReloadHandler TreeReload = null;
		public event ChooseNewContentFormatHandler ChooseNewContentFormat = null;
		public readonly string InstanceId = "";

		private HierarchicalContentDocument m_NotepadXDocument = null;
		private string m_DefaultNewNoteTitle = "*New*";

		#region Standard event wiring code
		protected void RaiseTreeReloadEvent(EventArgs e)
		{
			OnTreeReload(e);	//Let derived class run it's event handler
			if (TreeReload != null) TreeReload(this, e);
		}
		protected void RaiseChooseNewContentFormatEvent(ChooseNewContentFormatEventArgs e)
		{
			OnChooseNewContentFormat(e);
			if (ChooseNewContentFormat != null) ChooseNewContentFormat(this, e);
		}
		protected virtual void OnTreeReload(EventArgs e)
		{
		}
		protected virtual void OnChooseNewContentFormat(ChooseNewContentFormatEventArgs e)
		{
		}
		#endregion
		
		public HierarchicalContentDocument CatNoteDocumentToUse
		{
			get
			{
				return m_NotepadXDocument;
			}
			set
			{
				m_NotepadXDocument = value;
			}
		}
		
		public EditableTreeViewControl()
		{
			InstanceId = Guid.NewGuid().ToString();
			InitializeComponent();
		}
		
		#region Base event overrides
		protected override void OnAfterLabelEdit ( System.Windows.Forms.NodeLabelEditEventArgs e )
		{
			base_AfterLabelEdit(this, e);
		}
		
		protected override void OnDoubleClick ( System.EventArgs e )
		{
			base_DoubleClick(this, e);
		}
		
		protected override void OnKeyUp ( System.Windows.Forms.KeyEventArgs e )
		{
			base_KeyUp(this, e);
		}
		protected override void OnItemDrag ( System.Windows.Forms.ItemDragEventArgs e )
		{
			base_ItemDrag(this, e);
			base.OnItemDrag(e);
		}
		protected override void OnDragOver ( System.Windows.Forms.DragEventArgs e)
		{
			base_DragOver (this, e);
			base.OnDragOver(e);
		}
		protected override void OnDragDrop ( System.Windows.Forms.DragEventArgs e)
		{
			base_DragDrop(this, e);
			base.OnDragDrop(e);
		}
		protected override void OnAfterExpand ( System.Windows.Forms.TreeViewEventArgs e )
		{
			base_AfterExpand(this, e);
		}
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base_MouseDown(this, e);
		}
		protected override void OnAfterCollapse ( System.Windows.Forms.TreeViewEventArgs e )
		{
			base_AfterCollapse(this, e);
		}

		#endregion
		
		public string GetIdForSelectedNode()
		{
			string selectedNoteId = string.Empty;
			if (this.SelectedNode != null)
			{
				selectedNoteId = (string) this.SelectedNode.Tag;
			}
			
			return selectedNoteId;
		}

		public ContentDetail GetContentDetailForSelectedNode()
		{
			return GetContentDetailForNode(this.SelectedNode);
		}

		public void SelectTreeNode(TreeNode nodeToSelect)
		{
			if (nodeToSelect != null)
			{
				nodeToSelect.EnsureVisible();
				this.SelectedNode = nodeToSelect;
				//nodeToSelect.Expand();
			}
			
			OnAfterSelect(new TreeViewEventArgs(nodeToSelect));
		}

		public TreeNode RefreshTreeFromCatNoteDocumentAndSelectNote(string noteIdToSelect)
		{
			TreeNode nodeToSelect = RefreshTreeFromCatNoteDocument(noteIdToSelect);
			SelectTreeNode(nodeToSelect);
			return nodeToSelect;
		}

		public bool IsDirty
		{
			get
			{
				return m_NotepadXDocument.IsDirty;
			}
		}

		public void Clear()
		{
			m_NotepadXDocument.MakeBlankDocument();
			RefreshTreeFromCatNoteDocument(String.Empty);
		}

		public bool IsReadOnly
		{
			get
			{
				return m_NotepadXDocument.IsReadonly;
			}
			set
			{
				m_NotepadXDocument.IsReadonly = value;
				this.LabelEdit = !m_NotepadXDocument.IsReadonly;
			}
		}
		
		public string Xml
		{
			get
			{
				return m_NotepadXDocument.Xml;
			}
			set
			{
				m_NotepadXDocument.Xml = value;
				RefreshTreeWithSelectionIntactOrSelectRoot();
			}
		}
		
		public TreeNode RefreshTreeWithSelectionIntactOrSelectRoot()
		{
			if (this.SelectedNode != null)
				return RefreshTreeWithSelectionIntact();
			else
				return RefreshTreeFromCatNoteDocumentAndSelectNote(m_NotepadXDocument.Configuration.RootNoteId);
		}
		
		public TreeNode RefreshTreeFromCatNoteDocument(string treeNodeIdToReturn)
		{
			this.Nodes.Clear();

			RaiseTreeReloadEvent(null);			
			
			SelectTreeNode(null);
			
			TreeNode rootNode = CreateTreeNodeForCatNoteDetail(this.Nodes, m_NotepadXDocument.GetNoteById(m_NotepadXDocument.Configuration.RootNoteId));

			//Populate root node
			TreeNode treeNodeFoundForId = LoadTreeNodes(m_NotepadXDocument.GetChildNotes(m_NotepadXDocument.Configuration.RootNoteId), this.Nodes[0], treeNodeIdToReturn);
			
			if (treeNodeIdToReturn == rootNode.Tag.ToString())
				return rootNode;
			else
				return treeNodeFoundForId;
		}

		public ContentDetail GetContentDetailForNode(TreeNode thisNode)
		{
			if (thisNode != null)
			{
				return m_NotepadXDocument.GetNoteById((string) thisNode.Tag);
			}
			else return null;
		}

		private void base_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
		{
			//System.Diagnostics.Debug.WriteLine ("AfterLeabelEdit fired");
			if ((e.Node != null) && (e.Label != null))
			{
				//Get Node Id
				string selectedNodeId = (string) e.Node.Tag;
				 
				ContentDetail selectedCatNoteDetail = m_NotepadXDocument.GetNoteById(selectedNodeId);
				selectedCatNoteDetail.Title = e.Label;
			}
			else {};	//don't know why this should happen but it did once
		}
		
		public TreeNode RefreshTreeWithSelectionIntact()
		{
			TreeNode previouslySelectedNode = this.SelectedNode;
			string previouslySelectedNodeId = null;
			if (previouslySelectedNode != null)
				previouslySelectedNodeId = (string) previouslySelectedNode.Tag;
			
			TreeNode newNodeToSelect = RefreshTreeFromCatNoteDocument(previouslySelectedNodeId);
			
			SelectTreeNode(newNodeToSelect);
			
			return newNodeToSelect;
		}
		
		public void StartNodeTitleEdit(TreeNode nodeToEdit)
		{
			//TODO : it's better to show a message if readonly
			if (nodeToEdit != null)
			{
				if (GetContentDetailForNode(nodeToEdit).IsReadOnly == false)
					nodeToEdit.BeginEdit();
			}
		}
		
		private void base_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			//TODO : it's better to show a message if readonly
			if (m_NotepadXDocument.IsReadonly==false)
			{
				//System.Diagnostics.Debug.WriteLine ("KeyUp fired");
				if (e.KeyCode == Keys.F2)
				{
					StartNodeTitleEdit(this.SelectedNode);
				}
				else if (e.KeyCode == Keys.Insert)
				{
					ChooseNewContentFormatEventArgs newNoteFormatEventArgs = new ChooseNewContentFormatEventArgs(e);
					RaiseChooseNewContentFormatEvent(newNoteFormatEventArgs);
					AddNewChildNoteForSelectedNode(newNoteFormatEventArgs.SelectedNewContentFormat);
				}
				else if (e.KeyCode == Keys.Delete)
				{
					RemoveNoteForSelectedNote();
				}
			}
			else {}; //ignore keystroks in read-only mode
		}
		
		private void base_DoubleClick(object sender, System.EventArgs e)
		{
			StartNodeTitleEdit(this.SelectedNode);
		}
		private void base_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine ("ItemDrag fired");
			TreeNode draggedNode = (TreeNode) e.Item;
			ContentDetail noteForDraggedNode = GetContentDetailForNode(draggedNode);
			this.DoDragDrop(new DraggedNodeData(this.InstanceId,noteForDraggedNode.Id, noteForDraggedNode.Xml), DragDropEffects.Move | DragDropEffects.Copy);
		}
		private void base_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine ("DragOver fired");
			if (e.Data.GetDataPresent(typeof(DraggedNodeData).FullName, false)==true)
			{
				DraggedNodeData nodeData = (DraggedNodeData) e.Data.GetData(typeof(DraggedNodeData).FullName, false);
				if (nodeData.DraggedTreeEditorId == InstanceId)
					e.Effect = DragDropEffects.Move;
				else
					e.Effect = DragDropEffects.Copy;
			}
			else if (e.Data.GetDataPresent(typeof(string))==true)
			{
				string draggedText = (string) e.Data.GetData(typeof(string));
				if (draggedText != null)
					e.Effect = DragDropEffects.Copy;
				else
					e.Effect = DragDropEffects.None;
			}
			else
				e.Effect = DragDropEffects.None;
		}

		private void base_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			TreeNode destinationNode = this.GetNodeAt(this.PointToClient(new System.Drawing.Point(e.X, e.Y)));
			string destinationNoteId = (string) destinationNode.Tag;
			
			bool isNoDropDone = false;
			if (e.Data.GetDataPresent(typeof(DraggedNodeData).FullName, false)==true)
			{
				DraggedNodeData nodeData = (DraggedNodeData) e.Data.GetData(typeof(DraggedNodeData).FullName, false);
				if (nodeData.DraggedTreeEditorId == InstanceId)
				{
					try
					{
						m_NotepadXDocument.MoveNoteById(nodeData.DraggedNodeId, destinationNoteId);
					}
					catch(ArgumentException moveException)
					{
						if (moveException.ParamName != "InvalidSourceId")
							MessageBox.Show("Can not move this note because this note is a parent of the note where you want to move it");
					}
				}
				else
					m_NotepadXDocument.CopyNoteByContent(nodeData.DraggedNodeXml, destinationNoteId);
			}
			else if (e.Data.GetDataPresent(typeof(string))==true)
			{
				string draggedText = (string) e.Data.GetData(typeof(string));
				if (draggedText != null)
				{
					if (MessageBox.Show ("Do you wish to create new note?", "Create Note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					{
						AddNewChildNoteForTreeNode(destinationNoteId, draggedText, ContentFormats.FORMAT_DEFAULT);
					}
				}
				else
				{
					isNoDropDone = true;
				};	//ignore the drag drop for null texts
			}
			else
			{
				isNoDropDone = true;
			};	//ignore the drag drop for other formats
			
			if (isNoDropDone == false)
			{
				destinationNode = RefreshTreeFromCatNoteDocument(destinationNoteId);
				SelectTreeNode(destinationNode);
			}
		}

		public string DefaultNewNoteTitle
		{
			get
			{
				return m_DefaultNewNoteTitle;
			}
			set
			{
				m_DefaultNewNoteTitle = value;
			}
		}

		private void base_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (e.Node != null)
			{
				//Get Node Id
				string selectedNodeId = (string) e.Node.Tag;
				 
				ContentDetail selectedCatNoteDetail = m_NotepadXDocument.GetNoteById(selectedNodeId);
				selectedCatNoteDetail.ExpandNodeByDefault = true;
			}
			else {};	//don't know why this should happen but it did once
		}
		private void base_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e )
		{
			if (e.Button==MouseButtons.Right)
			{
				TreeNode nodeUnderMouse = this.GetNodeAt(e.X, e.Y);
				this.SelectTreeNode( nodeUnderMouse);
			}
			else
			{
				//Ignore for left mouse clicks
			}
			base.OnMouseDown(e);
		}
		private void base_AfterCollapse(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (e.Node != null)
			{
				//Get Node Id
				string selectedNodeId = (string) e.Node.Tag;
				 
				ContentDetail selectedCatNoteDetail = m_NotepadXDocument.GetNoteById(selectedNodeId);
				selectedCatNoteDetail.ExpandNodeByDefault = false;
			}
			else {};	//don't know why this should happen but it did once
		}

		private void UpdateNodeFromCatNoteDetail(TreeNode nodeToUpdate, ContentDetail sourceNote)
		{
			nodeToUpdate.BackColor = sourceNote.NodeBackColor;
			nodeToUpdate.ForeColor = sourceNote.NodeForeColor;
			if (sourceNote.NodeFont != null)
				nodeToUpdate.NodeFont = sourceNote.NodeFont;
			nodeToUpdate.Text = sourceNote.Title;
			if (sourceNote.ExpandNodeByDefault == true)
			{
				bool allowExpand;
				if (nodeToUpdate.Parent != null)
					allowExpand = nodeToUpdate.Parent.IsExpanded;
				else
					allowExpand = true;
						
				if (allowExpand == true)
					nodeToUpdate.Expand();
			}
		}

		public TreeNode CreateTreeNodeForCatNoteDetail(TreeNodeCollection  parentTreeNodeCollection, ContentDetail thisCatNoteDetail)
		{
			TreeNode newNode = parentTreeNodeCollection.Add(thisCatNoteDetail.Title);
			newNode.Tag = thisCatNoteDetail.Id;
			UpdateNodeFromCatNoteDetail(newNode, thisCatNoteDetail);
			return newNode;
		}

		public void UpdateTreeNode(ContentDetail noteDetail)
		{
			TreeNode nodeToUpdate = FindNodeForId(noteDetail.Id);
			if (nodeToUpdate != null)
			{
				UpdateNodeFromCatNoteDetail(nodeToUpdate, noteDetail);
			}
		}

		public TreeNode FindNodeForId(string id)
		{
			return FindNodeForIdInNodes(id, this.Nodes);
		}
		public TreeNode FindNodeForIdInNodes(string id, TreeNodeCollection nodesToSearch)
		{
			foreach(TreeNode nodeToExamin in nodesToSearch)
			{
				if (((string) nodeToExamin.Tag) == id)
				{
					return nodeToExamin;
				}
				else 
				{
					TreeNode foundNode = FindNodeForIdInNodes(id, nodeToExamin.Nodes);
					if (foundNode != null)
						return foundNode;	//for loop
				}
			}
			return null;
		}

		public void AddNewChildNoteForTreeNode(string noteId, string noteContent, string noteFormat)
		{
			//Get child notes
			ContentDetailCollection childsForSelectedNote = m_NotepadXDocument.GetChildNotes(noteId);
			ContentDetail newNote = childsForSelectedNote.Add ();
			newNote.Title = DefaultNewNoteTitle;
			newNote.Content = noteContent;
			newNote.Format = noteFormat;
				
			TreeNode newAddedNode = RefreshTreeFromCatNoteDocumentAndSelectNote(newNote.Id);
			if (newAddedNode != null) newAddedNode.BeginEdit();
		}

		public void AddNewChildNoteForSelectedNode()
		{
			AddNewChildNoteForSelectedNode(ContentFormats.FORMAT_DEFAULT);
		}

		public void AddNewChildNoteForSelectedNode(string noteFormat)
		{
			string selectedNoteId = GetIdForSelectedNode();
			
			if (selectedNoteId == string.Empty)
			{
				MessageBox.Show ("Please select a category in to which you wish to add new note");
			}
			else
			{
				AddNewChildNoteForTreeNode(selectedNoteId, string.Empty, noteFormat);
			}
		}

		public void RemoveNoteForSelectedNote()
		{
			string selectedNoteId = GetIdForSelectedNode();

			if (selectedNoteId == string.Empty)
			{
				MessageBox.Show ("Please select a category which you wish to remove");
			}
			else
			{
				//Get this note and delete it
				ContentDetail noteDetailToDelete = m_NotepadXDocument.GetNoteById(selectedNoteId);
				noteDetailToDelete.Remove();
				
				TreeNode previousVisibleNode = this.SelectedNode.PrevVisibleNode;
				string previousVisibleNodeId;
				if (previousVisibleNode != null)
					previousVisibleNodeId = (string) previousVisibleNode.Tag;
				else
					previousVisibleNodeId = m_NotepadXDocument.Configuration.RootNoteId;
				
				RefreshTreeFromCatNoteDocumentAndSelectNote(previousVisibleNodeId);
			}
		}

		private void InitializeComponent()
		{
			// 
			// EditableTreeViewControl
			// 
			this.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.LabelEdit = true;
			this.AllowDrop = true;
			this.HideSelection = false;
		}

		public TreeNode LoadTreeNodes(ContentDetailCollection notesCollectionForCategory, TreeNode rootNodeForCategory, string treeNodeIdToReturn)
		{
			TreeNode treeNodeFoundForId = null;
			if (notesCollectionForCategory != null)
			{
				foreach(DictionaryEntry thisCatNoteDetailDictionaryEntry in notesCollectionForCategory)
				{
					ContentDetail  thisCatNoteDetailNode = (ContentDetail) thisCatNoteDetailDictionaryEntry.Value;
					
					TreeNode newNode = CreateTreeNodeForCatNoteDetail(rootNodeForCategory.Nodes, thisCatNoteDetailNode);
					
					//Get category childs and recurse to fill childs
					TreeNode treeNodeFoundForIdFromChilds = LoadTreeNodes(m_NotepadXDocument.GetChildNotes(thisCatNoteDetailNode.Id), newNode, treeNodeIdToReturn);
		
					if (treeNodeIdToReturn == thisCatNoteDetailNode.Id)
						treeNodeFoundForId = newNode;
					else if (treeNodeFoundForIdFromChilds != null)
						treeNodeFoundForId = treeNodeFoundForIdFromChilds;
				}
			}			
			return treeNodeFoundForId;
		}

		[Serializable()]
		public class DraggedNodeData
		{
			public string DraggedTreeEditorId ="";
			public string DraggedNodeId ="";
			public string DraggedNodeXml = "";
			
			public DraggedNodeData(string treeEditorID, string nodeId, string nodeXml)
			{
				DraggedTreeEditorId = treeEditorID;
				DraggedNodeId = nodeId;
				DraggedNodeXml = nodeXml;
			}
		}
		
	}
}
