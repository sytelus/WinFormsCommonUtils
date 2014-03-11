using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Forms;

using Sytel.Common.ContentManagement;

namespace Sytel.Common.WinForms
{
	/// <summary>
	/// Summary description for ApplicationSettings.
	/// </summary>
	public class ApplicationSettings
	{
		private HierarchicalContentDocument m_NotepadXDocument;
		
		public bool IsIgnoreSaveErrorIfNoPermission = true;
		public const string DEFAULT_CONFIGURATION_SET = "default";
		
		private const string USAGE_STATISTICS_CONFIGURATION_SET = "usageStatistics";

		private string m_settingsFileName;

		public ApplicationSettings() : this(Path.GetFileName(Application.ExecutablePath) + ".config.npx")
		{
		}

		public ApplicationSettings(string settingsFileNameToUse)
		{
			m_NotepadXDocument = null;
			m_NotepadXDocument = new HierarchicalContentDocument(new DocumentConfiguration(null, "Categorial configuration file for product: " + Path.GetFileName(Application.ExecutablePath)));
			m_settingsFileName = settingsFileNameToUse;
		}

		public string SettingsFileName
		{
			get
			{
				return m_settingsFileName;
			}
		}

#region "usage statistics function"
		[System.ComponentModel.Browsable(false)]
		public int _UsageStatistics_ApplicationLoadedCounter
		{
			get
			{
				return int.Parse(this._GetUsageStatisticsInfo("Counters", "ApplicationLoadedCounter", "0"));
			}
			set
			{
				this._SetUsageStatisticsInfo("Counters", "ApplicationLoadedCounter", value.ToString());
			}
		}
		[System.ComponentModel.Browsable(false)]
		public int _UsageStatistics_ApplicationClosedCounter
		{
			get
			{
				return int.Parse(this._GetUsageStatisticsInfo("Counters", "ApplicationClosedCounter", "0"));
			}
			set
			{
				this._SetUsageStatisticsInfo("Counters", "ApplicationClosedCounter", value.ToString());
			}
		}
		[System.ComponentModel.Browsable(false)]
		public DateTime _UsageStatistics_ApplicationLastLoadedOn
		{
			get
			{
				return DateTime.Parse(this._GetUsageStatisticsInfo("DateTimes", "ApplicationLastLoaded", CommonFunctions.DateTimeToString(DateTime.MinValue)));
			}
			set
			{
				this._SetUsageStatisticsInfo("DateTimes", "ApplicationLastLoaded", CommonFunctions.DateTimeToString(value));
			}
		}
		[System.ComponentModel.Browsable(false)]
		public DateTime _UsageStatistics_ApplicationLastClosedOn
		{
			get
			{
				return DateTime.Parse(this._GetUsageStatisticsInfo("DateTimes", "ApplicationLastClosed", CommonFunctions.DateTimeToString(DateTime.MinValue)));
			}
			set
			{
				this._SetUsageStatisticsInfo("DateTimes", "ApplicationLastClosed", CommonFunctions.DateTimeToString(value));
			}
		}

		[System.ComponentModel.Browsable(false)]
		public string _GetUsageStatisticsInfo(string catagoryName, string keyName, string defaultValue)
		{
			string oldConfigSet = CurrentConfigurationSet;
			try
			{
				CurrentConfigurationSet = USAGE_STATISTICS_CONFIGURATION_SET;
				return this.GetSetting(catagoryName, keyName, defaultValue);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				CurrentConfigurationSet = oldConfigSet;
			}
		}

		[System.ComponentModel.Browsable(false)]
		public void _SetUsageStatisticsInfo(string catagoryName, string keyName, string valueToSet)
		{
			string oldConfigSet = CurrentConfigurationSet;
			try
			{
				CurrentConfigurationSet = USAGE_STATISTICS_CONFIGURATION_SET;
				this.SetSetting(catagoryName, keyName, valueToSet, true);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				CurrentConfigurationSet = oldConfigSet;
			}
		}

#endregion

		public bool OpenOrCreateSettingsDocument(bool isCreateIfNotExist, string settingsFileNameToUse)
		{
			m_settingsFileName = settingsFileNameToUse;

			//Create blank XML doc
			m_NotepadXDocument = new HierarchicalContentDocument(new DocumentConfiguration(null, "Categorial configuration file for product: " + Path.GetFileName(Application.ExecutablePath)));			
			return this.ReloadFromFile(isCreateIfNotExist);
		}

		public bool OpenOrCreateSettingsDocument(bool isCreateIfNotExist)
		{
			return this.OpenOrCreateSettingsDocument(isCreateIfNotExist, this.SettingsFileName);
		}

		public string CurrentConfigurationSet = DEFAULT_CONFIGURATION_SET;
		
		public string GetSetting(string settingCategoryName, string settingName)
		{
			return GetSetting(settingCategoryName, settingName, null);
		}
		
		public string GetSetting(string settingCategoryName, string settingName, string settingValueToReturnIfNotFound)
		{	
			ContentDetailCollection settingsInCategory = GetCategoryForSetting(settingCategoryName, false);
			
			if (settingsInCategory != null)
			{
				ContentDetail settinngDetail = settingsInCategory.GetNoteDetailByNoteTitle(settingName, false);
				if (settinngDetail == null)
					return settingValueToReturnIfNotFound;
				else
					return settinngDetail.Content;
			}
			else
				return settingValueToReturnIfNotFound;
		}
		
