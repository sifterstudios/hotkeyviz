using Sifter.Keyboard;
using UnityEngine;

namespace Sifter.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] KeyboardPopulator _keyboardPopulator;
        public SaveManager SaveManager = new();

        static GameManager Singleton { get; set; }

        void Awake()
        {
            Singleton = this;
        }
    }
}