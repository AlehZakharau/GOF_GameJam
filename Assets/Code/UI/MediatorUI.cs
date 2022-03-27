using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.UI
{
    public interface IMediator
    {
        public void Notify(EContext ev);
    }
    public class MediatorUI : IMediator
    {
        private readonly Dictionary<EWindows,Window> windows;
        private readonly IPlayerInput playerInput;
        private readonly AudioCenter audioCenter;

        private Window previousWindow;
        private Window currentWindow;

        private bool isPause;

        public MediatorUI(IEnumerable<Window> windows, IPlayerInput playerInput, AudioCenter audioCenter)
        {
            this.windows = new Dictionary<EWindows, Window>();
            this.playerInput = playerInput;
            this.audioCenter = audioCenter;
            
            
            foreach (var window in windows)
            {
                this.windows.Add(window.WindowType, window);
            }
            currentWindow = this.windows[EWindows.MainMenu];
        }
        public void Notify(EContext ev)
        {
            audioCenter.PlaySound(EAudioClips.Button);
            switch (ev)
            {
                case EContext.NewGame:
                    SceneManager.LoadScene(sceneBuildIndex: 1);
                    break;
                case EContext.Continue:
                    currentWindow.CloseWindow();
                    OpenWindow(windows[EWindows.MainMenu]);
                    isPause = false;
                    break;
                case EContext.Pause:
                    if (!isPause)
                    {
                        OpenWindow(windows[EWindows.Pause]);
                        isPause = true;
                    }
                    else
                    {
                        OpenWindow(windows[EWindows.MainMenu]);
                        isPause = false;
                    }
                    break;
                case EContext.Setting:
                    OpenWindow(windows[EWindows.Settings]);
                    break;
                case EContext.Credits:
                    OpenWindow(windows[EWindows.Credits]);
                    break;
                case EContext.Back:
                    OpenWindow(previousWindow);
                    break;
                case EContext.Tutorial:
                    OpenWindow(windows[EWindows.Tutorial]);
                    isPause = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ev), ev, null);
            }
        }
        
        private void OpenWindow(Window window)
        {
            currentWindow.CloseWindow();
            previousWindow = currentWindow;
            window.OpenWindow();
            currentWindow = window;
        }
    }

    public enum EContext
    {
        NewGame,
        Continue,
        Pause,
        Setting,
        Back,
        Credits,
        Tutorial
    }

    public enum EWindows
    {
        MainMenu,
        Settings,
        Credits,
        Pause,
        Tutorial
    }
}