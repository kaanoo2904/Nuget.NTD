﻿using System.Collections.Generic;
#if WINDOWS_STORE || WINDOWS_PHONE_APP
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Coding4Fun.Toolkit.Controls;
#elif WINDOWS_PHONE
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Coding4Fun.Toolkit.Controls;
#endif

namespace NTD.Controls
{
    public class ActionPopUpEx<T, TPopUpResult> : PopUp<T, TPopUpResult>
    {
        private const string ActionButtonAreaName = "actionButtonArea";
        protected Panel ActionButtonArea;

#if WINDOWS_STORE || WINDOWS_PHONE_APP
        protected override void OnApplyTemplate()
#elif WINDOWS_PHONE
        public override void OnApplyTemplate()
#endif
        {
            Focus(
#if WINDOWS_STORE || WINDOWS_PHONE_APP
                FocusState.Programmatic
#endif
);

            base.OnApplyTemplate();
            ActionButtonArea = GetTemplateChild(ActionButtonAreaName) as Panel;

            SetButtons();
        }

        #region Control Events
        #endregion

        #region helper methods
        private void SetButtons()
        {
            if (ActionButtonArea == null)
                return;

            ActionButtonArea.Children.Clear();

            var hasContent = false;

            foreach (var button in ActionPopUpButtons)
            {
                ActionButtonArea.Children.Add(button);

                hasContent |= (button.Content != null);
            }

            if (hasContent)
                ActionButtonArea.Margin = new Thickness();
        }
        #endregion

        #region Dependency Property Callbacks
        private static void OnActionPopUpButtonsChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var sender = ((ActionPopUpEx<T, TPopUpResult>)o);

            if (sender != null && e.NewValue != e.OldValue)
                sender.SetButtons();
        }
        #endregion

        #region Dependency Properties / Properties
        public List<ButtonPrompt> ActionPopUpButtons
        {
            get { return (List<ButtonPrompt>)GetValue(ActionPopUpButtonsProperty); }
            set { SetValue(ActionPopUpButtonsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActionPopUpButtons.  This enables animation, styling, binding, etc...
        public readonly DependencyProperty ActionPopUpButtonsProperty =
            DependencyProperty.Register("ActionPopUpButtons", typeof(List<ButtonPrompt>), typeof(ActionPopUpEx<T, TPopUpResult>), new PropertyMetadata(new List<ButtonPrompt>(), OnActionPopUpButtonsChanged));

        #endregion
    }
}
