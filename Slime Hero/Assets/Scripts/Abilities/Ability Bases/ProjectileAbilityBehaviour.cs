using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAbilityBehaviour : MonoBehaviour
{
    public AbilityScriptableObject abilityData;
    protected PlayerController pc;

    protected Vector3 direction;
    public float destroyAfterSeconds;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.z = Mathf.Atan2(dirx, diry) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(rotation);
        if(dirx < 0)
        {
            scale.x *= -1;
        }
    }
}
