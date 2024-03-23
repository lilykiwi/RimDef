using System.Collections.Generic;

namespace RimDef
{
    public class Def
    {
        public Mod mod;

        public string defType;
        public string defName;
        public string label;
        public string description;
        public string texture;
        public string xml;
        public string file;

        public List<string[]> details = new List<string[]>();

        public Def(
            Mod mod,
            string defType,
            string defName,
            string label,
            string description,
            string texture,
            string xml,
            string file
        )
        {
            this.mod = mod;
            this.defType = defType;
            this.defName = defName;
            this.label = label;
            this.description = description;
            this.texture = texture;
            this.xml = xml;
            this.file = file;
        }
    }
}
