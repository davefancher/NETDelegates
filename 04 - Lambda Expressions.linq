<Query Kind="Program" />

class PropertyChangedEventArgs : EventArgs
{
    private readonly string _propertyName;
    private readonly object _oldValue;
    private readonly object _newValue;
    
    public PropertyChangedEventArgs(string propertyName, object oldValue, object newValue)
    {
        _propertyName = propertyName;
        _oldValue = oldValue;
        _newValue = newValue;
    }

    public string PropertyName
    {
        get { return _propertyName; }
    }

    public object OldValue
    {
        get { return _oldValue; }
    }

    public object NewValue
    {
        get { return _newValue; }
    }
}

class Person
{
    public event EventHandler<PropertyChangedEventArgs> PropertyChanged;
    
    private string _firstName;
    private string _lastName;
    
    private void OnPropertyChanged(PropertyChangedEventArgs ea)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, ea);
        }
    }
    
    public string FirstName
    {
        get { return _firstName; }
        set
        {
            if (_firstName != value)
            {
                var oldValue = _firstName;
                _firstName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FirstName", oldValue, value));
            }
        }
    }

    public string LastName
    {
        get { return _lastName; }
        set
        {
            if (_lastName != value)
            {
                var oldValue = _lastName;
                _lastName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LastName", oldValue, value));
            }
        }
    }

    public override string ToString()
    {
        return String.Format("{0} {1}", _firstName, _lastName);
    }
}

void Main()
{
    var me = new Person();

    me.PropertyChanged +=
		(sender, ea) =>
			ea.Dump();

    me.PropertyChanged +=
		(sender, ea) =>
            String
                .Format(
                    "The {0} property changed from {1} to {2}",
                    ea.PropertyName,
                    ea.OldValue == null ? "[null]" : $"\"{ea.OldValue}\"",
                    ea.NewValue == null ? "[null]" : $"\"{ea.NewValue}\"")
                .Dump();

    me.FirstName = "Dave";
    me.LastName = "Fancher";
    
    me.ToString().Dump("Person");
}