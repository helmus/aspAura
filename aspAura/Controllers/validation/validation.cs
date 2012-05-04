using System.ComponentModel.DataAnnotations;
using aspAura.Models;

[MetadataType(typeof(Todo))]
public class TodoValidation
{
    [MaxLength(5, ErrorMessage = "Max 5")]
    public object content;  
}