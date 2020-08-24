using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvedLi
{
    public class ComboboxItem
    {
        public ComboboxItem()
        {

        }

        public string Text { get; set; }
        public object Value { get; set; }

        public  ComboboxItem(ComboboxItem my)
        {
            this.Text = my.Text;
            this.Value = my.Value;

        }
        public override string ToString()
        {
            return Text;
        }
    }
}
