using Sifter.Managers;
using UnityEngine;

namespace Sifter.UI.Modes
{
    public class ModeButton : MonoBehaviour
    {
        public void ChangeMode()
        {
            EventManager.Singleton.OnModeChange.Invoke();
        }

        public void ClearMode()
        {
            EventManager.Singleton.OnModeClear.Invoke();
        }
    }
}