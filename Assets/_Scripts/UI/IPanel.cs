using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.UI
{
    public interface IPanel
    {
        public string Name { get; set; }

        public void Show();
        public void Hide();
    }
}
