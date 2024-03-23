namespace RimDef
{
    public class Mod
    {
        public string name;
        public string packageId;
        public string version;
        public string dir;
        public string defPath;

        public Mod(string name, string packageId, string version, string dir, string defPath)
        {
            this.name = name;
            this.packageId = packageId;
            this.version = version;
            this.dir = dir;
            this.defPath = defPath;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
