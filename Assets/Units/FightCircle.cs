using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// A fight circle coordinates attackers.
public class FightCircle : MonoBehaviour {

    public Entity focus;
    public List<Entity> attackers = new List<Entity>();
    private HashSet<Entity> innerCircle = new HashSet<Entity>();

    public const float outerRing = 5f;

    private void StripDeadAttackers()
    {
        attackers.RemoveAll(entity => entity == null);
    }
    
    public float GetAngle(Vector3 direction)
    {
        return Vector2.Angle(direction, Vector2.up);
    }
    
    public Vector3 GetFightPosition(Entity entity)
    {
        Vector3 direction = (entity.transform.position - focus.transform.position);
        direction.z = 0;
        float distance = direction.magnitude;
        direction /= distance;

        float intendedDistance = Mathf.Min(distance, outerRing);
        // If we are in the inner circle, dart inwards.
        if ( innerCircle.Contains(entity) )
        {
            intendedDistance = entity.radius + focus.radius + 0.5f;
        }

        return (direction * intendedDistance);
    }

    private void RandomizeInnerCircle(int count)
    {
        innerCircle.Clear();

        List<Entity> entities = new List<Entity>(attackers);
        if (count > entities.Count)
        {
            count = entities.Count;
        }

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, entities.Count);
            innerCircle.Add(entities[index]);
            entities.RemoveAt(index);
        }
    }

    public bool CanAttack(Entity entity)
    {
        return true;
    }

    IEnumerator Start()
    {
        while(true)
        {
            RandomizeInnerCircle(Random.Range(1, 2));
            yield return new WaitForSeconds(Random.Range(3f,5f));
        }
    }

}
