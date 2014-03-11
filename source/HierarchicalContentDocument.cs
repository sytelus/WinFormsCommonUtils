using System;
using System.Collections;
using System.Xml;
using System.IO;

using System.Security.Cryptography;
using System.Drawing;
using Sytel.Common.WinForms;


namespace Sytel.Common.ContentManagement
{
	/// <summary>
	/// ContentManagement represents the entire NotepadX doc
	/// </summary>
	public delegate void FileNameChangedDelegate(object sender, System.EventArgs e);

	public class DocumentAuthorInformation
	{
		private string m_AuthorID = null;
		private string m_AuthorFullName = string.Empty;
		private string m_AuthorEmailAddress = string.Empty;

		public DocumentAuthorInformation()
		{
		}

		public DocumentAuthorInformation(string authorID, string authorName, string authorEmailAddress)
		{
			this.SetAuthorInfo(authorID, authorName, authorEmailAddress);
		}

		public string AuthorID
		{
			get
			{
				if (m_AuthorID == null)
				{
					return  System.Environment.UserDomainName + "." + System.Environment.MachineName + "." + System.Environment.UserName;
				}
				else return m_AuthorID;
			}
			set
			{
				m_AuthorID = value;
			}
		}

		public void SetAuthorInfo(string authorID, string authorName, string authorEmailAddress)
		{
			this.AuthorID = authorID;
			this.AuthorFullName = authorName;
			this.AuthorEmailAddress = authorEmailAddress;
		}

		public string AuthorFullName
		{
			get
			{
				return m_AuthorFullName;
			}
			set
			{
				m_AuthorFullName = value;
			}
		}

		public string AuthorEmailAddress
		{
			get
			{
				return m_AuthorEmailAddress;
			}
			set
			{
				m_AuthorEmailAddress = value;
			}
		}
	}


	public class DocumentConfiguration
	{
		private string m_xmlTagName = "catNote";
		private string m_rootNoteId = "*ROOT*";
		private string m_defaultRootNoteTitle = "My Notes";
		private bool m_isReadDocumentOnly = false;
		private bool m_isDirty = false;
		private bool m_isPersistNodeExpansionLayout = true;
		private DocumentAuthorInformation m_authorInfo = null;

		public bool IsDirty
		{	
			get 
			{ 
				return m_isDirty; 
			}
		}

		public DocumentAuthorInformation AuthorInformation
		{
			get
			{
				if (m_authorInfo == null)
				{
					m_authorInfo = new DocumentAuthorInformation();
				}
				return m_authorInfo;
			}
			set
			{
				m_authorInfo = value;
			}
		}

		protected internal void SetIsDirty(bool valueToSet)
		{
			m_isDirty = valueToSet;
		}

		public DocumentConfiguration(string xmlTagName, string rootNodeId, string defaultRootNodeTitle, DocumentAuthorInformation authorInfo) : this(xmlTagName, rootNodeId, defaultRootNodeTitle)
		{
			m_authorInfo = authorInfo;
		}		
		public DocumentConfiguration(string xmlTagName, string rootNodeId, string defaultRootNodeTitle)
		{
			m_xmlTagName = xmlTagName;
			m_rootNoteId = rootNodeId;
			m_defaultRootNoteTitle = defaultRootNodeTitle;
		}
		
		public bool IsPersistNodeExpansionLayout
		{
			get
			{
				return m_isPersistNodeExpansionLayout;
			}
			set
			{
				m_isPersistNodeExpansionLayout = value;
			}
		}
		
		public bool IsDocumentReadOnly
		{
			get
			{
				return m_isReadDocumentOnly;
			}
			
			set
			{
				m_isReadDocumentOnly = value;
			}
		}
		
		public DocumentConfiguration(string xmlTagName, string defaultRootNodeTitle)
		{
			if (xmlTagName != null)
				m_xmlTagName = xmlTagName;
			m_defaultRootNoteTitle = defaultRootNodeTitle;		
		}
		
		public DocumentConfiguration()
		{
			//leave vars to their default value		
		}
		
		public DocumentConfiguration(DocumentAuthorInformation authorInfo)
		{
			m_authorInfo = authorInfo;
		}
		public DocumentConfiguration(string xmlTagName, string defaultRootNodeTitle, DocumentAuthorInformation authorInfo) : this(xmlTagName, defaultRootNodeTitle)
		{
			m_authorInfo = authorInfo;
		}

		public string XmlTagName
		{
			get
			{
				return m_xmlTagName;
			}
			set
			{
				m_xmlTagName = value;
			}
		}

		public string RootNoteId
		{
			get
			{
				return m_rootNoteId;
			}
			set
			{
				m_rootNoteId = value;
			}
		}

		public string DefaultRootNoteTitle
		{
			get
			{
				return m_defaultRootNoteTitle;
			}
			set
			{
				m_defaultRootNoteTitle = value;
			}
		}
		
		private SavedPasswordCollection m_savedPasswordsForEncryptedNotes = new SavedPasswordCollection();
		public SavedPasswordCollection SavedPasswordsForEncryptedNotes
		{
			get
			{
				return m_savedPasswordsForEncryptedNotes;
			}
		}
	}
	
	
	internal class SavedPassword
	{
		private string m_Password;
		public  string Password
		{
			get
			{
				return m_Password;
			}
			set
			{
				m_Password = value;
				m_AddedOn = DateTime.Now;
			}
		}

