using System.Windows.Controls;

namespace Hotel_3.WPF.Views.Modal;

public partial class MessageModal : UserControl
{
    public string Message { get; }
    public string ButtonText { get; }
    
    
    public MessageModal(string message, string buttonText = "Ок")
    {
        InitializeComponent();
        DataContext = this;
        
        Message = message;
        ButtonText = buttonText;
    }

}