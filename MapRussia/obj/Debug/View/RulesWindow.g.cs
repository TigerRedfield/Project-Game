﻿#pragma checksum "..\..\..\View\RulesWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "EACF960050DC890DAB0F57F6B8FC1D7A8802E4BA5C5384EDE4D7D439936608D8"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MapRussia.View;
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


namespace MapRussia.View {
    
    
    /// <summary>
    /// RulesWindow
    /// </summary>
    public partial class RulesWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\View\RulesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonExitMenu;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\View\RulesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonHidden;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\View\RulesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImageHidden;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\View\RulesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonQ;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\View\RulesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonExit;
        
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
            System.Uri resourceLocater = new System.Uri("/MapRussia;component/view/ruleswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\RulesWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            
            #line 7 "..\..\..\View\RulesWindow.xaml"
            ((MapRussia.View.RulesWindow)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ButtonExitMenu = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\View\RulesWindow.xaml"
            this.ButtonExitMenu.Click += new System.Windows.RoutedEventHandler(this.ButtonExitMenu_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ButtonHidden = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\View\RulesWindow.xaml"
            this.ButtonHidden.Click += new System.Windows.RoutedEventHandler(this.ButtonHidden_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ImageHidden = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.ButtonQ = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\View\RulesWindow.xaml"
            this.ButtonQ.Click += new System.Windows.RoutedEventHandler(this.ButtonQ_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ButtonExit = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\..\View\RulesWindow.xaml"
            this.ButtonExit.Click += new System.Windows.RoutedEventHandler(this.ButtonExit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

