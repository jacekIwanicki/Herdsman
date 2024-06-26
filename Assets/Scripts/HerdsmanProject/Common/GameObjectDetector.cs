using System;
using System.Collections.Generic;
using UnityEngine;

namespace HerdsmanProject.Common
{
    public class GameObjectDetector : MonoBehaviour, IGameObjectDetector
    {
        public List<GameObject> GameObjects { get; private set; } = new List<GameObject>();  // maybe hashset? but in this game it will be small amount of entities

        public event Action<GameObject> OnGameObjectDetected;
        public event Action<GameObject> OnGameObjectLost;

        public T FindComponentInGameObjects<T>()
        {
            foreach (GameObject gameObject in GameObjects)
            {
                if (gameObject.TryGetComponent(out T component))
                {
                    return component;
                }
            }

            return default;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!GameObjects.Contains(collision.gameObject))
            {
                GameObjects.Add(collision.gameObject);
                OnGameObjectDetected?.Invoke(collision.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (GameObjects.Contains(collision.gameObject))
            {
                GameObjects.Remove(collision.gameObject);
                OnGameObjectLost?.Invoke(collision.gameObject);
            }
        }
    }
}