using System.Collections.Generic;
using UnityEngine;

namespace Sifter.Managers
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Singleton;
        public List<string> KeymapNames;

        void Awake()
        {
            if (Singleton != null)
            {
                Destroy(gameObject);
                return;
            }

            Singleton = this;
            LoadKeymap();
        }

        public void LoadKeymap()
        {
            KeymapNames = new List<string>();
            if (ES3.KeyExists("KeymapNames"))
                KeymapNames = ES3.Load<List<string>>("KeymapNames");

            else
                UIManager.Singleton.CreatePopupWithTextInput("No Keymaps Found",
                    "No keymaps were found. Please enter a name for your first keymap.",
                    EventManager.Singleton.OnKeymapCreateConfirmed, EventManager.Singleton.OnPopupCancelled);
        }

        public void SaveKeymap(string keymapName)
        {
            if (KeymapNames.Contains(keymapName))
            {
                UIManager.Singleton.CreatePopupWithTextInput("Keymap Already Exists",
                    "A keymap with that name already exists. Please enter a new name.",
                    EventManager.Singleton.OnKeymapCreateConfirmed, EventManager.Singleton.OnPopupCancelled);
                return;
            }

            KeymapNames.Add(keymapName);
            ES3.Save("KeymapNames", KeymapNames);
        }
    }
}