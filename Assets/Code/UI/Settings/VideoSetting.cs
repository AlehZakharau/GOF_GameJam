using System;
using CommonBaseUI.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace CommonBaseUI.Settings
{
    public enum ScreenResolutions16and9
    {
        R_3840_2160,
        R_2560_1440,
        R_2000_1080,
        R_1920_1080,
        R_1280_720,
    }
    public class VideoSetting : MonoBehaviour
    {
        [SerializeField] private Toggle fullScreen;
        [SerializeField] private TMP_Dropdown resolution;
        
        private GameSettingsDataManager data;
        
        [Inject]
        public void Construct(GameSettingsDataManager data)
        {
            this.data = data;
        }
        private void Start()
        {
            resolution.value = (int)data.Resolution;
        }

        public void ChangeScreenResolution()
        {
            var res = GetScreenParameters((ScreenResolutions16and9)resolution.value);
            
            Screen.SetResolution(res.Width, res.Height, Screen.fullScreen);
            data.ResWidth = res.Width;
            data.ResHeight = res.Width;
        }

        public void ChangeScreenMode()
        {
            Screen.fullScreen = fullScreen.isOn;
            data.FullScreen = fullScreen.isOn;
        }
        
        private ScreenResolution GetScreenParameters(ScreenResolutions16and9 screenResolution)
        {
            var res = new ScreenResolution();
            switch (screenResolution)
            {
                case (ScreenResolutions16and9.R_3840_2160):
                    res.Width = 3840;
                    res.Height = 2160;
                    break;
                case (ScreenResolutions16and9.R_2560_1440):
                    res.Width = 2560;
                    res.Height = 1440;
                    break;
                case (ScreenResolutions16and9.R_1920_1080):
                    res.Width = 1920;
                    res.Height = 1080;
                    break;
                case (ScreenResolutions16and9.R_1280_720):
                    res.Width = 1280;
                    res.Height = 720;
                    break;
                case (ScreenResolutions16and9.R_2000_1080):
                    res.Width = 2000;
                    res.Height = 1080;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return res;
        }
    }
    
    public struct ScreenResolution 
    {
        /// <summary>
        /// Ширина экрана
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Высота экрана
        /// </summary>
        public int Height { get; set; }
    }
}