using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe statica che contiene tutte le formule di gioco per il combattimento
// Non serve creare oggetti di questa classe, tutte le funzioni sono statiche

public static class GameFormulas
{
    // Controlla se l'attacco ha vantaggio elementale
    // Se l'elemento dell'attacco corrisponde alla debolezza dell'eroe difensore
    // allora l'attacco ha un bonus (moltiplicatore 1.5)

    public static bool HasElementAdvantage(ELEMENT attackElement, Hero defender)
    {
        return attackElement == defender.GetWeakness();
    }

    // Controlla se l'attacco ha svantaggio elementale
    // Se l'elemento dell'attacco corrisponde alla resistenza dell'eroe difensore
    // allora l'attacco subisce un malus (moltiplicatore 0.5)

    public static bool HasElementDisadvantage(ELEMENT attackElement, Hero defender)
    {
        return attackElement == defender.GetResistance();
    }

    // Restituisce il modificatore elementale
    // Usa le due funzioni precedenti per calcolare il moltiplicatore
    // 1.5 se vantaggio, 0.5 se svantaggio, 1 altrimenti

    public static float EvaluateElementalModifier(ELEMENT attackElement, Hero defender)
    {
        if (HasElementAdvantage(attackElement, defender))
            return 1.5f;

        if (HasElementDisadvantage(attackElement, defender))
            return 0.5f;

        return 1f;
    }

    // Controlla se l'attacco va a segno
    // Calcola hitChance sottraendo la statistica evasione del difensore
    // dalla precisione dell'attaccante, quindi genera un numero casuale
    // tra 0 e 99: se il numero e' maggiore di hitChance, l'attacco fallisce

    public static bool HasHit(Stats attacker, Stats defender)
    {
        int hitChange = attacker.aim - defender.eva; // calcolo hit chance

        int roll = Random.Range(0, 100); // tiro casuale

        if (roll > hitChange)
        {
            Debug.Log("MISS"); // log in console se il colpo manca
            return false;      // ritorna falso perche' colpo fallito
        }

        return true; // colpo riuscito

    }

    // Controlla se l'attacco e' critico
    // La probabilita' di critico dipende dalla statistica critico dell'attaccante
    // Se il tiro random e' inferiore al valore critico, il colpo e' critico

    public static bool IsCrit(int critValue)
    {
        int roll = Random.Range(0, 100); // tiro casuale

        if (roll < critValue)
        { 
        Debug.Log("CRIT"); // log in console se critico
            return true; // ritorna true
        }
        
        return false; // non critico
    }

    // Calcola il danno effettivo di un attacco

    public static int CalculateDamage(Hero attacker, Hero defender)
    {
        Stats atkStats = attacker.GetTotalStats(); // Uso la funzione creata nella classe Hero per trovare tutte le stats di attacker (base + bonus arma)
        Stats defStats = defender.GetTotalStats(); // Uso la funzione creata nella classe Hero per trovare tutte le stats di defender (base + bonus arma)

        int attackValue = atkStats.atk; // valore totale di attacco

        int defenseValue;

        if (attacker.GetWeapon().GetDmgType() == Weapon.DAMAGE_TYPE.PHYSICAL) // Se il tipo di danno e' fisico, difesa da usare = def

            defenseValue = defStats.def;


        else                                               // Altrimenti se il tipo di danno e' magico, difesa da usare = res

            defenseValue = defStats.res; 

        int damage = attackValue - defenseValue; // danno iniziale

        if (damage < 0) // il danno minimo non puo' essere negativo
            damage = 0; // assegna 0 se il danno e' negativo

        float modifier = EvaluateElementalModifier(attacker.GetWeapon().GetElement(), defender); // Applica il modificatore elementale (bonus/malus)
        damage = Mathf.RoundToInt(damage * modifier);

        if (IsCrit(atkStats.crt)) // Controllo critico: se critico, moltiplica il danno per 2
            damage *= 2;

        return damage; // restituisce il danno finale


    }
}