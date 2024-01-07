using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Ingredient
{
    public float amount;
    public Image dropper;
    public GameObject waterfall;
    [HideInInspector] public bool isAdded;

    public Ingredient(float amount, Image dropper, GameObject waterfall)
    {
        this.amount = amount;
        this.dropper = dropper;
        this.waterfall = waterfall;
        this.isAdded = false;
    }
}