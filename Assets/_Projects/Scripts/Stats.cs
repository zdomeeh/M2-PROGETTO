using UnityEngine;

// Classe che rappresenta le statistiche di un eroe o di un'arma

[System.Serializable] // permette di vederla nell'Inspector
public struct Stats
{
    public int atk;
    public int def;
    public int res;
    public int spd;
    public int crt;
    public int aim;
    public int eva;


    public Stats(int atk, int def, int res, int spd, int crt, int aim, int eva)  // Costruttore: inizializza tutte le statistiche
    {
        this.atk = atk;
        this.def = def;
        this.res = res;
        this.spd = spd;
        this.crt = crt;
        this.aim = aim;
        this.eva = eva;
    }

    public static Stats Sum(Stats a, Stats b) // Somma due oggetti Stats e restituisce un nuovo oggetto con la somma delle statistiche
    {
        return new Stats
        (
        a.atk + b.atk,
        a.def + b.def,
        a.res + b.res,
        a.spd + b.spd,
        a.crt + b.crt,
        a.aim + b.aim,
        a.eva + b.eva
        );
    }
}
