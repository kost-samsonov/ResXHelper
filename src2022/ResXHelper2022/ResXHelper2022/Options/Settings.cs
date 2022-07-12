﻿using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ResXHelper2022
{
    internal partial class OptionsProvider
    {
        // Register the options with this attribute on your package class:
        // [ProvideOptionPage(typeof(OptionsProvider.SettingsOptions), "ResXHelper2022", "Settings", 0, 0, true, SupportsProfiles = true)]
        [ComVisible(true)]
        public class SettingsOptions : BaseOptionPage<Settings> { }
        //public class SettingsOptions : DialogPage
        //{
        //    private const string _collectionName = "ResXHelperSettings";

        //    public List<ResourceLanguage> DefaultLanguages { get; internal set; } = new List<ResourceLanguage>();

        //    protected override IWin32Window Window
        //    {
        //        get
        //        {
        //            var page = new SettingsForm(DefaultLanguages);
        //            page.CustomOptionsPage = this;
        //            page.Initialize();
        //            return page;
        //        }
        //    }
        //    //From https://stackoverflow.com/questions/32751040/store-array-in-options-using-dialogpage
        //    public override void SaveSettingsToStorage()
        //    {
        //        ThreadHelper.ThrowIfNotOnUIThread();
        //        base.SaveSettingsToStorage();
        //        var settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
        //        var userSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

        //        if (!userSettingsStore.CollectionExists(_collectionName))
        //        {
        //            userSettingsStore.CreateCollection(_collectionName);
        //        }

        //        var json = JsonSerializer.Serialize(DefaultLanguages);
        //        userSettingsStore.SetString(_collectionName, nameof(DefaultLanguages), json);
        //    }

        //    public override void LoadSettingsFromStorage()
        //    {
        //        ThreadHelper.ThrowIfNotOnUIThread();
        //        base.LoadSettingsFromStorage();
        //        var settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
        //        var userSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

        //        if (!userSettingsStore.PropertyExists(_collectionName, nameof(DefaultLanguages)))
        //        {
        //            return;
        //        }
        //        DefaultLanguages = JsonSerializer.Deserialize<List<ResourceLanguage>>(userSettingsStore.GetString(_collectionName, nameof(DefaultLanguages)));
        //    }
        //}
    }

    public class Settings : BaseOptionModel<Settings>
    {
        [Category("ResX Helper 2022")]
        [DisplayName("Default languages")]
        [Description("An informative description.")]
        [DefaultValue(true)]
        public bool MyOption { get; set; } = true;
    }
}