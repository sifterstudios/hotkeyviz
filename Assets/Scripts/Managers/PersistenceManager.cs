using System.Collections.Generic;
using Sifter.DataManagement;
using UnityEngine;

namespace Sifter.Managers
{
    public class PersistenceManager : MonoBehaviour
    {
        public static PersistenceManager Singleton;
        public List<string> KeymapNames;
        public string CurrentKeymapName;
        public List<KeyBinding> CurrentKeymap;
        public Dictionary<string, List<KeyBinding>> Keymaps;

        void Awake()
        {
            if (Singleton != null)
            {
                Destroy(gameObject);
                return;
            }

            Singleton = this;
            LoadKeymapName();
            LoadKeymaps();
            EventManager.Singleton.OnKeymapLoadStart += LoadKeymap;
            EventManager.Singleton.OnKeymapChangedInGUI += LoadKeymap;
        }

        void LoadKeymap(string keymapName = null)
        {
            if (keymapName == "Add new keymap") return;
            if (keymapName != null && keymapName != CurrentKeymapName)
            {
                CurrentKeymapName = keymapName;
                CurrentKeymap = Keymaps[keymapName];
            }

            EventManager.Singleton.OnKeymapLoadComplete.Invoke(CurrentKeymap);
        }

        void LoadKeymaps()
        {
            if (ES3.KeyExists("Keymaps"))
                ES3.Load<Dictionary<string, List<KeyBinding>>>("Keymaps");
        }

        public void LoadKeymapName()
        {
            KeymapNames = new List<string>();
            if (ES3.KeyExists("KeymapNames"))
                KeymapNames = ES3.Load<List<string>>("KeymapNames");

            else
                UIManager.Singleton.CreatePopupWithTextInput("No Keymaps Found",
                    "No keymaps were found. Please enter a name for your first keymap.",
                    EventManager.Singleton.OnKeymapCreateConfirmed, EventManager.Singleton.OnPopupCancelled);
        }

        public void SaveKeymapName(string keymapName)
        {
            if (keymapName == "Add new keymap") return;
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