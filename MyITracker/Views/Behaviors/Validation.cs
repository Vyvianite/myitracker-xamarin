using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MyITracker.Views {

  public class EmailValidationBehavior : BehaviorBase<Entry> {

    protected override void OnAttachedTo(Entry bindable) {
      base.OnAttachedTo(bindable);

      bindable.TextChanged += BindableOnTextChanged;
    }

    protected override void OnDetachingFrom(Entry bindable) {

      bindable.TextChanged -= BindableOnTextChanged;

    }

    private void BindableOnTextChanged(object sender, TextChangedEventArgs e) {
      var Email = e.NewTextValue;
      var emailEntry = sender as Entry;

      if (Regex.Match(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success) {
        //Valid email   
        emailEntry.BackgroundColor = Color.LightGreen;
      }
      else {
        //Not valid email
        emailEntry.BackgroundColor = Color.LightPink;
      }
    }
  }

  public class NotEmptyValidationBehavior : BehaviorBase<Entry> {

    protected override void OnAttachedTo(Entry bindable) {
      base.OnAttachedTo(bindable);

      bindable.TextChanged += BindableOnTextChanged;
    }

    protected override void OnDetachingFrom(Entry bindable) {

      bindable.TextChanged -= BindableOnTextChanged;

    }


    private void BindableOnTextChanged(object sender, TextChangedEventArgs e) {

      var NewText = e.NewTextValue;
      var textEntry = sender as Entry;

      if (!string.IsNullOrEmpty(NewText)) {
        //Valid email   
        textEntry.BackgroundColor = Color.LightGreen;
      }
      else {
        //Not valid email
        textEntry.BackgroundColor = Color.LightPink;
      }

    }


  }



  /*
      var _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
      var _caZipRegEx = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";

      private bool IsUSOrCanadianZipCode(string zipCode)
      {
          var validZipCode = true;
          if ((!Regex.Match(zipCode, _usZipRegEx).Success) && (!Regex.Match(zipCode, _caZipRegEx).Success))
          {
              validZipCode = false;
          }
          return validZipCode;
      }

  */
  public class ZipCodeValidationBehavior : Behavior<Entry> {

    protected override void OnAttachedTo(Entry bindable) {
      base.OnAttachedTo(bindable);

      bindable.TextChanged += BindableOnTextChanged;
    }

    protected override void OnDetachingFrom(Entry bindable) {

      bindable.TextChanged -= BindableOnTextChanged;

    }


    private void BindableOnTextChanged(object sender, TextChangedEventArgs e) {


      var _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
      var _caZipRegEx = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";

      var ZipCode = e.NewTextValue;
      var zipcodeEntry = sender as Entry;

      if ((Regex.Match(ZipCode, _usZipRegEx).Success) && (!Regex.Match(ZipCode, _caZipRegEx).Success)) {
        //Valid ZipCode  
        zipcodeEntry.BackgroundColor = Color.LightGreen;
      }
      else {
        //Not valid ZipCode
        zipcodeEntry.BackgroundColor = Color.LightPink;
      }


    }


  }


  public class PhoneValidationBehavior : Behavior<Entry> {

    protected override void OnAttachedTo(Entry bindable) {
      base.OnAttachedTo(bindable);

      bindable.TextChanged += BindableOnTextChanged;
    }

    protected override void OnDetachingFrom(Entry bindable) {

      bindable.TextChanged -= BindableOnTextChanged;

    }


    private void BindableOnTextChanged(object sender, TextChangedEventArgs e) {



      var PhoneNumber = e.NewTextValue;
      var phoneEntry = sender as Entry;

      if (Regex.Match(PhoneNumber, @"^[1-9]\d{2}-[1-9]\d{2}-\d{4}$").Success) {
        //Valid Phone
        phoneEntry.BackgroundColor = Color.LightGreen;
      }
      else {
        //Not valid Phone
        phoneEntry.BackgroundColor = Color.LightPink;
      }


    }


  }

}