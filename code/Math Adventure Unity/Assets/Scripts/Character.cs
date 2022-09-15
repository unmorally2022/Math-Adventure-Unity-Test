using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected enum CharacterState {
        iddle, live, Deactive
    }
    [SerializeField]
    protected int HP;
    [SerializeField]
    protected int AttackDamage;
    [SerializeField]
    protected int AttackInterval;//seccond
    protected CharacterState characterState;

    [SerializeField]
    protected UnityEngine.UI.Slider sliderHP;
    [SerializeField]
    Animator animator;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    float TimerCounter = 0;

    private void Start()
    {
        
        characterState = CharacterState.iddle;
    }

    private void FixedUpdate()
    {
        if (characterState == CharacterState.live)
        {
            TimerCounter += Time.deltaTime;
            if (TimerCounter >= AttackInterval)
            {
                DoAttack();
                TimerCounter = 0;
            }
        }
    }

    protected virtual void DoAttack() {
        animator.SetTrigger("DoAttack");
    }

    protected bool GetHit(int _damage) {
        if (characterState == CharacterState.live) {
            animator.SetTrigger("GetHit");
            sliderHP.value -= _damage;

            if (sliderHP.value <= 0){
                characterState = CharacterState.Deactive;
                DoDeactivating();
                return true;
            }

            return false;
        }

        return false;
    }

    protected void DoDeactivating() {
        StartCoroutine(IEDoDeactivating());
    }

    IEnumerator IEDoDeactivating() {        
        float _alpha = spriteRenderer.color.a;
        while (_alpha > 0)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, _alpha);
            //spriteRenderer.color = new Color(1f, 1f, 1f, .5f);// is about 50 % transparent
            //spriteRenderer.color = new Color(1f, 1f, 1f, 0f);// is about 100 % transparent(Cant be seen at all, but still active)
            _alpha -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        gameObject.SetActive(false);
    }


    public bool DoGetHit(int _damage) {        
        return GetHit(_damage);
    }

    public void setToLive() {
        TimerCounter = 0;

        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        sliderHP.maxValue = HP;
        sliderHP.value = sliderHP.maxValue;

        characterState = CharacterState.live;
    }

    public bool GetIsDeactive() {
        if (characterState == CharacterState.Deactive)
            return true;
        else
            return false;
    }
}
