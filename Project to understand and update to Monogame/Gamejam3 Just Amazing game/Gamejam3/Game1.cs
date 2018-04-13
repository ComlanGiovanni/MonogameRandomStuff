using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Gamejam3
{
    // Classe principale du jeu
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int largeurEcran;
        int hauteurEcran;

        SpriteFont police;
        int score;

        List<Perso> lstPersos;
        List<Perso> lstPersosRecherches;
        Random rnd;
        MouseState ancienEtatSouris;

        Texture2D texture1Point;
        int vXTexture1Point;
        int largeurRectTemps;

        int nouveauNiveau;
        int ancienNiveau;

        private GameState _state;

        enum GameState
        {
            MainMenu,
            Gameplay,
            EndOfGame,
        }

        public void TransfertPerso(int pNbPersos, int pNbPerosCherches)
        {
            for (int i = 1; i <= pNbPerosCherches; i++)
            {
                int rndPersosRecherches = rnd.Next(0, pNbPersos);
                lstPersosRecherches.Add(lstPersos[rndPersosRecherches]);
            }
        }

        public void CreePerso(int pX, int pY, int pVitesse)
        {
            
                Perso myPerso = new Perso();
                int numero;

                //Corps
                myPerso.imgCorps = this.Content.Load<Texture2D>("Corps");
                myPerso.couleurCorps = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                //Yeux
                numero = rnd.Next(1, 3);
                myPerso.imgYeux = this.Content.Load<Texture2D>("Yeux_" + numero);
                //Bouche
                numero = rnd.Next(1, 3);
                myPerso.imgBouche = this.Content.Load<Texture2D>("Bouche_" + numero);
                //Cheveux
                numero = rnd.Next(1, 3);
                myPerso.imgCheveux = this.Content.Load<Texture2D>("Cheveux_" + numero);
                myPerso.couleurCheveux = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                //Ailes
                numero = rnd.Next(1, 3);
                myPerso.imgAiles = this.Content.Load<Texture2D>("Ailes_" + numero);
                myPerso.couleurAiles = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                //Bras
                numero = rnd.Next(1, 3);
                myPerso.imgBras = this.Content.Load<Texture2D>("Bras_" + numero);
                myPerso.couleurBras = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                //Corne
                numero = rnd.Next(1, 3);
                myPerso.imgCorne = this.Content.Load<Texture2D>("Corne_" + numero);
                myPerso.couleurCorne = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                //Jambe
                numero = rnd.Next(1, 3);
                myPerso.imgJambe = this.Content.Load<Texture2D>("Jambe_" + numero);
                myPerso.couleurJambe = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                //Nez
                numero = rnd.Next(1, 3);
                myPerso.imgNez = this.Content.Load<Texture2D>("Nez_" + numero);
                myPerso.couleurNez = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                //Oreille
                numero = rnd.Next(1, 3);
                myPerso.imgOreille = this.Content.Load<Texture2D>("Oreille_" + numero);
                myPerso.couleurOreille = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                //Habit
                myPerso.imgHabit = this.Content.Load<Texture2D>("Habit_1");
                myPerso.couleurHabit = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                //Queue
                numero = rnd.Next(1, 3);
                myPerso.imgQueue = this.Content.Load<Texture2D>("Queue_" + numero);
                myPerso.couleurQueue = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));


                //Position
                myPerso.position = new Vector2(pX, pY);
                myPerso.vitesse = pVitesse;

                lstPersos.Add(myPerso);
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            lstPersos = new List<Perso>();
            lstPersosRecherches = new List<Perso>();
            rnd = new Random();

            IsMouseVisible = true;
        }

        // Fonction appelée une fois pour initialiser le jeu
        protected override void Initialize()
        {
            // TODO: Ajoutez ici votre code d'initialisation
            largeurEcran = GraphicsDevice.Viewport.Width;
            hauteurEcran = GraphicsDevice.Viewport.Height;
            _state = GameState.MainMenu;
            nouveauNiveau = 1;
            largeurRectTemps = 0;
            score = 0;
            base.Initialize();
        }

        // Fonction appelée une seule fois pour charger le contenu du jeu
        protected override void LoadContent()
        {
            // Crée un nouveau SpriteBatch, qui sera utilisé pour afficher des images (textures)
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: Ajoutez ici votre code qui chargera le contenu du jeu
            police = Content.Load<SpriteFont>("Score");

            texture1Point = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            vXTexture1Point = 1;

        }

        // Fonction appelée une fois pour décharger le contenu du jeu (hors ContentManager)
        protected override void UnloadContent()
        {
            texture1Point.Dispose();
        }

        void UpdateMainMenu(GameTime gameTime)
        {
            Initialize();
            //Test si le joueur clique
            MouseState nouvelleEtatSouris = Mouse.GetState();

            if (nouvelleEtatSouris.LeftButton == ButtonState.Pressed && ancienEtatSouris.LeftButton == ButtonState.Released)
            {
                if (nouvelleEtatSouris.X >= 0 &&
                    nouvelleEtatSouris.X <= largeurEcran &&
                    nouvelleEtatSouris.Y >= 0 &&
                    nouvelleEtatSouris.Y <= hauteurEcran)
                {
                    Console.WriteLine("Le bouton est enfoncé !");
                    _state = GameState.Gameplay;
                }
            }
            ancienEtatSouris = nouvelleEtatSouris;
        }

        void UpdateGameplay(GameTime gameTime)
        {
            //Gestion des différents niveaux
            if (nouveauNiveau != ancienNiveau)
            {
                switch (nouveauNiveau)
                {
                    case 1:
                        Console.WriteLine("Chargement niveau {0} ...", nouveauNiveau);
                        CreePerso(largeurEcran / 2, 50,0);
                        CreePerso(largeurEcran / 2, 150,0);
                        CreePerso(largeurEcran / 2, 250, 0);
                        CreePerso(largeurEcran / 2,350, 0);
                        CreePerso(largeurEcran / 3, 50, 0);
                        CreePerso(largeurEcran / 3, 150, 0);
                        CreePerso(largeurEcran / 3, 250, 0);
                        CreePerso(largeurEcran / 3, 350, 0);
                        TransfertPerso(8, 1);
                        break;
                    case 2:
                        Console.WriteLine("Chargement niveau {0} ...", nouveauNiveau);
                       
                        CreePerso(100, 100, 0);
                        CreePerso(200, 100, 0);
                        CreePerso(300, 100, 0);
                        CreePerso(400, 100, 0);
                        CreePerso(500, 100, 0);
                        CreePerso(600, 100, 0);
                        CreePerso(700, 100, 0);
                        CreePerso(50, 100 + 100, 0);
                        CreePerso(150, 100 + 100, 0);
                        CreePerso(250, 100 + 100, 0);
                        CreePerso(350, 100 + 100, 0);
                        CreePerso(450, 100 + 100, 0);
                        CreePerso(550, 100 + 100, 0);
                        CreePerso(650, 100 + 100, 0);
                        CreePerso(100, 100 + 200, 0);
                        CreePerso(200, 100 + 200, 0);
                        CreePerso(300, 100 + 200, 0);
                        CreePerso(400, 100 + 200, 0);
                        CreePerso(500, 100 + 200, 0);
                        CreePerso(600, 100 + 200, 0);
                        CreePerso(700, 100 + 200, 0);

                        TransfertPerso(21, 3);
                        break;

                    case 3:
                        Console.WriteLine("Chargement niveau {0} ...", nouveauNiveau);

                        CreePerso(100, 100, 1);
                        CreePerso(200, 100, 1);
                        CreePerso(300, 100, 1);
                        CreePerso(400, 100, 1);
                        CreePerso(500, 100, 1);
                        CreePerso(600, 100, 1);
                        CreePerso(700, 100, 1);
                        CreePerso(50, 100 + 100, 1);
                        CreePerso(150, 100 + 100, 1);
                        CreePerso(250, 100 + 100, 1);
                        CreePerso(350, 100 + 100, 1);
                        CreePerso(450, 100 + 100, 1);
                        CreePerso(550, 100 + 100, 1);
                        CreePerso(650, 100 + 100, 1);
                        CreePerso(100, 100 + 200, 1);
                        CreePerso(200, 100 + 200, 1);
                        CreePerso(300, 100 + 200, 1);
                        CreePerso(400, 100 + 200, 1);
                        CreePerso(500, 100 + 200, 1);
                        CreePerso(600, 100 + 200, 1);
                        CreePerso(700, 100 + 200, 1);

                        TransfertPerso(21, 3);
                        break;

                    default:
                        CreePerso(largeurEcran / 2, hauteurEcran / 2, 3);
                        CreePerso(largeurEcran / 2, hauteurEcran / 2, 1);
                        CreePerso(largeurEcran / 2, hauteurEcran / 2, 2);
                        CreePerso(largeurEcran / 2, hauteurEcran / 2, 4);
                        CreePerso(largeurEcran / 2, hauteurEcran / 2, 5);
                        CreePerso(largeurEcran / 2, hauteurEcran / 2, 6);
                        CreePerso(largeurEcran / 2, hauteurEcran / 2, 7);
                        CreePerso(largeurEcran / 2, hauteurEcran / 2, 8);
                        CreePerso(largeurEcran / 2, hauteurEcran / 2, 9);
                        CreePerso(largeurEcran / 2, hauteurEcran / 2, 10);

                        TransfertPerso(10, 2);
                        Console.WriteLine("Fin de la partie .....");
                        break;
                }
                ancienNiveau = nouveauNiveau;
            }

            //Gestion de la position de chaque personnage de lstPerso
            foreach (Perso item in lstPersos)
            {
                item.position.X += item.vitesse;

                if (item.position.X > largeurEcran - item.imgCorps.Width)
                {
                    item.position.X = largeurEcran - item.imgCorps.Width;
                    item.vitesse = 0 - item.vitesse;
                }
                if (item.position.X <= 0)
                {
                    item.position.X = 0;
                    item.vitesse = 0 - item.vitesse;
                }
            }

            //Test si le joueur clique
            MouseState nouvelleEtatSouris = Mouse.GetState();
            bool bClic = false;
            bool bVide = true;

            if (nouvelleEtatSouris.LeftButton == ButtonState.Pressed && ancienEtatSouris.LeftButton == ButtonState.Released)
            {
                if (nouvelleEtatSouris.X >= 0 &&
                    nouvelleEtatSouris.X <= largeurEcran &&
                    nouvelleEtatSouris.Y >= 0 &&
                    nouvelleEtatSouris.Y <= hauteurEcran)
                {
                    Console.WriteLine("Le bouton est enfoncé !");
                    bClic = true;
                }
            }
            ancienEtatSouris = nouvelleEtatSouris;

            //Test si l'item est cliqué
            int nbPersosCherches = lstPersosRecherches.Count;
            int i = 0;
            foreach (Perso item in lstPersosRecherches)
            {
                if (bClic)
                {
                    if (nouvelleEtatSouris.X >= item.position.X &&
                        nouvelleEtatSouris.Y >= item.position.Y &&
                        nouvelleEtatSouris.X <= item.position.X + item.imgCorps.Width &&
                        nouvelleEtatSouris.Y <= item.position.Y + item.imgCorps.Height &&
                        i == 0)
                    {
                        score += 100;
                        nbPersosCherches = nbPersosCherches - 1;
                        Console.WriteLine("Image recherché touchée !!! Il reste " + nbPersosCherches);
                        largeurRectTemps = largeurRectTemps - 20;
                        bVide = false;
                    }
                    else
                    {
                        Console.WriteLine("Clic a coté");
                        score -= 50;
                    }

                }
                i++;
                bClic = false;
            }

            //Test si la liste des persos à rechercher est vide
            if (bVide == false)
            {
                Console.WriteLine("Un personnage retiré de la liste");

                lstPersosRecherches.RemoveAt(0);

                bVide = true;

                if (nbPersosCherches == 0)
                {
                    Console.WriteLine("La liste est vide...");
                    lstPersos.Clear();
                    nouveauNiveau += 1;
                    Console.WriteLine("Niveau = " + nouveauNiveau);
                }
            }

            //Condition PARTIE FINI / Ligne de temps 
            if (largeurRectTemps <= largeurEcran)
            {
                largeurRectTemps += vXTexture1Point;
            }
            else
            {
                Console.WriteLine("**********La partie est finie !**********");
                _state = GameState.EndOfGame;
            }

            if (largeurRectTemps <= largeurEcran - largeurEcran / 4)
            {
                texture1Point.SetData(new[] { Color.Green });
            }
            else
            {
                texture1Point.SetData(new[] { Color.Red });
            }

        }

        void UpdateEndOfGame(GameTime gameTime)
        {
        }
        // Fonction appelée 60x par seconde pour mettre à jour l'état du jeu
        // Reçoit "gametime" qui contient le temps écoulé depuis le dernier update
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Ajoutez le code de mise à jour ici

            switch (_state)
            {
                case GameState.MainMenu:
                    UpdateMainMenu(gameTime);
                    break;
                case GameState.Gameplay:
                    UpdateGameplay(gameTime);
                    break;
                case GameState.EndOfGame:
                    UpdateEndOfGame(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        void DrawMainMenu(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSeaGreen);
            spriteBatch.DrawString(police, "MENU", new Vector2(350, 200), Color.Black);
            spriteBatch.DrawString(police, "CLIQUE POUR COMMENCER UNE PARTIE.", new Vector2(200, 400), Color.Black);
        }

        void DrawGameplay(GameTime gameTime)
        {
            int plusY = 0;
            int i = 0;
            spriteBatch.DrawString(police, "SCORE   " + score, new Vector2(largeurEcran - 150, 10), Color.Black);

            foreach (Perso item in lstPersos)
            {
                spriteBatch.Draw(item.imgQueue, item.position, null, item.couleurQueue, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(item.imgJambe, item.position, null, item.couleurJambe, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(item.imgAiles, item.position, null, item.couleurAiles, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(item.imgCorps, item.position, null, item.couleurCorps, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(item.imgHabit, item.position, null, item.couleurHabit, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(item.imgYeux, item.position, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(item.imgBouche, item.position, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(item.imgCheveux, item.position, null, item.couleurCheveux, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(item.imgBras, item.position, null, item.couleurBras, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(item.imgCorne, item.position, null, item.couleurCorne, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(item.imgNez, item.position, null, item.couleurNez, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(item.imgOreille, item.position, null, item.couleurOreille, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                i++;

            }

            i = 0;
            foreach (Perso item in lstPersosRecherches)
            {
                if (i > 0)
                {
                    spriteBatch.Draw(item.imgQueue, new Vector2(0, plusY), null, Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgAiles, new Vector2(0, plusY), null, Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgJambe, new Vector2(0, plusY), null, Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgCorps, new Vector2(0, plusY), null, Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgHabit, new Vector2(0, plusY), null, Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgYeux, new Vector2(0, plusY), null, Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgBouche, new Vector2(0, plusY), null, Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgCheveux, new Vector2(0, plusY), null, Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgBras, new Vector2(0, plusY), null, Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgCorne, new Vector2(0, plusY), null, Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgNez, new Vector2(0, plusY), null, Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgOreille, new Vector2(0, plusY), null, Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(item.imgQueue, new Vector2(0, plusY), null, item.couleurQueue, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgAiles, new Vector2(0, plusY), null, item.couleurAiles, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgJambe, new Vector2(0, plusY), null, item.couleurJambe, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgCorps, new Vector2(0, plusY), null, item.couleurCorps, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgHabit, new Vector2(0, plusY), null, item.couleurHabit, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgYeux, new Vector2(0, plusY), null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgBouche, new Vector2(0, plusY), null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgCheveux, new Vector2(0, plusY), null, item.couleurCheveux, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgBras, new Vector2(0, plusY), null, item.couleurBras, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgCorne, new Vector2(0, plusY), null, item.couleurCorne, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgNez, new Vector2(0, plusY), null, item.couleurNez, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    spriteBatch.Draw(item.imgOreille, new Vector2(0, plusY), null, item.couleurOreille, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                }
                i++;
                plusY = +100;
            }

            //Ligne du temps
            spriteBatch.Draw(texture1Point, new Rectangle(0, hauteurEcran - 20, largeurRectTemps, 10), Color.White);
        }

        void DrawEndOfGame(GameTime gameTime)
        {
            spriteBatch.DrawString(police, "FIN DE LA PARTIE", new Vector2(300, 200), Color.Black);
            spriteBatch.DrawString(police, "SCORE  " + score, new Vector2(300, 300), Color.Black);
        }
        // Fonction appelée aussi souvent que possible (jusqu'à 60x par seconde) pour afficher le jeu
        // Reçoit "gametime" qui contient le temps écoulé depuis le dernier update
        protected override void Draw(GameTime gameTime)
        {


            // TODO: Ajouter le code d'affichage ici
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.White);
            switch (_state)
            {
                case GameState.MainMenu:
                    DrawMainMenu(gameTime);
                    break;
                case GameState.Gameplay:

                    DrawGameplay(gameTime);
                    break;
                case GameState.EndOfGame:

                    DrawEndOfGame(gameTime);
                    break;
                default:
                    Console.WriteLine("Erreur : le GameState n'existe pas !");
                    break;
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}