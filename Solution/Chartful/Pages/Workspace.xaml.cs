﻿using Chartful.Controls;
using Chartful.Model;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Chartful.Net;
using System.Collections.Specialized;

namespace Chartful.Pages
{
    /// <summary>
    /// Interaction logic for Workspace.xaml
    /// </summary>
    public partial class Workspace : UserControl
    {
        public ObservableCollection<Document> Documents { get; private set; }
        public Document Selected { get; set; }

        public Workspace()
        {
            InitializeComponent();
            DataContext = this;

            (Application.Current.MainWindow as MainWindow).Editor = this;
        }

        /// <summary>
        /// Called when the page is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Refresh the editor
        /// </summary>
        public void Refresh()
        {
            var wnd = Application.Current.MainWindow as MainWindow;

            // Get all opened documents
            Documents = wnd.DocManager.Documents;

            // Get selected document
            Selected = wnd.DocManager.Selected;

            // Push in the Canvas every Document's UIOject
            SetUIObject();

            // Refresh Data Context
            DataContext = null;
            DataContext = this;
        }

        /// <summary>
        /// Change the selected document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextItemList_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var wnd = Application.Current.MainWindow as MainWindow;
            wnd.DocManager.SelectDocument(((TextItemList)sender).TextItem);
            Refresh();
        }

        /// <summary>
        /// Push in the document every UIElement's Canvas
        /// </summary>
        public void GetUIObject()
        {
            if (null != Selected)
            {
                Selected.Content.Clear();

                foreach (UIElement e in dragCanvas.Children)
                {
                    UIObject o = new UIObject();

                    o.ID = ((TextBlock)e).Name;
                    o.Content = ((TextBlock)e).Text;
                    o.FontSize = ((TextBlock)e).FontSize;
                    o.Left = Canvas.GetLeft(e);
                    o.Top = Canvas.GetTop(e);

                    Selected.Content.Add(o);
                }

                // Write the modifications in the file
                Selected.ParseToXML();
            }
        }

        /// <summary>
        /// Push in the Canvas every Document's UIOject
        /// </summary>
        public void SetUIObject()
        {
            if (null != Selected)
            {
                dragCanvas.Children.Clear();

                foreach (UIObject o in Selected.Content)
                {
                    TextBlock item = new TextBlock { Text = o.Content };
                    item.Name = o.ID;
                    item.FontSize = 36;
                    item.FontWeight = FontWeights.Bold;
                    item.Background = Brushes.Transparent;

                    dragCanvas.Children.Add(item);
                    Canvas.SetLeft(item, o.Left);
                    Canvas.SetTop(item, o.Top);
                }
            }
        }
        
        /// <summary>
        /// Get the dragged Element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pickData_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDropEffects effects;
                DataObject obj = new DataObject();
                Label source = (Label)sender;
                obj.SetData(typeof(WrapPanel), source.Content);
                effects = DragDrop.DoDragDrop(source, obj, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        /// <summary>
        /// Drop the dragged Element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void receiver_Drop(object sender, DragEventArgs e)
        {
            if (Selected == null)
            {
                ModernDialog.ShowMessage("before editing, you should create or open a document", "No document", MessageBoxButton.OK);
                return;
            }

            if (e.Data.GetDataPresent(typeof(WrapPanel)))
            {
                e.Effects = DragDropEffects.Copy;
                string typeName = ((TextBlock)((WrapPanel)e.Data.GetData(typeof(WrapPanel))).Children[1]).Text;

                if (typeName == "Title")
                {
                    //Get the Canvas'position
                    Point relativePoint = dragCanvas.TransformToAncestor(this)
                                  .Transform(new Point(0, 0));

                    UIObject o = new UIObject();
                    o.ID = typeName + ++Selected.LastID;
                    o.Content = "New " + o.ID;

                    //Set the new object's position
                    o.Left = e.GetPosition(this).X - relativePoint.X;
                    o.Top = e.GetPosition(this).Y - relativePoint.Y;

                    // Add the new Object to the content List
                    Selected.Content.Add(o);
                    (Application.Current.MainWindow as MainWindow).MyPeerChannel.Send(o);
                }

                //log the modification
                this.StatusContent.Children.Add(new TextBlock { Text = "Add : " + typeName });

                Refresh();
            }
            else
                e.Effects = DragDropEffects.None;
        }

        /// <summary>
        /// Update the document's content when modifications are over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dragCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            GetUIObject();
        }

        private void UIObjectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != UIObjectList.SelectedItem)
            {
                Selected.Focused = Selected.FindUIObject(((UIObject)UIObjectList.SelectedItem).ID);
                ContentPropertyBox.Text = ((UIObject)UIObjectList.SelectedItem).Content;
            }
            else
            {
                Selected.Focused = -1;
                ContentPropertyBox.Text = "";
            }
        }

        private void ContentPropertyBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (-1 < Selected.Focused)
            {
                Selected.UpdateUIObject(Selected.Content[Selected.Focused]);
                foreach (TextBlock tb in dragCanvas.Children)
                    if (tb.Name == ((UIObject)UIObjectList.SelectedItem).ID)
                        tb.Text = ContentPropertyBox.Text;

                UIObject o = (UIObject)UIObjectList.SelectedItem;
                o.Content = ContentPropertyBox.Text;
                (Application.Current.MainWindow as MainWindow).MyPeerChannel.Send(o);
            }
        }

        int mouveNumber = 0;

        public void UpdateUIObject(UIObject o)
        {
            if (0 == mouveNumber++)
            {
                Selected.UpdateUIObject(o);
                (Application.Current.MainWindow as MainWindow).MyPeerChannel.Send(o);
            }
            else
                mouveNumber++;

            if (5 < mouveNumber)
                mouveNumber = 0;
            //GetUIObject();
        }
    }
}
