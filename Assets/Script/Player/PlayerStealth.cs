using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealth : MonoBehaviour
{
    public bool stealth   ; 
    [SerializeField] private float stealthAlpha = 0.05f;
    [SerializeField] private Color StealthColor;

    private Color originalColor;
    private SpriteRenderer renderer;
    void Start(){
        renderer = GetComponent<SpriteRenderer>();
        originalColor = renderer.color;
    }
   public bool setStealth(float duration){
        if(!stealth){
            StartCoroutine(Stealthing(duration));
            return true;
        } else {
            return false;
        };
    }
    private IEnumerator Stealthing(float duration){
        startStealthing();
        yield return new WaitForSeconds(duration);
        endStealthing();
    }

    private void startStealthing(){
        Debug.Log("startStealthing");
        stealth = true;
        renderer.color = StealthColor;

    }

    public void endStealthing(){
        if (stealth){
            Debug.Log("startStealthing");

            renderer.color = originalColor;
            stealth = false;

        }
    }

}
