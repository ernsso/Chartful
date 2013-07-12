﻿#pragma checksum "..\..\..\Pages\Workspace.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A5BB4322A03A6C2EED8A6779F242EF2A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18046
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Chartful.Controls;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Converters;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Chartful.Pages {
    
    
    /// <summary>
    /// Workspace
    /// </summary>
    public partial class Workspace : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\Pages\Workspace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl ItemListControl;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\Pages\Workspace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Chartful.Controls.DragCanvas dragCanvas;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\Pages\Workspace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel controls;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\Pages\Workspace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label data1;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\Pages\Workspace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox UIObjectList;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\Pages\Workspace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ContentPropertyBox;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\Pages\Workspace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel StatusContent;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\Pages\Workspace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Position;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Chartful;component/pages/workspace.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Workspace.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\Pages\Workspace.xaml"
            ((Chartful.Pages.Workspace)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ItemListControl = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 3:
            this.dragCanvas = ((Chartful.Controls.DragCanvas)(target));
            return;
            case 4:
            this.controls = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.data1 = ((System.Windows.Controls.Label)(target));
            
            #line 79 "..\..\..\Pages\Workspace.xaml"
            this.data1.MouseMove += new System.Windows.Input.MouseEventHandler(this.pickData_MouseMove);
            
            #line default
            #line hidden
            return;
            case 6:
            this.UIObjectList = ((System.Windows.Controls.ComboBox)(target));
            
            #line 90 "..\..\..\Pages\Workspace.xaml"
            this.UIObjectList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.UIObjectList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ContentPropertyBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 95 "..\..\..\Pages\Workspace.xaml"
            this.ContentPropertyBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ContentPropertyBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.StatusContent = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 9:
            this.Position = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

