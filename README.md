# Blazor Navigation Tab
Helper class to enable url change on Blazor tab index change and mapping url to Blazor tag index.
Nuget package: https://www.nuget.org/packages/ShahJe.Blazor.NavigationTab

Currently supports Radzen Tabs.

Following code snippet shows how to setup:

    @page "/radzentab"
    @page "/radzentab/tab2"
    @page "/radzentab/tab3"

    @inject NavigationManager NavigationManager

    <h1>Radzen Blazor Navigation Tab Demo</h1>
    <p>Click on tab buttons and notice url also changes. Change the url manually to point to some other tab and appropriate tab will be selected.</p>

    <RadzenTabs SelectedIndex="@NavigationTab.TabIndex" Change="@NavigationTab.TabIndexChanged">
        <Tabs>
            <RadzenTabsItem Text="Tab 1">
                <p>This is tab1</p>
            </RadzenTabsItem>
            <RadzenTabsItem Text="Tab 2">
                <p>This is tab2</p>
            </RadzenTabsItem>
            <RadzenTabsItem Text="Tab 3">
                <p>This is tab3</p>
            </RadzenTabsItem>
        </Tabs>
    </RadzenTabs>

    @code{
      private NavigationTab NavigationTab { get; set; }

      protected override Task OnInitializedAsync()
      {
          if (NavigationTab is null)
          {
              NavigationTab = new NavigationTab(
                  "/radzentab",
                  new Dictionary<int, string>
                  {
                      { 0, "" },
                      { 1, "/tab2" },
                      { 2, "/tab3" }
                          }, NavigationManager);

              NavigationTab.OnNavigatedTo();
          }

          return Task.CompletedTask;
      }

See sample app `ShahJe.Blazor.NavigationTab.Sample` for a working example.
