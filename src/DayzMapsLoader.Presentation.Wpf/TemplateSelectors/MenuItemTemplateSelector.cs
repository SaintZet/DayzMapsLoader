using System.Windows;
using System.Windows.Controls;

using MahApps.Metro.Controls;

namespace DayzMapsLoader.Presentation.Wpf.TemplateSelectors;

public class MenuItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate GlyphDataTemplate { get; set; }

    public DataTemplate ImageDataTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container) =>
        item switch
        {
            HamburgerMenuGlyphItem => GlyphDataTemplate,
            HamburgerMenuImageItem => ImageDataTemplate,
            _ => base.SelectTemplate(item, container)
        };
}
