#pragma checksum "C:\Users\CAN FAMİLY\Documents\GitHub\Nova\NovaErp\NovaErp.UI\Views\_ViewStart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "89063adca045b2e7379cff8f06439e21bda71e62"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views__ViewStart), @"mvc.1.0.view", @"/Views/_ViewStart.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\CAN FAMİLY\Documents\GitHub\Nova\NovaErp\NovaErp.UI\Views\_ViewImports.cshtml"
using NovaErp.UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\CAN FAMİLY\Documents\GitHub\Nova\NovaErp\NovaErp.UI\Views\_ViewImports.cshtml"
using NovaErp.UI.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\CAN FAMİLY\Documents\GitHub\Nova\NovaErp\NovaErp.UI\Views\_ViewImports.cshtml"
using NovaErp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\CAN FAMİLY\Documents\GitHub\Nova\NovaErp\NovaErp.UI\Views\_ViewImports.cshtml"
using NovaErp.Models.SiparisModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\CAN FAMİLY\Documents\GitHub\Nova\NovaErp\NovaErp.UI\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\CAN FAMİLY\Documents\GitHub\Nova\NovaErp\NovaErp.UI\Views\_ViewImports.cshtml"
using FormHelper;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\CAN FAMİLY\Documents\GitHub\Nova\NovaErp\NovaErp.UI\Views\_ViewImports.cshtml"
using FluentValidation.AspNetCore;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"89063adca045b2e7379cff8f06439e21bda71e62", @"/Views/_ViewStart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5e376f31cfd09b79d8d551f69fe4712ff35193dd", @"/Views/_ViewImports.cshtml")]
    public class Views__ViewStart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\CAN FAMİLY\Documents\GitHub\Nova\NovaErp\NovaErp.UI\Views\_ViewStart.cshtml"
  
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\CAN FAMİLY\Documents\GitHub\Nova\NovaErp\NovaErp.UI\Views\_ViewStart.cshtml"
   int? kullanıcıId = Context.Session.GetInt32("Id");

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\CAN FAMİLY\Documents\GitHub\Nova\NovaErp\NovaErp.UI\Views\_ViewStart.cshtml"
   string kullanıcıadsoyad = Context.Session.GetString("AdSoyad");

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
