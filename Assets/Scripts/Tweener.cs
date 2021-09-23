using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    public Animator pacmanAnimatorController;

    [SerializeField]
    private GameObject player;
    private Tween activeTween;
    private Vector3 currentPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AddPlayerTween();
        if (activeTween != null)
        {
            // Linear interpolation
            float timeFraction = (Time.time - activeTween.StartTime) / activeTween.Duration;

            // Distance between current pos to end
            float dist = Vector3.Distance(activeTween.Target.position, activeTween.EndPos);

            if (dist > 0.1f)
            {
                activeTween.Target.transform.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, timeFraction);
            }
            else
            {
                activeTween.Target.position = activeTween.EndPos;
                activeTween = null;
            }
        }

        currentPosition = player.transform.position;
    }

    public void AddPlayerTween()
    {
        if (activeTween == null)
        {
            if (Mathf.Approximately(currentPosition.x, -2.693f) && Mathf.Approximately(currentPosition.y, 4.18f)) // Move right
            {
                pacmanAnimatorController.SetTrigger("goingRight");
                activeTween = new Tween(player.transform, player.transform.position, new Vector3(-1.1f, 4.18f, 0), Time.time, 1.5f);                
            }
            if (Mathf.Approximately(currentPosition.x, -1.1f) && Mathf.Approximately(currentPosition.y, 4.18f)) // Move down
            {
                pacmanAnimatorController.SetTrigger("goingDown");
                activeTween = new Tween(player.transform, player.transform.position, new Vector3(-1.1f, 2.92f, 0), Time.time, 1.5f);
            }
            if (Mathf.Approximately(currentPosition.x, -1.1f) && Mathf.Approximately(currentPosition.y, 2.92f)) // Move left
            {
                pacmanAnimatorController.SetTrigger("goingLeft");
                activeTween = new Tween(player.transform, player.transform.position, new Vector3(-2.693f, 2.92f, 0), Time.time, 1.5f);
            }
            if (Mathf.Approximately(currentPosition.x, -2.693f) && Mathf.Approximately(currentPosition.y, 2.92f)) // Move up
            {
                pacmanAnimatorController.SetTrigger("goingUp");
                activeTween = new Tween(player.transform, player.transform.position, new Vector3(-2.693f, 4.18f, 0), Time.time, 1.5f);
            }
        }
    }
}
