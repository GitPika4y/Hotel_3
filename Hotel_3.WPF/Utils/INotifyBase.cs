using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hotel_3.WPF.Utils;

public interface INotifyBase : INotifyPropertyChanged
{
    void OnPropertyChanged([CallerMemberName] string propertyName = "");
    void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "");
}