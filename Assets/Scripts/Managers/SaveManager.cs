using System.Collections.Generic;
using UnityEngine;

namespace Sifter.Managers
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Singleton;
        public List<string> KeymapNames { get; } = new();

        void Awake()
        {
            if (Singleton != null)
            {
                Destroy(gameObject);
                return;
            }

            Singleton = this;
        }

        void Start()
        {
            ES3.LoadInto<string>("KeymapNames", KeymapNames);
        }
    }
}