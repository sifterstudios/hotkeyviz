using Sifter.Keyboard;
using UnityEngine;

namespace Sifter.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Singleton = new();
        [SerializeField] KeyboardPopulator _keyboardPopulator;
        public SaveManager SaveManager = new();

        void Awake()
        {
            if (Singleton != null)
            {
                Destroy(gameObject);
                return;
            }

            Singleton = this;
        }
    }
}