using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignmnet.EntityLayer.Models;

public partial class Material
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Material Code is required.")]
    public string Code { get; set; } = null!;
    [Required(ErrorMessage = "Short Text is required.")]
    [StringLength(50, ErrorMessage = "Short Text cannot exceed 50 characters.")]
    public string ShortText { get; set; } = null!;
    [Required(ErrorMessage = "Long Text is required.")]
    public string LongTexts { get; set; } = null!;
    [Required(ErrorMessage = "Unit is required.")]
    public int Unit { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Reorder Level must be a non-negative number.")]
    public int ReorderLevel { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Minimum Order Quantity must be a non-negative number.")]
    public int MinOrderQuantity { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Purchaseorderdetail> Purchaseorderdetails { get; set; } = new List<Purchaseorderdetail>();
}
