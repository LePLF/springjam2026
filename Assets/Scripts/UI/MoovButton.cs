using UnityEngine;
using UnityEngine.SceneManagement;

public class MoovButton : MonoBehaviour
{
    [Header("Mouvement")]
    public float speed = 2f;
    public float range = 3f;

    private Vector2 startPos;
    private Vector2 targetPos;
    private Rigidbody2D rb;

    public EMenuState buttonChoice;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        startPos = transform.position;
        NewTarget();
    }

    void FixedUpdate()
    {
        rb.MovePosition(Vector2.MoveTowards(
            rb.position, targetPos, speed * Time.fixedDeltaTime
        ));

        if (Vector2.Distance(rb.position, targetPos) < 0.1f)
            NewTarget();
    }

    void NewTarget()
    {
        targetPos = startPos + new Vector2(
            Random.Range(-range, range),
            Random.Range(-range, range)
        );
    }

    public void LoadSceneOnSwitch()
    {
        switch (buttonChoice)
        {
            case EMenuState.Credits: Credits(); break;
            case EMenuState.Menu:    Menu();    break;
            case EMenuState.Play:    PlayGame(); break;
        }
    }

    public void PlayGame() { SceneManager.LoadScene("SampleScene"); }
    public void Credits()  { SceneManager.LoadScene("Credit"); }
    public void Menu()     { SceneManager.LoadScene("MainMenu"); }
}

 