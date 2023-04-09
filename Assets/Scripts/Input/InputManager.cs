using UnityEngine;

namespace Sifter
{
    public class InputManager : MonoBehaviour
    {
        public Inputactions Inputactions;
        public static InputManager Singleton { get; private set; }

        void Awake()
        {
            if (Singleton == null)
                Singleton = this;
            else
                Destroy(gameObject);
            Inputactions = new Inputactions();
            Inputactions.Enable();
        }
    }
}