using System;
using Sirenix.OdinInspector;

namespace Sifter.Keyboard
{
    [Serializable]
    public record KeyboardButton
    {
        [ShowInInspector] public KeyboardPosition Position;

        public string Key;
        public string ShiftKey;

        public override string ToString()
        {
            return "Key: " + Key + ", ShiftKey: " + ShiftKey + ", Position: " + Position;
        }
    }
}