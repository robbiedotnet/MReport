
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSReportGenerator.Models
{
     public class ReportFiles 
    {
        public string OutputDirectory { get; set; }

        public ReportFiles()
        {
            var dirFileOutputPath = Path.Combine(ConfigurationManager.AppSettings["MBSReport_Base"],
                ConfigurationManager.AppSettings["MBSReport_ResultsFolder"]);

            OutputDirectory = dirFileOutputPath;
        }

        public ReportFiles(string directory)
        {
            OutputDirectory = directory;
        }

        public void WriteReportFile(StringBuilder sbResultFile, string fileName)
        {
            using (var sw = new StreamWriter(Path.Combine(OutputDirectory, fileName)))
            {
                sw.Write(sbResultFile.ToString());
            }
        }
       
    }
}
