using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeRename
{
    public  class Settings
    {

        public Dictionary<string , string > Set { get; set; }

        public Settings ()
        {
            Set = new Dictionary<string, string>();
            Set.Add("file", @"C:\my_work_files\pinterest\source_all_account_for_blaster.txt");
            Set.Add("result", @"C:\my_work_files\pinterest\source_all_account_for_blaster_renamed.txt");
            Set.Add("result_bad", @"C:\my_work_files\pinterest\errors_rename.txt");
            Set.Add("result_good", @"C:\my_work_files\pinterest\add_me.csv");
            Set.Add("suffix", "_qf");
           



        }
    }
}