		private DateTime m_AddedOn;
		public  DateTime AddedOn
		{
			get
			{
				return m_AddedOn;
			}
			set
			{
				m_AddedOn = value;
			}
		}

		public SavedPassword(string passwordToSet)
		{
			Password = passwordToSet;
			AddedOn = DateTime.Now;
		}
	}

	public class SavedPasswordCollection
	{
		private Hashtable m_savedPasswordsForEncryptedNotes = new Hashtable();

		private System.UInt32 m_IntervalAfterExpirePasswords = int.MaxValue;
		public System.UInt32 IntervalAfterExpirePasswords
		{
			get
			{
				return m_IntervalAfterExpirePasswords;
			}
			set
			{
				m_IntervalAfterExpirePasswords = value;
			}
		}		

		public bool IsSavedPasswordExistForNoteID(string noteID)
		{
			ExpirePasswordForNoteIfTimeDue(noteID);
			return m_savedPasswordsForEncryptedNotes.Contains(noteID);
		}
		private bool IsSavedPasswordExistForNoteIDWithoutExpirationCheck(string noteID)
		{
			return m_savedPasswordsForEncryptedNotes.Contains(noteID);
		}
		public bool IsPasswordExpired(string noteID)
		{
			SavedPassword savedPW = this.GetSavedPasswordForNoteIDWithoutExpirationCheck(noteID);
			if (savedPW != null)
			{
				return (DateTime.Now.Subtract(savedPW.AddedOn).Minutes >= this.IntervalAfterExpirePasswords);
			}
			else return true;
		}

		public void ExpirePasswordForNoteIfTimeDue(string noteID)
		{
			if (IsPasswordExpired(noteID)==true)
				ForceExpirePasswordForNote(noteID);
		}

		public void ForceExpirePasswordForNote(string noteID)
		{
			if (m_savedPasswordsForEncryptedNotes.Contains(noteID)==true)
				m_savedPasswordsForEncryptedNotes.Remove(noteID);
		}

		private SavedPassword GetSavedPasswordForNoteIDWithoutExpirationCheck(string noteID)
		{
			if (IsSavedPasswordExistForNoteIDWithoutExpirationCheck(noteID)==true)
				return ((SavedPassword) m_savedPasswordsForEncryptedNotes[noteID]);
			else
				return null;
		}

		public string GetSavedPasswordForNoteID(string noteID)
		{
			this.ExpirePasswordForNoteIfTimeDue(noteID);
			return GetSavedPasswordForNoteIDWithoutExpirationCheck(noteID).Password;
		}

		public void AddOrChangeSavedPasswordForNoteID(string noteID, string password)
		{
			if (this.IsSavedPasswordExistForNoteID(noteID)==true)
			{
				((SavedPassword) m_savedPasswordsForEncryptedNotes[noteID]).Password = password;
			}
			else
				m_savedPasswordsForEncryptedNotes.Add(noteID, new SavedPassword(password));
		}
	}

	public class HierarchicalContentDocument
	{
		public event FileNameChangedDelegate FileNameChanged;
		private DocumentConfiguration m_catNoteConfiguration;
	
		//hold the wrapped XML doc
		XmlDocument m_NotepadXXmlDocument;
		string m_noteBookmark = null;	//Null -> not initialized
		string m_fileName = "";
		bool m_isPersistNoteBookMark = true;
		
		public bool IsPersistNoteBookMark
		{
			get
			{
				return (m_isPersistNoteBookMark && (!m_catNoteConfiguration.IsDocumentReadOnly));
			}
			set
			{
				m_isPersistNoteBookMark = value;
			}
		}
		
		public string FileName
		{
			get
			{
			return m_fileName;
			}
			set
			{
				m_fileName=value;
				if (FileNameChanged != null)
					FileNameChanged.DynamicInvoke(new object[] {this, new EventArgs()});
			}
		}

		public DocumentConfiguration Configuration
		{
			get
			{
				return m_catNoteConfiguration;
			}
		}
		
		public void ReloadFromFile()
		{
			if (FileName == string.Empty)
			{
				throw new ArgumentNullException("CatNoteDocument.FileName", "Can Not reload from file because file name is not set for this CatNoteDocument");
			}
			else
			{
				LoadFromFile(FileName);
			}
		}
		
		public string NoteBookmark
		{
			get
			{
				if (m_noteBookmark == null)
				{
					//restore from XML
					if (m_isPersistNoteBookMark == true)
					{
						ContentDetail rootCatNoteDetail = this.GetNoteById(Configuration.RootNoteId);
						m_noteBookmark = rootCatNoteDetail.PersistedNoteBookMark;
					}
					else m_noteBookmark = string.Empty;
				} 
				return m_noteBookmark;
			}
			set
			{
				m_noteBookmark = value;
				if (IsPersistNoteBookMark == true)
				{
					ContentDetail rootCatNoteDetail = this.GetNoteById(Configuration.RootNoteId);
					rootCatNoteDetail.PersistedNoteBookMark = m_noteBookmark;
				}
			}
		}
		
		//Create XML DOM by default
		public HierarchicalContentDocument() : this(new DocumentConfiguration(), false, true)
		{
		}

