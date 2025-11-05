using DevTalk.Domain.BackOffice.Exception.Menssage;

namespace DevTalk.Domain.BackOffice.Entities;

public class Message : Entity
{
    public Message(string title, User user, string description)
    {
        if (string.IsNullOrWhiteSpace(title) || title.Length < 4 ||
            string.IsNullOrWhiteSpace(description) || description.Length < 2)
            throw new MessageException("Error in constrcutor from menssage!");
        Title = title;
        User = user;
        Description = description;
    }

    public string Title { get; set; }
    public User User { get; set; }
    public string Description { get; set; }

    private int _numberOfComplaints;

    private int GetNumberOfComplaints()
        => _numberOfComplaints;
    private void AddNumberOfComplaints()
        => _numberOfComplaints++;
    
}