#pragma checksum "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyChecker.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c7360b73dc252926555a12f786cd52b6caad261e"
// <auto-generated/>
#pragma warning disable 1591
namespace PWA.Shared.Componets
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\_Imports.razor"
using PWA;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\_Imports.razor"
using PWA.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\_Imports.razor"
using PWA.Shared.Componets;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\_Imports.razor"
using PWA.Shared.Models;

#line default
#line hidden
#nullable disable
    public partial class MyChecker : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "row");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", "col-sm-6");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "card");
            __builder.AddMarkupContent(6, "<div class=\"card card-header\">??????????????????</div>\r\n            ");
            __builder.OpenElement(7, "div");
            __builder.AddAttribute(8, "class", "card card-body");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputFile>(9);
            __builder.AddAttribute(10, "OnChange", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Forms.InputFileChangeEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Forms.InputFileChangeEventArgs>(this, 
#nullable restore
#line 8 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyChecker.razor"
                                      LoadProblemsFile

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(11, "accept", ".txt");
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(12, "\r\n    ");
            __builder.OpenElement(13, "div");
            __builder.AddAttribute(14, "class", "col-sm-6");
            __builder.OpenElement(15, "div");
            __builder.AddAttribute(16, "class", "card");
            __builder.AddMarkupContent(17, "<div class=\"card card-header\">??????????????????</div>\r\n            ");
            __builder.OpenElement(18, "div");
            __builder.AddAttribute(19, "class", "card card-body");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputFile>(20);
            __builder.AddAttribute(21, "OnChange", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Forms.InputFileChangeEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Forms.InputFileChangeEventArgs>(this, 
#nullable restore
#line 16 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyChecker.razor"
                                      LoadAnswersFile

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(22, "accept", ".txt");
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(23, "\r\n\r\n");
            __builder.OpenElement(24, "div");
            __builder.AddAttribute(25, "class", "btn-group-vertical");
            __builder.AddAttribute(26, "style", "display:table;margin:0 auto;");
            __builder.OpenElement(27, "button");
            __builder.AddAttribute(28, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 23 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyChecker.razor"
                       Check

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(29, "class", "btn btn-outline-primary");
            __builder.AddMarkupContent(30, "<span class=\"oi oi-check\"></span> ????????????");
            __builder.CloseElement();
            __builder.AddMarkupContent(31, "\r\n    ");
            __builder.OpenElement(32, "button");
            __builder.AddAttribute(33, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 24 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyChecker.razor"
                       SaveFiles

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(34, "class", "btn btn-outline-primary");
            __builder.AddMarkupContent(35, "<span class=\"oi oi-data-transfer-download\"></span> ????????????");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(36, "\r\n");
            __builder.OpenElement(37, "div");
            __builder.AddAttribute(38, "class", "card");
            __builder.OpenElement(39, "textarea");
            __builder.AddAttribute(40, "readonly");
            __builder.AddAttribute(41, "class", "form-control");
            __builder.AddAttribute(42, "spellcheck", "false");
            __builder.AddAttribute(43, "rows", "20");
            __builder.AddAttribute(44, "style", "font-family:Consolas;");
            __builder.AddAttribute(45, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 27 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyChecker.razor"
                               Text

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(46, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => Text = __value, Text));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JS { get; set; }
    }
}
#pragma warning restore 1591