		private ContentDetailCollection GetCategoryForSetting(string settingCategoryName, bool createCategoryIfNotExist)
		{
			ContentDetailCollection settingsInCategory = null;;
			ContentDetailCollection currentConfigurationSetCollection = null;
			if (CurrentConfigurationSet == "")	//no config set is defined
			{
				//category is in root node
				settingsInCategory = m_NotepadXDocument.GetChildNotesByNoteTitle(settingCategoryName, false);
				
				//if no category exit
				if ((settingsInCategory == null) && (createCategoryIfNotExist==true))
				{
					//Add this new category in root
					settingsInCategory = m_NotepadXDocument.CreateNoteInRoot(settingCategoryName).GetChildNotes();
				}
			}
			else
			{
				//first get configuration set
				currentConfigurationSetCollection = m_NotepadXDocument.GetChildNotesByNoteTitle (CurrentConfigurationSet, false);
				
				//If no configurationset exit
				if ((currentConfigurationSetCollection == null) && (createCategoryIfNotExist==true))
				{
					//Create configuration set in root, create category
					currentConfigurationSetCollection = m_NotepadXDocument.CreateNoteInRoot (CurrentConfigurationSet).GetChildNotes();
					settingsInCategory = currentConfigurationSetCollection.Add(settingCategoryName).GetChildNotes();
				}
				
				if (currentConfigurationSetCollection == null) 
				{
					settingsInCategory =  null;
				}
				else
				{
					settingsInCategory = currentConfigurationSetCollection.GetChildNotesByNoteTitle (settingCategoryName, false);
					if ((settingsInCategory == null) && (createCategoryIfNotExist==true))
					{
						settingsInCategory = currentConfigurationSetCollection.Add(settingCategoryName).GetChildNotes();
					}
				}
			}
			
			return settingsInCategory;
		}
		
		public void SetSetting(string settingCategoryName, string settingName, string settingValue, bool isSaveNow)
		{
			ContentDetailCollection settingsInCategory = GetCategoryForSetting(settingCategoryName, true);

			ContentDetail settinngDetail = settingsInCategory.GetNoteDetailByNoteTitle(settingName, false);
			if (settinngDetail == null)
			{
				//Create setting
				settinngDetail = settingsInCategory.Add();
				settinngDetail.Title = settingName;
			}
			
			settinngDetail.Content = settingValue;
			
			if (isSaveNow == true)
				this.Save();
		}

		public void Delete()
		{
			string settingsFileName = SettingsFileName;
			IsolatedStorageFile userStorage = IsolatedStorageFile.GetUserStoreForAssembly();
			userStorage.DeleteFile(settingsFileName);
		}

		public bool ReloadFromFile(bool isCreateIfNotExist)
		{
			bool wasCreated = false;

			//Load/create app config file
			string settingsFileName = SettingsFileName;

			IsolatedStorageFile userStorage = IsolatedStorageFile.GetUserStoreForAssembly();
			IsolatedStorageFileStream settingsFileStream = null;
			
			try
			{			
				if (userStorage.GetFileNames(settingsFileName).Length == 1)	//file exist
				{
					settingsFileStream = new IsolatedStorageFileStream(settingsFileName, FileMode.Open, FileAccess.ReadWrite, GetUserStorage()); 
					m_NotepadXDocument.LoadFromStream(settingsFileStream);
				}
				else
				{
					if (isCreateIfNotExist == true)
					{
						wasCreated = true;
						settingsFileStream = new IsolatedStorageFileStream(settingsFileName, FileMode.Create, FileAccess.ReadWrite, userStorage); 
						m_NotepadXDocument.Save(settingsFileStream);
					}
					else
						throw new FileNotFoundException("Application cat config file not found", settingsFileName);
				}
			}
			catch(Exception ex)
			{
				throw ex;	//reraise after finally
			}
			finally
			{
				if (settingsFileStream != null)
					settingsFileStream.Close();
			}

			return wasCreated;
		}
		
		public override string ToString()
		{
			return m_NotepadXDocument.ToString();
		}
		
		public string Xml
		{
			get
			{
				return m_NotepadXDocument.Xml;
			}
		}
		
		public void LoadFromString(string configDocumentContent)
		{
			m_NotepadXDocument.LoadFromString(configDocumentContent);
		}
		
		public void LoadFromFile(string fileName)
		{
			m_NotepadXDocument.LoadFromFile(fileName);
		}
		public void SaveCopy(string fileName)
		{
			m_NotepadXDocument.SaveCopy(fileName);
		}

		private IsolatedStorageFile GetUserStorage()
		{
			return IsolatedStorageFile.GetUserStoreForAssembly();
		}
			
		public void Save()
		{
			try
			{
				IsolatedStorageFileStream settingsFileStream = new IsolatedStorageFileStream(SettingsFileName, FileMode.Open, FileAccess.ReadWrite, GetUserStorage());
				settingsFileStream.SetLength(0);
				try
				{
					m_NotepadXDocument.Save(settingsFileStream);
				}
				catch(Exception ex)
				{
					settingsFileStream.Close();
					throw new Exception(ex.Message, ex);
				}
				settingsFileStream.Close();
			}
			catch (System.Security.SecurityException saveException)
			{
				if (IsIgnoreSaveErrorIfNoPermission == false)
					throw saveException;
				else {}; //remain silent
			}
		}
		
	}
}
