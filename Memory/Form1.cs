using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using dllLoto;



namespace Memory
{
    public partial class Memory : Form
    {
        int nbCartesDansSabot;
        int nbCartesSurTapis;
        int nb_cartes=0;
        int score = 0;
        PictureBox Image_1;
        PictureBox Image_2;
        int time = 10;
        int gagne = 0;
        bool onGame = false;


        public Memory()
        {
            InitializeComponent();

        }

        private void Distribution_Aleatoire()
        {

            clean();

            int i_carte,i_paire;
            Random g = new Random();
            // On utilise la LotoMachine pour générer une série aléatoire
            LotoMachine hasard = new LotoMachine(nbCartesDansSabot);
            // On veut une série de nbCartesSurTapis cartes parmi celles 
            // du réservoir
            int[] tImagesCartes = hasard.TirageAleatoire(nbCartesSurTapis/2, false);
            // La série d'entiers retournée par la LotoMachine correspondra
            // aux indices des cartes dans le "sabot"

            // Affectation des images aux picturebox
            PictureBox carte,paire;
            int i_image;
            for (int i = 0; i < nbCartesSurTapis/2; i++)
            {
                i_carte = g.Next(0, nbCartesSurTapis);
                carte = (PictureBox)tlp_cartes.Controls[i_carte];
                i_paire = g.Next(0, nbCartesSurTapis);
                paire = (PictureBox)tlp_cartes.Controls[i_paire];

                while (carte.Tag != null || i_carte==i_paire)
                {
                    i_carte = g.Next(0, nbCartesSurTapis);
                    carte = (PictureBox)tlp_cartes.Controls[i_carte];
                }

                while (paire.Tag != null || i_carte == i_paire)
                {
                    i_paire = g.Next(0, nbCartesSurTapis);
                    paire = (PictureBox)tlp_cartes.Controls[i_paire];
                }

                i_image = tImagesCartes[i+1]; // i_carte + 1 à cause                
                                                      // des pbs d'indices
                carte.Tag = i_image;
                paire.Tag = i_image;
                carte.Image = il_Sabot.Images[0];
                paire.Image = il_Sabot.Images[0];

                //carte = (PictureBox)tlp_cartes.Controls[rand];
                //carte.Tag = i_image;

            }
            
        }

        private void clean()
        {
            PictureBox carte;
            int i_carte = 1;

            foreach (Control ctrl in tlp_cartes.Controls)
            {
                carte = (PictureBox)ctrl;
                carte.Tag = null;
                i_carte++;
            }
        }

        private void retourner_cartes()
        {
            PictureBox carte;
            int i_carte = 1;

            foreach (Control ctrl in tlp_cartes.Controls)
            {
                carte = (PictureBox)ctrl;
                if (carte.Tag.ToString()!="trouve")
                carte.Image = il_Sabot.Images[0];
                i_carte++;
            }
        }

        private void afficher_cartes()
        {
            PictureBox carte;
            int i_carte = 1;

         
            foreach (Control ctrl in tlp_cartes.Controls)
            {   
              
                carte = (PictureBox)ctrl;
                if (carte.Tag.ToString() != "trouve")
                {
                    carte.Image = il_Sabot.Images[Convert.ToInt32(carte.Tag)];
                }
                i_carte++;
            }
        }

        private void btn_Distribuer_Click(object sender, EventArgs e)
        {
            score = 0;
            onGame = false;
            this.Valeur_Score.Text = score.ToString();
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
           

        }

        private void pb_XX_Click(object sender, EventArgs e)
        {
            PictureBox carte;
            int i_carte;
            if (nb_cartes == 0)
            {
                retourner_cartes();
            }
            //if (Image_1 == null)
            //    MessageBox.Show("L'image 1 n'est pas référencée");
            //if (Image_2 == null)
            //    MessageBox.Show("L'image 2 n'est pas référencée");

                carte = (PictureBox)sender;
                if (carte.Tag.ToString() == "trouve")
                {
                return;
                }
                i_carte = Convert.ToInt32(carte.Tag);
                //i_image = tapisCARTES[i_carte];
                carte.Image = il_Sabot.Images[i_carte];

                if (nb_cartes == 0)
                {
                    Image_1 = carte;
                    nb_cartes++;
                }
                else //(nb_cartes == 1)
                {
              
                    Image_2 = carte;
                    if (Image_1.Name == Image_2.Name)
                    {
                        return;
                    }
                    check();
                    nb_cartes = 0;

                
            }





        }
        public void check() {
           

                if (Image_1.Tag.ToString() == Image_2.Tag.ToString())
                {


                    Image_1.Tag = "trouve";
                    Image_2.Tag = "trouve";

                    score += 10;
                    this.Valeur_Score.Text = score.ToString();

                    gagne += 2;

                    if (gagne == nbCartesSurTapis)
                    {
                        MessageBox.Show("Gagné");
                        this.Close();
                    }

                    score -= 2;
                    this.Valeur_Score.Text = score.ToString();
                }
            
           
                
                Image_1 = null;
                Image_2 = null;
            }

        private void play_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Tag == null || onGame == true )
            {
                return;
            }
            afficher_cartes();
            time = 10;
            this.timer.Start();
            onGame = true;

        }


        private void timer_Tick(object sender, EventArgs e)
        {
            time -= 1;
            this.chrono.Text = time.ToString();
            if (time == 0)
            {
                retourner_cartes();
                this.timer.Stop();
                chrono.Visible = false;
                text_chrono.Visible = false;
            }
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
  