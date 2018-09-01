using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedCore : MonoBehaviour {

    public string coreName;
    public NameType nameType;

    public enum NameType {
        Star,
        BlackHole,
        Earth,
        Mars
    }

    void Start() {
        switch (nameType) {
            case NameType.Star:
            coreName = NameGenerator.GetStarName();
            break;
            case NameType.BlackHole:
            coreName = NameGenerator.GetBlackHoleName();
            break;
            case NameType.Earth:
            coreName = "Earth";
            break;
            case NameType.Mars:
            coreName = "Mars";
            break;
            default:
            break;
        }
    }
 
}


public static class NameGenerator {
    static Dictionary<int, List<string>> names = new Dictionary<int, List<string>>();

    static NameGenerator() {
        init();
    }

    public static void init() {
        var listA = new List<string> {
            "Elephantus",
            "Papilio",
            "Lupus",
            "Serpens",
            "Equus",
            "Aranea",
            "Vulpes",
        };

        names.Add(1, listA);


        var listD = new List<string> {
            "Alpha",
            "Beta",
            "Gamma",
            "Delta",
            "Epsilon",
            "Kappa",
            "Lambda",
            "Omicron",
            "Sigma",
            "Omega",
        };
        names.Add(2, listD);


        var listE = new List<string> {
            "Alcyoneus",
            "Cerastes",
            "Chimera",
            "Eidolon",
            "Hippalectryon",
            "Hippocampus",
            "Manticore",
            "Ophiotaurus",
            "Triton",
            "Typhaon",
        };

        names.Add(3, listE);

        var listI = new List<string> {
            "Friaça",
            "Orsini",
            "Damineli",
            "Barbuy",
            "Riedel",
            "Cruls",
            "Gleiser",
            "Zurita",
            "Pastoriza",
            "Holvorcem",
            "Bellegarde",
            "Atulim",
            "Storchi",
        };

        names.Add(4, listI);


        var listJ = new List<string> {
            "Ahiu",
            "Asha",
            "Biuft",
            "Braays",
            "Brook",
            "Brudsiah",
            "Chikkaux",
            "Cigroc",
            "Cray",
            "Croint",
            "Denseiy",
            "Eflauc",
            "Elafoops",
            "Ekau",
            "Fle",
            "Fumlias",
            "Grif",
            "Hoaproms",
            "Ivezli",
            "Jag",
            "Klans",
            "Kliay",
            "Kod",
            "Koistils",
            "Kreulsec",
            "Leay",
            "Leddu",
            "Loil",
            "Nacrat",
            "Nihlem",
            "Maon",
            "Meiqo",
            "Ofe",
            "Phuens",
            "Pribag",
            "Qiakki",
            "Qiuhaks",
            "Rinsib",
            "Roglias",
            "Shaf",
            "Sheaflur",
            "Slocceim",
            "Taifut",
            "Thar",
            "Toash",
            "Tred",
            "Ugeizru",
            "Umeojont",
            "Uklau",
            "Uzrubroks",
            "Veer",
            "Vlaslex",
            "Wad",
            "Wuhuays",
            "Xan",
            "Yua",
            "Zacra",
            "Zeuxek",
            "Zikkent",
            "Zlainlex",
            "Zok",
            "Zoocent",
            "Zriomlas",
        };

        names.Add(5, listJ);
    }

    public static string GetLetter() {
        int num = Random.Range(0, 26); // Zero to 25
        char let = (char)('a' + num);
        return let.ToString().ToUpper();
    }

    public static int GetNumber(int start = 0, int end = 10) {
        return Random.Range(start, end);
    }

    public static int GetYear() {
        return Random.Range(2010, 2019);
    }

    public static string GetPair() {
        return GetLetter() + GetLetter();
    }

    public static string GetStarName () {

        var probability = new int[] {
            1,1,1,1,
            2,2,2,2,2,2,2,2,2,2,
            3,3,
            4,
            5,5,5};

        var range = probability[Random.Range(0, probability.Length)];

        switch (range) {
            case 1: return NameOne();
            case 2: return NameTwo();
            case 3: return NameThree();
            case 4: return NameFour();
            case 5: return NameFive();
        default:
            break;
        }

        return string.Empty;


    }

    public static string NameOne() {

        var mid = GetRandomFromNameDB(1);
        var suffix1 = GetLetter() + "-";
        var suffix2 = GetNumber();

        return mid + " " + suffix1 + suffix2;

    }

    public static string NameTwo() {
        var preffix = GetRandomFromNameDB(2);
        var mid = GetRandomFromNameDB(3);

        return preffix + " " + mid;

    }

    public static string NameThree() {
        var preffix = GetYear();
        var mid = GetPair();
        var suffix1 = GetNumber(1);
        return preffix + " " + mid + suffix1;
    }

    public static string NameFour() {
        var mid = "of";
        var suffix1 = GetRandomFromNameDB(4);
        return mid + " " + suffix1;
    }

    public static string NameFive() {
        return GetRandomFromNameDB(5);
    }

    public static string GetRandomFromNameDB(int key) {
        var range = Random.Range(0, names[key].Count);
        return names[key][range];
    }

    public static string GetBlackHoleName() {
        var probability = new int[] {
            1,1,1,1,
            2,2,2,2,2,2,2,2,2,2,
            3,3,
            4,
            5,5,5};

        var range = probability[Random.Range(0, probability.Length)];

        switch (range) {
            case 1: return NameBhOne();
            case 2: return NameBhTwo();
            case 3: return NameBhThree();
            case 4: return NameBhFour();
            case 5: return NameBhFive();
            default:
            break;
        }

        return string.Empty;
    }

    private static string NameBhOne() {
        return GetNameFrom("NGC", GetNumber(1000, 10000).ToString());

    }

    private static string GetNameFrom(string preffix, string mid) {
        return preffix + " " + mid;
    }

    static string NameBhTwo() {
        return GetNameFrom("Messier", GetNumber(100, 1000).ToString());
    }

    public static string NameBhThree() {
        var preffix = "PKS";
        var mid = GetNumber(100, 10000).ToString("0000");
        var suffix = GetNumber(10, 1000).ToString("000");

        return preffix + " " + mid + (GetNumber(0, 2) == 1 ? "+" : "-") + suffix;
    }

    public static string NameBhFour() {
        return GetNameFrom("Mrk", GetNumber(100, 1000).ToString());
    }

    public static string NameBhFive() {
        var prefix = GetNumber(1, 10).ToString() + GetLetter();
        var mid = GetNumber(100, 1000);
        var suffix = GetNumber(100, 1000);
        return prefix + " " + mid + "." + suffix;
    }




}
