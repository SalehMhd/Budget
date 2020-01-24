using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace Budget.Behaviors
{
    public class CurrencyBehavior : Behavior<MaterialTextField>
    {
        string currencyExpression1 = @"^\d+(,\d{2})*$";
        string currencyExpression2 = @"^\d+(,\d{1})*$";
        string currencyExpression3 = @"^\d+(,)$";

        protected override void OnAttachedTo(MaterialTextField bindable)
        {
            bindable.TextChanged += Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            var match = Regex.Match(e.NewTextValue, currencyExpression1);

            if(match.Value == string.Empty)
            {
                match = Regex.Match(e.NewTextValue, currencyExpression2);
                if(match.Value == string.Empty)
                {
                    match = Regex.Match(e.NewTextValue, currencyExpression3);
                }
            }
            ((MaterialTextField)sender).Text = match.Value;
        }

        protected override void OnDetachingFrom(MaterialTextField bindable)
        {
            bindable.TextChanged -= Bindable_TextChanged;
        }
    }
}
