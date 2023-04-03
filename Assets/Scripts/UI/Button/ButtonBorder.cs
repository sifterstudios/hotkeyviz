using UnityEngine;

namespace Sifter.UI.Button
{
    public class ButtonBorder : MonoBehaviour
    {
        void OnMouseEnter()
        {
            print("Enter");
            gameObject.SetActive(true);
        }

        void OnMouseExit()
        {
            print("Exit");
            gameObject.SetActive(false);
        }
    }
}