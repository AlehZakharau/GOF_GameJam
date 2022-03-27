using Code.GamePlay;
using Code.UI;
using CommonBaseUI.Data;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;
using VContainer.Unity;

namespace Code
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private DogView dogView;
        [Header("Data Base")]
        [SerializeField] private GameConfig gameConfig;
        [Header("Audio")]
        [SerializeField] private AudioDB audioDB;
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AudioSourceFabric audioFabric;
        [Header("UI")]
        [SerializeField] private ButtonView[] buttons;
        [SerializeField] private Window[] windows;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<DogMoveSystem>();
            builder.RegisterEntryPoint<PlatformController>();
            builder.Register<IPlayerInput, PlayerInput>(Lifetime.Singleton);
            builder.Register<IMediator, MediatorUI>(Lifetime.Singleton);

            RegisterComponents(builder);
            RegisterDataBase(builder);
            RegisterUI(builder);
            RegisterAudio(builder);
            
            base.Configure(builder);
        }

        private void RegisterDataBase(IContainerBuilder builder)
        {
            builder.Register<IDataManager, DataManager>(Lifetime.Singleton);
            builder.Register<GameSettingsDataManager>(Lifetime.Singleton);
            builder.RegisterInstance(gameConfig).As<IGameConfig>();
            
#if UNITY_WEBGL
            //TODO Create Prefab JsonUtilWeb
            builder.RegisterComponentOnNewGameObject<JsonUtilWeb>(Lifetime.Scoped, "JsonUtilWeb")
                .As<IJsonUtil>();
#else
            builder.Register<IJsonUtil, JsonUtil>(Lifetime.Singleton);
#endif
            
        }

        private void RegisterAudio(IContainerBuilder builder)
        {
            builder.Register<IAudioCenter, AudioCenter>(Lifetime.Singleton);
            builder.RegisterComponent(audioFabric).As<IAudioSourceFabric>();
            builder.RegisterInstance(audioDB);
            //builder.RegisterComponent(mixer); // TODO check this, Audio mixer is not Monobehaviour
            builder.Register<AudioMixer>(Lifetime.Singleton);
        }

        private void RegisterUI(IContainerBuilder builder)
        {
            foreach (var button in buttons)
            {
                builder.RegisterComponent(button);
            }

            foreach (var window in windows)
            {
                builder.RegisterComponent(window);
            }
        }

        private void RegisterComponents(IContainerBuilder builder)
        {
            builder.RegisterComponent(dogView);
        }
    }
}