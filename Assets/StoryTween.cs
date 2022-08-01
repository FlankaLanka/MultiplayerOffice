using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class StoryTween : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform doorLeft;
    [SerializeField] private Transform doorRight;
    [SerializeField] private Image fadePanel;

    [SerializeField] private Transform playerEnd;
    [SerializeField] private Transform doorRightEnd;
    [SerializeField] private Transform doorLeftEnd;
    [SerializeField] private Transform camEnd;


    private bool animStarted = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!animStarted)
            {
                animStarted = true;
                StartCoroutine(IntroAnim());
            }
        }
    }

    private IEnumerator IntroAnim()
    {
        float cycletime = 3f;
        player.DOJump(playerEnd.position, 1f, 3, cycletime);
        yield return new WaitForSeconds(cycletime);

        cycletime = 2f;
        doorRight.DOMove(doorRightEnd.position, cycletime);
        doorLeft.DOMove(doorLeftEnd.position, cycletime);
        yield return new WaitForSeconds(cycletime);

        cycletime = 3f;
        cam.DOMove(camEnd.position, cycletime);
        yield return new WaitForSeconds(cycletime);


        cycletime = 2.5f;
        fadePanel.DOColor(new Color(255, 255, 255, 1), cycletime);
        yield return new WaitForSeconds(cycletime + .5f);
        SceneManager.LoadScene("Office");

    }
}
