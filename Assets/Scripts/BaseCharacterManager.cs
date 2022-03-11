using UnityEngine;

public class BaseCharacterManager : MonoBehaviour
{
    [Header("Health")]
    public int health = 5;
    [SerializeField] protected int healthLimit = 5;
    public bool canMove;
    public bool isFalling;

}
