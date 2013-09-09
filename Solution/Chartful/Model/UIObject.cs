using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chartful.Model
{
    public class UIObject
    {
        public string Name { get; set; }
        public string UIType { get; set; }
        public string Content { get; set; }
        public double FontSize { get; set; }
        public double Top { get; set; }
        public double Left { get; set; }

        public UIObject() { }

        public override string ToString()
        {
            return this.Name;
        }

        public UIElement ToUIElement()
        {
            TextBlock element = new TextBlock { Text = this.Content };
            element.Name = this.Name;
            element.FontSize = this.FontSize;
            element.FontWeight = FontWeights.Bold;
            element.Background = Brushes.Transparent;

            return element;
        }
    }
}
