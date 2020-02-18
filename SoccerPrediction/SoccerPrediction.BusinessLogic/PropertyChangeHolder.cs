using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SoccerPrediction.BusinessLogic
{
    public class PropertyChangeHolder
    {
        private List<PropertyChangeItem> Changes { get; set; }
        public PropertyChangeHolder()
        {
            Debug.WriteLine("Create New Instance of PropertychangeHolder");
            Changes = new List<PropertyChangeItem>();
        }

        public void AddItem(string oldValue, string newValue, [CallerMemberName] string propertyName = "")
        {
            Debug.WriteLine("PropertyChangeHolder.AddItem (1st) :");
            Debug.WriteLine($"PropertyName: {propertyName ?? "{null}"}, OldValue: {oldValue ?? "{null}"}, NewValue: {newValue ?? "{null}"}");
            if (!Changes.Where(p => p.PropertyName != null && propertyName.ToLower() == p.PropertyName.ToLower()).Any())
            {
                Debug.WriteLine("Could not found an entry in 'Changes'. Add Item...");
                Changes.Add(new PropertyChangeItem(propertyName, oldValue, newValue));
                Debug.WriteLine("Successfully added item.");
            }
            else
            {
                Debug.WriteLine("Found an entry in 'Changes'. Try only to change that...");
                var entry = Changes.Where(p => p.PropertyName == propertyName).Single();
                entry.NewValue = newValue;
                Debug.WriteLine("Successfully changed the 'newVal' Property of the item.");
            }
        }

        public void AddItem(PropertyChangeItem item)
        {
            Debug.WriteLine("PropertyChangeHolder.AddItem (2nd) :");
            AddItem(item.PropertyName, item.OldValue, item.NewValue);
            Debug.WriteLine("Successfully added item.");
        }

        public void AddItems(List<PropertyChangeItem> items)
        {
            Debug.WriteLine("PropertyChangeHolder.AddItem (3rd) :");
            foreach (var prop in items)
            {
                AddItem(prop.OldValue, prop.NewValue, prop.PropertyName);
            }
            Debug.WriteLine("Successfully added item.");
        }

        public void AddManualText(string text)
        {
            Debug.WriteLine("PropertyChangeHolder.AddManualText:");
            Changes.Add(new PropertyChangeItem(null, null, text));
            Debug.WriteLine("Successfully added ManualText.");
        }

        public bool HasChanges()
        {
            Debug.WriteLine("Try to get HasChanges in PropertyChangeHolder...");
            var ret = Changes != null && Changes.Count > 0;
            Debug.WriteLine($"Returning {ret.ToString()}");
            return ret;
        }

        public void ResetChanges()
        {
            Debug.WriteLine("Try to ResetChanges in PropertyChangeHolder...");
            Changes.Clear();
            Debug.WriteLine("Successfully reset changes");
        }

        public List<PropertyChangeItem> GetAllChangedItems()
        {
            return Changes.Where(c => c.OldValue != c.NewValue).ToList();
        }

        public string GetAllChangesText(bool forNewRecord = false)
        {
            var changedItems = GetAllChangedItems();
            Debug.WriteLine($"There are {changedItems.Count.ToString()} Changes in PropertyChangeHolder. Try to get string in GetAllChangedText...");
            var retText = string.Empty;
            foreach (var c in changedItems)
            {
                if (forNewRecord)
                    retText += $"Neuer Datensatz: '{c.PropertyName }' mit Wert '{c.NewValue }'{Environment.NewLine }";
                else
                {
                    if (c.PropertyName == null)
                    {
                        retText += $"{c.NewValue }{Environment.NewLine }";
                    }
                    else
                    {
                        retText += $"'{c.PropertyName }' wurde von Wert '{c.OldValue }' auf '{c.NewValue }' geändert.{Environment.NewLine }";
                    }
                }
            }
            Debug.WriteLine($"GetAllchanges in PropertyChangeHolder returns the following String: {retText }");
            return retText;
        }

    }

    public class PropertyChangeItem
    {
        public PropertyChangeItem(string propName, string oldValue, string newValue)
        {
            PropertyName = propName;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public override string ToString()
        {
            return $"PropertyName: {PropertyName ?? "{null}"}, OldValue: {OldValue ?? "{null}"}, NewValue: {NewValue ?? "{null}"}";
        }
    }
}
