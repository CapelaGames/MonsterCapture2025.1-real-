using System;
using System.Collections;
using UnityEngine;


public class StateMachine : MonoBehaviour
{
   public enum State
    {
        Patrol,
        Chasing,
        Attack,
    }
    public State state;

    public GameObject player;

    private void Start()
    {
        NextState();
    }

    void NextState()
    {
        switch (state)
        {
            case State.Patrol:
                StartCoroutine(PatrolState());
                break;
            case State.Chasing:
                StartCoroutine(ChasingState());
                break;
            case State.Attack:
                break;
            default:
                break;
        }
    }

    IEnumerator PatrolState()
    {
        Debug.Log("Entering Patrol State");
        while(state == State.Patrol)
        {
            transform.rotation *= Quaternion.Euler(0f, 50f * Time.deltaTime, 0f);

            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.Normalize();

            float dotResult = Vector3.Dot(directionToPlayer, transform.forward);

            if(dotResult >= 0.95f)
            {
                state = State.Chasing;
            }

            yield return null; // Waits for a frame
        }
        Debug.Log("Exiting Patrol State");
        NextState();
    }

    IEnumerator ChasingState()
    {
        Debug.Log("Entering Chasing State");
        while (state == State.Chasing)
        {
            float wave = Mathf.Sin(Time.time * 20f) * 0.1f + 1f;
            float wave2 = Mathf.Cos(Time.time * 20f) * 0.1f + 1f;
            transform.localScale = new Vector3(wave, wave2, wave);

            yield return null; // Waits for a frame
        }
        Debug.Log("Exiting Chasing State");
        NextState();
    }

}
