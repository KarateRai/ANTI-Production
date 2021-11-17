using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Stats Pickup")]
public class Pickup_stats : Pickup_item
{
    public enum StatsType
    {
        health,
        speed
    }
    public int amount;
    public StatsType type = StatsType.health;
    public override bool Use(Collider player)
    {
        PlayerController controller = player.GetComponent<PlayerController>();

        switch (type)
        {
            case StatsType.health:
                controller.GainHealth(amount);
                return false;
            case StatsType.speed:
                controller.stats.GainSpeed(amount);
                return true;
        }
        return false;
    }

    public override void Remove(Collider player)
    {
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.stats.ResetSpeed();
    }
}
