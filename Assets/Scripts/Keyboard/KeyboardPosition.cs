using System;
using Sirenix.OdinInspector;

namespace Sifter.Keyboard
{
    [Serializable]
    public record KeyboardPosition
    {
        [ShowInInspector] public int Row { get; set; }

        [ShowInInspector] public int Column { get; set; }

        public override string ToString()
        {
            return "Row: " + Row + ", Column: " + Column;
        }
    }
}