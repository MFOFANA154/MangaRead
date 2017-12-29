using ProjetMobile.Models;
using ProjetMobile.Services;
using ProjetMobile.Views;
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
      public class MangasViewModel : BaseViewModel
    {
        public ObservableCollection<Manga> Mangas { get; set; }
        public Command LoadMangasCommand { get; set; }
        public User User { get; set; }
        public MangasViewModel()
        {
            
            Mangas = new ObservableCollection<Manga>();
            MangaData();
            LoadMangasCommand = new Command(async () => await ExecuteLoadMangasCommand());
            

        }
        public MangasViewModel(User user = null)
        {
            Title = "My Favorite List";
            User = user;
            Mangas = new ObservableCollection<Manga>();  
            LoadFavoriteCommand = new Command(async () => await OnFavoriteList());
        }

        public ICommand LoadFavoriteCommand { get; private set; }

        async Task ExecuteLoadMangasCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                
                Mangas.Clear();
                //BindingEmail = "mfofana154@gmail.com";
                var mangas = await DataStoreManga.GetMangasAsync(true); // on filtre la liste de tous les mangas dans la base
                foreach (var manga in mangas)
                {
                    Mangas.Add(manga);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task OnFavoriteList()
        {

            try
            {
                    Mangas.Clear();
                    var list = await DataStoreUserManga.GetMangasFavoriteAsync(User.Id);
                    
                foreach (var manga in list)
                     {
                        var _manga = await DataStoreManga.GetMangaAsync(manga.MangaIdM);
                        Mangas.Add(_manga);
                     }
              
            }
            

            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }

        }
        //------------------------------------CHARGEMENT DES MANGAS-----------------------------------------------------------//
        async private void MangaData()
        {
            using (MyDbContext context = new MyDbContext())
            {
                if (context.Mangas.Count() == 0)
                {
                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Dragon Ball Super",
                        Auteur = "Akira Toriyama",
                        Episode = 122,
                        Descriptif = "Au lendemain de son féroce combat avec Majin Buu, Son Goku tente de maintenir la paix sur Terre.",
                        Status = "Ongoing",
                        Genre = " Action, Aventure, Comedie, Fantastique, Martial Arts, Shounen, Super Power",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Nanatsu no Taizai",
                        Auteur = "Nakaba Suzuki",
                        Episode = 24,
                        Descriptif = "Dans le royaume de Liones, les Chevaliers Sacrés, des soldats aux pouvoirs terrifiants au service de la couronne, viennent de trahir et renverser le roi. Elizabeth Liones, la 3e fille du roi, réussit à s’enfuir et part chercher de l'aide. Son seul espoir est la compagnie des Seven Deadly Sins, un groupe de sept anciens Chevaliers Sacrés surpuissants, recherchés depuis 10 ans pour un crime qu’ils n'ont pas commis : l'assassinat du général en chef des Chevaliers Sacrés. Elizabeth arrive un jour dans une taverne, le Boar Hat, et rencontre le tenancier, un jeune garçon nommé Meliodas, et Hawk, son cochon loquace. Mais, surprise, le garçon se trouve être l’ancien chef des Seven Deadly Sins, le dragon de la Colère. À eux deux, ils vont partir à la recherche des six autres Deadly Sins et tenter de défaire les Chevaliers Sacrés.",
                        Genre = "Action, Aventure, Ecchi, Fantastique, Shounen, Super Power, Surnaturel",
                        Status = "Completed",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Naruto Shippuden",
                        Auteur = "Masashi Kishimoto",
                        Episode = 500,
                        Descriptif = "Après une formation de 2 ans et demi avec Jiraya, Naruto, de retour à Konoha, retrouve ses amis avec lesquels il reforme l'équipe d'antan. Il leur faut faire face à une terrible menace qui pèse sur le monde entier...",
                        Genre = "Action, Comedie, Martial Arts, Shounen, Super Power",
                        Status = "Completed",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Noragami",
                        Auteur = "Deko Akao",
                        Episode = 12,
                        Descriptif = "Yato est un dieu errant oublié par les humains. Il parcourt le Japon afin d’exécuter les vœux des mortels dans l'espoir de pouvoir convertir assez de fidèles pour avoir enfin un temple à son honneur et un culte digne de ce nom. Son destin se retrouve bouleversé le jour où il croise une collégienne, Iki Hiyori, qui risque sa vie pour le sauver d'un accident de la route. Cet événement transforme cette dernière en demi-fantôme et, déterminée à retrouver son état normal, elle décide de suivre Yato jusqu'à ce qu'il accepte de résoudre son problème.",
                        Genre = "Action, Aventure, Shounen, Surnaturel",
                        Status = "Completed",
                         
                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Hunter x Hunter (2011)",
                        Auteur = "Yoshihiro Togashi",
                        Episode = 148,
                        Descriptif = "Gon est un jeune garçon intrépide et aventureux qui vit chez sa tante Mito. Son rêve : devenir un Hunter, comme son père. Les Hunters forment une caste à part. Ce sont des gens qui ont dédié toute leur existence à la recherche d'espèces rares, de mets inconnus, de territoires inexplorés ou encore de trésors enfouis. Mais pour en faire partie, Gon va devoir passer un examen particulièrement difficile truffé d'épreuves plus dangereuses les unes que les autres. Heureusement, il pourra compter sur son ingéniosité et sur son courage, ainsi que sur trois compagnons qu'il rencontre au cours de l'examen : Leolio, Kurapika et Kirua. Ensemble, ils comptent bien triompher de tous les obstacles et obtenir l'un des trésors les plus convoités de la planète: la carte de Hunter !",
                        Genre = "Action, Aventure, Mystérieux, Shounen, Super Power, Thriller",
                        Status = "Completed",

                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "One Piece",
                        Auteur = "Eiichirō Oda",
                        Episode = 819,
                        Descriptif = "Le capitaine Monkey D. Luffy est à la tête des Straw Hat, un groupe d'héroïques pirates. Luffy, doté de superpouvoirs obtenus grâce à des fruits magiques, désire plus que tout découvrir One Piece, un fabuleux trésor...",
                        Genre = " Action, Aventure, Comedie, Drame, Fantastique, Shounen, Super Power",
                        Status = "Ongoing",

                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "One Punch Man",
                        Auteur = "Tomohiro Suzuki",
                        Episode = 12,
                        Descriptif = "Saitama est un jeune homme sans emploi. Un jour, il rencontre un homme crabe qui recherche un jeune garçon au menton fendu. Saitama finit par rencontrer ce jeune garçon et décide de le sauver de l'homme crabe, qu'il arrive à battre difficilement. Dès lors, Saitama décide de devenir un super-héros et s’entraîne pendant trois ans. À la fin de son entrainement, il remarque qu'il est devenu fort, tellement fort qu'il arrive désormais à battre tous ses adversaires avec un seul coup de poing. Sa force monstrueuse est pour lui source de problème, puisqu'il ne trouve pas d'adversaire à sa mesure et s'ennuie dans son métier de héros. Bien qu'il ait mis un terme a bon nombre de menaces toutes plus dangereuses les unes que les autres, personne ne semble remarquer l'incroyable capacité de Saitama, à l'exception de son ami et disciple Genos.",
                        Genre = "Action, Comedie, Parodie, Sci-Fi, Seinen, Super Power, Surnaturel",
                        Status = "Completed",
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Prison School (Non Censuré)",
                        Auteur = "HIRAMOTO Akira",
                        Episode = 12,
                        Descriptif = "L'histoire se déroule dans une école renommée et très stricte auparavant réservée aux filles. Mais l'établissement change sa politique et scolarise cinq jeunes hommes qui par leur comportement, se retrouveront très rapidement en quarantaine. Ils devront faire face à toutes sortes d’embûches pour sortir de cette situation critique et échapper aux griffes du BDE.",
                        Genre = "Action, Comedie, Ecchi, Romance, Seinen",
                        Status = "Completed",

                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "My Hero Academia",
                        Auteur = "Nagasaki Kenji",
                        Status = "Completed",
                        Episode = 12,
                        Descriptif = "On suit les aventures d'Izuku Midoriya qui, malgré qu'il vive dans un monde où avoir des pouvoirs est commun, est l'un des seuls à ne pas en posséder.",
                        Genre = "Action, Comedie, Shounen, Super Power, Surnaturel",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Ajin",
                        Auteur = "Sakurai Gamon",
                        Status = "Completed",
                        Episode = 12,
                        Descriptif = "Les Ajin sont des humains qui ne peuvent pas mourir. Il y a dix-sept ans, ils sont d'abord apparus sur un champ de bataille en Afrique. Plusieurs années plus tard, cette espèce vit parmi la société mais sont extrêmement rares. Le gouvernement promet une grosse somme d'argent à quiconque lui livre Ajin. Dans ce monde, nous suivons un lycéen qui après un accident mortel va découvrir qu'il est un de ces monstres. Comment ce dernier va-t-il faire pour survivre dans ce monde ?",
                        Genre = " Action, Aventure, Drame, Horreur, Mystérieux, Seinen, Surnaturel",
                         
                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Blood-C",
                        Auteur = "Nanase Ohkawa",
                        Episode = 12,
                        Descriptif = "Le sanctuaire Ukishima est situé dans une vieille ville pittoresque au bord d’un lac. Kisaragi Saya, une jeune prêtresse du temple vit là avec son père, Tadayoshi. Le jour, elle vit la vie d’une lycéenne normale à l’académie Sanbara. Mais la nuit, elle chasse les « Anciens », des créatures possédant des aptitudes physiques surnaturelles qui prennent pour proies les humains et dont elle seule a la capacité de les vaincre.",
                        Genre = "Action, Horreur, Surnaturel, Vampire",
                        Status = "Completed",

                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Evil or Live",
                        Auteur = "Inconnu",
                        Status = "Ongoing",
                        Episode = 12,
                        Descriptif = "Basé sur le Manhua Lixiang Jinqu de Zhannan Li. Il existe une certaine maladie touchant les adolescents. Solitude, rébellion, être exclu des autres et l'addiction à Internet et les appareils électroniques. Cette maladie se nomme l'addiction à Internet. Un établissement est construit pour \"remettre la jeunesse dans le droit chemin.\" Pour Hibiki, l'un des patient, le plus fortement addict, cet endroit n'est pas une éducation de réadaptation mais un prison. Pourra-t-il un jour s'échapper de cette \"prison\" ?",
                        Genre = " Drame, Horreur, Mystérieux, Psychologique",
                         
                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Fairy Tail",
                        Auteur = "Hiro Mashima",
                        Status = "Ongoing",
                        Episode = 276,
                        Descriptif = "Dans un monde magique au beau milieu du pays de Fiore, la jeune Lucy Heartfiria rejoint la Guilde Magique de Fairy Tail. L'attendent de nombreuses et palpitantes aventures aux côtés de Natsu Dragnir, Happy, Erza Scarlett et Grey Fullbuster.",
                        Genre = " Action, Aventure, Comedie, Fantastique, Magie, Shounen",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Kuroko no Basket S1",
                        Auteur = "Tadatoshi Fujimaki",
                        Status = "Completed",
                        Episode = 25,
                        Descriptif = "Dans le collège Teikô, le club de Basketball était connu pour être l'un des meilleurs du pays. Au sein de l'établissement, cinq génies du sport étaient connus sous le nom de « Génération des Miracles » (キセキの世代, Kiseki no Sedai?). Toutefois, les cinq membres considéraient un sixième joueur comme un élément tout aussi prodigieux qu'ils ne l'étaient : le mystérieux joueur fantôme. À la fin de leur scolarité dans le collège de Teikô, les cinq prodiges se dispersèrent dans des lycées de renommés, désirant chacun mener leur équipe au sommet. C'est ainsi que Tetsuya Kuroko (le joueur fantôme en question), un jeune garçon à l'apparence chétive, ayant la faculté de diriger ailleurs l'attention des autres pour se rendre invisible, intègre le modeste lycée de Seirin, fraîchement construit et avide de dénicher de nouveaux talents pour ses divers clubs de sport. À son arrivée au lycée, la Coach de l'équipe de Basket, Riko Aida prend les inscriptions des premières années. C'est ainsi que Kuroko est devancé à son inscription par l'imposant Taïga Kagami venant tout droit des États-Unis, et désireux de pratiquer le Basketball au Japon, bien qu'ayant une très mauvaise impression de ce dernier. Rapidement, les nouveaux arrivés dans le club de Basketball sont testés dans un match face à l'équipe du lycée. Kuroko fait alors démonstration de son immense talent de passeur alors que Kagami étale son talent inné et destructeur. Bien que tout les différencie, les deux joueurs finissent par sympathiser et deviennent une paire terriblement efficace. Kagami se promet de devenir le meilleur joueur du pays en surpassant les cinq membres de la Génération des Miracles, alors que Kuroko décide de devenir l'ombre de Kagami en l'aidant à réaliser son rêve. Plus tard, les deux amis avouent qu'ils désirent, avant tout, faire de l'équipe de basket-ball de Seirin la meilleure du Japon.",
                        Genre = "Comedie, Shounen, Sport",
                         
                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Juuni Taisen",
                        Auteur = "Hosoda Naoto",
                        Status = "Completed",
                        Episode = 12,
                        Descriptif = "Basé sur le roman Juuni Taisen de NisiOisiN. Douze guerriers portants les noms des douze signes astrologiques chinois s'affrontent pour réaliser un seul souhait. Parmi ces douze combattants, l'histoire nous entraîne aux côtés de Usagi, un homme portant le nom du signe astrologique chinois du lapin.",
                        Genre = "Action, Fantastique, Surnaturel",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Inuyashiki",
                        Auteur = "Hiroya Oku",
                        Status = "Completed",
                        Episode = 11,
                        Descriptif = "Basé sur le manga Inuyashiki de Oku Hiroya. L'histoire suit Inuyashiki Ichirou un vieil homme seulement âgé de 58 ans qui est complétement ignoré et méprisé par sa propre famille malgré tout ce qu'il a fait pour elle. Pour couronner le tout, le médecin vient de lui révéler qu'il avait un cancer et qu'il ne lui restait que trois mois à vivre. Juste quand cela ne pouvait être pire, il perd soudainement la vie à cause des extraterrestres. Il se réveille un peu plus tard, indemne, cependant, il remarque que quelque chose, quelque chose de différent à propos de lui.",
                        Genre = "Action,Aventure,Combat,Comédie,Drame,Magie",
                         
                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Kakegurui",
                        Auteur = "Taku Kawamura",
                        Status = "Completed",
                        Episode = 12,
                        Descriptif = "Basé sur le manga Kakegurui de Kawamoto Homura. L'Académie privée de Hyakkaou est un établissement pour les personnes privilégiées un peu particulier. Quand vous êtes les enfants des plus riches des riches, ce n'est pas votre prouesse athlétique ou la sagesse des livres qui vous maintient tout en haut. C'est savoir lire votre adversaire lors de duels. Quelle meilleure façon de perfectionner ses compétences qu'avec un programme rigoureux de jeu ? Dans l'Académie privée de Hyakkaou, les gagnants vivent comme des rois et les perdants sont condamnés à devenir les esclaves et à souffrir. L'histoire se centre sur Yumeko Jabami une mystérieuse étudiante récemment transférée qui a plus d'un tour dans son sac et de Suzui, un élève persécuté par Mary Saotome, une élève sadique qui prend un malin",
                        Genre = "Action, Drame, Horreur, Mystérieux, Psychologique",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Naruto",
                        Auteur = "Kishimoto Masashi",
                        Status = "Completed",
                        Episode = 220,
                        Descriptif = "Dans le village de Konoha vie Naruto, un jeune garçon détesté et craint des villageois. Il est craint du faite qu'il détient en lui Kyuubi (démon renard à neuf queues) d'une incroyable force, qui a tué un grand nombre de personnes. Le ninja le plus puissant de Konoha à l'époque, le quatrième Hokage, Yondaime réussit à sceller ce démon dans le corps de Naruto. Malheureusement il y laissa la vie. C'est ainsi que douze ans plus tard, Naruto rêve de devenir le plus grand Hokage de Konoha afin que tous le reconnaissent. Mais la route pour devenir Hokage est très longue et Naruto doit remplir un bon nombre d'épreuves et affronter de nombreux ennemis pour atteindre son but !",
                        Genre = "Action, Comedie, Martial Arts, Shounen, Super Power, Aventure, Drame, Fantastique, Surnaturel",
                         
                    });

                    
                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Kujira no Kora wa Sajou ni Utau",
                        Auteur = "Ishiguro Kyohei",
                        Status = "Ongoing",
                        Episode = 12,
                        Descriptif = "Adaptation du manga Les Enfants de la Baleine de Umeda Abi. La baleine de glaise est un vaisseau flottant sur la mer de sable depuis des années. La plupart de ses habitants possèdent le Saimia, un pouvoir surnaturel qui les empêchent de vivre aussi longtemps que les humains normaux. Chakuro, un garçon atteint de la maladie d'hypergraphie vit une vie paisible sur ce vaisseau en compagnie de ses amis. Mais ce calme pourrait prendre fin suite à sa rencontre avec une étrange jeune fille: Lycos.",
                        Genre = "Action, Drame, Fantastique, Mystérieux",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "New Game",
                        Auteur = "Tokuno Shotaro",
                        Status = "Completed",
                        Episode = 12,
                        Descriptif = "Aoba Sukuzake, malgré ses apparences de collégienne, vient d'entrer dans le monde du travail. Elle va alors travailler dans une société de jeux vidéo en tant qu'artiste.",
                        Genre = "Comedie, Jeu, Tranche de vie",
                         
                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Re:Zero kara Hajimeru Isekai Seikatsu",
                        Auteur = "Watanabe Masaharu",
                        Status = "Completed",
                        Episode = 25,
                        Descriptif = "Il s'agit de l'adaptation du roman Re:Zero de Nagatsuki Tappei et de Ootsuka Shinichirou. Un jour un lycéen nommé Natsuki Subaru est transporté dans un monde parallèle sans aucune explication. En essayant de prendre ses marques et en se promenant dans ce monde, Subaru est attaqué par une bande de brigands mais est sauvé par une jeune fille nommée Emilia. Pour lui rendre la pareille, il décide de l'aider dans ses tâches quotidiennes. Un beau jour, Emilia et Subaru sont attaqués et tués par une mystérieuse personne. Cependant, Subaru se réveille au lieu et au jour où il est arrivé dans ce monde. C'est à ce moment-là que il se rend compte qu'il peut retourner dans le passé après être mort. Pour échapper à son funeste destin, Subaru décide d'utiliser son pouvoir pour sauver Emilia et pour se sauver lui-même.",
                        Genre = "Comedie, Drame, Fantastique, Romance",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Shokugeki no Soma",
                        Auteur = "Shogo Yasukawa",
                        Status = "Completed",
                        Episode = 24,
                        Descriptif = "Sôma Yukihira rêve de devenir le chef cuisinier dans le restaurant familial et ainsi surpasser les talents culinaires de son père.Alors que Sôma vient juste d'être diplômé au collège, son père Jôichirô Yukihira ferme le restaurant pour cuisiner aux États-Unis. L'esprit de compétition de Sôma va alors être mis à l'épreuve par son père qui lui conseille de rejoindre une école d'élite culinaire, où seuls 10 % des élèves sont diplômés.Sôma va - t - il parvenir à atteindre son objectif ? ",
                        Genre = "Ecchi, Shounen",
                         
                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Sakamoto desu ga",
                        Auteur = "Takamatsu Shinji",
                        Status = "Completed",
                        Episode = 121,
                        Descriptif = "Sakamoto, en première année, est l'élève le plus cool de son lycée, ce qui le rend très populaire auprès des jeunes filles. Les garçons, quant à eux, le jalousent et rêve de lui faire \"payer son arrogance\". Pour cela, tout est bon : intimidations, tentative d'humiliation, etc... Mais rien de cela ne fonctionne sur Sakamoto qui, en plus, se sort de toutes les situations en devenant toujours plus cool.",
                        Genre = "Action, Comedie",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Shoukoku no Altair",
                        Auteur = "Furuhashi Kazuhiro",
                        Status = "Completed",
                        Episode = 24,
                        Descriptif = "Il s'agit d'une adaptation du manga Shôkoku no Altair. Dans un monde fantastique où la guerre est un bon moyen d'étendre son empire, Mahmut, jeune prodige, parcourt les pays afin de prôner la paix.",
                        Genre = "Action, Drame, Fantastique, Historique",
                         
                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Shijou Saikyou no Deshi Kenichi",
                        Auteur = "Kamegaki Hajime",
                        Status = "Completed",
                        Episode = 50,
                        Descriptif = "Souffre douleur pendant le collège, Kenichi Shirahama semble entrer sous les mêmes auspices au lycée... Mais un matin, sur le chemin le conduisant au lycée, il fait la rencontre de Fuurinji Miu, une nouvelle élève qui se révèle être une experte en arts martiaux. Afin de ne plus être un punching-ball vivant pour tous les voyous, Kenichi décide sous les conseils de sa nouvelle amie d'intégrer le dojô du grand-père de cette dernière. Il va devenir le seul disciple des plus grands maîtres de cinq disciplines qui sont : le karate, la boxe thaï, l'aïkijutsu, le ninjitsu et le kung-fu. Ces maîtres se révélant un peu spéciaux, vont lui apprendre à se défendre et à ne plus fuir face à ses adversaires, et ce par tous les moyens...",
                        Genre = "Action, Comedie, Ecchi, Romance, Sport",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Shingeki no Kyojin Saison 1",
                        Auteur = "Araki Tetsuro",
                        Status = "Completed",
                        Episode = 25,
                        Descriptif = "Il y a 107 ans, les Titans ont presque exterminés la race humaine. Ces Titans mesurent principalement une dizaine de mètres et ils se nourrissent d'humains. Les humains ayant survécus à cette extermination ont construit une cité fortifiée avec des murs d'enceinte de 50 mètres de haut pour pouvoir se protéger des Titans. Pendant 100 ans les humains ont connus la paix. Eren est un jeune garçon qui rêve de sortir de la ville pour explorer le monde extérieur. Il mène une vie paisible avec ses parents et sa sœur Mikasa dans le district de Shiganshina. Mais un jour de l'année 845, un Titan de plus de 60 mètres de haut apparait. Il démolit une partie du mur du district de Shiganshina et provoque une invasion de Titans. Eren verra sa mère se faire dévorer sous ses yeux sans rien pouvoir faire. Il décidera après ces événements traumatisants de s'engager dans les forces militaires de la ville avec pour but d'exterminer tous les Titans qui existent.",
                        Genre = "Drame, Horreur, Mystérieux, Psychologique, Shounen, Surnaturel",
                         
                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Toaru Kagaku no Railgun S1",
                        Auteur = "Kamachi Kazuma",
                        Status = "Completed",
                        Episode = 24,
                        Descriptif = "L'histoire se déroule dans un monde où se mêle pouvoir surnaturel et magie. Dans la cité académique comptant pas moins de 2,3 millions d'habitants, 80% sont des étudiants travaillent pour un programme de développement du cerveau humain. Mikoto Misaka est une étudiante de la Cité Scolaire. Cette grande ville universitaire accueille les étudiants pourvus de dons qu'on appelles espers. Ils sont classés par niveau; de 0, absence de don, à 5, les dons les plus puissants. Misaka est la troisième plus puissante des sept niveau 5. Elle contrôle les champs électriques et magnétiques. Cette jeune fille vie dans la partie de l'école la plus réputée de la Cité Scolaire et cohabite avec Kuroko Shirai, esper de niveau 4 qui possède le don de téléportation et faisant partie de Judgment, une organisation qui enquête pour régler les problème de la Cité Scolaire. Accompagnées par deux autres amies elles vont tenter de résoudre certains problèmes qui pourraient mettre en danger la cité toute entière.",
                        Genre = "Action, Aventure, Comedie, Drame, Sci-Fi, Super Power",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Toaru Majutsu no Index S1",
                        Auteur = "Kamachi Kazuma",
                        Status = "Completed",
                        Episode = 24,
                        Descriptif = "L'histoire se déroule dans un univers où la magie et les pouvoirs surnaturels existent et se focalise autour d'un lycéen, Toma Kamijo, qui a le pouvoir d'annuler toutes magies ou pouvoirs. Toma Kamijo va faire la rencontre d'une nonne qui est poursuivi parce qu'elle a dans son cerveau une connaissance interdite...",
                        Genre = "Action, Comedie, Drame, Fantastique, Harem, Magie, Sci-Fi, Super Power",
                         
                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Ushio to Tora",
                        Auteur = "Toshiki Inoue, Kazuhiro Fujita",
                        Status = "Completed",
                        Episode = 26,
                        Descriptif = "Ushio découvre un jour dans son sous-sol un monstre, celui là même qui a été « empalé » par l’un de ses ancêtres il y a 500 ans avec la Lance du Fauve ! Ushio, qui n’avait jamais cru à cette histoire, constate que cette découverte va poser problème car le monstre va libérer des énergies négatives qui vont faire apparaître des spectres qui s’attaquent à tout le monde ! Ushio n’a plus le choix, il est obligé de libérer ce monstre mangeur d’hommes, afin de l’aider à ramener l’ordre !",
                        Genre = "Action, Aventure, Comedie, Démons, Shounen, Surnaturel",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "ViVid Strike",
                        Auteur = "Nishimura Junji",
                        Status = "Completed",
                        Episode = 12,
                        Descriptif = "Il s'agit d'un nouveau projet créé et scénarisé par Masaki Tsuzuki, le créateur de la saga Mahou Shoujo Lyrical Nanoha. Ce nouveau projet a pour slogan : \"Vous n'aurez pas peur, je suis avec vous.\" L'histoire nous entraine aux côtés de Fuuka Leventon et de son amie Rinne Bellinetta.Vivant dans la rue avec son amie lorsqu'elles étaient jeunes, Fuuka s'est jurée de devenir plus forte pour protéger ceux qu'elle aime. Aujourd'hui, nous suivons les progrès de ces deux amies.",
                        Genre = "Action, Aventure, Magie, Sci-Fi",
                         
                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Kono Subarashii Sekai ni Shukufuku wo",
                        Auteur = "Kanasaki Takaomi",
                        Status = "Completed",
                        Episode = 10,
                        Descriptif = "La vie de Satou Kazuma, un hikikomori aimant les jeux, se termine bien trop tôt, dû à un accident de la route...Alors que ce dernier pense être mort, une déesse apparaît devant lui et lui propose d'être réincarné dans un monde fantastique. Après s'être bien adapté à ce monde et vivant avec une petite équipe dont fait partie la déesse, ce dernier est chargé d'une mission, vaincre le roi-démon. Cependant, ce dernier n'en a strictement rien à faire de jouer au héros. Après avoir refusé la mission, ce dernier passe ses journées à ne rien faire avec son équipe. Un beau jour, la déesse, comme à son habitude, est impulsive au point qu'aujourd'hui cette dernière s'est fait remarquer par l'armée du Roi. Comment va-t-elle se sortir de son pétrin sans aggraver la situation plus qu'elle ne l'est déjà… ?",
                        Genre = "Aventure, Comedie, Fantastique, Romance, Surnaturel",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Kiseijuu: Sei no Kakuritsu",
                        Auteur = "Iwaaki Hitoshi",
                        Status = "Completed",
                        Episode = 24,
                        Descriptif = "Une nuit, des sphères de la taille d'une balle de tennis, contenant des créatures à l'apparence de serpents, tombent en nombre inconnu partout dans le monde. Ils sont programmés pour prendre la place des cerveaux humains. Un de ceux-ci s'attaque à un jeune homme, Shin'ichi, durant son sommeil, en essayant de s'introduire par son oreille mais ne peut l'atteindre, ce dernier ayant gardé ses écouteurs pour la nuit. Réveillé en sursaut alors que le parasite tente de s'introduire par son nez, il essaye de se défendre mais finit par se faire perforer la main droite. Le lycéen prend alors ses écouteurs et les enroule autour de son bras, empêchant le parasite de grimper jusqu'au cerveau. Ne pouvant quitter son bras, ce dernier fusionne finalement avec sa main droite. Pendant ce temps, d'autres parasites, ayant réussi à prendre possession du cerveau de leur hôte, commencent à se nourrir d'êtres humains, tandis que la créature et Shin'ichi sont forcés de cohabiter.",
                        Genre = "Action, Drame, Horreur, Psychologique, Sci-Fi, Seinen",
                         
                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Koutetsujou no Kabaneri",
                        Auteur = "Araki Tetsuro",
                        Status = "Completed",
                        Episode = 12,
                        Descriptif = "L’histoire se déroule sur l’île de Hinomoto où les humains se cachent dans des forteresses pour survivre aux Kabane, des monstres zombies au cœur d’acier. Pour protéger la population, des soldats volontaires tentent de repousser l’invasion. Nous suivons Ikoma, un jeune forgeron qui travaille sur une arme spéciale permettant de combattre plus efficacement les Kabane. Sa rencontre avec la mytérieuse Mumei, une fille au lourd secret qui maîtrise l’armement, va bouleverser sa vie.",
                        Genre = "Action, Drame, Fantastique, Sci-Fi, Shounen",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Black Clover",
                        Auteur = "Tabata Yuuki",
                        Status = "Ongoing",
                        Episode = 14,
                        Descriptif = "Il s'agit d'une série animée adaptant le manga Black Clover de Tabata Yuuki. Asta et Yuno ont tout les deux été abandonnés à la même église, depuis, ils sont inséparables. Étant enfants, ils se sont promis de devenir le prochain Roi Sorcier. Cependant en grandissant, un grand fossé s'est creusé entre eux. Alors que Yuno est un magicien de génie, Asta semble ne pas pouvoir utiliser la magie, il décide donc de s'entraîner physiquement. Quand ils ont reçu leur grimoire à l'âge de 15 ans, Yuno reçoit un grimoire avec un trèfle à quatre feuille (la plupart en reçoivent un avec un trèfle à 3 feuille), tandis que Asta ne reçoit rien du tout. Toutefois, lorsque Yuno se retrouve menacé, le vrai pouvoir de Asta se réveille et celui-ci reçoit un grimoire avec un trèfle à 5 feuilles. Yuno et Asta vont commencer à prendre des chemins différents, cependant leur but reste le même : devenir le prochain Roi Sorcier !",
                        Genre = "Action, Comedie, Fantastique, Shounen",
                         
                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Classroom of the Elite",
                        Auteur = "Shunsaku Tomose",
                        Status = "Ongoing",
                        Episode = 12,
                        Descriptif = "Le prestigieux lycée Koudo Ikusei est à la pointe de la technologie ou presque 100% des élèves passent ensuite à l’université ou trouvent un travail. Les élèves y ont une large liberté mais la vérité est que seul l’élite des élèves y reçoit des traitements favorables. Nous suivons le quotidien de Kiyotaka Ayanokoji qui fait partie de la Classe D, qui est celle dans laquelle l’école met les élèves dit « inférieur » afin de les ridiculiser. Kiyotaka Ayanokoji a été négligent lors de ses examens d’entrée et a fini dans la Classe D. Dans cette classe atypique, il se lie notamment d’amitié avec Suzune Horikita et Kikyo Kushida.",
                        Genre = "Comedie, Psychologique, Romance, Seinen",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Akame ga Kill! (Non Censuré)",
                        Auteur = "Ikariya, Atsushi",
                        Status = "Completed",
                        Episode = 24,
                        Descriptif = "Tatsumi est un combattant qui part pour la capitale à la recherche d'un moyen de gagner de l'argent pour aider son village pauvre. Après avoir été victime d'un vol par une femme et avoir perdu tout son argent, Tatsumi est pris en charge par une aristocrate du nom d'Aria. La nuit suivante, le manoir d'Aria est attaqué par un groupe d'assassins connus sous le nom de Night Raid. Alors que Tatsumi tente de défendre Aria de l'assassin Akame, un autre membre du groupe met fin au combat. Il révèle qu'Aria a enlevé plusieurs villageois et les a torturés pour son plaisir. Choqué en découvrant les villageois torturés, qui incluent ses deux meilleurs amis, Tatsumi tue Aria. Après avoir vu le potentiel de Tatsumi, les Night Raid invitent le garçon dans leur groupe et le préviennent que peu importe leur manière d'assassiner les corrompus, ils restent des assassins. Tatsumi accepte afin de devenir plus fort et de sauver la capitale. Dans leur lutte contre l'Empire, les Night Raid se défendent face à un groupe d'élite connu sous le nom de Jaegers, mené par Esdeath, la combattante la plus puissante de l'Empire.",
                        Genre = "Action, Aventure, Fantastique, Shounen, Super Power",
                         
                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Rokudenashi Majutsu Koushi to Akashic Records",
                        Auteur = "Kazuto Minato",
                        Status = "Completed",
                        Episode = 12,
                        Descriptif = "Adaptation du roman Rokudenashi Majutsu Koushi to Akashic Records de Tarou Hitsuji. Sisti, une jeune magicienne intègre une académie de magie reconnue pour aiguiser ses compétences et résoudre le mystère du château dans le ciel (Sky Castle). Alors que son professeur préféré part en retraite, ce dernier est remplacé par Glen, un paresseux qui ne possède aucune compétence pour enseigner. Elle se pose alors une question : \"Comment le meilleur magicien de cette académie a pu recruter un tel énergumène ?\"",
                        Genre = "Action, Fantastique, Shounen",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Mob Psycho 100",
                        Auteur = "ONE",
                        Status = "Completed",
                        Episode = 12,
                        Descriptif = "Mob Psych0 100 c'est l'histoire de Kageyama Shigeo, alias Mob , est un collégien avec des pouvoirs sensoriels...un médium. Il est capable de plier des cuillères et soulever des objets avec son esprit depuis son plus jeune âge. Cependant, il a récemment arreté d'utiliser ses pouvoirs en public à cause de la mauvaise attention qu'il reçoit à chaque fois qu'il les utilisent On suit donc sa vie quotidienne avec \"son maitre psychique\" (qui n'a pas vraiment de pouvoir), tentant de réaliser un but : celui de devenir ami (ou plus ) avec une fille de sa classe : Tsubomi. Nombreux d'entre vous reconnaitront les coups de crayon de ONE, l'auteur de OnePunch-Man. Je pense que cela vaut vraiment la peine d'y jeter un coup d'oeil malgré la qualité des dessins qui néamoins s'améliore au fil des chapitres et qui est largement supérieure à sa version de OnePunch-Man! Aucune team ne proposant de VF pour l'instant, on vous proposera 2 chapitres par semaine, ce qui nous laissera le temps de nous occuper de la suite. Le manga dénombre actuellement 9 tomes réliés et est publié par le magazine URA SUNDAY.",
                        Genre = "Action, Comedie, Psychologique, Shounen, Surnaturel, Tranche de vie",
                         
                    });


                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Youjo Senki",
                        Auteur = "Carlo Zen",
                        Status = "Completed",
                        Episode = 12,
                        Descriptif = "L'histoire suit Tanya Degurechov, une jeune fille aux commandes d'une armée de sorciers. Malgré son jeune âge et son allure frêle, elle est capable d'anéantir à elle seule une armée entière. Banal salarié dans sa vie antérieure, Tanya fut réincarné en jeune fille pour avoir mit en colère Dieu. Avec ses nouveaux pouvoirs en poche, Tanya va devenir l'entité la plus dangereuse et la plus crainte de l'armée.",
                        Genre = "Action, Drame, Magie, Militaire, Surnaturel",
                         
                    });

                    context.Add(new Manga
                    {
                        IdM = Guid.NewGuid().ToString(),
                        Titre = "Re:Creators",
                        Auteur = "Aoki Ei",
                        Status = "Ongoing",
                        Episode = 22,
                        Descriptif = "Il s'agit d'un anime original de Hiroe Rei et Ei Aoki. Les humains ont créé plusieurs histoires. Joie, tristesse, la colère, profondes émotions. Ces histoires qui bouleversent nos émotions et nous fascinent. Si les personnages de ces histoires avaient leurs propres émotions et intentions, serions-nous des existences divines pour les avoir créé ? Notre monde a changé, dans Re:CREATORS, tout le monde devient un Créateur.",
                        Genre = "Action, Mystérieux, Sci-Fi",
                         
                    });



                    await context.SaveChangesAsync();

                }
            }

        }
    }
}
