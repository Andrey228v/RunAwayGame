using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets._Scripts.SceneLoading
{
    public class AsyncOperationGroup : IDisposable
    {
        private List<AsyncOperationHandle> _handles = new();
        private bool _isDisposed = false;

        public void Dispose()
        {
            if (_isDisposed) return;

            ReleaseAll();
            _isDisposed = true;
        }

        public AsyncOperationHandle this[int index] => _handles[index];

        /// Получить все handle'ы
        public IReadOnlyList<AsyncOperationHandle> GetHandles() => _handles.AsReadOnly();

        public bool IsDone => _handles.All(h => h.IsDone);

        public int Count => _handles.Count;

        public float Progress
        {
            get
            {
                if (_handles.Count == 0) return 1f;
                return _handles.Average(h => h.PercentComplete);
            }
        }



        public AsyncOperationGroup Add(AsyncOperationHandle handle)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(AsyncOperationGroup));

            _handles.Add(handle);
            return this;
        }

        public AsyncOperationGroup AddRange(IEnumerable<AsyncOperationHandle> handles)
        {
            foreach (var h in handles)
                Add(h);
            return this;
        }


        // Ожидать завершения всех операций
        public async UniTask WhenAll()
        {
            await UniTask.WhenAll(_handles.Select(h => h.ToUniTask()));
        }

        // Ожидать завершения всех операций с прогрессом
        public async UniTask WhenAll(IProgress<float> progress)
        {
            while (!IsDone)
            {
                progress.Report(Progress);
                await UniTask.Yield();
            }
            progress.Report(1f);
        }

        // Получить все результаты определённого типа
        public List<T> GetResults<T>() where T : UnityEngine.Object
        {
            return _handles
                .Where(h => h.IsDone && h.Status == AsyncOperationStatus.Succeeded)
                .Select(h => (T)h.Result)
                .Where(r => r != null)
                .ToList();
        }

        public void Clear()
        {
            _handles.Clear();
        }

        public void ReleaseAll()
        {
            foreach (var handle in _handles)
            {
                if (handle.IsValid())
                    Addressables.Release(handle);
            }
            _handles.Clear();
        }
    }
}
