﻿#pragma checksum "..\..\ZoomAndPanControlView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2DE5CA70A38DB6A702DA680B53F38E5A3743D15954F1FFF4EA8DA8C29B9C8E41"
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
using ZoomAndPan;
using ZoomAndPanSample;


namespace ZoomAndPanSample {
    
    
    /// <summary>
    /// ZoomAndPanScrollViewerView
    /// </summary>
    public partial class ZoomAndPanScrollViewerView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\ZoomAndPanControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CrosshairsCheckBox;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\ZoomAndPanControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ViewBoxContent;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\ZoomAndPanControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scroller;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\ZoomAndPanControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZoomAndPan.ZoomAndPanControl ZoomAndPanControl;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\ZoomAndPanControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid theGrid;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\ZoomAndPanControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas actualContent;
        
        #line default
        #line hidden
        
        
        #line 215 "..\..\ZoomAndPanControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox AnimationsCheckBox;
        
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
            System.Uri resourceLocater = new System.Uri("/AdvancedZoomAndPanSample;component/zoomandpancontrolview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ZoomAndPanControlView.xaml"
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
            this.CrosshairsCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 2:
            this.ViewBoxContent = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.scroller = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 4:
            this.ZoomAndPanControl = ((ZoomAndPan.ZoomAndPanControl)(target));
            return;
            case 5:
            this.theGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.actualContent = ((System.Windows.Controls.Canvas)(target));
            return;
            case 7:
            
            #line 165 "..\..\ZoomAndPanControlView.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseDown);
            
            #line default
            #line hidden
            
            #line 166 "..\..\ZoomAndPanControlView.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Rectangle_MouseMove);
            
            #line default
            #line hidden
            
            #line 167 "..\..\ZoomAndPanControlView.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseUp);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 174 "..\..\ZoomAndPanControlView.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseDown);
            
            #line default
            #line hidden
            
            #line 175 "..\..\ZoomAndPanControlView.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Rectangle_MouseMove);
            
            #line default
            #line hidden
            
            #line 176 "..\..\ZoomAndPanControlView.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseUp);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 183 "..\..\ZoomAndPanControlView.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseDown);
            
            #line default
            #line hidden
            
            #line 184 "..\..\ZoomAndPanControlView.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Rectangle_MouseMove);
            
            #line default
            #line hidden
            
            #line 185 "..\..\ZoomAndPanControlView.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseUp);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 192 "..\..\ZoomAndPanControlView.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseDown);
            
            #line default
            #line hidden
            
            #line 193 "..\..\ZoomAndPanControlView.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Rectangle_MouseMove);
            
            #line default
            #line hidden
            
            #line 194 "..\..\ZoomAndPanControlView.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseUp);
            
            #line default
            #line hidden
            return;
            case 11:
            this.AnimationsCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
