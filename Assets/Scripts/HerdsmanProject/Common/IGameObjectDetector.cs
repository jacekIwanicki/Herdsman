using System;
using System.Collections.Generic;
using UnityEngine;

namespace HerdsmanProject.Common
{
    public interface IGameObjectDetector
    {
        public event Action<GameObject> OnGameObjectDetected;
        public event Action<GameObject> OnGameObjectLost;

        public List<GameObject> GameObjects { get; }

        public T FindComponentInGameObjects<T>();
    }
}