		public HierarchicalContentDocument(DocumentConfiguration thisNotepadXConfiguration) : this(thisNotepadXConfiguration, thisNotepadXConfiguration.IsDirty, true)
		{
		}

		private HierarchicalContentDocument(DocumentConfiguration thisNotepadXConfiguration, bool isDirty, bool makeNewDocument)
		{
			InitializePrivateVars(thisNotepadXConfiguration);
			if (makeNewDocument)
				MakeBlankDocument();
			m_catNoteConfiguration.SetIsDirty(isDirty);
		}
		
		private void InitializePrivateVars(DocumentConfiguration thisNotepadXConfiguration)
		{
			CleanUpPrivateVariables();
			m_NotepadXXmlDocument = new XmlDocument();
			m_catNoteConfiguration = thisNotepadXConfiguration;
		}
		
		private void CleanUpPrivateVariables()
		{
			m_noteBookmark = null;
			FileName = "";
		}

		public HierarchicalContentDocument(System.IO.Stream inStream) : this(inStream, new DocumentConfiguration())
		{
		}		
		public HierarchicalContentDocument(System.IO.Stream inStream, DocumentConfiguration config) : this(config, false, false)
		{
			m_NotepadXXmlDocument.Load(inStream);
			m_catNoteConfiguration.SetIsDirty(false);
		}
		public HierarchicalContentDocument(string fileName, DocumentConfiguration config) : this(config, false, false)
		{
			LoadFromFile(fileName);
		}
		public HierarchicalContentDocument(string fileName) : this(fileName, new DocumentConfiguration())
		{
		}
		
		public void LoadFromFile(string fileName)
		{
			m_NotepadXXmlDocument.Load(fileName);
			FileName = fileName;
			m_catNoteConfiguration.IsDocumentReadOnly = CommonFunctions.IsFileReadOnly(fileName);
			m_catNoteConfiguration.SetIsDirty(false);
		}
		
		public void LoadFromString(string NotepadXDocumentContent)
		{
			m_NotepadXXmlDocument.LoadXml(NotepadXDocumentContent);
			FileName = string.Empty;
			m_catNoteConfiguration.IsDocumentReadOnly = false;
			m_catNoteConfiguration.SetIsDirty(false);			
		}

		public void LoadFromStream(System.IO.Stream inStream)
		{
			m_NotepadXXmlDocument.Load(inStream);
			FileName = string.Empty;
			m_catNoteConfiguration.IsDocumentReadOnly = false;
			m_catNoteConfiguration.SetIsDirty(false);			
		}
	
