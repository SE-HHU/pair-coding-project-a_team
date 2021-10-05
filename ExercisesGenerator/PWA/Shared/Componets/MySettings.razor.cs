using PWA.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWA.Shared.Componets
{
    public partial class MySettings
    {
        [Parameter]
        public WebSettings LocalSettings { get; set; }

        protected async override Task<Task> OnInitializedAsync()
        {
            await LoadLocalSettings();
            LocalSettings.SetSettings();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 重置为默认设置
        /// </summary>
        private void SetDefault()
        {
            LocalSettings.SetDefault();
            SaveLocalSettings();
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

        /// <summary>
        /// 保存设置到 Local Storage
        /// </summary>
        private async void SaveLocalSettings()
        {
            await JS.InvokeVoidAsync("BlazorSetLocalStorage", "LocalSettings", JsonConvert.SerializeObject(LocalSettings));
            LocalSettings.SetSettings();
        }
    }
}
