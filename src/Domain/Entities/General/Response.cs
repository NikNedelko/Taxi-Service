namespace Domain.Entities.General;

[Serializable]
public class Response
{ 
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
    public string AdditionalInformation { get; set; }
}