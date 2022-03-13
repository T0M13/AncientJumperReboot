using UnityEngine;

public class BaseCharacterManager : MonoBehaviour
{
    [Header("Health")]
    public int health = 5;
    [SerializeField] protected int healthLimit = 5;
    [Header("Movement")]
    public bool canMove;
    public bool isFalling;

}
