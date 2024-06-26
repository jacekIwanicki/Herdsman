using HerdsmanProject.Animals;
using System.Collections.Generic;
using UnityEngine;

namespace HerdsmanProject.Common
{
    public class HerdOwner : MonoBehaviour
    {
        [SerializeField]
        private int groupSize = 5;

        private List<IAnimal> animalsInGroup;

        public Transform Transfrom => transform;

        private void Awake()
        {
            animalsInGroup = new List<IAnimal>(groupSize);
        }

        public bool TryAddToGroup(IAnimal animal)
        {
            if (animalsInGroup.Contains(animal) || animalsInGroup.Count >= 5)
            {
                return false;
            }

            animalsInGroup.Add(animal);
            return true;
        }

        public void RemoveFromGroup(IAnimal animal)
        {
            if (animalsInGroup.Contains(animal))
            {
                animalsInGroup.Remove(animal);
            }
        }
    }
}