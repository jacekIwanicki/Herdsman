using HerdsmanProject.Animals;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace HerdsmanProject.Common
{
    public class AnimalSpawner : MonoBehaviour
    {
        [SerializeField]
        private AnimalController animalPrefab;
        [SerializeField]
        private int initialAnimalsToSpawn = 6;
        [SerializeField]
        private Collider2D gameAreaCollider;
        [SerializeField]
        private float minSpawnInterval = 2f;
        [SerializeField]
        private float maxSpawnInterval = 5f;
        [SerializeField]
        private int minAnimalsToSpawn = 1;
        [SerializeField]
        private int maxAnimalsToSpawn = 4;
        [SerializeField]
        private bool intervalSpawnEnable = false;

        private ObjectPool<AnimalController> objectPool;
        private Coroutine spawnCoroutine;
        private Vector2 spawnPosition;

        public static event Action<AnimalController> OnAnimalReturnToPool;

        private void Start()
        {
            objectPool = new ObjectPool<AnimalController>
            (
                CreateAnimal,
                OnTakeFromPool,
                OnReturnToPool,
                OnDestroyAnimal,
                false,
                initialAnimalsToSpawn
            );
            SpawnInitialAnimals();
        }

        private AnimalController CreateAnimal()
        {
            AnimalController newAnimal = Instantiate(animalPrefab);
            newAnimal.GetComponent<AnimalController>().Initialize(this);
            return newAnimal;
        }

        private void OnTakeFromPool(AnimalController animal)
        {
            animal.gameObject.SetActive(true);
        }

        private void OnReturnToPool(AnimalController animal)
        {
            OnAnimalReturnToPool?.Invoke(animal);
            animal.gameObject.SetActive(false);
        }

        private void OnDestroyAnimal(AnimalController animal)
        {
            Destroy(animal.gameObject);
        }

        public Collider2D GetAreaCollider()
        {
            return gameAreaCollider;
        }
        private void SpawnInitialAnimals()
        {
            for (int i = 0; i < initialAnimalsToSpawn; i++)
            {
                SpawnAnimal();
            }

            if (intervalSpawnEnable)
            {
                spawnCoroutine = StartCoroutine(OngoingSpawn());
            }
        }

        private IEnumerator OngoingSpawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnInterval, maxSpawnInterval));
                int animalsToSpawn = UnityEngine.Random.Range(minAnimalsToSpawn, maxAnimalsToSpawn);
                for (int i = 0; i < animalsToSpawn; i++)
                {
                    SpawnAnimal();
                }
            }
        }

        private void SpawnAnimal()
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            AnimalController animalToSpawn = objectPool.Get();
            animalToSpawn.transform.position = spawnPosition;
        }

        private Vector2 GetRandomSpawnPosition()
        {
            spawnPosition = new Vector2
            (
                UnityEngine.Random.Range(gameAreaCollider.bounds.min.x, gameAreaCollider.bounds.max.x),
                UnityEngine.Random.Range(gameAreaCollider.bounds.min.y, gameAreaCollider.bounds.max.y)
            );

            return spawnPosition;
        }

        public void ReturnAnimalToPool(AnimalController animal)
        {
            objectPool.Release(animal);
        }
    }
}