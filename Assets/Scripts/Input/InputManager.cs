using UnityEngine;

namespace Sifter
{
    public class InputManager : MonoBehaviour
    {
        public Inputactions Inputactions;
        public static InputManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            Inputactions = new Inputactions();
            Inputactions.Enable();
        }
    }
}