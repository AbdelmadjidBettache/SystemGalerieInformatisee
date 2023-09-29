using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TP_Structure
{
    class Program
    {
        struct Conservateur
        {
            public string id, nom;
            public double commission;

            public Conservateur(string id, string nom, double commission)
            {
                this.id = id;
                this.nom = nom;
                this.commission = commission;
            }

        }

        struct Artiste
        {
            public string id, nom, conserv;

            public Artiste(string id, string nom, string conserv)
            {
                this.id = id;
                this.nom = nom;
                this.conserv = conserv;
            }
        }
        struct Oeuvre
        {
            public string id, idArtiste, anneeAquis, titre;
            public double prixVente, valeurEst;
            public char etat;

            public Oeuvre(string id, string idArtiste, string anneeAquis, string titre, double prixVente, double valeurEst, char etat)
            {
                this.id = id;
                this.idArtiste = idArtiste;
                this.anneeAquis = anneeAquis;
                this.titre = titre;
                this.prixVente = prixVente;
                this.valeurEst = valeurEst;
                this.etat = etat;
            }
        }

        static int comptIdCons = 101;
        static int comptIdArt = 201;
        static int comptIdOeuvre = 301;
        static int nbreConservateur = 0;
        static int nbreArtiste = 0;
        static int nbreOeuvre = 0;
        static Conservateur[] conservateurs = new Conservateur[10];
        static Artiste[] artistes = new Artiste[10];
        static Oeuvre[] oeuvres = new Oeuvre[10];
       // static Regex nomReg = new Regex("^([a-zéèîôêëïç]{2,}$)|([a-zéèîôêëïç]{2,}-{0,1}[a-zéèîôêëïç]{2,}$)");
        // Regex anneeReg = new Regex("^[1-9]{1}[0-9]{3}$");

        static void Main(string[] args)
        {
            // Declaration variable
            char again='o';
            int choix;
            string yesorno;
            Regex againReg = new Regex("^[a-z]{1,3}$");

            
            do
            {
                choix = 0;
                Console.Clear();
                Console.WriteLine("~-------------------------------------------------~\n"
                                + "|                    GALERIE                      |\n"
                            +     "~-------------------------------------------------~\n");
                
                //  Choix du menu
                Console.Write("=================================================\n"
                            + "  Veuillez choisir une option du menu suivant:   \n"
                            + "-------------------------------------------------\n"
                            + "1. Ajouter un conservateur\n"
                            + "2. Ajouter un artiste\n"
                            + "3. Ajouter une oeuvre\n"
                            + "4. Afficher les conservateurs\n"
                            + "5. Afficher les artistes\n"
                            + "6. Afficher les oeuvres\n");
                do
                {
                    try
                    {
                        Console.Write("Votre choix: ");
                        choix = int.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.Write("Choix invalide\n");
                        continue;
                    }
                } while (choix < 1 || choix > 6);

                // 
                switch (choix)
                {
                    case 1:
                        ajouterConservateur();
                        break;
                    case 2:
                        ajouterArtiste();
                        break;
                    case 3:
                        ajouterOeuvre();
                        break;
                    case 4:
                        if (nbreConservateur > 0)
                            afficherConservateur();
                        else
                            Console.WriteLine("Aucun conservateur à afficher.");
                        break;
                    case 5:
                        if (nbreArtiste > 0)
                            afficherArtiste();
                        else
                            Console.WriteLine("Aucun artiste à afficher.");
                        break;
                    case 6:
                        if (nbreOeuvre > 0)
                            afficherOeuvre();
                        else
                            Console.WriteLine("Aucune oeuvre à afficher.");
                        break;
                }

                do
                {
                    do
                    {
                        Console.Write("\nVoulez-vous faire une autre tâche? oui ou non: ");
                        yesorno = Console.ReadLine();
                    } while (!againReg.IsMatch(yesorno));

                    again = yesorno.ToLower()[0];
                } while (again != 'o' && again != 'n');
            } while (again=='o');
            
        }

        private static void ajouterOeuvre()
        {
            string titre, anneeAcquis, idArtiste, vendu;
            double prixVente=-1, valeurEst=-1;
            char etat;

            Console.WriteLine("--------Ajout oeuvre-------\n"
                            + "===========================");

            Console.Write("Entrez le titre de l'oeuvre: ");
            titre = Console.ReadLine();

           
                Console.Write("Entrez l'année d'acquisition: ");
                anneeAcquis = Console.ReadLine();
           

            afficherArtiste();
            Console.Write("\nEntrez le id de l'artiste de l'oeuvre(Entrez 0 si l'artiste n'est pas présent): ");
            idArtiste = Console.ReadLine();
            while (!checkIdArt(idArtiste) && !idArtiste.Equals("0"))
            {
                Console.Write("Id invalide, entrez un id valide: ");
                idArtiste = Console.ReadLine();
            }

            if (idArtiste.Equals("0")) // Ajoute un artiste s'il n'est existe pas encore
            {
                Console.WriteLine();
                ajouterArtiste();
                idArtiste = artistes[nbreArtiste - 1].id;

                Console.WriteLine("\n--------Ajout oeuvre suite-------"
                                + "\n=================================\n");

            }

            do
            {
                Console.Write("Entrez la valeur estimé: ");
                try
                {
                    valeurEst = double.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Valeur invalide.");
                    continue;
                }
            } while (valeurEst < 0);

            do
            {
                Console.Write("Est-ce que l'oeuvre est vendu? oui ou non: ");
                vendu = Console.ReadLine().ToLower();
            } while (!vendu.Equals("oui") && !vendu.Equals("non"));

            if (vendu.Equals("oui")) // Si l'oeuvre est vendu, on demande le prix de vente et on change l'etat pour vendu
            {
                etat = 'V';
                do
                {
                    Console.Write("Entrez le prix de vente: ");
                    try
                    {
                        prixVente = double.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Prix invalide.");
                        continue;
                    }
                } while (prixVente < 0);
            }
            else
            {
                prixVente = 0;
                etat = 'E';
            }

            oeuvres[nbreOeuvre] = new Oeuvre(generateIdOeuvre(titre), idArtiste, anneeAcquis, titre, prixVente, valeurEst, etat);

            nbreOeuvre++;

        }

        public static void afficherOeuvre()
        {
            Console.Write("===============================================================================================================\n"
                        + "|                                            Liste des Oeuvres                                                |\n"
                        + "|-------------------------------------------------------------------------------------------------------------|\n");
            Console.Write("|____ID____|_______Titre________|_Annee acquis_|__Id Artiste__|___Valeur Estimé___|____Prix vente____|__Etat__|\n");
            for (int i = 0; i < nbreOeuvre; i++)
            {
                Console.Write("|  ");
                Console.Write(String.Format("{0,-7} | ", oeuvres[i].id));
                Console.Write(String.Format("{0,-18} | ", oeuvres[i].titre));
                Console.Write(String.Format("{0,-12} | ", oeuvres[i].anneeAquis));
                Console.Write(String.Format("{0,-12} | ", oeuvres[i].idArtiste));
                Console.Write(String.Format("{0,-17:C} | ", oeuvres[i].valeurEst));
                Console.Write(String.Format("{0,-16:C} | ", oeuvres[i].prixVente));
                Console.Write(String.Format("{0,-6} | ", oeuvres[i].etat));
                Console.WriteLine();
            }
            Console.Write("===============================================================================================================\n");
        }

        private static void ajouterArtiste()
        {
            string nom, idConservateur;

            Console.WriteLine("-------Ajout artiste-------\n"
                            + "===========================");

            Console.Write("Entrez le nom de l'artiste: ");
            nom = Console.ReadLine().ToLower();
           

            afficherConservateur();

            Console.Write("\nEntrez le id du conservateur associé à l'artiste(Entrez 0 si le conservateur n'est pas présent): ");
            idConservateur = Console.ReadLine();
            while (!checkIdCons(idConservateur) && !idConservateur.Equals("0"))
            {
                Console.Write("Id invalide, entrez un id valide: ");
                idConservateur = Console.ReadLine();
            }
            

            if (idConservateur.Equals("0")) // Si id du conservateur est 0, on ajoute un conservateur
            {
                ajouterConservateur();
                artistes[nbreArtiste] = new Artiste(generateIdArt(nom), nom, conservateurs[nbreConservateur-1].id);
            }
            else
                artistes[nbreArtiste] = new Artiste(generateIdArt(nom), nom, idConservateur);

            nbreArtiste++;
        }

        public static void afficherArtiste()
        {
            Console.Write("=================================================\n"
                        + "|               Liste des Artiste               |\n"
                        + "|-----------------------------------------------|\n");
            Console.Write("|____ID____|_______Nom________|_Id Conservateur_|\n");
            for (int i = 0; i < nbreArtiste; i++)
            {
                Console.Write("|  ");
                Console.Write(String.Format("{0,-7} | ", artistes[i].id));
                Console.Write(String.Format("{0,-16} | ", artistes[i].nom.Substring(0,1).ToUpper()+artistes[i].nom.Substring(1)));
                Console.Write(String.Format("{0,-15} | ", artistes[i].conserv));
                Console.WriteLine();
            }
            Console.Write("=================================================\n");
        }

        public static void ajouterConservateur()
        {
            string nom;
            double commission=-1;

            Console.WriteLine("-------Ajout conservateur-------\n"
                            + "================================");

            Console.Write("Entrez le nom du conservateur: ");
            nom = Console.ReadLine().ToLower();
           

            do
            {
                Console.Write("Entrez le % de la commission du conservateur: ");
                try
                {
                    commission = double.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Commission invalide.");
                    continue;
                }
            } while (commission < 0 || commission > 100);

            conservateurs[nbreConservateur] = new Conservateur(generateIdCons(nom), nom, commission);
            nbreConservateur++;
        }

        public static void afficherConservateur()
        {
            Console.Write("============================================\n"
                        + "|          Liste des Conservateurs         |\n"
                        + "|------------------------------------------|\n");
            Console.Write("|____ID____|_______Nom________|_Commission_|\n");
            for (int i = 0; i < nbreConservateur; i++)
            {
                Console.Write("|  ");
                Console.Write(String.Format("{0,-7} | ", conservateurs[i].id));
                Console.Write(String.Format("{0,-16} | ", conservateurs[i].nom.Substring(0,1).ToUpper()+conservateurs[i].nom.Substring(1)));
                Console.Write(String.Format("{0,-10:P} | ", (conservateurs[i].commission/100)));
                Console.WriteLine();
            }
            Console.Write("============================================\n");
        }

        public static bool checkIdArt(string artiste) // Verifie l'existence du id de l'artiste
        {
            bool check = false;

            for (int i = 0; i < nbreArtiste; i++)
            {
                if (artistes[i].id.Equals(artiste))
                {
                    check = true;
                    break;
                }
            }

            return check;
        }

        public static bool checkIdCons(string conservateur) // Verifie l'existence du id de conservateur
        {
            bool check = false;

            for (int i = 0; i < nbreConservateur; i++)
            {
                if (conservateurs[i].id.Equals(conservateur))
                {
                    check = true;
                    break;
                }
            }

            return check;
        }

        public static string generateIdOeuvre(string titre) //  Genere le id de l'oeuvre avec les 3 premieres lettre du titre suivi de 3 chiffres qui s'autoincremente a chaque ajout
        {
            string id = titre.Substring(0, 3) + comptIdOeuvre.ToString();
            comptIdOeuvre++;
            return id;
        }

        public static string generateIdArt(string nom) //  Genere le id de l'artiste avec les 3 premieres lettre du nom suivi de 3 chiffres qui s'autoincremente a chaque ajout
        {
            string id = nom.Substring(0, 3) + comptIdArt.ToString();
            comptIdArt++;
            return id;
        }

        public static string generateIdCons(string nom) //  Genere le id du conservateur avec les 3 premieres lettre du nom suivi de 3 chiffres qui s'autoincremente a chaque ajout
        {
            string id = nom.Substring(0, 3) + comptIdCons.ToString();
            comptIdCons++;
            return id;
        }
    }
}
