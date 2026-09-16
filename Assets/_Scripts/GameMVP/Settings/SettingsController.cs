using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.UI._1MenuWindow;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.GameControllers.Settings
{

    public class SettingsController
    {
        private SettingsModel _model;
        private readonly List<ISettingsView> _views = new(); // сделать тут словарь... вроде как удобнее
        private bool _disposed;

        public SettingsController(SettingsModel model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public void Initialization(GameSaveData gameSaveData)
        {
            if (gameSaveData.SettingsData == null)
            {
                gameSaveData.SettingsData = new SettingsData();
            }
        }

        public void Load(GameSaveData gameSaveData)
        {
            _model.Load(gameSaveData);
        }

        public void Save(GameSaveData gameSaveData)
        {
            _model.Save(gameSaveData);
        }

        public void AddView(ISettingsView view)
        {
            if (view == null || _views.Contains(view)) return;

            _views.Add(view);

            //Тут сделать подвязки для модели...
        }

        public void RemoveView(ISettingsView view)
        {
            //Здесь сделать отписки от событий...
        }

    }
}