		public void MakeBlankDocument()
		{
			CleanUpPrivateVariables();
			m_NotepadXXmlDocument.LoadXml("<" + m_catNoteConfiguration.XmlTagName  + @" id=""" + m_catNoteConfiguration.RootNoteId  + @""" title=""" + m_catNoteConfiguration.DefaultRootNoteTitle + @"""/>");
			m_catNoteConfiguration.IsDocumentReadOnly = false;
			ContentDetail rootNote = this.GetNoteById(this.Configuration.RootNoteId);
			rootNote.SetCreatedByInfo();
			rootNote.SetCustomAttribute("openWithApplicationName", "NotepadX");
			rootNote.SetCustomAttribute("openWithApplicationHomePage", @"http://www.ShitalShah.com/notepadx");
			m_catNoteConfiguration.SetIsDirty(true);
		}

		public ContentDetailCollection GetChildNotes(string parentNoteId)
		{
			return GetChildNotes(parentNoteId, true);
		}
		public ContentDetailCollection GetChildNotes(string parentNoteId, bool isSearchRecursively)
		{
			XmlNode parentXmlNode;

			string xpathCriteriaPrefix;
			if (parentNoteId == Configuration.RootNoteId)
				xpathCriteriaPrefix = "/";
			else
				xpathCriteriaPrefix = (isSearchRecursively? @"//" : "/" + m_catNoteConfiguration.XmlTagName + "/");
			
			parentXmlNode = m_NotepadXXmlDocument.SelectSingleNode(xpathCriteriaPrefix + m_catNoteConfiguration.XmlTagName + "[@id='" + parentNoteId + "']");
			
			if (parentXmlNode == null)
			{
				return null;
			}
			else
			{
				return new ContentDetailCollection(parentXmlNode, m_catNoteConfiguration);
			}
		}

		public ContentDetail CreateNoteInRoot(string noteTitle)
		{
			ContentDetailCollection rootChilds = GetChildNotes(Configuration.RootNoteId, false);
			return rootChilds.Add(noteTitle);
		}
		
		public ContentDetailCollection GetChildNotesByNoteTitle(string noteTitle)
		{
			return GetChildNotesByNoteTitle(noteTitle, true);
		}

		public ContentDetailCollection GetChildNotesByNoteTitle(string noteTitle, bool isSearchRecursively)
		{
			XmlNode parentXmlNode;
			string xpathCriteriaPrefix = (isSearchRecursively? @"//" : "/" + m_catNoteConfiguration.XmlTagName + "/");
			
			parentXmlNode = m_NotepadXXmlDocument.SelectSingleNode(xpathCriteriaPrefix + m_catNoteConfiguration.XmlTagName + "[@title='" + noteTitle + "']");
			
			if (parentXmlNode == null)
			{
				return null;
			}
			else
			{
				return new ContentDetailCollection(parentXmlNode, m_catNoteConfiguration);
			}
		}

		
		public ContentDetail GetNoteById(string noteId)
		{
			XmlNode catNoteNode = m_NotepadXXmlDocument.SelectSingleNode("//" + m_catNoteConfiguration.XmlTagName + "[@id='" + noteId + "']");
			if (catNoteNode == null)
			{
				return null;
			}
				else
			{
				return new ContentDetail(catNoteNode, m_catNoteConfiguration);
			}
		}

		public void MoveNoteById(string sourceNoteIdToMove, string destinationNoteId)
		{
			if (m_catNoteConfiguration.IsDocumentReadOnly == false)
			{
				XmlNode sourceNode = m_NotepadXXmlDocument.SelectSingleNode("//" + m_catNoteConfiguration.XmlTagName + "[@id='" + sourceNoteIdToMove  + "']");
				XmlNode destinationNode = m_NotepadXXmlDocument.SelectSingleNode("//" + m_catNoteConfiguration.XmlTagName + "[@id='" + destinationNoteId  + "']");
				
				if (sourceNode != null)
				{
					//move source to destination
					destinationNode.AppendChild(sourceNode);
				}
				else
				{
					throw new ArgumentException("Source note ID is invalid", "InvalidSourceId");
				}
			}
			else
				throw new ReadOnlyDocumentException("Can not move notes because this document is read only")   ;
				
			m_catNoteConfiguration.SetIsDirty(true);				
		}
		
		public void CopyNoteByContent(string outerXmlForNote, string destinationNoteId)
		{
			if (m_catNoteConfiguration.IsDocumentReadOnly == false)
			{
				ContentDetailCollection childNotes = GetChildNotes(destinationNoteId);
				string idOfNewNode = childNotes.AddXmlDirect(outerXmlForNote);
				ResetNoteIDs(idOfNewNode, true, false);
			}
			else
				throw new ReadOnlyDocumentException("Can not move notes because this document is read only")   ;
				
			m_catNoteConfiguration.SetIsDirty(true);				
		}

		/// <summary>
		/// Reset this note's ID to new GUID and change all of child's IDs recursively
		/// </summary>
		/// <param name="noteToChange">Parent note whoes ID will be changed</param>
		public void ResetNoteIDs(string noteId, bool isRecurseChilds, bool isChangeParent)
		{
			ContentDetail noteToChange = GetNoteById(noteId);
			if (isChangeParent==true)
			{
				string newId = Guid.NewGuid().ToString();
				noteToChange.SetId(newId);	//This should be done before so we can find it uniquely
			}
				
			if (isRecurseChilds==true)
			{
				ContentDetailCollection childNotes = GetChildNotes(noteToChange.Id);
				if (childNotes != null)
				{
					foreach (DictionaryEntry childNoteDictionaryEntry in childNotes)
					{
						ContentDetail childCatNote = ((ContentDetail)childNoteDictionaryEntry.Value);
						ResetNoteIDs(childCatNote.Id, true, true);	//Recurse
					}
				}
			}
		}
		
		public ContentDetail GetNoteByTitle(string noteTitle)
		{
			XmlNode catNoteNode = m_NotepadXXmlDocument.SelectSingleNode("//" + m_catNoteConfiguration.XmlTagName + "[@title='" + noteTitle + "']");
			if (catNoteNode == null)
			{
				return null;
			}
			else
			{
				return new ContentDetail(catNoteNode, m_catNoteConfiguration);
			}
		}

		public bool IsDirty
		{
			get
			{
				return m_catNoteConfiguration.IsDirty;
			}
		}
		
		public void Save(System.IO.Stream inStream)
		{
			m_NotepadXXmlDocument.Save(inStream);
			m_catNoteConfiguration.SetIsDirty(false);				
		}
		
		public void Save(string fileName)
		{
			Save(fileName, false);
		}

		public bool IsReadonly
		{
			get
			{
				return m_catNoteConfiguration.IsDocumentReadOnly;
			}
			
			set
			{
				m_catNoteConfiguration.IsDocumentReadOnly = value;
			}
		}
		
		public string Xml
		{
			get
			{
				return m_NotepadXXmlDocument.OuterXml; 
			}
			set
			{
				LoadFromString(value);
			}
		}

		public override string ToString()
		{
			return this.Xml;
		}
		
		public void SaveCopy(string fileName)
		{
			m_NotepadXXmlDocument.Save(fileName);
		}
		
		public void Save(string fileName, bool isOverwriteReadOnlyFile)
		{
			if (CommonFunctions.IsFileReadOnly(fileName, true))
			{
				if (isOverwriteReadOnlyFile == false)
					throw new ReadOnlyDocumentException("Can not save to file because this file is read-only");
				else
					CommonFunctions.ResetReadOnlyFileAttribute(fileName, true);
			}
			else
			{
				m_NotepadXXmlDocument.Save(fileName);
				FileName = fileName;
				m_catNoteConfiguration.IsDocumentReadOnly = false;
			}
			
			m_catNoteConfiguration.SetIsDirty(false);				
		}
	}
	
	
	
	/// <summary>
	/// Collection of ContentDetail
	/// </summary>
	public class ContentDetailCollection : System.Collections.DictionaryBase 
	{
		XmlNode m_NotepadXDocumentParentXmlNode;
		DocumentConfiguration m_catNoteConfiguration;
		
		protected internal ContentDetailCollection(XmlNode NotepadXParentXmlNode, DocumentConfiguration thisNotepadXConfiguration)
		{
			m_NotepadXDocumentParentXmlNode = NotepadXParentXmlNode;
			m_catNoteConfiguration = thisNotepadXConfiguration;
			
			foreach (XmlNode catNoteDetailNode in NotepadXParentXmlNode.ChildNodes)
			{
				ContentDetail newNoteForCollection =new ContentDetail(catNoteDetailNode, m_catNoteConfiguration);
				Dictionary.Add(newNoteForCollection.Id, newNoteForCollection);
			}
		}

		public ContentDetailCollection GetChildNotesByNoteTitle(string noteTitle)
		{
			return GetChildNotesByNoteTitle(noteTitle, true);
		}

		public ContentDetailCollection GetChildNotesByNoteTitle(string noteTitle, bool isSearchRecursively)
		{
			XmlNode parentXmlNode;
			string xpathCriteriaPrefix = (isSearchRecursively? @"//" : "./");
			
			parentXmlNode = m_NotepadXDocumentParentXmlNode.SelectSingleNode(xpathCriteriaPrefix + m_catNoteConfiguration.XmlTagName + "[@title='" + noteTitle + "']");
			
			if (parentXmlNode == null)
			{
				return null;
			}
			else
			{
				return new ContentDetailCollection(parentXmlNode, m_catNoteConfiguration);
			}
		}


		public ContentDetailCollection GetChildNotes(string parentNoteId)
		{
			return GetChildNotes(parentNoteId, true);
		}
		public ContentDetailCollection GetChildNotes(string parentNoteId, bool isSearchRecursively)
		{
			XmlNode parentXmlNode;

			string xpathCriteriaPrefix = (isSearchRecursively? @"//" : "./");
			parentXmlNode = m_NotepadXDocumentParentXmlNode.SelectSingleNode(xpathCriteriaPrefix + m_catNoteConfiguration.XmlTagName + "[@id='" + parentNoteId + "']");
			
			if (parentXmlNode == null)
			{
				return null;
			}
			else
			{
				return new ContentDetailCollection(parentXmlNode, m_catNoteConfiguration);
			}
		}
		
		public ContentDetail this[string noteId]
		{
			get
			{
				return (ContentDetail) base.Dictionary[noteId];
			}
		}
		
		public ContentDetail GetNoteDetailByNoteTitle(string noteTitle)
		{
			return GetNoteDetailByNoteTitle(noteTitle, true);
		}
		
		public ContentDetail GetNoteDetailByNoteTitle(string noteTitle, bool isSearchRecursively)
		{
			string xpathCriteriaPrefix = (isSearchRecursively? @"//" : "./");

			XmlNode foundNoteDetail = m_NotepadXDocumentParentXmlNode.SelectSingleNode(xpathCriteriaPrefix + m_catNoteConfiguration.XmlTagName + "[@title='" + noteTitle + "']");
			if (foundNoteDetail != null)
			{
				return new ContentDetail(foundNoteDetail, m_catNoteConfiguration);
			}
			else return null;
		}
		
		protected override void OnClear()
		{
			if (m_catNoteConfiguration.IsDocumentReadOnly == false)
			{
				foreach (XmlNode catNoteDetailNode in m_NotepadXDocumentParentXmlNode.ChildNodes)
				{
					m_NotepadXDocumentParentXmlNode.RemoveChild(catNoteDetailNode);
				}
			}
			else
				throw new ReadOnlyDocumentException("Can not remove all notes because this document is read-only");

			m_catNoteConfiguration.SetIsDirty(true);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="outerXmlForNote"></param>
		/// <returns>GUID of new node created</returns>
		protected internal string AddXmlDirect(string outerXmlForNote)
		{
			XmlDocumentFragment newNoteXmlFragment = m_NotepadXDocumentParentXmlNode.OwnerDocument.CreateDocumentFragment();
			newNoteXmlFragment.InnerXml = outerXmlForNote;
			ContentDetail newNote = new ContentDetail(newNoteXmlFragment.FirstChild, m_catNoteConfiguration);
			string newId = Guid.NewGuid().ToString();
			newNote.SetId (newId);
			
			newNoteXmlFragment.InnerXml = newNote.Xml;
			m_NotepadXDocumentParentXmlNode.AppendChild(newNoteXmlFragment);
			
			return newId;
		}
				
		public ContentDetail Add()
		{
			return this.Add("");
		}
		
		public ContentDetail Add(string noteTitle)
		{
			if (m_catNoteConfiguration.IsDocumentReadOnly == false)
			{
				ContentDetail newNoteForCollection = ContentDetail.CreateCatNoteDetail(m_NotepadXDocumentParentXmlNode, m_catNoteConfiguration);
				newNoteForCollection.Title = noteTitle;
				newNoteForCollection.SetCreatedByInfo();
				Dictionary.Add(newNoteForCollection.Id, newNoteForCollection);

				m_catNoteConfiguration.SetIsDirty(true);
				
				return newNoteForCollection;
			}
			else
				throw new ReadOnlyDocumentException("Can not add new note because this document is read-only");
		}		
		
		public void Remove(string catNoteId)
		{
			if (m_catNoteConfiguration.IsDocumentReadOnly == false)
			{
				ContentDetail catNoteDetailForId = (ContentDetail) Dictionary[catNoteId];
				m_NotepadXDocumentParentXmlNode.RemoveChild(catNoteDetailForId.XmlNode);
				Dictionary.Remove(catNoteDetailForId);
			}
			else
				throw new ReadOnlyDocumentException("Can not remove note because this document is read-only");

			m_catNoteConfiguration.SetIsDirty(true);
		}
		
		public bool Contains(string catNoteId)
		{
			return Dictionary.Contains(catNoteId);
		}
	}
	
	public class ContentDetail
	{
		XmlNode m_catNoteDetailXmlNode;
		DocumentConfiguration m_catNoteConfiguration;
		string m_passwordForEncryptedNote = null;	//null -> no pw set so far, string.Empty != null

		private const string ENCRYPTED_TEXT_HEADER = "CatNotesEncryptedText:";
		
		protected internal ContentDetail(XmlNode catNoteDetailXmlNode, DocumentConfiguration thisNotepadXConfiguration)
		{
			m_catNoteConfiguration = thisNotepadXConfiguration;
			m_catNoteDetailXmlNode = catNoteDetailXmlNode;

			if (this.IsEncrypted==true)
			{
				//Restore password from cache
				if (m_catNoteConfiguration.SavedPasswordsForEncryptedNotes.IsSavedPasswordExistForNoteID(this.Id) == true)
				{
					m_passwordForEncryptedNote = (string) m_catNoteConfiguration.SavedPasswordsForEncryptedNotes.GetSavedPasswordForNoteID(this.Id);
				}
			}
			else {}; //no pw processing required
		}
		
		protected internal static ContentDetail CreateCatNoteDetail(XmlNode parentXmlNode, DocumentConfiguration thisNotepadXConfiguration)
		{
			XmlDocument ownerDocument;
			if (parentXmlNode.OwnerDocument != null)
			{
				ownerDocument = parentXmlNode.OwnerDocument;
			}
			else
			{	
				ownerDocument = (XmlDocument) parentXmlNode;
			}
			XmlNode newCatNoteDetailXmlNode = ownerDocument.CreateElement(thisNotepadXConfiguration.XmlTagName);
			XmlAttribute idXmlAttribute = ownerDocument.CreateAttribute("id");
			idXmlAttribute.Value = System.Guid.NewGuid().ToString();
			newCatNoteDetailXmlNode.Attributes.Append(idXmlAttribute);
			
			XmlAttribute titleXmlAttribute = ownerDocument.CreateAttribute("title");
			newCatNoteDetailXmlNode.Attributes.Append(titleXmlAttribute);
			XmlAttribute contentXmlAttribute = ownerDocument.CreateAttribute("content");
			newCatNoteDetailXmlNode.Attributes.Append(contentXmlAttribute);
			
			parentXmlNode.AppendChild(newCatNoteDetailXmlNode);

			thisNotepadXConfiguration.SetIsDirty(true);
						
			return new ContentDetail(newCatNoteDetailXmlNode, thisNotepadXConfiguration);
		}
		
		public void Remove()
		{
			if (this.IsReadOnly == false)
			{
				m_catNoteDetailXmlNode.ParentNode.RemoveChild (m_catNoteDetailXmlNode);

				m_catNoteConfiguration.SetIsDirty(true);
			}
			else
				throw new ReadOnlyNoteException("Can not remove this note because it is read-only. You must remove read-only attribute before deleting this note");
		}
		
		
		
		/// <summary>
		/// ContentDetail class to access note's content and other attributes
		/// </summary>
		public ContentDetail ParentNote
		{
			get
			{
				if (m_catNoteDetailXmlNode.ParentNode != null)
					return new ContentDetail(m_catNoteDetailXmlNode.ParentNode, m_catNoteConfiguration);
				else
					return null;
			}
		}
		
		public string Id
		{
			get
			{
				return GetAttributeValue("id", string.Empty);
			}
		}
		
		protected internal void SetId(string newId)
		{
			SetAttributeValue("id", newId);
		}
		
		public ContentDetailCollection GetChildNotes()
		{
			return new ContentDetailCollection(m_catNoteDetailXmlNode, m_catNoteConfiguration);
		}
		
		private string GetAttributeValue(string attributeName, string defaultValueIfNotExist)
		{
			XmlAttribute thisXmlAttribute = (XmlAttribute) m_catNoteDetailXmlNode.Attributes.GetNamedItem(attributeName);
			if (thisXmlAttribute != null)
				return thisXmlAttribute.Value;
			else
				return defaultValueIfNotExist;
		}

		private void SetAttributeValue(string attributeName, string attributeValue)
		{
			SetAttributeValue(attributeName, attributeValue, true, false);
		}
		
		private void SetAttributeValue(string attributeName, string attributeValue, bool setIsDirtyFlag, bool allowChangeInReadOnlyNote)
		{
			bool isAttributeValueChanged = false;
			if ((IsReadOnly == false) || (allowChangeInReadOnlyNote == true))
			{
				XmlAttribute thisXmlAttribute = (XmlAttribute) m_catNoteDetailXmlNode.Attributes.GetNamedItem(attributeName);
				if (thisXmlAttribute == null)
				{
					thisXmlAttribute = m_catNoteDetailXmlNode.OwnerDocument.CreateAttribute(attributeName);
					m_catNoteDetailXmlNode.Attributes.Append(thisXmlAttribute);
					isAttributeValueChanged = true;
				}
				
				if ((thisXmlAttribute.Value != attributeValue) || (isAttributeValueChanged == true))
				{
					isAttributeValueChanged = true;
					thisXmlAttribute.Value = attributeValue;
				}
			}
			else	//TODO : add check if value being set is same as current value then don't do anything
				throw new ReadOnlyNoteException  ("Can not change note's property '" + attributeName + "' because this note is read only");	//TODO : improve msg to indicate if whole doc is read only

			if ((setIsDirtyFlag == true) && (isAttributeValueChanged == true))
				m_catNoteConfiguration.SetIsDirty(true);
		}

		public DateTime CreatedOn
		{
			get
			{
				string createdOnString = GetAttributeValue("createdOn", string.Empty);
				if (createdOnString == string.Empty)
					return DateTime.MinValue;
				else
					return DateTime.Parse(createdOnString);
			}
			set
			{
				SetAttributeValue("createdOn", CommonFunctions.DateTimeToString(value));
			}
		}

		public void SetCreatedByInfo()
		{
			this.CreatedOn = DateTime.Now;
			this.CreatedByAuthorID = m_catNoteConfiguration.AuthorInformation.AuthorID;
			this.CreatedByAuthorFullName = m_catNoteConfiguration.AuthorInformation.AuthorFullName;
			this.CreatedByAuthorEmailAddress = m_catNoteConfiguration.AuthorInformation.AuthorEmailAddress;
		}

		public void SetModifiedByInfo()
		{
			this.ModifiedOn = DateTime.Now;
			this.ModifiedByAuthorID = m_catNoteConfiguration.AuthorInformation.AuthorID;
			this.ModifiedByAuthorFullName = m_catNoteConfiguration.AuthorInformation.AuthorFullName;
			this.ModifiedByAuthorEmailAddress = m_catNoteConfiguration.AuthorInformation.AuthorEmailAddress;
		}

		public DateTime ModifiedOn
		{
			get
			{
				string modifiedOnString = GetAttributeValue("modifiedOn", string.Empty);
				if (modifiedOnString == string.Empty)
					return DateTime.MinValue;
				else
					return DateTime.Parse(modifiedOnString);
			}
			set
			{
				SetAttributeValue("modifiedOn", value.ToUniversalTime().ToString("u"));
			}
		}

		public string CreatedByAuthorID
		{
			get
			{
				return GetAttributeValue("createdByAuthorID", string.Empty);
			}
			set
			{
				SetAttributeValue("createdByAuthorID", value);
			}
		}
		public string CreatedByAuthorFullName
		{
			get
			{
				return GetAttributeValue("createdByAuthorFullName", string.Empty);
			}
			set
			{
				SetAttributeValue("createdByAuthorFullName", value);
			}
		}
		public string CreatedByAuthorEmailAddress
		{
			get
			{
				return GetAttributeValue("createdByAuthorEmailAddress", string.Empty);
			}
			set
			{
				SetAttributeValue("createdByAuthorEmailAddress", value);
			}
		}		
		public string ModifiedByAuthorID
		{
			get
			{
				return GetAttributeValue("modifiedByAuthorID", string.Empty);
			}
			set
			{
				SetAttributeValue("modifiedByAuthorID", value);
			}
		}
		public string ModifiedByAuthorFullName
		{
			get
			{
				return GetAttributeValue("modifiedByAuthorFullName", string.Empty);
			}
			set
			{
				SetAttributeValue("modifiedByAuthorFullName", value);
			}
		}
		public string ModifiedByAuthorEmailAddress
		{
			get
			{
				return GetAttributeValue("modifiedByAuthorEmailAddress", string.Empty);
			}
			set
			{
				SetAttributeValue("modifiedByAuthorEmailAddress", value);
			}
		}		

		
		public string Title
		{
			get
			{
				return GetAttributeValue("title", string.Empty);
			}
			set
			{
				SetAttributeValue("title", value);
			}
		}

		public bool ExpandNodeByDefault
		{
			get
			{
				return bool.Parse(GetAttributeValue("expandNodeByDefault", false.ToString()));
			}
			set
			{
				if (m_catNoteConfiguration.IsPersistNodeExpansionLayout==true)
					SetAttributeValue("expandNodeByDefault", value.ToString(), false, true);
			}
		}

		public string Format
		{
			get
			{
				return GetAttributeValue("format", String.Empty);
			}
			set
			{
				SetAttributeValue("format", value);
			}
		}

		public Color NodeBackColor
		{
			get
			{
				return Color.FromName(GetAttributeValue("nodeBackColor", Color.FromKnownColor(KnownColor.Window).Name));
			}
			set
			{
				SetAttributeValue("nodeBackColor", value.Name);
			}
		}

		public Color NodeForeColor
		{
			get
			{
				return Color.FromName(GetAttributeValue("nodeForeColor", Color.FromKnownColor(KnownColor.WindowText).Name));
			}
			set
			{
				SetAttributeValue("nodeForeColor", value.Name);
			}
		}
		
		public Font NodeFont
		{
			get
			{
				string serializedFont = GetAttributeValue("nodeFont", string.Empty);
				if (serializedFont != string.Empty)
				{
					return (Font) CommonFunctions.DeserializeObjectFromString(serializedFont);
				}
				else return null;
			}
			set
			{
				if (value == null)
					SetAttributeValue("nodeFont", string.Empty);
				else
					SetAttributeValue("nodeFont", CommonFunctions.SerializeObjectToString(value));
			}
		}

		public string VarifyPasswordAndGetContent(string password)
		{
			string noteContent;
			try
			{
				noteContent = CommonFunctions.DecryptString(password, this.RawContent);			
				if (noteContent.StartsWith(ENCRYPTED_TEXT_HEADER)==false)
				{
					throw new System.Security.SecurityException("Invalid password");
				}
				else
				{
					noteContent = noteContent.Substring(ENCRYPTED_TEXT_HEADER.Length);
				}
			}
			catch (System.Security.Cryptography.CryptographicException decryptException)
			{
				PasswordForEncryptedNote = null;	//remove bad password
				throw new System.Security.SecurityException("Decryption failed", decryptException);
			}
			return noteContent;
		}

		public string RawContent
		{
			get
			{
				return GetAttributeValue("content", string.Empty);
			}
			set
			{
				SetAttributeValue("content", value);
				this.SetModifiedByInfo();
			}
		}

		public string Content
		{
			get
			{
				if (IsEncrypted == true)
				{
					//Use password to decrypt before returning the content
					return VarifyPasswordAndGetContent(PasswordForEncryptedNote);
				}
				else return this.RawContent;
			}
			set
			{
				string noteContent = value;
				if (IsEncrypted == true)
				{
					if (PasswordForEncryptedNote == null)
					{
						throw new System.Security.SecurityException("Could not encrypt the data because password is not provided");
					}
					else
					{
						VarifyPasswordAndGetContent(PasswordForEncryptedNote);
						//Use password to encrypt before saving the content
						noteContent = CommonFunctions.EncryptString(PasswordForEncryptedNote, ENCRYPTED_TEXT_HEADER + noteContent);				
					}
				}
				this.RawContent = noteContent;
			}
		}

		public string PasswordForEncryptedNote
		{
			get
			{
				return m_passwordForEncryptedNote;
			}
			set
			{
				m_passwordForEncryptedNote = value;
				
				//Cache password
				m_catNoteConfiguration.SavedPasswordsForEncryptedNotes.AddOrChangeSavedPasswordForNoteID(this.Id, m_passwordForEncryptedNote);
			}
		}

		public bool IsEncrypted
		{
			get
			{
				return bool.Parse(GetAttributeValue("isEncrypted", "False"));
			}
			set
			{
				//Is state is being toggled
				if (IsEncrypted != value)
				{
					string decryptedContent = Content;
					
					//If it's already encrypted
					if (IsEncrypted == true)
					{
						SetAttributeValue("isEncrypted", false.ToString());
						Content = decryptedContent;
					}
					else
					{
						SetAttributeValue("isEncrypted", true.ToString());
						Content = decryptedContent;
					}
				}
				else {};	//ignore the same value setting
			}
		}

		

		public string GetCustomAttribute(string attributeName, string defaultValueIfNotExist)
		{
			return GetAttributeValue(attributeName, defaultValueIfNotExist);
		}

		public void SetCustomAttribute(string attributeName, string attributeValue)
		{
			SetAttributeValue(attributeName, attributeValue);
		}

		protected internal string PersistedNoteBookMark
		{
			get
			{
				return GetAttributeValue("persistedNoteBookMark", string.Empty);
			}
			set
			{
				SetAttributeValue("persistedNoteBookMark", value, false, true);
			}
		}
		
		public bool IsReadOnly
		{
			get
			{
				return (bool.Parse(GetAttributeValue("isReadOnly", "False")) || m_catNoteConfiguration.IsDocumentReadOnly);
			}
			set
			{
				if (m_catNoteConfiguration.IsDocumentReadOnly == false)
					SetAttributeValue("isReadOnly", value.ToString(), true, true);
				else
					throw new  ReadOnlyDocumentException("Can not change Read Only setting for this note because entire document is read-only");
			}
		}
		
		protected internal XmlNode XmlNode
		{
			//WARNING : no security checks method! Do note expose!!
			get
			{
				return m_catNoteDetailXmlNode;
			}
		}
		
		public string Xml
		{
			get
			{
				return XmlNode.OuterXml;
			}
		}	
		
		protected internal void SetInnerXml(string innerXml)
		{
			XmlNode.InnerXml = innerXml;
		}
		
		public override string ToString()
		{
			return this.Xml;
		}
	}
	
	//These exceptions are declared just so they can be detected
	public class ReadOnlyDocumentException : UnauthorizedAccessException 
	{
		public ReadOnlyDocumentException(string message) : base(message) 
		{
		}
	}
	public class ReadOnlyNoteException : UnauthorizedAccessException 
	{
		public ReadOnlyNoteException(string message) : base(message)
		{
		}
	}
}
