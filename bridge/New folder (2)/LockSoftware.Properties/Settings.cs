using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LockSoftware.Properties;

[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
[CompilerGenerated]
internal sealed class Settings : ApplicationSettingsBase
{
	private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

	public static Settings Default => defaultInstance;

	[SpecialSetting(SpecialSetting.ConnectionString)]
	[DebuggerNonUserCode]
	[DefaultSettingValue("Data Source=.\\SQLEXPRESS;Initial Catalog=RadioLock;Integrated Security=True")]
	[ApplicationScopedSetting]
	public string RadioLockConnStr => (string)this["RadioLockConnStr"];
}
