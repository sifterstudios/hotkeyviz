using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sifter.Keyboard;

namespace Sifter.Json
{
    public class LayoutLoader
    {
        public static IList<KeyboardButton> LoadLayout()
        {
            var json = File.ReadAllText("Assets/Json/ProgrammerDvorak.json");
            var jsonObject = JObject.Parse(json);
            IList<JToken> results = jsonObject["KeyboardButtons"]?.Children().ToList();
            IList<KeyboardButton> keyboardButtons = new List<KeyboardButton>();
            if (results != null)
                foreach (var button in results)
                {
                    var kb = button.ToObject<KeyboardButton>();
                    keyboardButtons.Add(kb);
                }

            JsonConvert.DeserializeObject<List<KeyboardButton>>(json);
            // Return the list of KeyboardButton objects
            return keyboardButtons;
        }
    }
}