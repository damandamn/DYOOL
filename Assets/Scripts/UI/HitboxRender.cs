using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxRender : MonoBehaviour {

    public GameObject source;
    HitBox sourceHitbox;
    public Material material;
    Color active;
    Color inactive;
    float radius;

    public void InitiateVisualization()
    {
        sourceHitbox = source.GetComponent<HitBox>();
        material = GetComponent<Renderer>().material;
        material = GameLoader.SetAlpha(material, 0);

        //sets position based on user direction
        Vector3 modifiedOffset = new Vector3(sourceHitbox.offset.x / 2.3F, sourceHitbox.offset.y);

        if (!sourceHitbox.user.isFacingLeft)
        {
            transform.position = source.transform.position + new Vector3(-modifiedOffset.x, modifiedOffset.y, 1);
        }
        else
        {
            transform.position = source.transform.position + modifiedOffset + Vector3.forward;
        }
        //sets size
        radius = sourceHitbox.radius * 2;
        transform.localScale = new Vector3(radius * 2.3F, radius, radius);
    }

    private void Update()
    {
        if (source == null)
        {
            Destroy(gameObject);
        }

        if (sourceHitbox.isActive)
        {
            material = GameLoader.SetAlpha(material, 100);
        } else
        {
            material = GameLoader.SetAlpha(material, 0);
        }

        //sets position based on user direction
        Vector3 modifiedOffset = new Vector3(sourceHitbox.offset.x / 2.3F, sourceHitbox.offset.y);

        if (!sourceHitbox.user.isFacingLeft)
        {
            transform.position = source.transform.position + new Vector3(-modifiedOffset.x, modifiedOffset.y, 1);
        }
        else
        {
            transform.position = source.transform.position + modifiedOffset + Vector3.forward;
        }
        //sets size
        radius = sourceHitbox.radius * 2;
        transform.localScale = new Vector3(radius * 2.3F, radius, radius);
    }
}
