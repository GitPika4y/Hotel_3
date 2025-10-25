using System.Windows.Controls;

namespace Hotel_3.WPF.Views.Modal;

public partial class ConfirmModal : UserControl
{
    public Answer Answer { get; } = new();
    public string Title { get; }
    public string Description { get; }

    public ConfirmModal(string title, string description)
    {
        InitializeComponent();
        
        DataContext = this;
        
        Title = title;
        Description = description;
    }


}

public class Answer
{
    public bool Confirmed { get; } = true;
    public bool Denied { get; } = false;
}