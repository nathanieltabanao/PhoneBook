using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phonebook
{
    class ErrorMessages
    {
        public void NotComplete()
        {
            MessageBox.Show("Please fill all fields", "Error");
        }
    }
}
