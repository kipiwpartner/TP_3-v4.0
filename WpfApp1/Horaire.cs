using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Horaire
    {
        //Constructor
        public Horaire(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11)
        {
            NoGroupe = s1;
            NoCours = s2;
            NomCours = s3;
            NumeroLocal = s4;
            idLocal = s5;
            TypeDeLocal = s6;
            DateDebut = s7;
            HeureDebut = s8;
            DateFin = s9;
            HeureFin = s10;
            NoJour = s11;
        }
        
        //S1
        private string nogroupe;
        public string NoGroupe
        {
            get
            {
                return nogroupe;
            }
            set
            {
                nogroupe = value;
            }
        }

        //S2
        private string nocours;
        public string NoCours
        {
            get
            {
                return nocours;
            }
            set
            {
                nocours = value;
            }
        }

        //S3
        private string nomcours;
        public string NomCours
        {
            get
            {
                return nomcours;
            }
            set
            {
                nomcours = value;
            }
        }

        //S4
        private string numerolocal;
        public string NumeroLocal
        {
            get
            {
                return numerolocal;
            }
            set
            {
                numerolocal = value;
            }
        }

        //S5
        private string idlocal;
        public string idLocal
        {
            get
            {
                return idlocal;
            }
            set
            {
                idlocal = value;
            }
        }

        //S6
        private string typedelocal;
        public string TypeDeLocal
        {
            get
            {
                return typedelocal;
            }
            set
            {
                typedelocal = value;
            }
        }

        //S7
        private string datedebut;
        public string DateDebut
        {
            get
            {
                return datedebut;
            }
            set
            {
                datedebut = value;
            }
        }

        //S8
        private string heuredebut;
        public string HeureDebut
        {
            get
            {
                return heuredebut;
            }
            set
            {
                heuredebut = value;
            }
        }

        //S9
        private string datefin;
        public string DateFin
        {
            get
            {
                return datefin;
            }
            set
            {
                datefin = value;
            }
        }

        //S10
        private string heurefin;
        public string HeureFin
        {
            get
            {
                return heurefin;
            }
            set
            {
                heurefin = value;
            }
        }

        //S11
        private string nojour;
        public string NoJour
        {
            get
            {
                return nojour;
            }
            set
            {
                nojour = value;
            }
        }
    }
}

