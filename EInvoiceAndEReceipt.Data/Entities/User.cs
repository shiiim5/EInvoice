using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EInvoiceAndEReceipt.Data.Entities
{
    public class User
    {
        [Key]
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    [ForeignKey("Role")]
    public int? RoleId {get; set;} =1;
    public Role? Role { get; set; }
    }
}