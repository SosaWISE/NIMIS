﻿using System;
using System.Windows;
using System.ComponentModel;
using System.Linq.Expressions;
using WpfRichText.Ex.Commands;

namespace WpfRichText.Ex
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            this.DataContext = new PageViewModel();
        }
    }

    #region PageViewModel
    public class PageViewModel : ObservableBase
    {  
        public DelegateCommand GetXamlCommand { get; private set; }

        #region Constructor
        public PageViewModel()
        {
            GetXamlCommand = new DelegateCommand(() =>
            {
                MessageBox.Show(this.Text);
            });
        }
        #endregion
        
        #region Name
        private string text = string.Empty;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                this.RaisePropertyChanged(p => p.Text);
            }
        }
        #endregion
    }
    #endregion

    #region Observable
    public abstract class ObservableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public static class ObservableBaseEx
    {
        public static void RaisePropertyChanged<T, TProperty>(this T observableBase, Expression<Func<T, TProperty>> expression) where T : ObservableBase
        {
            observableBase.RaisePropertyChanged(observableBase.GetPropertyName(expression));
        }

        public static string GetPropertyName<T, TProperty>(this T owner, Expression<Func<T, TProperty>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                var unaryExpression = expression.Body as UnaryExpression;
                if (unaryExpression != null)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;

                    if (memberExpression == null)
                        throw new NotImplementedException();
                }
                else
                    throw new NotImplementedException();
            }

            var propertyName = memberExpression.Member.Name;
            return propertyName;
        }
    }
    #endregion
}
