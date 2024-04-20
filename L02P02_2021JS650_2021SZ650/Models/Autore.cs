using System;
using System.Collections.Generic;

namespace L02P02_2021JS650_2021SZ650.Models;

public partial class Autore
{
    public int Id { get; set; }

    public string? Autor { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
