using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LearningStreamsV2._0
{
    class Excuse
    {
        public string Description { get; set; }
        public string Result { get; set; }
        public DateTime LastUsed { get; set; }
        public string ExcusePath { get; set; }

        public Excuse()
        {
            ExcusePath = "";
        }
        public Excuse(string excusePath)
        {
            this.ExcusePath = excusePath;
        }
        public Excuse(Random random, string folder)
        {
            string[] files = Directory.GetFiles(folder);
            OpenFile(files[random.Next(files.Length)]);
        }

        public void OpenFile(string excusePath)
        {
            this.ExcusePath = excusePath;
            using (StreamReader reader = new StreamReader(excusePath))
            {
                Description = reader.ReadLine();
                Result = reader.ReadLine();
                LastUsed = Convert.ToDateTime(reader.ReadLine());
            }
        }
        public void SaveFile(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(Description);
                writer.WriteLine(Result);
                writer.WriteLine(LastUsed);
            }
        }
    }
   

}
