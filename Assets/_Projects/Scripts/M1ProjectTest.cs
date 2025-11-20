using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Componente MonoBehaviour per testare un duello tra due Hero
public class M1ProjectTest : MonoBehaviour
{
    [SerializeField] private Hero a; // primo eroe, assegnabile nell'Inspector
    [SerializeField] private Hero b; // secondo eroe, assegnabile nell'Inspector


    void Update()
    {
        if (!a.IsAlive() || !b.IsAlive())  // Se uno dei due è morto, non fare nulla

            return;

        int speedA = a.GetBaseStats().spd + a.GetWeapon().GetBonusStats().spd; // Calcolo velocità totale (base + arma)
        int speedB = b.GetBaseStats().spd + b.GetWeapon().GetBonusStats().spd; // Calcolo velocità totale (base + arma)

        if (speedA == speedB) // Coin flip in caso di pareggio di velocità
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

        if (speedA > speedB) // Chi attacca per primo
        {
            Attack(a, b);

            if (b.IsAlive()) // secondo attacca solo se vivo

                Attack(b, a);
        }

        else
        {
            Attack(b, a); // Questo else indica l'opposto del primo if ovvero se speedA < speedB

            if (a.IsAlive()) // secondo attacca solo se vivo

                Attack(a, b);
        }
        
    }

    // Funzione che gestisce un attacco completo per evitare di duplicare e allungare codice 
    private void Attack(Hero attacker, Hero defender)
    {
        Debug.Log(attacker.GetName() + " ATTACCA " + defender.GetName());
         
        if (!GameFormulas.HasHit(attacker.GetBaseStats(), defender.GetBaseStats())) // Controllo se colpisce
        {
            return;
        }

        ELEMENT atkElement = attacker.GetWeapon().GetElement();

        if (GameFormulas.HasElementAdvantage(atkElement, defender))
            Debug.Log("WEAKNESS!");

        else if (GameFormulas.HasElementDisadvantage(atkElement, defender))
            Debug.Log("RESIST!");

        int damage = GameFormulas.CalculateDamage(attacker, defender); // Calcolo danno
        Debug.Log("IL DANNO E' DI: " + damage);

        defender.TakeDamage(damage);

        if (!defender.IsAlive()) // Se difensore morto, stampa vincitore
        {
            Debug.Log(defender.GetName() + " E' STATO SCONFITTO!!");
            Debug.Log(attacker.GetName() + " E' IL VINCITORE DEL MATCH!!");
        }

    }
}
