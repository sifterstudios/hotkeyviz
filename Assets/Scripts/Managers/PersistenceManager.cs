using System;
using System.Collections.Generic;
using DTT.UI.ProceduralUI;
using Sifter.DataManagement;
using Sifter.UI.Button;
using Sifter.Util;
using TMPro;
using UnityEngine;

namespace Sifter.Managers
{
    public class PersistenceManager : MonoBehaviour
    {
        public static PersistenceManager Singleton;
        [SerializeField] TMP_InputField _descriptionText;
        public List<string> _keymapNames;

        public string _currentKeymapName;
        readonly Dictionary<string, List<KeyBinding>> Keymaps = new();
        List<KeyBinding> _currentKeymap = new();
        KeyBinding _newKeybinding = new();
        bool isRecording;

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
        }

        void OnEnable()
        {
            EventManager.Singleton.OnKeymapLoadStart += LoadKeymap;
            EventManager.Singleton.OnKeymapChangedInGUI += LoadKeymap;
            EventManager.Singleton.OnStateChanged += HandleOnStateChanged;
            EventManager.Singleton.OnButtonClickedInRecordMode += HandleOnButtonClickedInRecordMode;
        }

        void OnDisable()
        {
            EventManager.Singleton.OnKeymapLoadStart -= LoadKeymap;
            EventManager.Singleton.OnKeymapChangedInGUI -= LoadKeymap;
            EventManager.Singleton.OnStateChanged -= HandleOnStateChanged;
        }

        void HandleOnButtonClickedInRecordMode(Button button)
        {
            button.gameObject.GetComponent<RoundedImage>().color = Constants.Red;
            _newKeybinding._buttonsOrdered.Add(button);
            _newKeybinding.Description = _descriptionText.text;
        }

        void HandleOnStateChanged(StateEnum state)
        {
            switch (state)
            {
                case StateEnum.BROWSE:
                    InitiateBrowse();
                    break;
                case StateEnum.EDIT:
                    InitiateEdit();
                    break;
                case StateEnum.RECORD:
                    InitiateRecording();
                    break;
                case StateEnum.POPUP:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, "No such state");
            }
        }

        void InitiateBrowse()
        {
            if (_newKeybinding._buttonsOrdered.Count == 0) return;
            if (!isRecording) return;
            _currentKeymap.Add(_newKeybinding);
            foreach (var button in _newKeybinding._buttonsOrdered) button.RedrawKey();
            _newKeybinding = new KeyBinding();
            isRecording = false;
        }

        static void InitiateEdit()
        {
            // TODO: If we're adding an edit mode, this will need to be implemented
            throw new NotImplementedException();
        }

        void InitiateRecording()
        {
            if (_newKeybinding._buttonsOrdered.Count > 0) _newKeybinding = new KeyBinding();
            isRecording = true;
        }

        void LoadKeymap(string keymapName = null)
        {
            if (keymapName == "Add new keymap") return;
            if (keymapName != null && keymapName != _currentKeymapName)
            {
                _currentKeymapName = keymapName;
                _currentKeymap = Keymaps[keymapName];
            }

            EventManager.Singleton.OnKeymapLoadComplete.Invoke(_currentKeymap);
        }

        static void LoadKeymaps()
        {
            if (ES3.KeyExists("Keymaps"))
                ES3.Load<Dictionary<string, List<KeyBinding>>>("Keymaps");
        }

        public void LoadKeymapName()
        {
            _keymapNames = new List<string>();
            if (ES3.KeyExists("KeymapNames"))
                _keymapNames = ES3.Load<List<string>>("KeymapNames");

            else
                UIManager.Singleton.CreatePopupWithTextInput("No Keymaps Found",
                    "No keymaps were found. Please enter a name for your first keymap.",
                    EventManager.Singleton.OnKeymapCreateConfirmed, EventManager.Singleton.OnPopupCancelled);
        }

        public void SaveKeymapName(string keymapName)
        {
            if (keymapName == "Add new keymap") return;
            if (_keymapNames.Contains(keymapName))
            {
                UIManager.Singleton.CreatePopupWithTextInput("Keymap Already Exists",
                    "A keymap with that name already exists. Please enter a new name.",
                    EventManager.Singleton.OnKeymapCreateConfirmed, EventManager.Singleton.OnPopupCancelled);
                return;
            }

            _keymapNames.Add(keymapName);
            ES3.Save("KeymapNames", _keymapNames);
        }
    }
}