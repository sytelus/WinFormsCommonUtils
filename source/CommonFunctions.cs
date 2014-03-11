using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using Microsoft.Win32;


namespace Sytel.Common.WinForms
{
	/// <summary>
	/// Summary description for CommonFunctions.
	/// </summary>
	
	public class PropertyNameValuePair
	{
		private string m_name="";
		private object m_value=null;
		
		public string PropertyName
		{
			get
			{
				return m_name;
			}
			set
			{
				m_name = value;
			}
		}
		public object PropertyValue
		{
			get
			{
				return m_value;
			}
			set
			{
				m_value = value;
			}
		}
		public PropertyNameValuePair(string propertyName, object propertyValue)
		{
			m_name = propertyName;
			m_value = PropertyValue;
		}		
	}
	public class CommonFunctions
	{
		[DllImport("User32",EntryPoint="SetForegroundWindow")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);
		private const int HWND_BROADCAST  = 0xffff;      
		private const int WM_USER = 0x400;
		private const int SW_RESTORE = 9;


		[DllImport("user32",EntryPoint="SendMessage")]
		private static extern int SendMessage(int hwnd,int msg,int wparam,int lparam);
      
		[DllImport("user32",EntryPoint="PostMessage")]
		private static extern bool PostMessage(int hwnd,int msg,int wparam,int lparam);
      
		[DllImport("user32",EntryPoint="RegisterWindowMessage")]
		private static extern int RegisterWindowMessage(string msgString);

		[DllImport("user32.dll")] 
		private static extern bool ShowWindowAsync(IntPtr hWnd,int nCmdShow);
		[DllImport("user32.dll")] 
		private static extern bool IsIconic(IntPtr hWnd);

		public static String ReplaceString(String SourceString, String SearchString, String ReplaceString, bool IsCaseInsensetive)
		{
			return Regex.Replace (SourceString, Regex.Escape(SearchString), ReplaceString, (IsCaseInsensetive==true)?RegexOptions.IgnoreCase: RegexOptions.None);  
		}
		public static String[] SplitString(String SourceString, String SearchString, bool IsCaseInsensetive)
		{
			return Regex.Split (SourceString, Regex.Escape(SearchString), (IsCaseInsensetive==true)?RegexOptions.IgnoreCase: RegexOptions.None);  
		}
		public static  bool IsSubStringExist(String StringToSearch, String SubStringToLookFor, bool IsCaseInsensitive)
		{
			return Regex.IsMatch(StringToSearch, Regex.Escape(SubStringToLookFor), (IsCaseInsensitive==true)?RegexOptions.IgnoreCase: RegexOptions.None);
		}

