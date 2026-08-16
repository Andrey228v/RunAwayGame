using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.SaveLoad.Data.Interfaces.Game;
using Assets._Scripts.UI._1MenuWindow.Language;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.GameMVP.Language
{
    public class LanguageController : IInitGame, ISaveGame, ILoadGame, IDisposable
    {
        private readonly LanguageModel _model;
        private readonly List<ILanguageView> _views = new();
        private bool _disposed;

        public LanguageController(LanguageModel model)
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

        public void AddView(ILanguageView view)
        {
            if (view == null || _views.Contains(view)) return;

            _views.Add(view);

            _model.OnLanguageChanged += view.UpdateLanguageDisplay;
            _model.OnMenuVisibilityChanged += view.UpdateVisibility;

            view.OnToggleClicked += _model.ToggleMenuVisibility;
            view.OnLanguageSelected += _model.SetLanguage;

            view.UpdateLanguageDisplay(_model.CurrentLanguage);
            view.UpdateVisibility(false);
        }

        public void RemoveView(ILanguageView view)
        {
            _model.OnLanguageChanged -= view.UpdateLanguageDisplay;
            _model.OnMenuVisibilityChanged -= view.UpdateVisibility;
            view.OnToggleClicked -= _model.ToggleMenuVisibility;
            view.OnLanguageSelected -= _model.SetLanguage;
        }

        public void Dispose()
        {
            if (_disposed) return;

            foreach (var view in _views)
            {
                RemoveView(view);
            }
            _views.Clear();
            _disposed = true;
        }
    }
}
