using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veritas.Library.Database.Schema.Account;

[Table("tUserAuth")]
public class UserAuthentication
{
    [Column("nAuthID")]
    public required byte Id { get; set; }

    [Column("sAuthName")]
    public required string Name { get; set; }

    [Column("bIsLoginAble")]
    public required byte CanLogin { get; set; }
}