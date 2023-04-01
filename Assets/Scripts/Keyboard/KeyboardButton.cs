using System;
using Sirenix.OdinInspector;

namespace Sifter.Keyboard
{
    [Serializable]
    public record KeyboardButton
    {
        [ShowInInspector] public KeyboardPosition Position = new();

        [ShowInInspector] public string Key { get; set; }

        [ShowInInspector] public string ShiftKey { get; set; }


        public override string ToString()
        {
            return "Key: " + Key + ", ShiftKey: " + ShiftKey + ", Position: " + Position;
        }
    }
}