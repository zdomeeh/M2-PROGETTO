using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1ProjectTest : MonoBehaviour
{
    [SerializeField] private Hero a;
    [SerializeField] private Hero b;


    void Update()
    {
        if (!a.IsAlive() || !b.IsAlive())

            return;

        int speedA = a.GetBaseStats().spd + a.GetWeapon().GetBonusStats().spd;
        int speedB = b.GetBaseStats().spd + b.GetWeapon().GetBonusStats().spd;

        if (speedA == speedB)
        {
            if (Random.value < 0.5f)
            {
                speedA++;
                Debug.Log("TIE-BREAK! COIN FLIP VINTO DA: " + a.GetName());
            }
            else
            {
                speedB++;
                Debug.Log("TIE BREAK  COIN FLIP VINTO DA: " + b.GetName());
            }
        }

        if (speedA > speedB)
        {
            Attack(a, b);

            if (b.IsAlive())
                
                Attack(b, a);
        }

        else
        {
            Attack(b, a);

            if (a.IsAlive())
                
                Attack(a, b);
        }
        
    }

    private void Attack(Hero attacker, Hero defender)
    {
        Debug.Log(attacker.GetName() + " ATTACCA " + defender.GetName());
         
        if (!GameFormulas.HasHit(attacker.GetBaseStats(), defender.GetBaseStats()))
        {
            return;
        }

        ELEMENT atkElement = attacker.GetWeapon().GetElement();

        if (GameFormulas.HasElementAdvantage(atkElement, defender))
            Debug.Log("WEAKNESS!");

        else if (GameFormulas.HasElementDisadvantage(atkElement, defender))
            Debug.Log("RESIST!");

        int damage = GameFormulas.CalculateDamage(attacker, defender);
        Debug.Log("IL DANNO E' DI: " + damage);

        defender.TakeDamage(damage);

        if (!defender.IsAlive())
        {
            Debug.Log(defender.GetName() + " E' STATO SCONFITTO!!");
            Debug.Log(attacker.GetName() + " E' IL VINCITORE DEL MATCH!!");
        }

    }
}
