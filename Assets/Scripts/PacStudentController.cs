using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GhostState
{
    Death,
    Walking,
    Scared,
    Recovery
};

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

    int score;
    int lives;
    int remainingPellets;
    float powerTime;

    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject wallParticle;
    [SerializeField] GameObject deathParticle;
    GameObject collisionParticle;

    Tweener tweener;
    float speed;


    List<int> moveable;
    Vector3 startPos;
    Vector3 prevPos;
    Vector3 endPos;
    Vector2Int mapPos;

    KeyCode lastInput;
    KeyCode currentInput;

    [SerializeField] AudioSource backgroundSource;
    [SerializeField] List<AudioClip> backgroundClips;
    [SerializeField] AudioSource soundSource;
    [SerializeField] List<AudioClip> soundClips;

    [SerializeField] GameObject teleLeft;
    Vector2Int teleLeftPos;
    [SerializeField] GameObject teleRight;
    Vector2Int teleRightPos;

    [SerializeField] List<GameObject> ghosts;

    UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        score = 0;
        lives = 3;
        powerTime = 0;
        remainingPellets = 218;

        moveable = new List<int>();
        moveable.Add(0);
        moveable.Add(5);
        moveable.Add(6);

        tweener = gameObject.GetComponent<Tweener>();
        speed = 0.25f;

        startPos = transform.position;
        prevPos = startPos;
        mapPos = new Vector2Int(1, 1);
        teleLeftPos = new Vector2Int(14, 0);
        teleRightPos = new Vector2Int(14, 26);
    }

    // Update is called once per frame
    void Update()
    {
        uiManager.UpdateScore(score);

        IsGameOver();

        if (uiManager.started)
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
                if (mapPos == teleLeftPos)
                {
                    TeleportPlayer(teleRight.transform.position, teleRightPos);
                }
                else if (mapPos == teleRightPos)
                {
                    TeleportPlayer(teleLeft.transform.position, teleLeftPos);
                }

                if (walkable(lastInput))
                {
                    currentInput = lastInput;
                    tweener.AddTween(transform, transform.position, endPos, speed);
                }
                else
                {
                    if (walkable(currentInput))
                    {
                        lastInput = currentInput;
                        tweener.AddTween(transform, transform.position, endPos, speed);
                    }
                }

                PlaySound();
            }

            prevPos = transform.position;

            if (powerTime > 0)
            {
                powerTime -= Time.deltaTime;

                foreach (GameObject ghost in ghosts)
                {
                    Animator ghostAnimator = ghost.GetComponent<Animator>();
                    if (powerTime <= 3.0f && ghostAnimator.GetInteger("state") == (int)GhostState.Scared)
                    {
                        ghostAnimator.SetInteger("state", (int)GhostState.Recovery);
                    } else if (powerTime <= 0.0f && ghostAnimator.GetInteger("state") == (int)GhostState.Recovery)
                    {
                        ghostAnimator.SetInteger("state", (int)GhostState.Walking);
                    }
                }
            }
            else
            {
                UpdateBackground();
            }
        }
    }

    bool walkable(KeyCode key)
    {
        endPos = transform.position;

        if (key == KeyCode.D)
        {
            if (moveable.Contains(levelMap[mapPos.x, mapPos.y + 1]))
            {
                Destroy(collisionParticle);
                mapPos.y += 1;
                endPos.x += 1f;
                return true;
            }
            else
            {
                if (collisionParticle == null)
                {
                    collisionParticle = Instantiate(wallParticle, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                    soundSource.PlayOneShot(soundClips[2]);
                    return false;
                }
            }
        }

        if (key == KeyCode.A)
        {
            if (moveable.Contains(levelMap[mapPos.x, mapPos.y - 1]))
            {
                Destroy(collisionParticle);
                mapPos.y -= 1;
                endPos.x -= 1f;
                return true;
            }
            else
            {
                if (collisionParticle == null)
                {
                    collisionParticle = Instantiate(wallParticle, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
                    soundSource.PlayOneShot(soundClips[2]);
                    return false;
                }
            }
        }

        if (key == KeyCode.W)
        {
            if (moveable.Contains(levelMap[mapPos.x - 1, mapPos.y]))
            {
                Destroy(collisionParticle);
                mapPos.x -= 1;
                endPos.y += 1f;
                return true;
            }
            else
            {
                if (collisionParticle == null)
                {
                    collisionParticle = Instantiate(wallParticle, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    soundSource.PlayOneShot(soundClips[2]);
                    return false;
                }
            }
        }

        if (key == KeyCode.S)
        {

            if (moveable.Contains(levelMap[mapPos.x + 1, mapPos.y]))
            {
                Destroy(collisionParticle);
                mapPos.x += 1;
                endPos.y -= 1f;
                return true;
            }
            else
            {
                if (collisionParticle == null)
                {
                    collisionParticle = Instantiate(wallParticle, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
                    soundSource.PlayOneShot(soundClips[2]);
                    return false;
                }
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
        }
        else
        {
            particle.Play();
        }
    }

    void PlaySound()
    {
        if (transform.position != prevPos)
        {
            if (levelMap[mapPos.x, mapPos.y] == 0)
            {
                soundSource.PlayOneShot(soundClips[0]);
            }
        }
    }

    void UpdateBackground()
    {
        if (backgroundSource.clip == backgroundClips[0])
        {
            return;
        }

        int death = 0;
        foreach (GameObject ghost in ghosts)
        {
            if (ghost.GetComponent<Animator>().GetInteger("state") == (int)GhostState.Death)
            {
                death++;
            }
        }

        if (death == 0)
        {
            backgroundSource.clip = backgroundClips[0];
            backgroundSource.Play();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (uiManager.started)
        {
            switch (collision.gameObject.tag)
            {
                case "pellet":
                    score += 10;
                    remainingPellets -= 1;
                    soundSource.PlayOneShot(soundClips[1]);
                    levelMap[mapPos.x, mapPos.y] = 0;
                    Destroy(collision.gameObject);
                    break;
                case "cherry":
                    score += 100;
                    soundSource.PlayOneShot(soundClips[1]);
                    Destroy(collision.gameObject);
                    break;
                case "power":
                    backgroundSource.clip = backgroundClips[1];
                    backgroundSource.Play();
                    soundSource.PlayOneShot(soundClips[1]);
                    powerTime = 10f;
                    uiManager.UpdateScaredTime();
                    foreach (GameObject ghost in ghosts)
                    {
                        ghost.GetComponent<Animator>().SetInteger("state", (int)GhostState.Scared);
                    }

                    Destroy(collision.gameObject);
                    break;
                case "Ghost":
                    int ghostState = collision.gameObject.GetComponent<Animator>().GetInteger("state");
                    if (ghostState == (int)GhostState.Walking)
                    {
                        lives--;
                        // Death particles
                        GameObject currentDeathParticles = Instantiate(deathParticle, transform.position, Quaternion.identity);
                        Destroy(currentDeathParticles, 2);

                        //Reset pos
                        tweener.RemoveTween();
                        transform.position = startPos;
                        mapPos = new Vector2Int(1, 1);
                        lastInput = KeyCode.None;
                        currentInput = lastInput;

                        //Update UI
                        uiManager.UpdateLife(lives);
                    }

                    if (ghostState == (int)GhostState.Scared || ghostState == (int)GhostState.Recovery)
                    {
                        backgroundSource.clip = backgroundClips[2];
                        backgroundSource.Play();
                        score += 300;
                        Animator ghostAnimator = collision.gameObject.GetComponent<Animator>();
                        StartCoroutine(KillGhost(ghostAnimator));
                    }

                    break;
            }
        }
    }

    IEnumerator KillGhost(Animator ghostAnimator)
    {
        ghostAnimator.SetInteger("state", (int)GhostState.Death);
        yield return new WaitForSeconds(5.0f);
        ghostAnimator.SetInteger("state", (int)GhostState.Walking);
    }

    void TeleportPlayer(Vector3 pos, Vector2Int newMapPos)
    {
        if (!(tweener.TweenExists(transform)))
        {
            gameObject.transform.position = pos;
            mapPos = newMapPos;
        }
    }

    bool IsGameOver()
    {
        if (remainingPellets == 0 || lives == 0)
        {
            uiManager.DisplayGameOver();
            return true;
        }

        return false;
    }
}
