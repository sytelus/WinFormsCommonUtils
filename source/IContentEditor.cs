namespace Sytel.Common.WinForms.ContentEditors
{
	public interface IContentEditor
	{
		string GetContent();
		void SetContentInfo(string contentID, string contentTitle, string content);
		bool IsReadOnly {get;set;}
		bool IsModified {get;}
		bool IsVisible {get;set;}
		void ResetAll();
		void ClearContent();
		string ContentFormat {get;set;}
		string EditorUserFriendlyName {get;}
		string EditorName{get;}
		string ContentCaption {get;set;}
		string ContentID {get;set;}
	}
}