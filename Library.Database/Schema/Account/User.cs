using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Database.Schema.Account;

[Table("tUser")]
public class User
{
    [Key]
    [Column("nUserNo")]
    public int Id { get; set; }
    
    [Column("sUserID")]
    public required string Username { get; set; }

    [Column("sUserPW")]
    public required string Password { get; set; }

    [Column("bIsBlock")]
    public required bool IsBlocked { get; set; } = false;

    [Column("bIsDelete")]
    public required bool IsDeleted { get; set; } = false;

    [Column("nAuthID")]
    public byte AuthenticationId { get; set; }

    [ForeignKey(nameof(AuthenticationId))]
    public UserAuthentication Authentication { get; set; } = null!;
}