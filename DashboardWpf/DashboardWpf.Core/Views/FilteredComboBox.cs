using DashboardWpf.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DashboardWpf.Core.Views
{
    public class FilteredComboBox : ComboBox
    {
        public static readonly DependencyProperty MinimumSearchLengthProperty =
                    DependencyProperty.Register(
                        "MinimumSearchLength",
                        typeof(int),
                        typeof(FilteredComboBox),
                        new UIPropertyMetadata(1));

        private string oldFilter = string.Empty;
        private string currentFilter = string.Empty;

        private List<Employee> copyDataSource = new List<Employee>();

        public FilteredComboBox()
        {
            IsTextSearchEnabled = false;
        }

        [Description("Length of the search string that triggers filtering.")]
        [Category("Filtered ComboBox")]
        [DefaultValue(1)]
        public int MinimumSearchLength
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (int)this.GetValue(MinimumSearchLengthProperty);
            }

            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                this.SetValue(MinimumSearchLengthProperty, value);
            }
        }

        protected TextBox EditableTextBox
        {
            get
            {
                //return this.GetTemplateChild("PART_EditableTextBox") as TextBox;
                return this.Template.FindName("PART_EditableTextBox", this) as TextBox;
            }
        }

        private ICollectionView _dataSourceView;

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (newValue != null)
            {
                //ICollectionView view = BuildDataSource(newValue);
                _dataSourceView = new CollectionViewSource()
                    { Source = newValue }.View;
                _dataSourceView.Filter += this.FilterPredicate;
            }

            if (oldValue != null && _dataSourceView != null)
            {
                //ICollectionView view = CollectionViewSource.GetDefaultView(oldValue);
                //view.Filter -= this.FilterPredicate;
                _dataSourceView.Filter -= this.FilterPredicate;
            }

            base.OnItemsSourceChanged(oldValue, newValue);
        }

        private ICollectionView BuildDataSource(IEnumerable items)
        {
            var employees = items as IList<Employee>;
            copyDataSource.Clear();
            if (employees != null)
            {
                foreach (var emp in employees)
                {
                    copyDataSource.Add(emp.Clone);
                }
            }
            ICollectionView view = CollectionViewSource.GetDefaultView(items);
            return view;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                // Explicit Selection -> Close ItemsPanel
                this.IsDropDownOpen = false;
            }
            else if (e.Key == Key.Escape)
            {
                // Escape -> Close DropDown and redisplay Filter
                this.IsDropDownOpen = false;
                this.SelectedIndex = -1;
                this.Text = this.currentFilter;
            }
            else
            {
                if (e.Key == Key.Down)
                {
                    // Arrow Down -> Open DropDown
                    this.IsDropDownOpen = true;
                }

                base.OnPreviewKeyDown(e);
            }

            // Cache text
            this.oldFilter = this.Text;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                // Navigation keys are ignored
            }
            else if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                // Explicit Select -> Clear Filter
                this.ClearFilter();
            }
            else
            {
                // The text was changed
                if (this.Text != this.oldFilter)
                {
                    //this.oldFilter = this.Text;

                    // Clear the filter if the text is empty,
                    // apply the filter if the text is long enough
                    if (this.Text.Length == 0 || this.Text.Length >= this.MinimumSearchLength)
                    {
                        this.RefreshFilter();
                        this.IsDropDownOpen = true;

                        this.EditableTextBox.SelectionStart = int.MaxValue;
                    }
                }

                base.OnKeyUp(e);

                // Update Filter Value
                this.currentFilter = this.Text;
                
                //this.currentFilter = this.Text = this.oldFilter;
            }
        }

        protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            this.ClearFilter();
            int temp = this.SelectedIndex;
            this.SelectedIndex = -1;
            this.Text = string.Empty;
            this.SelectedIndex = temp;
            base.OnPreviewLostKeyboardFocus(e);
        }

        /// <summary>
        /// Re-apply the Filter.
        /// </summary>
        private void RefreshFilter()
        {
            if (this.ItemsSource != null)
            //if (this.ItemsSource != null && _dataSourceView != null)
            {
                //_dataSourceView = new CollectionViewSource()
                //    { Source = this.ItemsSource }.View;
                //_dataSourceView.Filter += this.FilterPredicate;

                this.Items.Filter += this.FilterPredicate;

                //ICollectionView view = CollectionViewSource.GetDefaultView(this.ItemsSource);
                //view.Filter = x => FilterPredicate(x);
                //view.Refresh();
            }
        }

        /// <summary>
        /// Clear the Filter.
        /// </summary>
        private void ClearFilter()
        {
            this.currentFilter = string.Empty;
            this.RefreshFilter();
        }

        private bool FilterPredicate(object value)
        {
            // No filter, no text
            if (value == null)
            {
                return false;
            }

            // No text, no filter
            if (this.Text.Length == 0)
            {
                return true;
            }

            // Case insensitive search
            return value.ToString().ToLower().Contains(this.Text.ToLower());
        }
    }
}
