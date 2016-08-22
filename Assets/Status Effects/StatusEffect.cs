using UnityEngine;
using System.Collections;
using System.Reflection;

public class Effect : MonoBehaviour
{
    float duration;
    Sprite icon;

    void Start()
    {
        setEffect();
    }

    public virtual void setEffect()
    {

    }
}