		public static bool IsFileReadOnly(string fileName, bool ignoreIfNotExist)
		{
			bool isIgnoreOperation = false;
			if (ignoreIfNotExist == true)
			{
				if (File.Exists(fileName) == false)
					isIgnoreOperation= true;
			}

			if (isIgnoreOperation == false)
				return ((File.GetAttributes(fileName) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);
			else
				return false;
		}

		public static bool IsFileReadOnly(string fileName)
		{
			return IsFileReadOnly(fileName, false);
		}

		public static void ResetReadOnlyFileAttribute(string fileName, bool isIgnoreIfNotExist)
		{
			bool ignoreOperation = false;
			if (isIgnoreIfNotExist == true)
			{
				if (File.Exists(fileName) == false)
					ignoreOperation = true;
			}
					
			if (ignoreOperation == false)
				File.SetAttributes(fileName, File.GetAttributes(fileName) & (~FileAttributes.ReadOnly));
		}
		
		public static void ShowNotImplementedMessage(string functionName)
		{
			MessageBox.Show("The " + functionName  + " is not implemented yet. This might be however included in future relases.", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public static void DebugPrint(object message)
		{
			System.Diagnostics.Debug.WriteLine(message);
		}
		
		public static string EncryptString(string password, string stringToEncrypt)
		{
			//Normalize password to exact 8 char as req by DES
			if (password.Length > 8)
				password=password.Substring(0, 8);
			else if (password.Length < 8)
			{
				int add=8-password.Length;
				for (int i=0; i<add; i++)
					password=password+i;
			}

			UnicodeEncoding thisUnicodeEncoding = new UnicodeEncoding();
			byte[] key = System.Text.Encoding.UTF8.GetBytes(password);
			byte[] textToEncryptAsBytes = thisUnicodeEncoding.GetBytes(stringToEncrypt);
			
			
			MemoryStream encryptedMemoryStream = new MemoryStream();
			
			DESCryptoServiceProvider desCryptoProvider = new DESCryptoServiceProvider();
			CryptoStream thisCryptoStream = new CryptoStream(encryptedMemoryStream,
				desCryptoProvider.CreateEncryptor(key, key),   
				CryptoStreamMode.Write);
			
			thisCryptoStream.Write(textToEncryptAsBytes, 0, textToEncryptAsBytes.Length);
			thisCryptoStream.FlushFinalBlock();
			
			return Convert.ToBase64String(encryptedMemoryStream.ToArray());
		}		
		
		public static string DecryptString(string password, string stringToDecrypt)
		{
			//Normalize password to exact 8 char as req by DES
			if (password != null)
			{
				if (password.Length > 8)
					password=password.Substring(0, 8);
				else if (password.Length < 8)
				{
					int add=8-password.Length;
					for (int i=0; i<add; i++)
						password=password+i;
				}
			}
			else
				for (int i=0; i<8; i++)
					password=password+i;
			
			UnicodeEncoding thisUnicodeEncoding = new UnicodeEncoding();
			byte[] key = System.Text.Encoding.UTF8.GetBytes(password);
			byte[] bytesToDecrypt = Convert.FromBase64String(stringToDecrypt);
			
			MemoryStream decryptedMemoryStream = new MemoryStream();
			
			DESCryptoServiceProvider desCryptoProvider = new DESCryptoServiceProvider();
			CryptoStream thisCryptoStream = new CryptoStream(decryptedMemoryStream,
				desCryptoProvider.CreateDecryptor(key, key),   
				CryptoStreamMode.Write);
			
			thisCryptoStream.Write(bytesToDecrypt, 0, bytesToDecrypt.Length);
			thisCryptoStream.FlushFinalBlock();
			
			return thisUnicodeEncoding.GetString(decryptedMemoryStream.ToArray());
		}		
		
		public static byte[] StringToByteArray(string stringToConvert)
		{
			return Convert.FromBase64String(stringToConvert);
		}
		
		public static string ByteArrayToString(byte[] byteArrayToConvert)
		{
			return Convert.ToBase64String(byteArrayToConvert);
		}
		
		public static string SerializeObjectToString(object anyObject)
		{
			BinaryFormatter serializationFormatter = new BinaryFormatter();
			MemoryStream buffer = new MemoryStream();
			serializationFormatter.Serialize(buffer, anyObject);
			return ByteArrayToString(buffer.ToArray());
		}
		
		public static object DeserializeObjectFromString(string serializedContent)
		{
			BinaryFormatter serializationFormatter = new BinaryFormatter();
			MemoryStream buffer = new MemoryStream(StringToByteArray(serializedContent));
			return serializationFormatter.Deserialize(buffer);
		}
		
		public static string GetLoggedInUserID()
		{
			return System.Environment.UserDomainName + "." + System.Environment.MachineName + "." + System.Environment.UserName;
		}


		public static bool DetectAndWarnForOtherProcessInstances(bool warn)
		{
			bool bIsPreviousInstanceActivated = false;
			
			//Get previous instances
			Process thisInstance = Process.GetCurrentProcess();
			Process[] previousInstances = Process.GetProcessesByName(thisInstance.ProcessName);
			if (previousInstances.Length > 1)
			{
				bool activatePreviousInstance = false;
				if (warn==true)
					activatePreviousInstance = (MessageBox.Show("There are other " + (previousInstances.Length-1).ToString() + " instances of " + thisInstance.ProcessName + " are running. Do you wish to bring other instance in front instead?", "Multiple Instance Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)==DialogResult.Yes);
				else
					activatePreviousInstance = true;

				if (activatePreviousInstance==true)
				{
					foreach (Process previousInstance in previousInstances)
					{
						if (previousInstance.Id != thisInstance.Id)
						{
							bIsPreviousInstanceActivated = true;
							if (IsIconic(previousInstance.MainWindowHandle))
							{
								ShowWindowAsync(previousInstance.MainWindowHandle,SW_RESTORE);
							}
							SetForegroundWindow(previousInstance.MainWindowHandle);

						}
					}
				}
			}
			
			return bIsPreviousInstanceActivated;
		}	
		
		/// <summary>
		/// Test command line: test "c:\...\abc.xyz" -nosave -backup "C:\temp\"
		/// </summary>
		/// <param name="commandLineParameters"></param>
		/// <param name="positionalParameterNames"></param>
		/// <returns></returns>
		public static StringDictionary CommandLineParameterArrayToOptions(string[] commandLineParameters, string[] positionalParameterNames, StringDictionary validSwitchesAndDescription)
		{
			StringDictionary commandLineNamedOptions = new StringDictionary();
			
			string lastParameterSwitch = "";
			string thisParameter ="";
			int nextpositionalParameterNameIndex = 0;
			
			//Loop throw each command line param
			for(int i=0; i<commandLineParameters.Length; i++)
			{
				thisParameter = commandLineParameters[i];
				
				//is this command line switch
				if (thisParameter.StartsWith("/") || thisParameter.StartsWith("-"))
				{
					//If this end of command line
					if (i == (commandLineParameters.Length-1))
					{
						//This must be last switch without value
						lastParameterSwitch = thisParameter.Substring(1);
					}

					//Is any value for past switch expected?
					if (lastParameterSwitch != "")
					{
						RaiseExceptionIfInvalidCommandLineSwitch(lastParameterSwitch, validSwitchesAndDescription);
						commandLineNamedOptions.Add(lastParameterSwitch, true.ToString());
					}
					lastParameterSwitch = thisParameter.Substring(1);	//Anything other then first char which indicates a switch
				}
				else
				{
					//Is any value for past switch expected?
					if (lastParameterSwitch != "")
					{
						RaiseExceptionIfInvalidCommandLineSwitch(lastParameterSwitch, validSwitchesAndDescription);
						commandLineNamedOptions.Add(lastParameterSwitch, thisParameter);
						lastParameterSwitch = "";
					}
					else
					{
						//TODO : check bounds & null reference for positionalParameterNames
						commandLineNamedOptions.Add(positionalParameterNames[nextpositionalParameterNameIndex++], thisParameter);
					}
				}
			}
			return commandLineNamedOptions;
		}

		public static string GetHelpMessageIfHelpSwitchInCommandLine(StringDictionary processedCommandLineOptions, StringDictionary validSwitchesAndDescription)
		{
			bool isHelpSwitchPresent = (processedCommandLineOptions.ContainsKey("?") || processedCommandLineOptions.ContainsKey("help"));
			if (isHelpSwitchPresent==true)
			{
				return BuildCommandLineHelpString(validSwitchesAndDescription);
			}
			else
				return string.Empty;
		}
		
		private static void RaiseExceptionIfInvalidCommandLineSwitch(string switchNameToTest, StringDictionary validSwitchesAndDescription)
		{
			if (validSwitchesAndDescription != null)
			{
				if ((((switchNameToTest == "?") || (switchNameToTest == "help"))==false) && (validSwitchesAndDescription.ContainsKey(switchNameToTest)==false))
				{
					string exceptionMessage = "The command line switch '"+ switchNameToTest  +"' is not recognized.\n" + BuildCommandLineHelpString(validSwitchesAndDescription);
					
					throw new System.ArgumentException(exceptionMessage, switchNameToTest);
				}
			}
		}
		
		public static string BuildCommandLineHelpString(StringDictionary validSwitchesAndDescription)
		{
			string helpString = "Command line arguments:\n" + Path.GetFileNameWithoutExtension(Application.ExecutablePath) + " -[optionName1] optionValue1 -[optionName2] optionValue2 ..." + "\n\nFollowing are the valid command line switches:\n";
			foreach(string switchName in validSwitchesAndDescription.Keys)
			{
				helpString += ( "-" + switchName + " : " + validSwitchesAndDescription[switchName] + "\n ");
			}
			if (helpString.EndsWith("\n")==true) helpString = helpString.Substring(0, helpString.Length - "\n".Length);
			
			return helpString;
		}
		
		public static PropertyBag GetPropertyBagForStaticMembers(Type t)
		{
			MemberInfo[] staticFields = t.GetMembers(BindingFlags.Public | BindingFlags.Static);
			PropertyBag staticFieldInfoBag = new PropertyBag();

			foreach (MemberInfo staticField in staticFields)
			{
				if ((staticField.MemberType == MemberTypes.Field) || (staticField.MemberType == MemberTypes.Property))
				{
					BindingFlags bindingFlagsForFieldInvoke = ((staticField.MemberType==MemberTypes.Field)?BindingFlags.GetField:BindingFlags.GetProperty) | BindingFlags.Static | BindingFlags.Default | BindingFlags.Public;
					object propertyValue = t.InvokeMember(staticField.Name, bindingFlagsForFieldInvoke, null, null, new object[]{});
					staticFieldInfoBag.Properties.Add(new PropertySpec(staticField.Name, staticField.DeclaringType, "Misc", "", 
						propertyValue));
				}
			}
			staticFieldInfoBag.GetValue  += new PropertySpecEventHandler(CommonFunctions.PropertyBagForStaticMembersGetValueHandler);
			return staticFieldInfoBag;
		}
		
		public static void PropertyBagForStaticMembersGetValueHandler(object sender, PropertySpecEventArgs e)
		{
			e.Value = e.Property.DefaultValue;		
		}

		public static string SelectFileUsingSaveDialog(SaveFileDialog saveDialog, string defaultFileName)
		{
			string fileNameSelected = defaultFileName;
			bool isFileNameSelected = false;
			while(!isFileNameSelected)
			{
				if ((fileNameSelected == string.Empty))
				{
					if (saveDialog.ShowDialog()== DialogResult.OK)
					{
						fileNameSelected = saveDialog.FileName;
					}
					else
					{	
						fileNameSelected = "";
						isFileNameSelected = true;	//User cancelled saving
					}
				}
				
				//Check if file is read only
				if (fileNameSelected != string.Empty)
				{
					if (CommonFunctions.IsFileReadOnly(fileNameSelected, true)==true)
					{
						if (MessageBox.Show("The document file is readonly. Do you wish to overwrite this file?", "Read-only file", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)==DialogResult.Yes)
							isFileNameSelected = true;
						else
						{
							fileNameSelected = string.Empty;
							isFileNameSelected = false;
						}
					}
					else isFileNameSelected = true;
				}
			}
			return fileNameSelected;
		}

		public static void AssociateFileTypeExtention(string fileTypeExtention, string mimeType, string fileTypeDescription, string commandLineToOpenFileType)
		{
			// Register custom extension with the shell
			using( RegistryKey key =
					   Registry.ClassesRoot.CreateSubKey("." + fileTypeExtention) ) 
			{
				// Map custom  extension to a ProgID
				key.SetValue(null, fileTypeExtention + "file");
			}

			//Put file type description
			using( RegistryKey key =
					   Registry.ClassesRoot.CreateSubKey(fileTypeExtention + "file") ) 
			{
				// Map ProgID to an Open action for the shell
				key.SetValue(null, fileTypeDescription);
				key.SetValue("Content Type", mimeType);
			}

			// Register open command with the shell
			string cmdkey = fileTypeExtention + @"file\shell\open\command";
			using( RegistryKey key =
					   Registry.ClassesRoot.CreateSubKey(cmdkey) ) 
			{
				// Map ProgID to an Open action for the shell
				key.SetValue(null, commandLineToOpenFileType);
			}
		}

		public static string DateTimeToString(DateTime valueToConvert)
		{
			return valueToConvert.ToUniversalTime().ToString("r");
		}
	}
}
