using PWA.Shared.Models;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PWA.Shared.Models.TableData;

namespace PWA.Pages
{
    public partial class Generator
    {
        public static TableData tableData = new TableData();

        public WebSettings LocalSettings = new WebSettings();

    }
}
