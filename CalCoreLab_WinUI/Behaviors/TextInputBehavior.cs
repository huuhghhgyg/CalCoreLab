using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers.Provider;
using static System.Net.Mime.MediaTypeNames;

namespace CalCoreLab_WinUI.Behaviors
{
    internal class Insert2TextBehavior : Behavior<Button>
    {
        public string Text { get; set; }

        public TextBox Target
        {
            get => GetValue(TargetProperty) as TextBox;
            set => SetValue(TargetProperty, value);
        }

        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            "Target",
            typeof(TextBox),
            typeof(Insert2TextBehavior),
            new PropertyMetadata(null)
        );

        protected override void OnAttached()
        {
            AssociatedObject.Click += ButtonClick;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (Target.Text == null) return;

            int index = Target.SelectionStart; // 获取光标的索引
            Target.Text = Target.Text.Insert(index, Text); // 在光标位置插入
            Target.SelectionStart = index + 1;
        }
    }

    internal class DelFromTextBehaviour : Behavior<Button>
    {
        public TextBox Target
        {
            get => GetValue(TargetProperty) as TextBox;
            set => SetValue(TargetProperty, value);
        }

        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            "Target",
            typeof(TextBox),
            typeof(DelFromTextBehaviour),
            new PropertyMetadata(null)
        );

        protected override void OnAttached()
        {
            AssociatedObject.Click += ButtonClick;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Target.Text)) return;

            int index = Target.SelectionStart; // 获取光标的索引
            Target.Text = Target.Text.Remove(index - 1, 1); // 在光标位置删除
            Target.SelectionStart = index - 1;
        }
    }
}
