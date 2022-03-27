
using CommonBaseUI.Settings;

namespace CommonBaseUI.Data
{
    public class GameSettingsDataManager
    {
        public GameSettingsDataManager(IJsonUtil jsonUtil)
        {
            this.jsonUtil = jsonUtil;
        }

        private readonly GameSettingsData gameSettingsData = new GameSettingsData();
        private readonly IJsonUtil jsonUtil;

        private const string Filename = "GameSettings";

        #region Property

        public float SoundVolume
        {
            get => gameSettingsData.soundVolume;
            set
            {
                gameSettingsData.soundVolume = value;
                jsonUtil.SaveToJson(Filename, gameSettingsData);
            }
        }

        public float MusicVolume
        {
            get => gameSettingsData.musicVolume;
            set
            {
                gameSettingsData.musicVolume = value;
                jsonUtil.SaveToJson(Filename, gameSettingsData);
            }
        }

        public float VoiceVolume
        {
            get => gameSettingsData.voiceVolume;
            set
            {
                gameSettingsData.voiceVolume = value;
                jsonUtil.SaveToJson(Filename, gameSettingsData);
            }
        }

        public bool FullScreen
        {
            get => gameSettingsData.fullScreen;
            set
            {
                gameSettingsData.fullScreen = value;
                jsonUtil.SaveToJson(Filename, gameSettingsData);
            }
        }

        public int ResWidth
        {
            get => gameSettingsData.resWidth;
            set
            {
                gameSettingsData.resWidth = value;
                jsonUtil.SaveToJson(Filename, gameSettingsData);
            }
        }

        public int ResHeight
        {
            get => gameSettingsData.resHeight;
            set
            {
                gameSettingsData.resHeight = value;
                jsonUtil.SaveToJson(Filename, gameSettingsData);
            }
        }

        public ScreenResolutions16and9 Resolution
        {
            get => gameSettingsData.resolution;
            set
            {
                gameSettingsData.resolution = value;
                jsonUtil.SaveToJson(Filename, gameSettingsData);
            }
        }

        // public Languages Languages
        // {
        //     get => gameSettingsData.currentLanguage;
        //     set
        //     {
        //         gameSettingsData.currentLanguage = value;
        //         saveLoadJson.SaveToJson(Filename, gameSettingsData);
        //     }
        // }

        #endregion

    }
}