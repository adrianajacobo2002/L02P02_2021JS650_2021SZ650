using System;
using System.Collections.Generic;

namespace L02P02_2021JS650_2021SZ650.Models;

public partial class Categoria
{
    public int Id { get; set; }

    public string? Categoria1 { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
