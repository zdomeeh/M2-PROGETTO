
using UnityEngine;

 // Script che gestisce l'intero combattimento tra due hero

public class M1ProjectTest : MonoBehaviour
{
    [SerializeField] private Hero a; // primo eroe, assegnabile nell'Inspector
    [SerializeField] private Hero b; // secondo eroe, assegnabile nell'Inspector


    void Update()
    {
        if (!a.IsAlive() || !b.IsAlive())  // Se uno dei due e' morto, non fa nulla quindi interrompe il combattimento

            return;

        Hero firstAttacker = GetFirstAttacker(a, b); // Determino chi attacca per primo usando la funzione GetFirstAttacker, dichiarata dopo il void


        Hero secondAttacker; // Determino chi attacca per secondo
        
        if (firstAttacker == a) // Ciclo if/else che controlla chi attacca per primo e per secondo
        {
            secondAttacker = b;
        }
        else
        {
            secondAttacker = a;
        }

        Attack(firstAttacker, secondAttacker); // Attraverso la funzione Attack, dichiarata in seguito, esegue il primo attacco in se tra i due eroi
                                               // attaccando per primo chi ha velocita' piu' alta

        if (secondAttacker.IsAlive())         // Il secondo tramite questo if attacca solamente se rimane vivo
            
            Attack(secondAttacker, firstAttacker);
        
    }

    private Hero GetFirstAttacker(Hero a, Hero b)
    {
        int speedA = a.GetTotalStats().spd; // Mi trovo la speed totale (base + bonus arma) del primo hero tramite la funzione GetTotalStats
        int speedB = b.GetTotalStats().spd; // Mi trovo la speed totale (base + bonus arma) del secondo hero tramite la funzione GetTotalStats

        if (speedA == speedB)
        {
            if (Random.value < 0.5f) // In caso di parita' di speed, ho aggiunto un coin flip casuale da (0 a 1), se il valore e' sotto 0.5 vince l'hero A altrimento l'hero B
            {
                Debug.Log("TIE-BREAK! COIN FLIP VINTO DA: " + a.GetName());
                return a;
            }

            else
            {
                Debug.Log("TIE-BREAK! COIN FLIP VINTO DA: " + b.GetName());
                return b; 
            }
                
        }

        if (speedA > speedB)  // Utilizza if/else per determinare il primo attaccante in base alla velocita' totale restituendo l'hero con velocita' maggiore
        {
            return a;
        }
        else
        {
            return b;
        }
    }

    // Funzione che gestisce un attacco completo per evitare di duplicare e allungare codice 
    private void Attack(Hero attacker, Hero defender)
    {
        Debug.Log(attacker.GetName() + " ATTACCA " + defender.GetName());
         
        if (!GameFormulas.HasHit(attacker.GetTotalStats(), defender.GetTotalStats())) // Controllo se colpisce
        {
            return;
        }

        ELEMENT atkElement = attacker.GetWeapon().GetElement();  // Gestisce la parte su vantaggio/svantaggio elementale e la restituisce in console

        if (GameFormulas.HasElementAdvantage(atkElement, defender))
            Debug.Log("WEAKNESS!");

        else if (GameFormulas.HasElementDisadvantage(atkElement, defender))
            Debug.Log("RESIST!");

        int damage = GameFormulas.CalculateDamage(attacker, defender); // Calcolo del danno
        Debug.Log("IL DANNO E' DI: " + damage);

        defender.TakeDamage(damage); // Viene applicato il danno

        if (!defender.IsAlive()) // Se difensore morto, stampa vincitore e lo sconfitto
        {
            Debug.Log(defender.GetName() + " E' STATO SCONFITTO!!");
            Debug.Log(attacker.GetName() + " E' IL VINCITORE DEL MATCH!!");
        }

    }
}
