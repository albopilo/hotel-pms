namespace ComponentDll;

public class TmpClass
{
	protected static string RegKey;

	protected static string userID;

	protected static string userKey;

	static TmpClass()
	{
		RegKey = "";
		userID = "";
		userKey = "";
		RegKey = "&56~01'][Manson]v%#@";
	}

	public bool ChkKey(string Key)
	{
		if (Key.Trim().ToUpper() != RegKey.ToUpper())
		{
			return false;
		}
		return true;
	}

	public void InitRegInfo(string UserID, string UserKey)
	{
		userID = UserID.Trim();
		userKey = UserKey.Trim();
	}
}
