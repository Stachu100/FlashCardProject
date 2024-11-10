using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiszkiApp.EntityClasses.Models
{
    public class LocalCategoryTable
    {
        [PrimaryKey, AutoIncrement]
        public int IdCategory { get; set; }

        public string CategoryName { get; set; }

        public string FrontLanguage { get; set; }

        public string BackLanguage { get; set; }

        public string LanguageLevel { get; set; }
        public int IsSent { get; set; }

        [Ignore]
        public string FrontFlagUrl { get; set; }
        [Ignore]
        public string BackFlagUrl { get; set; }
    }
}
