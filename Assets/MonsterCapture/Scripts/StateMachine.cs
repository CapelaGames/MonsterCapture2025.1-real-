using System;
using System.Collections;
using UnityEngine;


public class StateMachine : MonoBehaviour, ITrappable
{
   public enum State
    {
        Patrol,
        Chasing,
        Attack,
    }
    public State state;
    
    public bool isBeingCaptured { get; set; }
    private float timer = 1;
    public GameObject player;
    private Rigidbody rigidbody;
    private Vector3 originalScale;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
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
                StartCoroutine(AttackState());
                break;
            default:
                break;
        }
    }

    bool IsFacingPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        directionToPlayer.Normalize();
        float dotResult = Vector3.Dot(directionToPlayer, transform.forward);
        return dotResult >= 0.95f;
    }

    IEnumerator PatrolState()
    {
        Debug.Log("Entering Patrol State");
        while(state == State.Patrol)
        {
            transform.rotation *= Quaternion.Euler(0f, 50f * Time.deltaTime, 0f);
            if(IsFacingPlayer())
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

            Vector3 direction = player.transform.position - transform.position;
            rigidbody.AddForce(direction.normalized * (800f * Time.deltaTime));
            
            if (direction.magnitude < 10f)
            {
                state = State.Attack;
            }
            
            if (!IsFacingPlayer())
            {
                state = State.Patrol;
            }
            
            yield return null; // Waits for a frame
        }
        Debug.Log("Exiting Chasing State");
        NextState();
    }

    IEnumerator AttackState()
    {
        Debug.Log("Entering Attack State");
        
        transform.localScale = new Vector3(transform.localScale.x * 0.4f,
            transform.localScale.y * 0.4f, 
            transform.localScale.z * 3);
            
        Vector3 direction = player.transform.position - transform.position;
        rigidbody.AddForce(direction.normalized * 800f);
        
        while (state == State.Attack)
        {
            yield return new WaitForSeconds(2f);
            state = State.Patrol;
        }

        transform.localScale = originalScale;
        Debug.Log("Exiting Attack State");
        NextState();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(state != State.Attack) return;
        if (other.gameObject == player)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();

            Vector3 hitDir = player.transform.position - other.contacts[0].point;
            
            rb.AddForce(hitDir.normalized * 100f * rigidbody.linearVelocity.magnitude);
        }
    }

    
    public bool CaptureAnimation()
    {
        isBeingCaptured = true;
        timer -= Time.deltaTime * 1f;
        transform.localScale = Vector3.Lerp(Vector3.zero,originalScale , timer);

        if (timer <= 0)
        {
            return false;
        }
        
        return true;
    }

    public int PointValue()
    {
        return 2;
    }
}
