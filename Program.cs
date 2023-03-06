using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

class EredmenyElemzo
{
    private string Eredmenyek;

    private int DontetlenekSzama
    {
        get
        {
            return Megszamol('X');
        }
    }

    private int Megszamol(char kimenet)
    {
        int darab = 0;
        foreach (var i in Eredmenyek)
        {
            if (i == kimenet) darab++;
        }
        return darab;
    }

    public bool NemvoltDontetlenMerkozes
    {
        get
        {
            return DontetlenekSzama == 0;
        }
    }

    public EredmenyElemzo(string eredmenyek) // konstruktor
    {
        Eredmenyek = eredmenyek;
    }
}

//Év;Hét;Forduló;T13p1;Ny13p1;Eredmények
public class Toto
{
    public string ev { set; get; }
    public string het { set; get; }
    public string fordulo { set; get; }
    public int tp { set; get; }
    public int nyp { set; get; }
    public string eredmenyek { set; get; }

    public Toto(string sor)
    {
        var s = sor.Split(';');
        ev = s[0];
        het = s[1];
        fordulo = s[2];
        tp = int.Parse(s[3]);
        nyp = int.Parse(s[4]);
        eredmenyek = s[5];
    }
}

public class Program
{

    public static void Main(string[] args)
    {
        var lista = new List<Toto>();
        var sr = new StreamReader("toto.txt");
        var elsosor = sr.ReadLine();

        while (!sr.EndOfStream)
        {
            lista.Add(new Toto(sr.ReadLine()));
        }

        Console.WriteLine($"3. feladat: Fordulók száma: {lista.Count()}");

        var telitalalat = (
          from sor in lista
          select sor.tp
        ).Sum();
        Console.WriteLine($"4. feladat: Telitalálatoks szelvények száma: {telitalalat} db");

        var atlag = (
          from sor in lista
          select sor.nyp * sor.tp
        ).Average();

        Console.WriteLine($"5. feladat: Átlag: {atlag:.} FT");

        var talalatok = (
          from sor in lista
          where sor.tp > 0
          orderby sor.nyp
          select sor
        );
        var min = talalatok.First();
        var max = talalatok.Last();
        Console.WriteLine($"6. feladat:");
        Console.WriteLine($"        Legnagyobb:");
        Console.WriteLine($"        Év: {max.ev}");
        Console.WriteLine($"        Hét: {max.het}.");
        Console.WriteLine($"        Forduló: {max.fordulo}.");
        Console.WriteLine($"        Telitalálat: {max.tp} db");
        Console.WriteLine($"        Nyeremény: {max.nyp} Ft");
        Console.WriteLine($"        Eredmények: {max.eredmenyek}");
        Console.WriteLine();
        Console.WriteLine($"        Legkisebb:");
        Console.WriteLine($"        Év: {min.ev}");
        Console.WriteLine($"        Hét: {min.het}.");
        Console.WriteLine($"        Forduló: {min.fordulo}.");
        Console.WriteLine($"        Telitalálat: {min.tp} db");
        Console.WriteLine($"        Nyeremény: {min.nyp} Ft");
        Console.WriteLine($"        Eredmények: {min.eredmenyek}");

        var nincs = true;
        foreach (var sor in lista)
        {
            if (new EredmenyElemzo(sor.eredmenyek).NemvoltDontetlenMerkozes)
            {
                nincs = false;
                break;
            }
        }
        if (!nincs)
        {
            Console.WriteLine("8. feladat: Volt döntetlen nélküli forduló!");
        }
        else
        {
            Console.WriteLine($"8. feladat: Nem volt döntetlen nélküli forduló!");
        }

        Console.Read();
    }
}