using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    int[,] levelMap = {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,5,4,0,0,4,6,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,5,2},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,5,1,2,2,2,2,1},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,0,0,0,0,4,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0,0,4,4,3,0,4,4,5,2,0,0,0,0,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0,0,0,0,4,0,0,0,5,0,0,0,0,0,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0,0,4,4,3,0,4,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,0,0,0,0,4,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,5,1,2,2,2,2,1},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,3,4,4,3,5,2},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,5,4,0,0,4,6,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1},
    };

    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem particle;
    Tweener tweener;
    List<int> moveable;
    Vector3 prevPos;
    Vector3 endPos;
    Vector2Int mapPos;
    KeyCode lastInput;
    KeyCode currentInput;
    [SerializeField] AudioSource soundSource;
    [SerializeField] List<AudioClip> soundClips;
    // Start is called before the first frame update
    void Start()
    {
        moveable = new List<int>();
        moveable.Add(0);
        moveable.Add(5);
        moveable.Add(6);
        tweener = gameObject.GetComponent<Tweener>();
        prevPos = transform.position;
        mapPos = new Vector2Int(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();

        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = KeyCode.D;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = KeyCode.A;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = KeyCode.W;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = KeyCode.S;
        }

        if (!(tweener.TweenExists(transform)))
        {
            if (walkable(lastInput))
            {
                currentInput = lastInput;
                tweener.AddTween(transform, transform.position, endPos, 1f);
            }
            else
            {
                if (walkable(currentInput))
                {
                    tweener.AddTween(transform, transform.position, endPos, 1f);
                }
            }

            PlaySound();
        }

        prevPos = transform.position;
    }

    bool walkable(KeyCode key)
    {
        endPos = transform.position;

        if (key == KeyCode.D)
        {
            if (moveable.Contains(levelMap[mapPos.x, mapPos.y + 1]))
            {

                mapPos.y += 1;
                endPos.x += 1f;
                return true;
            }
        }

        if (key == KeyCode.A)
        {
            if (moveable.Contains(levelMap[mapPos.x, mapPos.y - 1]))
            {
  
                mapPos.y -= 1;
                endPos.x -= 1f;
                return true;
            }
        }

        if (key == KeyCode.W)
        {
            if (moveable.Contains(levelMap[mapPos.x - 1, mapPos.y]))
            {

                mapPos.x -= 1;
                endPos.y += 1f;
                return true;
            }
        }

        if (key == KeyCode.S)
        {
   
            if (moveable.Contains(levelMap[mapPos.x + 1, mapPos.y]))
            {
                mapPos.x += 1;
                endPos.y -= 1f;
                return true;
            }
        }

        return false;
    }

    void UpdateAnimation()
    {

        if (transform.position.x > prevPos.x)
        {
            animator.SetTrigger("goingRight");
        }
        if (transform.position.x < prevPos.x)
        {
            animator.SetTrigger("goingLeft");
        }
        if (transform.position.y > prevPos.y)
        {
            animator.SetTrigger("goingUp");
        }
        if (transform.position.y < prevPos.y)
        {
            animator.SetTrigger("goingDown");
        }

        if (transform.position == prevPos)
        {
            animator.SetTrigger("stop");
            particle.Stop();
        }
        else
        {
            particle.Play();
        }
    }

    void PlaySound()
    {
        if (levelMap[mapPos.x, mapPos.y] == 5)
        {
            soundSource.clip = soundClips[1];
        }
        else
        {
            soundSource.clip = soundClips[0];
        }


        if (!soundSource.isPlaying)
        {
            soundSource.Play();
        }

    }
}
