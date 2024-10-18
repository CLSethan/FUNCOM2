using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentDetection : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float transparencyAmount = .8f;
    [SerializeField] private float fadeTime = .4f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            StartCoroutine(CO_FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            StartCoroutine(CO_FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, 1f));
        }
    }

    private IEnumerator CO_FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime/fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }
}
