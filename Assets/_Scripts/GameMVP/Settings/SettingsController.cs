using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.SaveLoad.Data.Interfaces.Game;
using Assets._Scripts.UI._1MenuWindow;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Assets._Scripts.GameControllers.Settings
{

    public class SettingsController : IInitGame, ISaveGame, ILoadGame, IDisposable, IResetGame
    {
        private SettingsModel _model;
        private readonly List<ISettingsView> _views = new(); // сделать тут словарь... вроде как удобнее... или нет??
        private bool _disposed;

        public SettingsController(SettingsModel model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public void Dispose()
        {
            if (_disposed) return;

            for (int i = _views.Count - 1; i >= 0; i--)
            {
                RemoveView(_views[i]);
            }

            _views.Clear();
            _disposed = true;
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

            _model.OnAudioValueChanged += view.SetAudioValue;
            _model.OnMusicValueChanged += view.SetMusicValue;
            _model.OnEffectsValueChanged += view.SetEffectValue;

            view.OnAudioValueChanged += _model.SetAudioValue;
            view.OnMusicValueChanged += _model.SetMusicValue;
            view.OnEffectValueChanged += _model.SetEffectsValue;
        }

        public void RemoveView(ISettingsView view)
        {
            _model.OnAudioValueChanged -= view.SetAudioValue;
            _model.OnMusicValueChanged -= view.SetMusicValue;
            _model.OnEffectsValueChanged -= view.SetEffectValue;

            view.OnAudioValueChanged -= _model.SetAudioValue;
            view.OnMusicValueChanged -= _model.SetMusicValue;
            view.OnEffectValueChanged -= _model.SetEffectsValue;

            _views.Remove(view);
        }

        public void Reset(GameSaveData gameSaveData)
        {
            _model.Reset();
        }

    }
}
