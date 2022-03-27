using System;
using System.Collections.Generic;
using UnityEngine;

namespace CommonBaseUI.Data
{
    public interface IDataManager
    {
        public void Save();
        public void Load();
    }

    public class DataManager : IDataManager
    {
        private readonly IGameConfig gameConfig;
        private readonly IJsonUtil jsonUtil;
        
        public DataManager(IJsonUtil jsonUtil, IGameConfig gameConfig)
        {
            this.gameConfig = gameConfig;
            this.jsonUtil = jsonUtil;
        }
        
        public void Save()
        {
            jsonUtil.SaveToJson(gameConfig.CommonData.ToString(), gameConfig.CommonData);
        }

        public void Load()
        {
            jsonUtil.LoadFromJson(gameConfig.CommonData.ToString(), gameConfig.CommonData);
        }
    }
}