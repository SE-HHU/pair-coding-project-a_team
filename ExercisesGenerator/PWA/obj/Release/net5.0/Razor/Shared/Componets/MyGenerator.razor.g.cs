#pragma checksum "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyGenerator.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d959d0c4ac99f6a6e13ab864a2bce9326ead0a99"
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
    public partial class MyGenerator : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "button");
            __builder.AddAttribute(1, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 3 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyGenerator.razor"
                   Generate

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(2, "Generate");
            __builder.CloseElement();
            __builder.AddMarkupContent(3, "\r\n");
            __builder.OpenElement(4, "button");
            __builder.AddAttribute(5, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 4 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyGenerator.razor"
                   SaveFiles

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(6, "Save Files");
            __builder.CloseElement();
            __builder.AddMarkupContent(7, "\r\n");
            __builder.OpenElement(8, "button");
            __builder.AddAttribute(9, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 5 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyGenerator.razor"
                   Print

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(10, "Print");
            __builder.CloseElement();
            __builder.AddMarkupContent(11, "\r\n");
            __builder.OpenElement(12, "button");
            __builder.AddAttribute(13, "id", "hide");
            __builder.AddAttribute(14, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 6 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyGenerator.razor"
                             ChangeAnswersDisplay

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(15, "Hide");
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\r\n\r\n");
            __builder.OpenElement(17, "div");
            __builder.AddAttribute(18, "id", "showdiv");
            __builder.OpenElement(19, "table");
            __builder.AddAttribute(20, "id", "showexercises");
            __builder.AddAttribute(21, "class", "table table-bordered table-striped");
            __builder.AddAttribute(22, "style", "word-break:break-all; word-wrap:break-word; font-family:Consolas; font-size:large");
            __builder.OpenElement(23, "tbody");
            __builder.AddMarkupContent(24, "<tr><th>\r\n                    题号\r\n                </th>\r\n                <th>\r\n                    习题\r\n                </th>\r\n                <th>\r\n                    答案\r\n                </th></tr>");
#nullable restore
#line 23 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyGenerator.razor"
             foreach (var anExercise in Exercises.Exercises)
            {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(25, "tr");
            __builder.OpenElement(26, "td");
            __builder.AddAttribute(27, "width", "10%");
            __builder.AddContent(28, 
#nullable restore
#line 26 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyGenerator.razor"
                                      anExercise.Number

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(29, "\r\n                    ");
            __builder.OpenElement(30, "td");
            __builder.AddAttribute(31, "width", "60%");
            __builder.AddContent(32, 
#nullable restore
#line 27 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyGenerator.razor"
                                      new MarkupString(Tools.Expression.ExpressionToHTML(anExercise.Problem))

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(33, "\r\n                    ");
            __builder.OpenElement(34, "td");
            __builder.AddAttribute(35, "width", "30%");
            __builder.AddContent(36, 
#nullable restore
#line 28 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyGenerator.razor"
                                      new MarkupString(anExercise.Answer.ToHTML())

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 30 "D:\PairCoding\pair-coding-project-a_team\ExercisesGenerator\PWA\Shared\Componets\MyGenerator.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JS { get; set; }
    }
}
#pragma warning restore 1591
