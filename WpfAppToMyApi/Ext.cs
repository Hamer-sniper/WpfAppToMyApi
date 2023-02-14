using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppToMyApi
{
    public static class Ext
    {
        public static ObservableCollection<DataBook> ToObservableCollection(this IEnumerable<DataBook> e)
        {
            ObservableCollection<DataBook> t = new ObservableCollection<DataBook>();
            foreach (var item in e)
            {
                t.Add(item);
            }
            return t;
        }


    }
}
