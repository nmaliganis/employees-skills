// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace employee.skill.fe.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#line 1 "C:\dev\nmal\employees-skills\fe\src\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#line 2 "C:\dev\nmal\employees-skills\fe\src\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#line 3 "C:\dev\nmal\employees-skills\fe\src\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#line 4 "C:\dev\nmal\employees-skills\fe\src\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#line 5 "C:\dev\nmal\employees-skills\fe\src\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#line 6 "C:\dev\nmal\employees-skills\fe\src\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#line 7 "C:\dev\nmal\employees-skills\fe\src\_Imports.razor"
using smarthotel.ui;

#line default
#line hidden
#line 8 "C:\dev\nmal\employees-skills\fe\src\_Imports.razor"
using smarthotel.ui.Shared;

#line default
#line hidden
#line 9 "C:\dev\nmal\employees-skills\fe\src\_Imports.razor"
using Telerik.Blazor;

#line default
#line hidden
#line 10 "C:\dev\nmal\employees-skills\fe\src\_Imports.razor"
using Telerik.Blazor.Components;

#line default
#line hidden
    public partial class MainLayout : LayoutComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#line 61 "C:\dev\nmal\employees-skills\fe\src\Shared\MainLayout.razor"
       
	bool expandNavMenu = false;

       void ToggleNavMenu()
       {
           expandNavMenu = !expandNavMenu;
       }

       bool IsNavAllowed()
       {
           string currUrl = navigationManager.Uri;
           if (currUrl.EndsWith("signin"))
           {
               return false;
           }
           return true;
       }

       bool IsAtRoot()
       {
           string currUrl = navigationManager.Uri;
           if (currUrl.EndsWith("/") || currUrl.EndsWith("/dashboard"))
           {
               return true;
           }
           return false;
       }

       protected override void OnInitialized()
       {
           navigationManager.LocationChanged += OnLocationChanges;
       }

       private void OnLocationChanges(object sender, LocationChangedEventArgs args)
       {
           expandNavMenu = false;
           StateHasChanged();
       }

#line default
#line hidden
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Microsoft.AspNetCore.Components.NavigationManager navigationManager { get; set; }
    }
}
#pragma warning restore 1591