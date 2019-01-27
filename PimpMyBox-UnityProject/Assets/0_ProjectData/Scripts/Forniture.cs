using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FornitureType { TAPPETO, CADREGA, TAVOLO, VASO, LAMPADARIO, QUADRO, TROFEI}
public enum FornitureSet { FURRY, MOSCONI, MEME}

[CreateAssetMenu(fileName = "FornitureObject", menuName = "Create/FornitureObject", order = 1)]
public class Forniture : ScriptableObject
{
    public FornitureType type;
    public FornitureSet set;
    public Sprite onMapView = null;
    public Sprite HUDView = null;
    public int value = 0;

}
