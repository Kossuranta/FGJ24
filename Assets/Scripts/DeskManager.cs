using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeskManager : MonoBehaviour
{
    public RecipeManager m_recipeManager;
    public Mixer m_mixer;
    public Image[] m_ingredientSlots;
    public float m_fadeInDuration = 1f;
    public float m_fadeOutDuration = 1f;
    public float m_mixerDuration = 2f;

    public void Initialize()
    {
        m_recipeManager.Initialize();
    }

    public void StartDay(int _day)
    {
        m_recipeManager.GenerateRecipes(_day);
        ResetIngredients();
    }

    public void ClearIngredients()
    {
        StartCoroutine(nameof(BlendIngredients));
    }

    private void BlendIngredients()
    {
        m_mixer.StartMixer();
        foreach (Image image in m_ingredientSlots)
        {
            StartCoroutine(nameof(FadeOut), image);
        }
    }

    private void ResetIngredients()
    {
        foreach (Image image in m_ingredientSlots)
        {
            image.enabled = false;
            Color color = image.color;
            color.a = 1f;
            image.color = color;
            image.rectTransform.localScale = Vector3.one;
        }
    }

    public void SetIngredient(int _index, IngredientType _ingredient)
    {
        Sprite sprite = GameManager.Instance.m_ingredientSprites.GetSprite(_ingredient);
        Image image = m_ingredientSlots[_index];
        image.sprite = sprite;

        if (m_fadeInDuration > 0)
        {
            Color color = image.color;
            color.a = 0;
            image.color = color;
            image.enabled = true;
            StartCoroutine(nameof(FadeIn), image);
        }
        else
        {
            image.enabled = true;
        }
    }

    private IEnumerator FadeIn(Image _image)
    {
        float timer = 0;
        Color color = _image.color;
        while (timer < 1)
        {
            timer += Time.deltaTime / m_fadeInDuration;
            color.a = timer;
            _image.color = color;
            yield return 0;
        }
    }

    private IEnumerator FadeOut(Image _image)
    {
        float timer = 0;
        Color color = _image.color;
        while (timer < 1)
        {
            timer += Time.deltaTime / m_fadeOutDuration;
            color.a = 1f - timer;
            _image.color = color;
            yield return 0;
        }

        timer = 0;
        while (timer < m_mixerDuration)
        {
            timer += Time.deltaTime / m_mixerDuration;
            yield return 0;
        }
        
        m_mixer.StopMixer();
        ResetIngredients();
        GameManager.Instance.MixingCompleted();
    }
}
