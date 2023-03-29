using UnityEngine;
using UnityEngine.InputSystem;

namespace Sifter
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] KeyboardPopulator _keyboardPopulator;
       public SaveManager _saveManager = new SaveManager();

        static GameManager Singleton { get; set; }

        void Awake()
        {
            Singleton = this;
        }
    }
}