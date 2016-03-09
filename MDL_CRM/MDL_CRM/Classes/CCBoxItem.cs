using System;
using System.Collections.Generic;
using System.Text;

namespace CheckComboBoxTest {
    public class CCBoxItem {
        private int val;
        private string sval;
        public int Value
        {
            get { return val; }
            set { val = value; }
        }
        public string SValue
        {
            get { return sval; }
            set { sval = value; }
        }
        
        private string name;
        public string Name {
            get { return name; }
            set { name = value; }
        }

        public CCBoxItem() {
        }

        public CCBoxItem(string name, int val) {
            this.name = name;
            this.val = val;
        }
        public CCBoxItem(string name, int val ,string strVal)
        {
            this.name = name;
            this.val = val;
            this.sval = strVal;
        }

        public override string ToString() {
            return string.Format("name: '{0}', value: {1}", name, val);
        }
    }
}
