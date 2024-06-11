using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] levelButtons;
    private bool[] levelUnlocked; // Mang luu trang thai da mo khoa cua tung level

    private void Start()
    {
        int numberOfLevels = SceneManager.sceneCountInBuildSettings;
        levelUnlocked = new bool[numberOfLevels - 1]; // Khoi tao mang luu trang thai da mo khoa
        UpdateLevelButtons(); // Cap nhat trang thai ban dau cua cac nut level
    }

    public void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }

    public void ResetLevel()
    {
        int numberOfLevels = SceneManager.sceneCountInBuildSettings;
        for (int i = 1; i < numberOfLevels; i++)
        {
            PlayerPrefs.DeleteKey("Level" + i);
        }
        PlayerPrefs.Save();
        UpdateLevelButtons(); // Cap nhat lai trang thai cua cac nut level sau khi reset
    }

    private void UpdateLevelButtons()
    {
        bool level1Completed = PlayerPrefs.GetInt("Level1", 0) == 1;
        levelUnlocked[0] = level1Completed;
        levelButtons[0].interactable = level1Completed || SceneManager.GetActiveScene().buildIndex == 0;
        for (int i = 1; i < levelButtons.Length; i++)
        {
            bool levelCompleted = PlayerPrefs.GetInt("Level" + (i + 1), 0) == 1;
            levelUnlocked[i] = levelCompleted;
            // Mo khoa level i+1 neu level i da hoan thanh hoac da duoc mo khoa
            levelButtons[i].interactable = levelCompleted || levelUnlocked[i - 1];
        }
    }
}