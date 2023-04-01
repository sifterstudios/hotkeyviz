using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using Sifter.Keyboard;

namespace Sifter.Json
{
    public static class LayoutLoader
    {
        public static List<KeyboardButton> LoadLayout()
        {
            var json = File.ReadAllText("Assets/Scripts/Json/Data/Layouts/ProgrammerDvorak.json");
            var jsonObject = JObject.Parse(json);
            IList<JToken> results = jsonObject["keyboardButtons"]?.Children().ToList();
            var keyboardButtons = new List<KeyboardButton>();

            if (results == null) return keyboardButtons;

            keyboardButtons.AddRange(results.Select(button => button.ToObject<KeyboardButton>()));

            return keyboardButtons;
        }
    }
}