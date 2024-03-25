namespace RimDef
{
  // TODO: this is just sad.
  class ThingDef : Def
  {
    public ThingDef(
        Mod mod,
        string defType,
        string defName,
        string label,
        string description,
        string texture,
        string xml,
        string file
    )
        : base(mod, defType, defName, label, description, texture, xml, file) { }
  }
}
