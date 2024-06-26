using HerdsmanProject.Animals;
using HerdsmanProject.UI;
using UnityEngine;

namespace HerdsmanProject.Common
{
    public class YardContoller : MonoBehaviour
    {
        [SerializeField]
        private ScoreController scoreController;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out AnimalController animalController) && animalController.HerdOwner != null)
            {
                animalController.DespawnAnimal();
                scoreController.Score++;
            }
        }
    }
}