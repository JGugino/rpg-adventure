using UnityEngine;
using TMPro;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour {

    public TextMeshProUGUI healthText;

    private Camera mainCamera;

    private int playerHealth = 100, playerMaxHealth = 100;

    private int baseDamage = 3;

    private float attackRange = 20, moveRange = 80;

    private PlayerMotor pMotor;

    private bool controllingCreature = false;

    private Vector3 startPoint;

    private void Awake()
    {
        pMotor = GetComponent<PlayerMotor>();

        startPoint = transform.position;

        playerHealth = playerMaxHealth;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update () {

        if (!GameController.instance.isPaused)
        {
            //Player Death
            if (playerHealth <= 0)
            {
                killPlayer();
            }
        }

        //healthText.text = "Health: " + playerHealth;
	}

    #region Player Movement
    public void moveCharacter()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitFromRay;

        if (Physics.Raycast(ray, out hitFromRay))
        {
            int distance = (int)Vector3.Distance(transform.position, hitFromRay.point);

            if (distance <= moveRange)
            {
                #region Player Attacking
                if (distance <= attackRange)
                {
                    //Attack Creatures
                    if (hitFromRay.collider.GetComponent<CreatureController>())
                    {
                        if (InventoryController.instance.getEquippedWeapon() != null)
                        {
                            hitFromRay.collider.GetComponent<CreatureController>().takeDamage(this.transform, InventoryController.instance.getEquippedWeapon().damage);
                            return;
                        }
                        else
                        {
                            hitFromRay.collider.GetComponent<CreatureController>().takeDamage(this.transform, baseDamage);
                            return;
                        }
                    }

                    //Attack Enemy
                    if (hitFromRay.collider.GetComponent<EnemyController>())
                    {
                        if (InventoryController.instance.getEquippedWeapon() != null)
                        {
                            hitFromRay.collider.GetComponent<EnemyController>().takeDamage(InventoryController.instance.getEquippedWeapon().damage);
                            return;
                        }
                        else
                        {
                            hitFromRay.collider.GetComponent<EnemyController>().takeDamage(baseDamage);
                            return;
                        }
                    }
                }
                #endregion

                pMotor.moveToPoint(hitFromRay.point);
            }
        }
    }

    public void moveCreature()
    {
        if (InventoryController.instance.getEquippedCreature() != null)
        {
            CreatureMotor equippedCreature = InventoryController.instance.getEquippedCreature().GetComponent<CreatureMotor>();

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitPoint;

            if (Physics.Raycast(ray, out hitPoint))
            {
                equippedCreature.moveToPoint(hitPoint.point);
            }
        }
    }
    #endregion

    public void takeDamage(int _damage)
    {
        playerHealth -= _damage;

        Debug.Log("Player Health: " + playerHealth);

        if (playerHealth <= 0)
        {
            killPlayer();
        }

        return;
    }

    private void killPlayer()
    {
        pMotor.getPlayerAgent().ResetPath();

        transform.position = startPoint;

        playerHealth = playerMaxHealth;
    }

    public bool getControllingCreature()
    {
        return controllingCreature;
    }

    public void setControllingCreature(bool _controlling)
    {
        controllingCreature = _controlling;
    }

    public void setPlayerHealth(int _health)
    {
        playerHealth = _health;
    }

    public int getPlayerHealth()
    {
        return playerHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<ItemController>())
        {
            Item item = collision.collider.GetComponent<ItemController>().item;

            ItemType _type = item.type;

            if (_type == ItemType.Weapon)
            {
                Weapon _weapon = (Weapon)item;

                InventoryController.instance.addItem(item, _weapon);
            }
            else if ((_type == ItemType.Head) || (_type == ItemType.Chest) || (_type == ItemType.Legs))
            {
                Armor _armor = (Armor)item;

                InventoryController.instance.addItem(item, null, _armor);
            }
            else
            {
                InventoryController.instance.addItem(item);
            }

            collision.collider.gameObject.SetActive(false);
        }
    }
}
