﻿#pragma checksum "..\..\..\..\MyPond\AddPond.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "39669DB1B95E40D2488D5A91692ADD0D317882B7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using WpfApp.MyPond;


namespace WpfApp.MyPond {
    
    
    /// <summary>
    /// AddPond
    /// </summary>
    public partial class AddPond : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\..\MyPond\AddPond.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TitleText;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\MyPond\AddPond.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PondImage;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\MyPond\AddPond.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUploadImage;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\..\MyPond\AddPond.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PondNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\..\MyPond\AddPond.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LengthTextBox;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\..\MyPond\AddPond.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WidthTextBox;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\..\MyPond\AddPond.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DepthTextBox;
        
        #line default
        #line hidden
        
        
        #line 148 "..\..\..\..\MyPond\AddPond.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp;V1.0.0.0;component/mypond/addpond.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\MyPond\AddPond.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TitleText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            
            #line 46 "..\..\..\..\MyPond\AddPond.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CancelButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.PondImage = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.btnUploadImage = ((System.Windows.Controls.Button)(target));
            
            #line 73 "..\..\..\..\MyPond\AddPond.xaml"
            this.btnUploadImage.Click += new System.Windows.RoutedEventHandler(this.btnUploadImage_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PondNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.LengthTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.WidthTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.DepthTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 150 "..\..\..\..\MyPond\AddPond.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

