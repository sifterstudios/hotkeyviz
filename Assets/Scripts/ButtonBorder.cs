using UnityEngine;

namespace Sifter
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