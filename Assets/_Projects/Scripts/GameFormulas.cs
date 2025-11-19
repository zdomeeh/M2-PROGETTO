using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameFormulas
{
    public static bool HasElementAdvantage(ELEMENT attackElement, Hero defender)
    {
        return attackElement == defender.GetWeakness();
    }

    public static bool HasElementDisadvantage(ELEMENT attackElement, Hero defender)
    {
        return attackElement == defender.GetResistance();
    }

    public static float EvaluateElementalModifier(ELEMENT attackElement, Hero defender)
    {
        if (HasElementAdvantage(attackElement, defender))
            return 1.5f;

        if (HasElementDisadvantage(attackElement, defender))
            return 0.5f;

        return 1f;
    }

    public static bool HasHit(Stats attacker, Stats defender)
    {
        int hitChange = attacker.aim - defender.eva;

        int roll = Random.Range(0, 100);

        if (roll > hitChange)
        {
            Debug.Log("MISS");
            return false;
        }

        return true;

    }

    public static bool IsCrit(int critValue)
    {
        int roll = Random.Range(0, 100);

        if (roll < critValue)
        { 
        Debug.Log("CRIT");
        return true;
        }
        
        return false;
    }

    public static int CalculateDamage(Hero attacker, Hero defender)
    {
        Stats atkStats = Stats.Sum(attacker.GetBaseStats(), attacker.GetWeapon().GetBonusStats());
        Stats defStats = Stats.Sum(defender.GetBaseStats(), defender.GetWeapon().GetBonusStats());

        int attackValue = atkStats.atk;

        int defenseValue;

        if (attacker.GetWeapon().GetDmgType() == Weapon.DAMAGE_TYPE.PHYSICAL)

            defenseValue = defStats.def;

        else

            defenseValue = defStats.res;

        int damage = attackValue - defenseValue;

        if (damage < 0)
            damage = 0;

        float modifier = EvaluateElementalModifier(attacker.GetWeapon().GetElement(), defender);
        damage = Mathf.RoundToInt(damage * modifier);

        if (IsCrit(atkStats.crt))
            damage *= 2;

        return damage;


    }
}