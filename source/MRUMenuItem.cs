/*Source code credit: Jan Wiggers 
 *http://www.codeproject.com/cs/miscctrl/mrumenuitem.asp?target=mru
 * 
*/

using System;
using System.Windows.Forms;
using System.Collections;

namespace Sytel.Common.WinForms
{
	/// <summary>
	/// Summary description for MRUMenuItem.
	/// </summary>
	public class MRUMenuItem : MenuItem	
	{
		ArrayList mru = new ArrayList();
		
		private System.UInt32 m_MaxMRUCount =10;
		public System.UInt32 MaxMRUCount
		{
			get { return m_MaxMRUCount; }
			set
			{
				m_MaxMRUCount=value;
				RebuildRecentMenuItems();
			}
		}

		private const string DEFAULT_MENU_TEXT  = "(Recent Documents)";
		private string m_originalMenuText = DEFAULT_MENU_TEXT;

		public string[] MRUFiles 
		{
			get 
			{
				return (string[])mru.ToArray(typeof(string));
			}
		}
		
		public string MRUData
		{
			get
			{
				string data = string.Empty;
				foreach(string mruFile in this.MRUFiles)
				{
					data += (mruFile + "\r\n");
				}
				return data;
			}
		}

		public void Initialize(string mruData, System.UInt32 maxEntriesAllowed) 
		{
			string[] mruFiles; 
			if (mruData != null)
				mruFiles = mruData.Split(new char[]{'\r','\n'});
			else
				mruFiles = new string[]{};
			this.Initialize(mruFiles, maxEntriesAllowed);
		}

		public void Initialize(string[] files, System.UInt32 maxEntriesAllowed) 
		{
			MaxMRUCount = maxEntriesAllowed;
			mru.AddRange (files);
			RebuildRecentMenuItems();
		}

		private void RebuildRecentMenuItems()
		{
			if (m_originalMenuText == DEFAULT_MENU_TEXT) m_originalMenuText = this.Text;
			this.MenuItems.Clear();
			for (int i=mru.Count-1;i>=0;i--) 
			{
				if (MaxMRUCount < i) 
				{
					mru.RemoveAt(i);
				}
				else
				{
					if ("" != ((string)mru[i]))
					{
						if (mru.IndexOf((string)mru[i])!= i)
						{
							mru.RemoveAt(i);
						}
						else
						{
							//This is valid menu item
						}
					}
					else
					{
						mru.RemoveAt(i);
					}
				}
			}

			for (int i=0;i<=mru.Count-1;i++) 
			{
				MenuItem mmru = new MenuItem((string)mru[i], new EventHandler(OnMRUClick));
				this.MenuItems.Add(mmru);
			}
			this.Text = m_originalMenuText + " (" + this.MenuItems.Count.ToString() + " documents)";
			this.Enabled = (this.MenuItems.Count > 0);
		}

		public void FileOpened(string file) 
		{
			//See if we already have entry for this
			int found=mru.IndexOf(file);
			
			if (found >= 0)
			{
				//Push down 0 to found-1 entries
				for(int i=found-1;i>=0;i--)
				{
					mru[i+1]=mru[i];
				}
				mru[0]=file;
			}
			else
			{
				mru.Insert(0,file);
			}

			RebuildRecentMenuItems();

			if (MRUChanged != null) 
			{
				MRUChanged(this, new EventArgs()); 
			}
		}

		/// <summary>
		/// Fired when a mru menuitem is clicked
		/// </summary>
		public event EventHandler MRUClicked; 
		protected virtual void OnMRUClick(object o, EventArgs e) 
		{
			if (MRUClicked != null) 
			{
				MRUClicked(o, e); 
			}
		}
		/// <summary>
		/// Fired when the mru is changed
		/// </summary>
		public event EventHandler MRUChanged; 
	}
}
