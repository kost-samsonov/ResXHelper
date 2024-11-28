using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Windows.Forms;
using VSIXProject4.Model;
using static VSIXProject4.OptionsProvider;

namespace VSIXProject4.Options
{
  public partial class SettingsForm : UserControl
  {
    public SettingsForm(IEnumerable<ResourceLanguage> defaultLanguages)
    {
      InitializeComponent();
      SelectedLanguages = new BindingList<ResourceLanguage>(defaultLanguages.ToList());
    }

    public void Initialize()
    {
      LoadLanguages();
      LBAllLanguages.DataSource = AllLanguages;
      LBAllLanguages.DisplayMember = nameof(ResourceLanguage.Name);
      LBSelectedLanguages.DataSource = SelectedLanguages;
      LBSelectedLanguages.DisplayMember = nameof(ResourceLanguage.Name);
    }

    public BindingList<ResourceLanguage> AllLanguages { get; set; } = new BindingList<ResourceLanguage>();

    public BindingList<ResourceLanguage> SelectedLanguages { get; set; } = new BindingList<ResourceLanguage>();

    internal SettingsOptions CustomOptionsPage { get; set; }

    private void LoadLanguages()
    {
      var assembly = Assembly.GetExecutingAssembly();
      const string resourceName = "VSIXProject4.Resources.languages.json";
      var stream = assembly.GetManifestResourceStream(resourceName);
      using var reader = new StreamReader(stream);
      var list = reader.ReadToEnd();
      List<ResourceLanguage> result = LoadLanguages2();  // JsonSerializer.Deserialize<List<ResourceLanguage>>(list);
      AllLanguages = new BindingList<ResourceLanguage>(result.Where(_ => !SelectedLanguages.Any(sl => sl.Code == _.Code)).ToList());
    }

    private List<ResourceLanguage> LoadLanguages2()
    {
      var allLang = new ObservableCollection<ResourceLanguage>();
      foreach (CultureInfo info in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        ResourceLanguage rl = new ResourceLanguage();
        rl.Code = info.Name;
        //if (info.Name.Length == 2)
        //{
        //  rl.Code = info.TextInfo.CultureName ;
        //}

        rl.NativeName = info.NativeName;
        rl.Name = string.Format("{0} ({1})", info.EnglishName, rl.Code);
        allLang.Add(rl);
      }
      CultureInfo cul = CultureInfo.GetCultureInfo("ru-RU");

      return allLang.ToList();

    }

    private void LBAllLanguages_DoubleClick(object sender, EventArgs e)
    {
      if (((ListBox)sender).SelectedItem is ResourceLanguage item)
      {
        AddSelectedItem(item);
      }
    }

    private void AddSelectedItem(ResourceLanguage item)
    {
      if (item != null)
      {
        AllLanguages.Remove(item);
        SelectedLanguages.Add(item);
        LBSelectedLanguages.Refresh();
        LBAllLanguages.Refresh();
        CustomOptionsPage.DefaultLanguages = SelectedLanguages.ToList();
      }
    }

    private void RemoveSelectedItem(ResourceLanguage item)
    {
      if (item != null)
      {
        SelectedLanguages.Remove(item);
        AllLanguages.Add(item);
        LBSelectedLanguages.Refresh();
        LBAllLanguages.Refresh();
        CustomOptionsPage.DefaultLanguages = SelectedLanguages.ToList();
      }
    }

    private void LBSelectedLanguages_DoubleClick(object sender, EventArgs e)
    {
      if (((ListBox)sender).SelectedItem is ResourceLanguage item)
      {
        RemoveSelectedItem(item);
      }
    }

    private void BtnAddOnClick(object sender, EventArgs e)
    {
      if (LBAllLanguages.SelectedItem is ResourceLanguage item)
      {
        AddSelectedItem(item);
      }
    }

    private void BtnRemoveOnClick(object sender, EventArgs e)
    {
      if (LBSelectedLanguages.SelectedItem is ResourceLanguage item)
      {
        RemoveSelectedItem(item);
      }
    }

  }
}
