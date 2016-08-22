using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroIcon : MonoBehaviour {

    public bool selected = false;
    public Sprite icon;

    public Image image;

    void Start() {
        image = GetComponent<Image>();
    }

	void Update () {
        if (icon != null)
            image.sprite = icon;

        if (selected) {
            GetComponent<LayoutElement>().preferredHeight = Mathf.Lerp(GetComponent<LayoutElement>().preferredHeight, 84, Time.deltaTime * 10);
            GetComponent<LayoutElement>().preferredWidth = Mathf.Lerp(GetComponent<LayoutElement>().preferredWidth, 84, Time.deltaTime * 10);
        } else {
            GetComponent<LayoutElement>().preferredHeight = Mathf.Lerp(GetComponent<LayoutElement>().preferredHeight, 64, Time.deltaTime * 10);
            GetComponent<LayoutElement>().preferredWidth = Mathf.Lerp(GetComponent<LayoutElement>().preferredWidth, 64, Time.deltaTime * 10);
        }

	}
}
