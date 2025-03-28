using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RecipeBookMenu : MonoBehaviour
{
    public GameObject menuUI; // The UI panel containing the recipe book
    public Text recipeTitle;
    public Text recipeDescription;
    public Text recipeDifficulty;
    public Image recipeImage;

    //private bool isMenuOpen = false;
    private int currentRecipeIndex = 0;

    private List<Recipe> recipes = new List<Recipe>
    {
        new Recipe("Gold Bar", "1. Smelt .\n2. Cut.", "Difficulty: Easy", "PancakeBeat", "iron_bar"),
        new Recipe("Sword", "1. Saw \n2. Smelt \n3. Hammer", "Difficulty: Medium", "SwordBeat", "sword"),
        new Recipe("Bow", "1. Saw \n2. Hammer \n3. Smelt \n4. Cut", "Difficulty: Hard", "BowBeat", "bow")
    };

    void Start()
    {
        //  menuUI.SetActive(false);
        UpdateRecipeDisplay();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextRecipe();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousRecipe();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            SelectRecipe();
        }
    }

    //void ToggleMenu()
    // {
    //   isMenuOpen = !isMenuOpen;
    // menuUI.SetActive(isMenuOpen);
    // }

    void NextRecipe()
    {
        currentRecipeIndex = (currentRecipeIndex + 1) % recipes.Count;
        UpdateRecipeDisplay();
    }

    void PreviousRecipe()
    {
        currentRecipeIndex = (currentRecipeIndex - 1 + recipes.Count) % recipes.Count;
        UpdateRecipeDisplay();
    }

    void UpdateRecipeDisplay()
    {
        Recipe currentRecipe = recipes[currentRecipeIndex];

        recipeTitle.text = currentRecipe.Title;
        recipeDescription.text = currentRecipe.Description;
        recipeDifficulty.text = currentRecipe.Difficulty;

        // Load the image from Resources
        Sprite newSprite = Resources.Load<Sprite>(currentRecipe.ImageName);
        if (newSprite != null)
        {
            recipeImage.sprite = newSprite;
        }
        else
        {
            Debug.LogWarning("Image not found: " + currentRecipe.ImageName);
        }
    }

    void SelectRecipe()
    {
        SceneManager.LoadScene(recipes[currentRecipeIndex].SceneName);
        Debug.Log("Loading Scene for Recipe: " + recipes[currentRecipeIndex].Title);
    }
}

[System.Serializable]
public class Recipe
{
    public string Title;
    public string Description;
    public string Difficulty;
    public string SceneName;
    public string ImageName;

    public Recipe(string title, string description, string difficulty, string sceneName, string imageName)
    {
        Title = title;
        Description = description;
        Difficulty = difficulty;
        SceneName = sceneName;
        ImageName = imageName;
    }
}