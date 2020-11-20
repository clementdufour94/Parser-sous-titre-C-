using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace StreamV1._0
{
    class SRTFile
    {
        private static Queue<Subtitle> _subtitles;


        private static Subtitle _activeSubtitle;

        private static TimeSpan _currentTime = new TimeSpan();

        static void Main(string[] args)
        {

            //Première fonction





            _subtitles = new Queue<Subtitle>();

            Subtitle st1 = new Subtitle()
            {
                StartTime = TimeSpan.Parse("00:00:00,000 "),
                EndTime = TimeSpan.Parse("00:00:09,000"), 
                Text = "Qui je suis ?"
                // Text = Console.WriteLine(lecteur.Sub[i].texte)
            };

            Subtitle st2 = new Subtitle()
            {
                StartTime = TimeSpan.Parse("00:00:09,150"),
                EndTime = TimeSpan.Parse("00:00:10,268"),
                Text = "Vous tenez à le savoir?"
            };

            Subtitle st3 = new Subtitle()
            {
                StartTime = TimeSpan.Parse("00:00:11,920 "),
                EndTime = TimeSpan.Parse("00:00:13,980"),
                Text = "Mon histoire n'est pas faite pour les âmes sensibles"
            };

            Subtitle st4 = new Subtitle()
            {
                StartTime = TimeSpan.Parse("00:00:14,290"),
                EndTime = TimeSpan.Parse("00:00:20,930"),
                Text = "Si on vous a dit que c'était un conte joyeux..."
            };
            Subtitle st5 = new Subtitle()
            {
                StartTime = TimeSpan.Parse("00:00:24,050"),
                EndTime = TimeSpan.Parse("00:00:33,050"),
                Text = "que j'étais un type normal,ordinaire, sans le moindre souci..."
            };

            Subtitle st6 = new Subtitle()
            {
                StartTime = TimeSpan.Parse("00:00:33,050 "),
                EndTime = TimeSpan.Parse("00:00:46,339"),
                Text = "on vous a menti"
            };

            Subtitle st7 = new Subtitle()
            {
                StartTime = TimeSpan.Parse("00:00:46,339"),
                EndTime = TimeSpan.Parse("00:00:53,690"),
                Text = "Mais croyez-moi."
            };




            _subtitles.Enqueue(st1);
            _subtitles.Enqueue(st2);
            _subtitles.Enqueue(st3);
            _subtitles.Enqueue(st4);
            _subtitles.Enqueue(st5);
            _subtitles.Enqueue(st6);
            _subtitles.Enqueue(st7);



            Timer timer = new Timer(ShowSubtitles, null, 0, 55);

            while (_currentTime <= new TimeSpan(0, 0, 0, 55))
            {

            }

            Console.WriteLine("End");

            try
         
            {
                string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                mydocpath += @"\sous-titre\Spiderman.srt";
                SRTFile lecteur = new SRTFile(mydocpath);

                int i = 0;

                while (i < lecteur.Sub.Count)
                //lecteur.Sub.Count
                {
                    Console.WriteLine(lecteur.Sub[i].texte);
                    i++;
            
                }

            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);

            }

        }


        private static void ShowSubtitles(object state)
        {

            if (_activeSubtitle == null && _subtitles.Count > 0)
                _activeSubtitle = _subtitles.Dequeue();


            if (_activeSubtitle != null)
            {


                if (_currentTime >= _activeSubtitle.StartTime && _currentTime <= _activeSubtitle.EndTime)
                    Console.WriteLine("\t{0}", _activeSubtitle.Text);

                if (_currentTime >= _activeSubtitle.EndTime)
                    _activeSubtitle = null;
            }
            _currentTime = _currentTime.Add(new TimeSpan(0, 0, 0, 0, 100));

        }





        string total;
        public List<Sub> Sub = new List<Sub>();

        public SRTFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string s;
                total = "";
                while ((s = sr.ReadLine()) != null)
                {
                    total += s + "\n";
                }
            }
        
            int i = 0;


            while (i + 1 < total.Length)
            {
                int j = 0;
                string start = "";
                string end = "";
                string valeur = "";

                while (total[i] != '\n' && i + 1 < total.Length)
                {
                    i++;
                }
                i++;
                

                j = i;
                while (total[i] != '\n' && i + 1 < total.Length)
                {
                    if (i - j < 12)
                    {
                        start += total[i];
                    }

                    else if (-1 < i - (j + 17) && i - (j + 17) < 12)
                    {
                        end += total[i];
                    }
                 
                    i++;
                }

                i++;
              
                while ((total[i] != '\n' || total[i + 1] != '\n') && i + 2 < total.Length)
                {
                   
                    valeur += total[i];
                    i++;
                }

                valeur += total[i];
                 
                i++;
                while ((total[i] < '0' || total[i] > '9') && i + 1 < total.Length)
                {
                    i++;
                }
                Sub.Add(new Sub(start, end, valeur));
                
            }

        }



    }

    internal class Subtitle
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Text { get; set; }
    }



}