using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShahJe.Blazor.NavigationTab
{
    public class NavigationTab
    {
        private Dictionary<int, string> Mapping { get; }

        private NavigationManager NavigationManager { get; }

        private string BaseUrl { get; }

        public int TabIndex { get; set; }

        public NavigationTab(string baseUrl, Dictionary<int, string> tabIndexUrlMapping, NavigationManager navigationManager)
        {
            BaseUrl = baseUrl;
            Mapping = tabIndexUrlMapping;
            NavigationManager = navigationManager;
        }

        private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            if (!e.IsNavigationIntercepted)
            {
                return;
            }

            var lastSlashIndex = e.Location.LastIndexOf("/");
            var route = e.Location.Substring(lastSlashIndex, e.Location.Length - lastSlashIndex);
            OnNavigatedTo(route);
        }

        public void TabIndexChanged(int tabIndex)
        {
            if (TabIndex == tabIndex)
            {
                return;
            }

            if (Mapping.TryGetValue(tabIndex, out var path))
            {
                TabIndex = tabIndex;
                NavigationManager.NavigateTo($"{BaseUrl}{path}");
                return;
            }

            throw new ArgumentOutOfRangeException(nameof(tabIndex), "NavigationTab is not configured properly.");
        }

        public void OnNavigatedTo()
        {
            OnNavigatedTo(NavigationManager.Uri);
        }

        public void OnNavigatedTo(string url)
        {
            var lastSlashIndex = url.LastIndexOf("/");
            var route = url.Substring(lastSlashIndex, url.Length - lastSlashIndex).Trim().ToLower();

            if (string.IsNullOrEmpty(route))
            {
                if (TabIndex != 0)
                {
                    TabIndexChanged(0);
                }

                return;
            }

            var map = Mapping.FirstOrDefault(x => StringComparer.OrdinalIgnoreCase.Equals(x.Value, route));

            if (string.IsNullOrEmpty(map.Value))
            {
                TabIndexChanged(0);
                return;
            }

            if (TabIndex == map.Key)
            {
                return;
            }

            TabIndex = map.Key;
        }
    }
}