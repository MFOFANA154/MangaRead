using Microsoft.EntityFrameworkCore;
using ProjetMobile.Models;
using ProjetMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjetMobile.ViewModels
{
    public class MangaDetailViewModel : BaseViewModel
    {

        private string _Titre;
        public string Titre
        { get { return _Titre; } set { SetProperty(ref _Titre, value); } }

        private string _Auteur;
        public string Auteur
        { get { return _Auteur; } set { SetProperty(ref _Auteur, value); } }

        private int _Episode;
        public int Episode
        { get { return _Episode; } set { SetProperty(ref _Episode, value); } }

        private string _Genre;
        public string Genre
        { get { return _Genre; } set { SetProperty(ref _Genre, value); } }

        private string _Descriptif;
        public string Descriptif
        { get { return _Descriptif; } set { SetProperty(ref _Descriptif, value); } }

        private string _Status;
        public string Status
        { get { return _Status; } set { SetProperty(ref _Status, value); } }


        private ImageSource _Image;
        public ImageSource Image
        { get { return _Image; } set { SetProperty(ref _Image, value); } }

        public User User { get; set; }
        public Manga Manga { get; set; }
        public UserManga UserManga { get; set; }
        public ObservableCollection<Manga> Mangas { get; set; }
        public int res { get; set; }
        public string lien { get; set; }
        public MangaDetailViewModel(Manga manga = null, User user = null)
        {
            Title = manga?.Titre;
            Manga = manga;
            DataSwitch();
            User = user;
            FavoriteClickedCommand = new Command(async () => await AddFavorite());
            DeleteFavoriteClickedCommand = new Command(async () => await DeleteFavorite());
            OpenWebCommand = new Command(() => Device.OpenUri(new Uri(lien)));
            Mangas = new ObservableCollection<Manga>();

            


        }

        public ICommand FavoriteClickedCommand { get; private set; }
        public ICommand DeleteFavoriteClickedCommand { get; private set; }
        public ICommand OpenWebCommand { get; }


        public async Task AddFavorite()
        {
            var userCourant = User;
            var mangaCourant = Manga;
            UserManga usermangaBase;
            UserManga userManga = new UserManga { };
            userManga.UserId = User.Id;
            userManga.MangaIdM = Manga.IdM;
            try
            {
                using (MyDbContext context = new MyDbContext())
                {
                    usermangaBase = await context.UserMangas.Where(um =>
                um.MangaIdM.Equals(userManga.MangaIdM) && um.UserId.Equals(userManga.UserId)).FirstOrDefaultAsync();
                }

                if (usermangaBase != null)
                {
                    res = 0;
                }
                else
                {
                    res = 1;
                    await DataStoreUser.AssignFavoriteToUserAsync(userManga);
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }

        }

        public async Task DeleteFavorite()
        {
            var userCourant = User;
            var mangaCourant = Manga;
            UserManga usermangaBase;
            UserManga userManga = new UserManga { };
            userManga.UserId = User.Id;
            userManga.MangaIdM = Manga.IdM;
            try
            {
                using (MyDbContext context = new MyDbContext())
                {
                    usermangaBase = await context.UserMangas.Where(um =>
                um.MangaIdM.Equals(userManga.MangaIdM) && um.UserId.Equals(userManga.UserId)).FirstOrDefaultAsync();
                }

                    await DataStoreUser.DeleteFavoriteToUserAsync(usermangaBase);
                
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }

        }

        //----------------CHARGEMENT DES IMAGES-----------------------//
        private void DataSwitch() {
            switch (Manga.Titre)
            {
                case "Dragon Ball Super":
                    Manga.Image = ImageSource.FromFile("dragon_ball_super.jpg");
                    lien = "https://www.youtube.com/watch?v=BmBxa8NY0os";
                    break;
                case "Nanatsu no Taizai":
                    Manga.Image = ImageSource.FromFile("nanatsu_no_taizai.jpg");
                    lien = "https://www.youtube.com/watch?v=Lwv2CQu2fYg&t=2s";
                    break;
                case "One Piece":
                    Manga.Image = ImageSource.FromFile("one_piece.jpg");
                    lien = "https://www.youtube.com/watch?v=WeOiasT-5iw";
                    break;
                case "Prison School (Non Censuré)":
                    Manga.Image = ImageSource.FromFile("prison_school.jpg");
                    lien = "https://www.youtube.com/watch?v=blrQ4fgQr5o";
                    break;
                case "Blood-C":
                    Manga.Image = ImageSource.FromFile("blood_c.jpg");
                    lien = "https://www.youtube.com/watch?v=ewvAGAluWC4";
                    break;
                case "My Hero Academia":
                    Manga.Image = ImageSource.FromFile("my_hero_academia.jpg");
                    lien = "https://www.youtube.com/watch?v=KxDgJE0OEBI";
                    break;
                case "Ajin":
                    Manga.Image = ImageSource.FromFile("ajin.jpg");
                    lien = "https://www.youtube.com/watch?v=SwrhJhR3PwQ&t=15s";
                    break;
                case "Evil or Live":
                    Manga.Image = ImageSource.FromFile("evil_or_live.jpg");
                    lien = "https://www.youtube.com/watch?v=vplX4pHWVtg";
                    break;
                case "Fairy Tail":
                    Manga.Image = ImageSource.FromFile("fairy_tail.jpg");
                    lien = "https://www.youtube.com/watch?v=5Wcev-cS_Lw";
                    break;
                case "Kuroko no Basket S1":
                    Manga.Image = ImageSource.FromFile("kuroko_no_basket.jpg");
                    lien = "https://www.youtube.com/watch?v=nb7e5_4CGag";
                    break;
                case "Juuni Taisen":
                    Manga.Image = ImageSource.FromFile("juuni_taisen.jpg");
                    lien = "https://www.youtube.com/watch?v=A5f5TRg7Cvg";
                    break;
                case "Inuyashiki":
                    Manga.Image = ImageSource.FromFile("inuyashiki.jpg");
                    lien = "https://www.youtube.com/watch?v=ECXrxMIp2I4";
                    break;
                case "Kakegurui":
                    Manga.Image = ImageSource.FromFile("kakegurui.jpg");
                    lien = "https://www.youtube.com/watch?v=cTlHQiRNVl0&t=22s";
                    break;
                case "Naruto":
                    Manga.Image = ImageSource.FromFile("naruto.jpg");
                    lien = "https://www.youtube.com/watch?v=j2hiC9BmJlQ";
                    break;
                case "One Punch Man":
                    Manga.Image = ImageSource.FromFile("one_punch_man.jpg");
                    lien = "https://www.youtube.com/watch?v=2JAElThbKrI";
                    break;
                case "Hunter x Hunter (2011)":
                    Manga.Image = ImageSource.FromFile("hunter_x_hunter.jpg");
                    lien = "https://www.youtube.com/watch?v=59rCbUdBpko";
                    break;
                case "Noragami":
                    Manga.Image = ImageSource.FromFile("noragami.jpg");
                    lien = "https://www.youtube.com/watch?v=8JGfx2_kL-I";
                    break;
                case "Naruto Shippuden":
                    Manga.Image = ImageSource.FromFile("naruto_shippuden.jpg");
                    lien = "https://www.youtube.com/watch?v=IsZyuxdzkV0";
                    break;
                case "Kujira no Kora wa Sajou ni Utau":
                    Manga.Image = ImageSource.FromFile("kujira_no_kora_wa_sajou_ni_utau.jpg");
                    lien = "https://www.youtube.com/watch?v=zyV47ynfTe0";
                    break;
                case "New Game":
                    Manga.Image = ImageSource.FromFile("new_game.jpg");
                    lien = "https://www.youtube.com/watch?v=Q2f1MeQzLTQ";
                    break;
                case "Re:Zero kara Hajimeru Isekai Seikatsu":
                    Manga.Image = ImageSource.FromFile("re_zero.jpg");
                    lien = "https://www.youtube.com/watch?v=Slz_rahWp6Y";
                    break;
                case "Sakamoto desu ga":
                    Manga.Image = ImageSource.FromFile("sakamoto.jpg");
                    lien = "https://www.youtube.com/watch?v=r-hePnrS0-Q";
                    break;
                case "Shoukoku no Altair":
                    Manga.Image = ImageSource.FromFile("shoukoku_no_altair.jpg");
                    lien = "https://www.youtube.com/watch?v=tN2M1gcN_k8";
                    break;
                case "Shijou Saikyou no Deshi Kenichi":
                    Manga.Image = ImageSource.FromFile("kenichi.jpg");
                    lien = "https://www.youtube.com/watch?v=mFj_fINg1Ag";
                    break;
                case "Shingeki no Kyojin Saison 1":
                    Manga.Image = ImageSource.FromFile("shingeki_no_kyojin.jpg");
                    lien = "https://www.youtube.com/watch?v=6uhOzJta6ss";
                    break;
                case "Toaru Kagaku no Railgun S1":
                    Manga.Image = ImageSource.FromFile("railgun.jpg");
                    lien = "https://www.youtube.com/watch?v=jL_vEw6hObw";
                    break;
                case "Toaru Majutsu no Index S1":
                    Manga.Image = ImageSource.FromFile("majustu.jpg");
                    lien = "https://www.youtube.com/watch?v=pvR7mnapER0";
                    break;
                case "Ushio to Tora":
                    Manga.Image = ImageSource.FromFile("ushio_to_tora.jpg");
                    lien = "https://www.youtube.com/watch?v=DalEcxJYGtU";
                    break;
                case "ViVid Strike":
                    Manga.Image = ImageSource.FromFile("vivid_strike.jpg");
                    lien = "https://www.youtube.com/watch?v=7WWrw73PMn4";
                    break;
                case "Kono Subarashii Sekai ni Shukufuku wo":
                    Manga.Image = ImageSource.FromFile("konosuba.jpg");
                    lien = "https://www.youtube.com/watch?v=D0bUJCPEduQ";
                    break;
                case "Kiseijuu: Sei no Kakuritsu":
                    Manga.Image = ImageSource.FromFile("kiseijuu.jpg");
                    lien = "https://www.youtube.com/watch?v=A7LzsRcGuec";
                    break;
                case "Koutetsujou no Kabaneri":
                    Manga.Image = ImageSource.FromFile("kabaneri.jpg");
                    lien = "https://www.youtube.com/watch?v=57s944PdlEA";
                    break;
                case "Black Clover":
                    Manga.Image = ImageSource.FromFile("black_clover.jpg");
                    lien = "https://www.youtube.com/watch?v=sU7CnetWC3c";
                    break;
                case "Classroom of the Elite":
                    Manga.Image = ImageSource.FromFile("elite.jpg");
                    lien = "https://www.youtube.com/watch?v=xcWg0KIolbA";
                    break;
                case "Akame ga Kill! (Non Censuré)":
                    Manga.Image = ImageSource.FromFile("akame_ga_kill.jpg");
                    lien = "https://www.youtube.com/watch?v=TGlwZFzzDrk";
                    break;
                case "Rokudenashi Majutsu Koushi to Akashic Records":
                    Manga.Image = ImageSource.FromFile("rokudenashi.jpg");
                    lien = "https://www.youtube.com/watch?v=W3MFmDRuT9M";
                    break;
                case "Mob Psycho 100":
                    Manga.Image = ImageSource.FromFile("mob_psycho_100.jpg");
                    lien = "https://www.youtube.com/watch?v=RC7ktAbK7vE";
                    break;
                case "Youjo Senki":
                    Manga.Image = ImageSource.FromFile("youjo_senki.jpg");
                    lien = "https://www.youtube.com/watch?v=gVvp_rdpe-k";
                    break;
                case "Re:Creators":
                    Manga.Image = ImageSource.FromFile("re_creators.jpg");
                    lien = "https://www.youtube.com/watch?v=0diZ76yLLyo";
                    break;
                case "Shokugeki no Soma":
                    Manga.Image = ImageSource.FromFile("shokugeki_no_soma.jpg");
                    lien = "https://www.youtube.com/watch?v=Ij4EPjLqF3s";
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }
}
}
