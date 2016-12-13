using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dllLoto;



namespace Memory
{
    public partial class Memory : Form
    {
        int nbCartesDansSabot;
        int nbCartesSurTapis;

        public Memory()
        {
            InitializeComponent();

        }

        private void Distribution_Aleatoire()
        {
            int rand;
            Random g = new Random();
            // On utilise la LotoMachine pour générer une série aléatoire
            LotoMachine hasard = new LotoMachine(nbCartesDansSabot);
            // On veut une série de nbCartesSurTapis cartes parmi celles 
            // du réservoir
            int[] tImagesCartes = hasard.TirageAleatoire(nbCartesSurTapis/2, false);
            // La série d'entiers retournée par la LotoMachine correspondra
            // aux indices des cartes dans le "sabot"

            // Affectation des images aux picturebox
            PictureBox carte;
            int i_image;
            for (int i_carte = 0; i_carte < nbCartesSurTapis/2; i_carte++)
            {
                carte = (PictureBox)tlp_cartes.Controls[i_carte];
                i_image = tImagesCartes[i_carte + 1]; // i_carte + 1 à cause
                                                      // des pbs d'indices
                carte.Tag = i_image;
                carte.Image = il_Sabot.Images[i_image];
                rand = g.Next(1,8);
                while ((PictureBox)tlp_cartes.Controls[rand].Tag.ToString())
                {
                    rand = g.Next(1, 8);
                }

                carte = (PictureBox)tlp_cartes.Controls[rand];
                carte.Tag = i_image;
                carte.Image = il_Sabot.Images[i_image];

            }
            
        }



       /* private void Distribution_Sequentielle()
        {
            PictureBox carte;
            int i_carte = 1;

            foreach (Control ctrl in tlp_cartes.Controls)
            {
                // Je sais que le contrôle est une PictureBox
                // donc je "caste" l'objet (le Contrôle) en PictureBox...
                carte = (PictureBox)ctrl;
                // Ensuite je peux accéder à la propriété Image
                // (je ne pourrais pas si je n'avais pas "casté" le contrôle)
                carte.Image = il_Sabot.Images[i_carte];
                i_carte++;
            }
        }*/

        private void btn_Distribuer_Click(object sender, EventArgs e)
        {
            
            // On récupère le nombre d'images dans le réservoir :
            nbCartesDansSabot = il_Sabot.Images.Count - 1;
            // On enlève 1 car :
            // -> la l'image 0 ne compte pas c’est l’image du dos de carte 
            // -> les indices vont de 0 à N-1, donc les indices vont jusqu’à 39
            //    s’il y a 40 images au total *

            // On récupère également le nombre de cartes à distribuées sur la tapis
            // autrement dit le nombre de contrôles présents sur le conteneur
            nbCartesSurTapis = tlp_cartes.Controls.Count;

            // On effectue la distribution (aléatoire) proprement dite
            Distribution_Aleatoire();

        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            // On utilise la LotoMachine pour générer une série aléatoire
            // On fixe à 49 le nombre maxi que retourne la machine
            LotoMachine hasard = new LotoMachine(49);
            // On veut une série de 6 numéros distincts parmi 49 (comme quand on joue au loto)
            int[] tirageLoto = hasard.TirageAleatoire(8, false);
            // false veut dire pas de doublon : une fois qu'une boule est sortie, 
            // elle ne peut pas sortir à nouveau ;-)
            // La série d'entiers retournée par la LotoMachine correspond au loto
            // affiché sur votre écran TV ce soir...
            string grilleLoto = "* ";
            for (int i = 1; i <= 8; i++)
            {
                grilleLoto = grilleLoto + tirageLoto[i] + " * ";
            }
            MessageBox.Show(grilleLoto, "Tirage du LOTO ce jour !");

        }

        private void pb_XX_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Memory_Load(object sender, EventArgs e)
        {

        }
    }
}
  


