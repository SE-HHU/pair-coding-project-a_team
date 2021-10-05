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

        private WebSettings LocalSettings = new WebSettings();

        protected async override Task<Task> OnInitializedAsync()
        {
            await LoadLocalSettings();
            //加载设置, 防止在未设置的情况下生成习题出错
            LocalSettings.SetSettings();

            return Task.CompletedTask;
        }

        /// <summary>
        /// 弹窗显示消息
        /// </summary>
        /// <param name="text">需要显示的消息</param>
        private async void ShowMessage(String text)
        {
            await JS.InvokeVoidAsync("ShowMessage", text);
        }

        /// <summary>
        /// 加载 Local Storage
        /// </summary>
        /// <returns></returns>
        private async Task<Task> LoadLocalSettings()
        {
            String json = await JS.InvokeAsync<String>("BlazorGetLocalStorage", "LocalSettings");
            WebSettings newSettings;
            try
            {
                newSettings = JsonConvert.DeserializeObject<WebSettings>(json);
                if (newSettings != null)
                {
                    LocalSettings.CopyFrom(newSettings);
                }
            }//应对首次使用时不存在 Local Storage 的情况
            catch
            {
                ShowMessage("未检测到本地设置, 已为您加载默认配置");
            }

            return Task.CompletedTask;
        }
    }
}
