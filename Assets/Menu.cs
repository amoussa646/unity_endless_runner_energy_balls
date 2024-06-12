using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public AudioSource soundEffectSource;
    public AudioSource gameMusic;

    public AudioClip titleScreenMusic;
    public GameObject howToPlayText;
    public GameObject howToPlayText2;
    public GameObject creditsText;
    private bool isShowcredits = false;
    public Toggle muteSoundToggle;

    public GameObject optionsPanel;

    private bool isSoundMuted = false;
    private bool isOptionPanelVisible = false;
    private bool isHowToPlayVisible = false;
    private void Start()
    {
        howToPlayText.gameObject.SetActive(false);
        howToPlayText2.gameObject.SetActive(false);

        optionsPanel.SetActive(false);
        creditsText.SetActive(false);
        soundEffectSource.clip = titleScreenMusic;
        soundEffectSource.Play();
    }

    public void HowToPlayButtonClicked()
    {
        isHowToPlayVisible = !isHowToPlayVisible;

        if (isHowToPlayVisible)
        {
            ShowHowToPlay();
        }
        else
        {
            HideHowToPlay();
            creditsText.SetActive(false);
        }
    }

    public void creditsButtonClicked()
    {
        isShowcredits = !isShowcredits;

        if (isShowcredits)
        {
            ShowCredits();
        }
        else
        {
            hideCredits();
            creditsText.SetActive(false);
        }
    }

    private void ShowHowToPlay()
    {
        howToPlayText.SetActive(true);
        howToPlayText2.SetActive(true);

    }

    private void HideHowToPlay()
    {
        howToPlayText.SetActive(false);
        howToPlayText2.SetActive(false);

    }
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

  

   
    public void ShowOptions()
    {
        isOptionPanelVisible = !isOptionPanelVisible;

        optionsPanel.SetActive(isOptionPanelVisible);
    }

    public void HideOptions()
    {
        creditsText.SetActive(false);

        optionsPanel.SetActive(false);
    }

    public void ShowCredits()
    {
        HideHowToPlay();
        creditsText.SetActive(true);


    }
    public void hideCredits()
    {
        HideHowToPlay();
        creditsText.SetActive(false);


    }

    public void ToggleMuteSound()
    {
        isSoundMuted = !isSoundMuted;

        if (gameMusic != null)
        {
            gameMusic.volume = isSoundMuted ? 0 : 1;
        }

        if (soundEffectSource != null)
        {
            soundEffectSource.volume = isSoundMuted ? 0 : 1;
        }

        PlayerPrefs.SetInt("IsSoundMuted", isSoundMuted ? 1 : 0);
        PlayerPrefs.Save();    
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

