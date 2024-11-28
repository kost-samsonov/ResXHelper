using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell.Settings;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;
using VSIXProject4.Model;
using VSIXProject4.Options;

namespace VSIXProject4
{
  internal partial class OptionsProvider
  {
    // Register the options with this attribute on your package class:
    // [ProvideOptionPage(typeof(OptionsProvider.SettingsOptions), "VSIXProject4", "Settings", 0, 0, true, SupportsProfiles = true)]
    [ComVisible(true)]
    public class SettingsOptions : DialogPage
    {
      private const string _collectionName = "ResXHelper2022Settings";

      public List<ResourceLanguage> DefaultLanguages { get; internal set; } = new List<ResourceLanguage>();
      public bool UseSuffix { get; internal set; } = true;
      public string SuffixString { get; internal set; } = "Resources";


      protected override IWin32Window Window
      {
        get
        {
          var page = new SettingsForm(DefaultLanguages)
          {
            CustomOptionsPage = this
          };
          page.Initialize();
          return page;
        }
      }
      //From https://stackoverflow.com/questions/32751040/store-array-in-options-using-dialogpage
      public override void SaveSettingsToStorage()
      {
        ThreadHelper.ThrowIfNotOnUIThread();
        base.SaveSettingsToStorage();
        var settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
        var userSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

        if (!userSettingsStore.CollectionExists(_collectionName))
        {
          userSettingsStore.CreateCollection(_collectionName);
        }

        var json = JsonSerializer.Serialize(DefaultLanguages);
        userSettingsStore.SetString(_collectionName, nameof(DefaultLanguages), json);
        userSettingsStore.SetString(_collectionName, nameof(UseSuffix), UseSuffix.ToString());
        userSettingsStore.SetString(_collectionName, nameof(SuffixString), SuffixString);

      }

      public override void LoadSettingsFromStorage()
      {
        ThreadHelper.ThrowIfNotOnUIThread();
        base.LoadSettingsFromStorage();
        var settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
        var userSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
        if (!userSettingsStore.CollectionExists(_collectionName))
        {
          return;
        }

        if (userSettingsStore.PropertyExists(_collectionName, nameof(DefaultLanguages)))
        {
          DefaultLanguages = JsonSerializer.Deserialize<List<ResourceLanguage>>(userSettingsStore.GetString(_collectionName, nameof(DefaultLanguages)));
        }
        if (userSettingsStore.PropertyExists(_collectionName, nameof(UseSuffix)))
        {
          UseSuffix = Convert.ToBoolean(userSettingsStore.GetString(_collectionName, nameof(UseSuffix)));
        }

        if (userSettingsStore.PropertyExists(_collectionName, nameof(SuffixString)))
        {

          SuffixString = userSettingsStore.GetString(_collectionName, nameof(SuffixString));
        }
      }
    }
  }

}
