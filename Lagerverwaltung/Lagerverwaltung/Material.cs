using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Lagerverwaltung
{
    internal class Material
    {
        //public Guid Id { get; set; }
        public string Bezeichnung { get; set; }
        public string Artikelnummer { get; set; }
        public string Lagerplatz { get; set; }
        public int Bestand { get; set; }

        public static string FilePath { get; } = "Material.json";
        public static List<Material> MaterialListe { get; private set; } = new List<Material>();

        public Material() { }

        public Material(string bezeichnung, string artikelnummer, string lagerplatz,  int bestand)
        {
            //Id = Guid.NewGuid();
            Bezeichnung = bezeichnung;
            Artikelnummer = artikelnummer;
            Lagerplatz = lagerplatz;
            Bestand = bestand;
        }

        public static void LadeDaten()
        {
            if (System.IO.File.Exists(FilePath))
            {
                var json = System.IO.File.ReadAllText(FilePath);
                MaterialListe = JsonSerializer.Deserialize<List<Material>>(json) ?? new List<Material>();
            }
            else
            {
                MaterialListe = new List<Material>();
            }
        }

        public static void SpeichereDaten()
        {
            var json = JsonSerializer.Serialize(MaterialListe, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(FilePath, json);
        }
    }
}
