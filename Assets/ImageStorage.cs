using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageStorage : StaticInstance<ImageStorage>
{
    public List<Texture> textures = new List<Texture>();
}
