using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public Ingredient[] ingredients;

    public GameObject mixButton;
    public Image flask;

    public Slider flaskSlider;
    public GameObject mixBar;
    public Image mixWater;
        
    public TMPro.TMP_Text result;

    private float amountOfMixWater = 10;
    private const int POUR_SPEED = 7;

    private void Start()
    {
        result.SetText("");
        UpdateMixWater();
        mixButton.SetActive(false);
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredients[i].waterfall.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (amountOfMixWater >= flaskSlider.maxValue) {
            mixWater.color = Color.red;
            return; 
        }

        if (amountOfMixWater >= 80f) {
            mixBar.SetActive(false);
            mixWater.color = Color.green;
            mixButton.SetActive(true); 
        }

        if (ingredients[0].isAdded) { AddIngredient(0); }
        if (ingredients[1].isAdded) { AddIngredient(1); }
        if (ingredients[2].isAdded) { AddIngredient(2); }
        if (ingredients[3].isAdded) { AddIngredient(3); }
        UpdateMixWater();
    }

    private void AddIngredient(int index)
    {   
        ingredients[index].amount += Time.deltaTime * POUR_SPEED;
        amountOfMixWater += Time.deltaTime * POUR_SPEED;
        flask.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(
            ingredients[index].dropper.GetComponent<Image>().rectTransform.anchoredPosition.x - 2.5f, 
            -400f
        );
        if (amountOfMixWater >= 100f) { ingredients[index].waterfall.SetActive(false); }
        else { ingredients[index].waterfall.SetActive(true); }
    }

    public void PressIngredient(int index) => ingredients[index].isAdded = true;

    public void ReleaseIngredient(int index)
    {
        ingredients[index].isAdded = false;
        ingredients[index].waterfall.SetActive(false);
    }

    public void UpdateMixWater() => flaskSlider.value = amountOfMixWater;

    public void Mix()
    {
        float[] amounts = new float[ingredients.Length];
        for (int i = 0; i < ingredients.Length; i++)
        {
            amounts[i] = ingredients[i].amount;
        }

        if (Mathf.Abs(amounts[0] - amounts[1]) < 2.5f && Mathf.Abs(amounts[1] - amounts[2]) < 2.5f &&
            Mathf.Abs(amounts[2] - amounts[3]) < 2.5f && Mathf.Abs(amounts[0] - amounts[2]) < 2.5f &&
            Mathf.Abs(amounts[1] - amounts[3]) < 2.5f && Mathf.Abs(amounts[0] - amounts[3]) < 2.5f)
        {

        }
        else if (amounts[3] / amounts[0] > 2.25f && amounts[3] / amounts[1] > 2.25f &&
            amounts[3] / amounts[2] > 2.25f)
        {
            GetComponent<Player>().gay("Skim");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
            return;
        }
        else if (amounts[0] / amounts[1] > 1.5f &&
                 amounts[0] / amounts[2] > 1.5f)
        {
            GetComponent<Player>().gay("Coffee");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            return;
        }
        else if (amounts[1] / amounts[0] > 1.5f &&
                 amounts[1] / amounts[2] > 1.5f)
        {
            GetComponent<Player>().gay("Sweet");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 6);
            return;
        }
        else if (Mathf.Abs(amounts[0] - amounts[1]) < 7.5f)
        {
            GetComponent<Player>().gay("Chocolate");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }
        else if (Mathf.Abs(amounts[1] - amounts[2]) < 7.5f)
        {
            GetComponent<Player>().gay("Strawberry");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
            return;
        }
        GetComponent<Player>().gay("Plain");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void Reset()
    {
        result.SetText("");
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredients[i].amount = 0;
        }
        amountOfMixWater = 0;
        UpdateMixWater();
    }

}
